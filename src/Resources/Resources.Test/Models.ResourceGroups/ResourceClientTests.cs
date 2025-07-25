﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkClient;
using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels;
using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.Deployments;
using Microsoft.Azure.Commands.Resources.Models;
using Microsoft.Azure.Commands.ScenarioTest;
using Microsoft.Azure.Commands.TestFx;
using Microsoft.Azure.Management.Authorization;
using Microsoft.Azure.Management.Resources;
using Microsoft.Azure.Management.Resources.Models;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Azure.OData;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Microsoft.WindowsAzure.Commands.Test.Utilities.Common;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Microsoft.Azure.Commands.Resources.Test.Models
{
    public class ResourceClientTests : RMTestBase
    {
        private Mock<IResourceManagementClient> resourceManagementClientMock;

        private Mock<IAuthorizationManagementClient> authorizationManagementClientMock;

        private Mock<IDeploymentsOperations> deploymentsMock;

        private Mock<IResourceGroupsOperations> resourceGroupMock;

        private Mock<IResourcesOperations> resourceOperationsMock;

        private Mock<IDeploymentOperations> deploymentOperationsMock;

        private Mock<IProvidersOperations> providersMock;

        private Mock<Action<string>> progressLoggerMock;

        private Mock<Action<string>> errorLoggerMock;

        private NewResourceManagerSdkClient resourcesClient;

        private string resourceGroupName = "myResourceGroup";

        private string resourceGroupLocation = "West US";

        private string deploymentName = "fooDeployment";

        private string templateFile = string.Empty;

        private string requestId = "1234567890";

        private string resourceName = "myResource";

        private ResourceIdentifier resourceIdentity;

        private Dictionary<string, object> properties;

        private string serializedProperties;

        private int ConfirmActionCounter = 0;

        private static GenericResourceExpanded CreateGenericResource(string location = null, string id = null, string name = null, string type = null)
        {
            GenericResourceExpanded resource = new GenericResourceExpanded();
            if (id != null)
            {
                typeof(Resource).GetProperty("Id").SetValue(resource, id);
            }
            if (name != null)
            {
                typeof(Resource).GetProperty("Name").SetValue(resource, name);
            }
            if (type != null)
            {
                typeof(Resource).GetProperty("Type").SetValue(resource, type);
            }
            if (location != null)
            {
                resource.Location = location;
            }

            return resource;
        }

        private static AzureOperationResponse<T> CreateAzureOperationResponse<T>(T type)
        {
            return new AzureOperationResponse<T>()
            {
                Body = type
            };
        }

        private void ConfirmAction(bool force, string actionMessage, string processMessage, string target, Action action, Func<bool> predicate)
        {
            ConfirmActionCounter++;
            action();
        }

        private int RejectActionCounter = 0;

        private void RejectAction(bool force, string actionMessage, string processMessage, string target, Action action, Func<bool> predicate)
        {
            RejectActionCounter++;
        }

        private IPage<T> GetPagableType<T>(List<T> collection)
        {
            var pagableResult = new Page<T>();
            pagableResult.SetItemValue<T>(collection);
            return pagableResult;
        }

        private void SetupListForResourceGroupAsync(string name, List<GenericResourceExpanded> result)
        {
            resourceOperationsMock.Setup(f => f.ListWithHttpMessagesAsync(
                null,
                null,
                new CancellationToken()))
            .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<IPage<GenericResourceExpanded>>()
            {
                Body = GetPagableType(result)
            }));
        }

        private void SetupListForResourceGroupAsync(string name, IPage<GenericResourceExpanded> result)
        {
            resourceOperationsMock.Setup(f => f.ListAsync(
                null,
                new CancellationToken()))
                    .Returns(Task.Factory.StartNew(() => result));
        }

        private void EqualsIgnoreWhitespace(string left, string right)
        {
            string normalized1 = Regex.Replace(left, @"\s", "");
            string normalized2 = Regex.Replace(right, @"\s", "");

            Assert.Equal(
                normalized1.ToLowerInvariant(),
                normalized2.ToLowerInvariant());
        }

        private void SetupClass()
        {
            resourceManagementClientMock = new Mock<IResourceManagementClient>();
            authorizationManagementClientMock = new Mock<IAuthorizationManagementClient>();
            deploymentsMock = new Mock<IDeploymentsOperations>();
            resourceGroupMock = new Mock<IResourceGroupsOperations>();
            resourceOperationsMock = new Mock<IResourcesOperations>();
            deploymentOperationsMock = new Mock<IDeploymentOperations>();
            providersMock = new Mock<IProvidersOperations>();
            providersMock.Setup(f => f.ListWithHttpMessagesAsync(null, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<IPage<Provider>>()
                {
                    Body = new Page<Provider>()
                }));
            progressLoggerMock = new Mock<Action<string>>();
            errorLoggerMock = new Mock<Action<string>>();
            resourceManagementClientMock.Setup(f => f.Deployments).Returns(deploymentsMock.Object);
            resourceManagementClientMock.Setup(f => f.ResourceGroups).Returns(resourceGroupMock.Object);
            resourceManagementClientMock.Setup(f => f.Resources).Returns(resourceOperationsMock.Object);
            resourceManagementClientMock.Setup(f => f.DeploymentOperations).Returns(deploymentOperationsMock.Object);
            resourceManagementClientMock.Setup(f => f.Providers).Returns(providersMock.Object);
            resourceManagementClientMock.Setup(f => f.ApiVersion).Returns("11-01-2015");
            resourcesClient = new NewResourceManagerSdkClient(
                resourceManagementClientMock.Object)
            {
                VerboseLogger = progressLoggerMock.Object,
                ErrorLogger = errorLoggerMock.Object,
            };

            resourceIdentity = new ResourceIdentifier
            {
                ParentResource = "sites/siteA",
                ResourceName = "myResource",
                ResourceGroupName = "Microsoft.Web", // ResourceProviderNamespace
                ResourceType = "sites"
            };
            properties = new Dictionary<string, object>
                {
                    {"name", "site1"},
                    {"siteMode", "Standard"},
                    {"computeMode", "Dedicated"},
                    {"misc", new Dictionary<string, object>
                        {
                            {"key1", "value1"},
                            {"key2", "value2"}
                        }}
                };
            serializedProperties = JsonConvert.SerializeObject(properties, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            });
            templateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources/sampleTemplateFile.json");
        }

        public ResourceClientTests()
        {
            SetupClass();
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void NewResourceGroupChecksForPermissionForExistingResource()
        {
            RejectActionCounter = 0;
            PSCreateResourceGroupParameters parameters = new PSCreateResourceGroupParameters() { ResourceGroupName = resourceGroupName, ConfirmAction = RejectAction };
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(parameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                    new AzureOperationResponse<bool>()
                    {
                        Body = true
                    }));

            resourceGroupMock.Setup(f => f.GetWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                "createdTime,changedTime",
                null,
                new CancellationToken()))
                    .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<ResourceGroup>()
                    {
                        Body = new ResourceGroup(location: parameters.Location, name: parameters.ResourceGroupName)
                    }));

            resourceOperationsMock.Setup(f => f.ListWithHttpMessagesAsync(null, null, It.IsAny<CancellationToken>()))
                .Returns(() => Task.Factory.StartNew(() =>
                {
                    var resources = new List<GenericResourceExpanded>()
                        {
                            CreateGenericResource(location: "West US", id: null, name: "foo", type: null),
                            CreateGenericResource(location: "West US", id: null, name: "bar", type: null)
                        };
                    var result = new Page<GenericResourceExpanded>();
                    result.SetItemValue(resources);

                    return new AzureOperationResponse<IPage<GenericResourceExpanded>>() { Body = result };
                }));

            resourcesClient.CreatePSResourceGroup(parameters);
            Assert.Equal(1, RejectActionCounter);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void NewResourceGroupWithoutDeploymentSucceeds()
        {
            PSCreateResourceGroupParameters parameters = new PSCreateResourceGroupParameters()
            {
                ResourceGroupName = resourceGroupName,
                Location = resourceGroupLocation,
                ConfirmAction = ConfirmAction
            };
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(parameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(false)));

            resourceGroupMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                It.IsAny<ResourceGroup>(),
                null,
                new CancellationToken()))
                    .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(new ResourceGroup(location: parameters.Location, name: parameters.ResourceGroupName))));
            SetupListForResourceGroupAsync(parameters.ResourceGroupName, new List<GenericResourceExpanded>());

            PSResourceGroup result = resourcesClient.CreatePSResourceGroup(parameters);

            Assert.Equal(parameters.ResourceGroupName, result.ResourceGroupName);
            Assert.Equal(parameters.Location, result.Location);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTemplateShowsSuccessMessage()
        {
            Uri templateUri = new Uri("http://templateuri.microsoft.com");
            Deployment deploymentFromValidate = new Deployment();
            PSDeploymentCmdletParameters parameters = new PSDeploymentCmdletParameters()
            {
                ScopeType = DeploymentScopeType.ResourceGroup,
                ResourceGroupName = resourceGroupName,
                DeploymentMode = DeploymentMode.Incremental,
                TemplateFile = templateFile,
            };
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(parameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(true)));

            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(resourceGroupName, It.IsAny<string>(), It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                {

                    var result = new AzureOperationResponse<DeploymentValidateResult>()
                    {
                        Body = new DeploymentValidateResult
                        {
                        }
                    };

                    result.Response = new System.Net.Http.HttpResponseMessage();
                    result.Response.StatusCode = HttpStatusCode.OK;

                    return result;
                }))
                .Callback((string rg, string dn, Deployment d, Dictionary<string, List<string>> customHeaders, CancellationToken c) => { deploymentFromValidate = d; });

            TemplateValidationInfo error = resourcesClient.ValidateDeployment(parameters);
            Assert.Empty(error.Errors);
            progressLoggerMock.Verify(f => f("Template is valid."), Times.Once());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTemplateShowsSuccessMessageWithObjectAsResponse()
        {
            Uri templateUri = new Uri("http://templateuri.microsoft.com");
            Deployment deploymentFromValidate = new Deployment();
            PSDeploymentCmdletParameters parameters = new PSDeploymentCmdletParameters()
            {
                ScopeType = DeploymentScopeType.ResourceGroup,
                ResourceGroupName = resourceGroupName,
                DeploymentMode = DeploymentMode.Incremental,
                TemplateFile = templateFile,
            };
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(parameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(true)));

            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(resourceGroupName, It.IsAny<string>(), It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                {

                    var result = new AzureOperationResponse<DeploymentValidateResult>()
                    {
                        Body = new DeploymentValidateResult()
                    };

                    result.Response = new System.Net.Http.HttpResponseMessage();
                    result.Response.StatusCode = HttpStatusCode.Accepted;

                    return result;
                }))
                .Callback((string rg, string dn, Deployment d, Dictionary<string, List<string>> customHeaders, CancellationToken c) => { deploymentFromValidate = d; });

            TemplateValidationInfo error = resourcesClient.ValidateDeployment(parameters);
            Assert.Empty(error.Errors);
            progressLoggerMock.Verify(f => f("Template is valid."), Times.Once());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTemplateShowsSuccessMessageWithDiagnostics()
        {
            Uri templateUri = new Uri("http://templateuri.microsoft.com");
            Deployment deploymentFromValidate = new Deployment();
            PSDeploymentCmdletParameters parameters = new PSDeploymentCmdletParameters()
            {
                ScopeType = DeploymentScopeType.ResourceGroup,
                ResourceGroupName = resourceGroupName,
                DeploymentMode = DeploymentMode.Incremental,
                TemplateFile = templateFile,
            };
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(parameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(true)));

            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(resourceGroupName, It.IsAny<string>(), It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                {

                    var result = new AzureOperationResponse<DeploymentValidateResult>()
                    {
                        Body = new DeploymentValidateResult
                        {
                            Properties = new DeploymentPropertiesExtended(diagnostics: new List<DeploymentDiagnosticsDefinition>
                                {
                                    new DeploymentDiagnosticsDefinition(level: Level.Warning, message: "Test Diagnostic", code: "Diagnostic", target: "Target")
                                })
                        }
                    };

                    result.Response = new System.Net.Http.HttpResponseMessage();
                    result.Response.StatusCode = HttpStatusCode.OK;

                    return result;
                }))
                .Callback((string rg, string dn, Deployment d, Dictionary<string, List<string>> customHeaders, CancellationToken c) => { deploymentFromValidate = d; });

            TemplateValidationInfo info = resourcesClient.ValidateDeployment(parameters);

            var error = info.Errors;
            var diagnostics = info.Diagnostics;

            var expected = new DeploymentDiagnosticsDefinition(level: Level.Warning, message: "Test Diagnostic", code: "Diagnostic", target: "Target");

            Assert.Empty(error);
            Assert.Equal(expected.Code, diagnostics[0].Code);
            Assert.Equal(expected.Level, diagnostics[0].Level);
            Assert.Equal(expected.Message, diagnostics[0].Message);

            progressLoggerMock.Verify(f => f("Template is valid."), Times.Once());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void NewResourceGroupUsesDeploymentNameForDeploymentName()
        {
            // fix test flakiness
            TestExecutionHelpers.RetryAction(
                () =>
                {
                    SetupClass();
                    string deploymentName = "abc123";
                    Deployment deploymentFromGet = new Deployment();
                    Deployment deploymentFromValidate = new Deployment();
                    PSDeploymentCmdletParameters parameters = new PSDeploymentCmdletParameters()
                    {
                        ScopeType = DeploymentScopeType.ResourceGroup,
                        ResourceGroupName = resourceGroupName,
                        DeploymentName = deploymentName,
                        TemplateFile = "http://path/file.html"
                    };


                    deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Deployment>(),
                        null,
                        new CancellationToken()))
                        .Returns(Task.Factory.StartNew(() =>
                            new AzureOperationResponse<DeploymentValidateResult>()
                            {
                                Body = new DeploymentValidateResult
                                {
                                }
                            }))
                        .Callback(
                            (string rg, string dn, Deployment d, Dictionary<string, List<string>> customHeaders,
                                CancellationToken c) =>
                            {
                                deploymentFromValidate = d;
                            });

                    deploymentsMock.Setup(f => f.BeginCreateOrUpdateWithHttpMessagesAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Deployment>(),
                        null,
                        new CancellationToken()))
                        .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<DeploymentExtended>()
                        {
                            Body = new DeploymentExtended(name: deploymentName, id: requestId)
                        }))
                        .Callback(
                            (string name, string dName, Deployment bDeploy,
                                Dictionary<string, List<string>> customHeaders, CancellationToken token) =>
                            {
                                deploymentFromGet = bDeploy;
                                deploymentName = dName;
                            });

                    deploymentsMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        null,
                        new CancellationToken()))
                        .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<bool>()
                        {
                            Body = true
                        }));

                    SetupListForResourceGroupAsync(parameters.ResourceGroupName, new List<GenericResourceExpanded>
                    {
                        CreateGenericResource(null, null, "website")
                    });

                    var operationId = Guid.NewGuid().ToString();
                    var operationQueue = new Queue<DeploymentOperation>();
                    operationQueue.Enqueue(
                        new DeploymentOperation(
                            operationId: operationId,
                            properties: new DeploymentOperationProperties(
                                provisioningState: "Accepted",
                                targetResource: new TargetResource()
                                {
                                    ResourceType = "Microsoft.Website",
                                    ResourceName = resourceName
                                })));

                    operationQueue.Enqueue(
                        new DeploymentOperation(
                            operationId: operationId,
                            properties: new DeploymentOperationProperties(
                                provisioningState: "Running",
                                targetResource: new TargetResource()
                                {
                                    ResourceType = "Microsoft.Website",
                                    ResourceName = resourceName
                                })));

                    operationQueue.Enqueue(
                        new DeploymentOperation(
                            operationId: operationId,
                            properties: new DeploymentOperationProperties(
                                provisioningState: "Succeeded",
                                targetResource: new TargetResource()
                                {
                                    ResourceType = "Microsoft.Website",
                                    ResourceName = resourceName
                                })));

                    deploymentOperationsMock.SetupSequence(
                        f =>
                            f.ListWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), null, null,
                                new CancellationToken()))
                        .Returns(Task.Factory.StartNew(() =>
                            new AzureOperationResponse<IPage<DeploymentOperation>>()
                            {
                                Body = GetPagableType(
                                    new List<DeploymentOperation>()
                                    {
                                        operationQueue.Dequeue()
                                    })
                            }))
                        .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<IPage<DeploymentOperation>>()
                        {
                            Body = GetPagableType(
                                new List<DeploymentOperation>()
                                {
                                    operationQueue.Dequeue()
                                })
                        }))
                        .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<IPage<DeploymentOperation>>()
                        {
                            Body = GetPagableType(
                                new List<DeploymentOperation>()
                                {
                                    operationQueue.Dequeue()
                                })
                        }));

                    var deploymentQueue = new Queue<DeploymentExtended>();
                    deploymentQueue.Enqueue(new DeploymentExtended(
                        name: deploymentName,
                        properties: new DeploymentPropertiesExtended(
                            mode: DeploymentMode.Incremental,
                            correlationId: "123",
                            provisioningState: "Accepted")));
                    deploymentQueue.Enqueue(new DeploymentExtended(
                        name: deploymentName,
                        properties: new DeploymentPropertiesExtended(
                            mode: DeploymentMode.Incremental,
                            correlationId: "123",
                            provisioningState: "Running")));
                    deploymentQueue.Enqueue(new DeploymentExtended(
                        name: deploymentName,
                        properties: new DeploymentPropertiesExtended(
                            mode: DeploymentMode.Incremental,
                            correlationId: "123",
                            provisioningState: "Succeeded")));
                    deploymentsMock.SetupSequence(
                        f =>
                            f.GetWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), null,
                                new CancellationToken()))
                        .Returns(
                            Task.Factory.StartNew(
                                () =>
                                    new AzureOperationResponse<DeploymentExtended>() { Body = deploymentQueue.Dequeue() }))
                        .Returns(
                            Task.Factory.StartNew(
                                () =>
                                    new AzureOperationResponse<DeploymentExtended>() { Body = deploymentQueue.Dequeue() }))
                        .Returns(
                            Task.Factory.StartNew(
                                () =>
                                    new AzureOperationResponse<DeploymentExtended>() { Body = deploymentQueue.Dequeue() }));

                    Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.PSResourceGroupDeployment result =
                        resourcesClient.ExecuteResourceGroupDeployment(parameters);
                    Assert.Equal(deploymentName, deploymentName);
                    Assert.Equal("Succeeded", result.ProvisioningState);
                    progressLoggerMock.Verify(
                        f =>
                            f(string.Format("Resource {0} '{1}' provisioning status is {2}", "Microsoft.Website",
                                resourceName, "Accepted".ToLower())),
                        Times.Once());
                    progressLoggerMock.Verify(
                        f =>
                            f(string.Format("Resource {0} '{1}' provisioning status is {2}", "Microsoft.Website",
                                resourceName, "Running".ToLower())),
                        Times.Once());
                    progressLoggerMock.Verify(
                        f =>
                            f(string.Format("Resource {0} '{1}' provisioning status is {2}", "Microsoft.Website",
                                resourceName, "Succeeded".ToLower())),
                        Times.Once());
                });
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void NewResourceGroupDeploymentWithDelay()
        {
            string deploymentName = "abc123";
            ConcurrentBag<string> deploymentNames = new ConcurrentBag<string>();

            PSDeploymentCmdletParameters parameters = new PSDeploymentCmdletParameters()
            {
                ScopeType = DeploymentScopeType.ResourceGroup,
                ResourceGroupName = resourceGroupName,
                Location = resourceGroupLocation,
                DeploymentName = deploymentName,
                TemplateFile = "http://path/file.html"
            };

            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                parameters.DeploymentName,
                It.IsAny<Deployment>(),
                null,
                new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                    new AzureOperationResponse<DeploymentValidateResult>()
                    {
                        Body = new DeploymentValidateResult
                        {
                        }
                    }));

            deploymentsMock.Setup(f => f.GetWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                parameters.DeploymentName,
                null,
                new CancellationToken()))
                .Returns<string, string, Dictionary<string, List<string>>, CancellationToken>(
                    async (getResourceGroupName, getDeploymentName, customHeaders, cancellationToken) =>
                    {
                        await Task.Delay(100, cancellationToken);

                        if (deploymentNames.Contains(getDeploymentName))
                        {
                            return new AzureOperationResponse<DeploymentExtended>()
                            {
                                Body = new DeploymentExtended(name: getDeploymentName, id: requestId)
                                {
                                    Properties = new DeploymentPropertiesExtended(
                                        mode: DeploymentMode.Incremental,
                                        correlationId: "123",
                                        provisioningState: "Succeeded"),
                                }
                            };
                        }

                        throw new CloudException(String.Format("Deployment '{0}' could not be found.", getDeploymentName));
                    });

            deploymentsMock.Setup(f => f.BeginCreateOrUpdateWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                parameters.DeploymentName,
                It.IsAny<Deployment>(),
                null,
                new CancellationToken()))
                .Returns<string, string, Deployment, Dictionary<string, List<string>>, CancellationToken>(
                    async (craeteResourceGroupName, createDeploymentName, createDeployment, customHeaders, cancellationToken) =>
                    {
                        await Task.Delay(500, cancellationToken);

                        deploymentNames.Add(createDeploymentName);

                        return new AzureOperationResponse<DeploymentExtended>()
                        {
                            Body = new DeploymentExtended(name: createDeploymentName, id: requestId)
                            {
                            }
                        };
                    });

            deploymentsMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                parameters.DeploymentName,
                It.IsAny<Deployment>(),
                null,
                new CancellationToken()))
                .Returns<string, string, Deployment, Dictionary<string, List<string>>, CancellationToken>(
                    async (craeteResourceGroupName, createDeploymentName, createDeployment, customHeaders, cancellationToken) =>
                    {
                        await Task.Delay(10000, cancellationToken);

                        deploymentNames.Add(createDeploymentName);

                        return new AzureOperationResponse<DeploymentExtended>()
                        {
                            Body = new DeploymentExtended(name: createDeploymentName, id: requestId)
                            {
                            }
                        };
                    });

            deploymentsMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                parameters.DeploymentName,
                null,
                new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<bool>()
                {
                    Body = true
                }));

            SetupListForResourceGroupAsync(parameters.ResourceGroupName, new List<GenericResourceExpanded>
            {
                CreateGenericResource(null, null, "website")
            });

            var operationId = Guid.NewGuid().ToString();
            var operationQueue = new Queue<DeploymentOperation>();
            operationQueue.Enqueue(
                new DeploymentOperation(
                    operationId: operationId,
                    properties: new DeploymentOperationProperties(
                        provisioningState: "Succeeded",
                        targetResource: new TargetResource()
                        {
                            ResourceType = "Microsoft.Website",
                            ResourceName = resourceName
                        })));

            deploymentOperationsMock.Setup(f => f.ListWithHttpMessagesAsync(
                parameters.ResourceGroupName,
                parameters.DeploymentName,
                null,
                null,
                new CancellationToken()))
                .Returns<string, string, int?, Dictionary<string, List<string>>, CancellationToken>(
                    async (getResourceGroupName, getDeploymentName, top, customHeaders, cancellationToken) =>
                    {
                        await Task.Delay(100, cancellationToken);

                        if (deploymentNames.Contains(getDeploymentName))
                        {
                            return new AzureOperationResponse<IPage<DeploymentOperation>>()
                            {
                                Body = GetPagableType(
                                    new List<DeploymentOperation>()
                                    {
                                        operationQueue.Dequeue()
                                    })
                            };
                        }

                        throw new CloudException(String.Format("Deployment '{0}' could not be found.", getDeploymentName));
                    });

            Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.PSResourceGroupDeployment result = resourcesClient.ExecuteResourceGroupDeployment(parameters);
            Assert.Equal(deploymentName, result.DeploymentName);
            Assert.Equal("Succeeded", result.ProvisioningState);
            progressLoggerMock.Verify(
                f => f(string.Format("Resource {0} '{1}' provisioning status is {2}", "Microsoft.Website", resourceName, "Succeeded".ToLower())),
                Times.Once());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void NewResourceGroupWithDeploymentSucceeds()
        {
            Uri templateUri = new Uri("http://templateuri.microsoft.com");
            Deployment deploymentFromGet = new Deployment();
            Deployment deploymentFromValidate = new Deployment();

            PSCreateResourceGroupParameters resourceGroupParameters = new PSCreateResourceGroupParameters()
            {
                ResourceGroupName = resourceGroupName,
                Location = resourceGroupLocation,
                ConfirmAction = ConfirmAction
            };

            PSDeploymentCmdletParameters deploymentParameters = new PSDeploymentCmdletParameters()
            {
                ScopeType = DeploymentScopeType.ResourceGroup,
                ResourceGroupName = resourceGroupName,
                DeploymentName = deploymentName,
                TemplateFile = templateFile
            };
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(resourceGroupParameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<bool>() { Body = false }));

            resourceGroupMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(
                resourceGroupParameters.ResourceGroupName,
                It.IsAny<ResourceGroup>(),
                null,
                new CancellationToken()))
                    .Returns(Task.Factory.StartNew(() =>
                    new AzureOperationResponse<ResourceGroup>()
                    {
                        Body = new ResourceGroup(location: resourceGroupParameters.Location, name: resourceGroupParameters.ResourceGroupName)
                    }));

            resourceGroupMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName,  "createdTime,changedTime", null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<ResourceGroup>()
                {
                    Body = new ResourceGroup() { Location = resourceGroupLocation }
                }));

            deploymentsMock.Setup(f => f.BeginCreateOrUpdateWithHttpMessagesAsync(resourceGroupName, deploymentName, It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<DeploymentExtended>()
                {
                    Body = new DeploymentExtended(name: deploymentName, id: requestId)
                    {
                    }
                }))
                .Callback((string name, string dName, Deployment bDeploy, Dictionary<string, List<string>> customHeaders, CancellationToken token) =>
                { deploymentFromGet = bDeploy; });
            deploymentsMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName, deploymentName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<DeploymentExtended>()
                {
                    Body = new DeploymentExtended(
                        name: deploymentName,
                        properties: new DeploymentPropertiesExtended(
                            mode: DeploymentMode.Incremental,
                            correlationId: "123",
                            provisioningState: "Succeeded"))
                }));

            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(resourceGroupName, It.IsAny<string>(), It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<DeploymentValidateResult>()
                    {
                        Body = new DeploymentValidateResult
                        {
                        }
                    }))
                .Callback((string rg, string dn, Deployment d, Dictionary<string, List<string>> customHeaders, CancellationToken c) => { deploymentFromValidate = d; });
            deploymentsMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                null,
                new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<bool>()
                {
                    Body = true
                }));

            SetupListForResourceGroupAsync(resourceGroupParameters.ResourceGroupName, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "website") });
            deploymentOperationsMock.Setup(f => f.ListWithHttpMessagesAsync(resourceGroupName, deploymentName, null, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(GetPagableType(new List<DeploymentOperation>()
                    {
                        new DeploymentOperation(
                            operationId: Guid.NewGuid().ToString(),
                            properties: new DeploymentOperationProperties(
                                provisioningState: "Succeeded",
                                targetResource: new TargetResource()
                                {
                                    ResourceType = "Microsoft.Website",
                                    ResourceName = resourceName
                                }))
                    }))));

            PSResourceGroup result = resourcesClient.CreatePSResourceGroup(resourceGroupParameters);
            Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.PSResourceGroupDeployment deploymentResult = resourcesClient.ExecuteResourceGroupDeployment(deploymentParameters);
            deploymentsMock.Verify((f => f.BeginCreateOrUpdateWithHttpMessagesAsync(resourceGroupName, deploymentName, deploymentFromGet, null, new CancellationToken())), Times.Once());
            Assert.Equal(deploymentParameters.ResourceGroupName, deploymentResult.ResourceGroupName);

            Assert.Equal(DeploymentMode.Incremental, deploymentFromGet.Properties.Mode);
            Assert.NotNull(deploymentFromGet.Properties.Template);

            progressLoggerMock.Verify(
                f => f(string.Format("Resource {0} '{1}' provisioning status is {2}",
                        "Microsoft.Website",
                        resourceName,
                        "Succeeded".ToLower())),
                Times.Once());
        }


        [Fact(Skip = "TODO: Fix the test to fit the new client post migration")]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void ShowsFailureErrorWhenResourceGroupWithDeploymentFails()
        {
            Uri templateUri = new Uri("http://templateuri.microsoft.com");
            Deployment deploymentFromGet = new Deployment();
            Deployment deploymentFromValidate = new Deployment();

            PSCreateResourceGroupParameters resourceGroupparameters = new PSCreateResourceGroupParameters()
            {
                ResourceGroupName = resourceGroupName,
                Location = resourceGroupLocation,
                ConfirmAction = ConfirmAction
            };

            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(resourceGroupparameters.ResourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(false)));

            resourceGroupMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(
                resourceGroupparameters.ResourceGroupName,
                It.IsAny<ResourceGroup>(),
                null,
                new CancellationToken()))
                    .Returns(Task.Factory.StartNew(() =>
                        CreateAzureOperationResponse(new ResourceGroup(location: resourceGroupparameters.Location, name: resourceGroupparameters.ResourceGroupName))
                    ));
            resourceGroupMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName, "createdTime,changedTime", null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(new ResourceGroup() { Location = resourceGroupLocation })
                ));
            deploymentsMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(resourceGroupName, deploymentName, It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(new DeploymentExtended(name: deploymentName, id: requestId)
                {
                })))
                .Callback((string name, string dName, Deployment bDeploy, Dictionary<string, List<string>> customHeaders, CancellationToken token) => { deploymentFromGet = bDeploy; });
            deploymentsMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName, deploymentName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(new DeploymentExtended(
                    name: deploymentName,
                    properties: new DeploymentPropertiesExtended(
                        mode: DeploymentMode.Incremental,
                        provisioningState: "Succeeded")))));
            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(resourceGroupName, It.IsAny<string>(), It.IsAny<Deployment>(), null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse(new DeploymentValidateResult{})))
                .Callback((string resourceGroup, string deployment, Deployment d, Dictionary<string, List<string>> customHeaders, CancellationToken c) => { deploymentFromValidate = d; });
            SetupListForResourceGroupAsync(resourceGroupparameters.ResourceGroupName, new List<GenericResourceExpanded>() {
                CreateGenericResource(null, null, "website") });
            var listOperations = new List<DeploymentOperation>() {
                new DeploymentOperation(
                    operationId: Guid.NewGuid().ToString(),
                    properties: new DeploymentOperationProperties(
                        provisioningState: "Failed",
                        statusMessage: new StatusMessage("{\"Code\":\"Conflict\"}"),
                        targetResource: new TargetResource()
                        {
                            ResourceType = "Microsoft.Website",
                            ResourceName = resourceName
                        }))
            };
            var pageableOperations = new Page<DeploymentOperation>();
            pageableOperations.SetItemValue(listOperations);

            deploymentOperationsMock.Setup(f => f.ListWithHttpMessagesAsync(resourceGroupName, deploymentName, null, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => CreateAzureOperationResponse((IPage<DeploymentOperation>)pageableOperations)));

            PSResourceGroup result = resourcesClient.CreatePSResourceGroup(resourceGroupparameters);

            deploymentsMock.Verify((f => f.CreateOrUpdateWithHttpMessagesAsync(resourceGroupName, deploymentName, deploymentFromGet, null, new CancellationToken())), Times.Once());
            Assert.Equal(resourceGroupparameters.ResourceGroupName, result.ResourceGroupName);
            Assert.Equal(resourceGroupparameters.Location, result.Location);

            Assert.Equal(DeploymentMode.Incremental, deploymentFromGet.Properties.Mode);
            Assert.NotNull(deploymentFromGet.Properties.Template);

            errorLoggerMock.Verify(
                f => f(string.Format("Resource {0} '{1}' failed with message '{2}'",
                        "Microsoft.Website",
                        resourceName,
                        "{\"Code\":\"Conflict\"}")),
                Times.Once());
        }

        [Fact(Skip = "TODO: Fix the test to fit the new client post migration")]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void ExtractsErrorMessageFromFailedDeploymentOperation()
        {
            Uri templateUri = new Uri("http://templateuri.microsoft.com");
            Deployment deploymentFromGet = new Deployment();
            Deployment deploymentFromValidate = new Deployment();

            PSCreateResourceGroupParameters resourceGroupparameters = new PSCreateResourceGroupParameters()
            {
                ResourceGroupName = resourceGroupName,
                Location = resourceGroupLocation,
                ConfirmAction = ConfirmAction
            };

            PSDeploymentCmdletParameters deploymentParameters = new PSDeploymentCmdletParameters()
            {
                ScopeType = DeploymentScopeType.ResourceGroup,
                ResourceGroupName = resourceGroupName,
                DeploymentName = deploymentName,
                TemplateFile = templateFile
            };

            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(resourceGroupparameters.ResourceGroupName, null, new CancellationToken()))
            .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<bool>()
            {
                Body = false
            }));

            resourceGroupMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(
                resourceGroupparameters.ResourceGroupName,
                It.IsAny<ResourceGroup>(),
                null,
                new CancellationToken()))
            .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<ResourceGroup>()
                {
                    Body = new ResourceGroup(location: resourceGroupparameters.Location, name: resourceGroupparameters.ResourceGroupName)
                }
            ));
            resourceGroupMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName,  "createdTime,changedTime", null, new CancellationToken()))
            .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<ResourceGroup>() { Body = new ResourceGroup() { Location = resourceGroupLocation } }
            ));
            deploymentsMock.Setup(f => f.CreateOrUpdateWithHttpMessagesAsync(
                resourceGroupName,
                deploymentName,
                It.IsAny<Deployment>(),
                null,
                new CancellationToken()))
            .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<DeploymentExtended>()
            {
                Body = new DeploymentExtended(name: deploymentName, id: requestId)
                { 
                }
            }))
            .Callback((string name, string dName, Deployment bDeploy, Dictionary<string, List<string>> customHeaders,
                    CancellationToken token) =>
            { deploymentFromGet = bDeploy; });
            deploymentsMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName, deploymentName, null, new CancellationToken()))
            .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<DeploymentExtended>()
                {
                    Body = new DeploymentExtended(
                        name: deploymentName,
                        properties: new DeploymentPropertiesExtended(
                            mode: DeploymentMode.Incremental,
                            provisioningState: "Succeeded"))
                }
            ));
            deploymentsMock.Setup(f => f.ValidateWithHttpMessagesAsync(
                resourceGroupName,
                It.IsAny<string>(),
                It.IsAny<Deployment>(),
                null,
                new CancellationToken()))
            .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<DeploymentValidateResult>()
                {
                }
            ))
            .Callback((string rg, string dn, Deployment d, Dictionary<string, List<string>> customHeaders,
                    CancellationToken c) =>
            { deploymentFromValidate = d; });

            SetupListForResourceGroupAsync(resourceGroupparameters.ResourceGroupName, new List<GenericResourceExpanded>() {
                CreateGenericResource(location: null, id: null, name: "website", type: null)});

            var listOperations = new List<DeploymentOperation>() {
                new DeploymentOperation(
                    operationId: Guid.NewGuid().ToString(),
                    properties: new DeploymentOperationProperties(
                        provisioningState: "Failed",
                        statusMessage: new StatusMessage("A really bad error occurred"),
                        targetResource: new TargetResource()
                        {
                            ResourceType = "Microsoft.Website",
                            ResourceName = resourceName
                        }))
            };

            deploymentOperationsMock.Setup(f => f.ListWithHttpMessagesAsync(
                resourceGroupName,
                deploymentName,
                null,
                null,
                new CancellationToken()))
            .Returns(Task.Factory.StartNew(() =>
                new AzureOperationResponse<IPage<DeploymentOperation>>()
                {
                    Body = GetPagableType<DeploymentOperation>(listOperations)
                }
            ));

            PSResourceGroup result = resourcesClient.CreatePSResourceGroup(resourceGroupparameters);
            Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.PSResourceGroupDeployment deploymentResult = resourcesClient.ExecuteResourceGroupDeployment(deploymentParameters);

            deploymentsMock.Verify((f => f.CreateOrUpdateWithHttpMessagesAsync(
                    resourceGroupName,
                    deploymentName,
                    deploymentFromGet,
                    null,
                    new CancellationToken())),
                Times.Never);
            Assert.Equal(deploymentParameters.ResourceGroupName, deploymentResult.ResourceGroupName);

            Assert.Equal(DeploymentMode.Incremental, deploymentFromGet.Properties.Mode);
            Assert.NotNull(deploymentFromGet.Properties.Template);

            errorLoggerMock.Verify(
                f => f("A really bad error occurred"),
                Times.Once());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void GetsSpecificResourceGroup()
        {
            string name = resourceGroupName;
            GenericResourceExpanded resource1 = CreateGenericResource(
                resourceGroupLocation,
                "/subscriptions/abc123/resourceGroups/group1/providers/Microsoft.Test/servers/r12345sql/db/r45678db",
                resourceName);
            GenericResourceExpanded resource2 = CreateGenericResource(
                resourceGroupLocation,
                "/subscriptions/abc123/resourceGroups/group1/providers/Microsoft.Test/servers/r12345sql/db/r45678db",
                resourceName + "2");
            ResourceGroup resourceGroup = new ResourceGroup(
                location: resourceGroupLocation,
                name: name,
                properties: new ResourceGroupProperties("Succeeded"));
            resourceGroupMock.Setup(f => f.GetWithHttpMessagesAsync(name, "createdTime,changedTime", null, new CancellationToken()))
                             .Returns(Task.Factory.StartNew(() =>
                                new AzureOperationResponse<ResourceGroup>()
                                {
                                    Body = resourceGroup
                                }));
            SetupListForResourceGroupAsync(name, new List<GenericResourceExpanded>() { resource1, resource2 });

            List<PSResourceGroup> actual = resourcesClient.FilterResourceGroups(name, null, true, null, true);

            Assert.Single(actual);
            Assert.Equal(name, actual[0].ResourceGroupName);
            Assert.Equal(resourceGroupLocation, actual[0].Location);
            Assert.Equal("Succeeded", actual[0].ProvisioningState);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void GetsAllResourceGroups()
        {
            ResourceGroup resourceGroup1 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 1);
            ResourceGroup resourceGroup2 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 2);
            ResourceGroup resourceGroup3 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 3);
            ResourceGroup resourceGroup4 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 4);
            var listResult = new List<ResourceGroup>() { resourceGroup1, resourceGroup2, resourceGroup3, resourceGroup4 };
            var pagableResult = new Page<ResourceGroup>();
            pagableResult.SetItemValue(listResult);
            resourceGroupMock.Setup(f => f.ListWithHttpMessagesAsync(
                It.IsAny<ODataQuery<ResourceGroupFilterWithExpand>>(), 
                It.IsAny<Dictionary<string, List<string>>>(), 
                new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<IPage<ResourceGroup>>()
                {
                    Body = pagableResult
                }));
            
            SetupListForResourceGroupAsync(resourceGroup1.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });
            SetupListForResourceGroupAsync(resourceGroup2.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });
            SetupListForResourceGroupAsync(resourceGroup3.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });
            SetupListForResourceGroupAsync(resourceGroup4.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });

            List<PSResourceGroup> actual = resourcesClient.FilterResourceGroups(null, null, false);

            Assert.Equal(4, actual.Count);
            Assert.Equal(resourceGroup1.Name, actual[0].ResourceGroupName);
            Assert.Equal(resourceGroup2.Name, actual[1].ResourceGroupName);
            Assert.Equal(resourceGroup3.Name, actual[2].ResourceGroupName);
            Assert.Equal(resourceGroup4.Name, actual[3].ResourceGroupName);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void GetsAllResourceGroupsWithDetails()
        {
            ResourceGroup resourceGroup1 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 1);
            ResourceGroup resourceGroup2 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 2);
            ResourceGroup resourceGroup3 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 3);
            ResourceGroup resourceGroup4 = new ResourceGroup(location: resourceGroupLocation, name: resourceGroupName + 4);
            var listResult = new List<ResourceGroup>() { resourceGroup1, resourceGroup2, resourceGroup3, resourceGroup4 };
            var pagableResult = new Page<ResourceGroup>();
            pagableResult.SetItemValue(listResult);
            resourceGroupMock.Setup(f => f.ListWithHttpMessagesAsync(
                It.IsAny<ODataQuery<ResourceGroupFilterWithExpand>>(), 
                It.IsAny<Dictionary<string, List<string>>>(), 
                new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<IPage<ResourceGroup>>()
                {
                    Body = pagableResult
                }));
            SetupListForResourceGroupAsync(resourceGroup1.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });
            SetupListForResourceGroupAsync(resourceGroup2.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });
            SetupListForResourceGroupAsync(resourceGroup3.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });
            SetupListForResourceGroupAsync(resourceGroup4.Name, new List<GenericResourceExpanded>() { CreateGenericResource(null, null, "resource") });

            List<PSResourceGroup> actual = resourcesClient.FilterResourceGroups(null, null, true);

            Assert.Equal(4, actual.Count);
            Assert.Equal(resourceGroup1.Name, actual[0].ResourceGroupName);
            Assert.Equal(resourceGroup2.Name, actual[1].ResourceGroupName);
            Assert.Equal(resourceGroup3.Name, actual[2].ResourceGroupName);
            Assert.Equal(resourceGroup4.Name, actual[3].ResourceGroupName);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void DeletesResourcesGroup()
        {
            resourceGroupMock.Setup(f => f.CheckExistenceWithHttpMessagesAsync(resourceGroupName, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() =>
                    new AzureOperationResponse<bool>()
                    {
                        Body = true
                    }));

            resourceGroupMock.Setup(f => f.DeleteWithHttpMessagesAsync(resourceGroupName, null, null, new CancellationToken()))
                .Returns(Task.Factory.StartNew(() => new Rest.Azure.AzureOperationHeaderResponse<ResourceGroupsDeleteHeaders>()));

            resourcesClient.DeleteResourceGroup(resourceGroupName);

            resourceGroupMock.Verify(f => f.DeleteWithHttpMessagesAsync(resourceGroupName, null, null, It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void FiltersOneResourceGroupDeployment()
        {
            FilterDeploymentOptions options = new FilterDeploymentOptions(DeploymentScopeType.ResourceGroup)
            {
                DeploymentName = deploymentName,
                ResourceGroupName = resourceGroupName
            };
            deploymentsMock.Setup(f => f.GetWithHttpMessagesAsync(resourceGroupName, deploymentName, null, new CancellationToken()))
            .Returns(Task.Factory.StartNew(() => new AzureOperationResponse<DeploymentExtended>()
            {
                Body = new DeploymentExtended(
                    name: deploymentName,
                    properties: new DeploymentPropertiesExtended(
                        mode: DeploymentMode.Incremental,
                        correlationId: "123",
                        templateLink: new TemplateLink()
                        {
                            Uri = "http://microsoft.com/"
                        }))
            }));

            List<Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.PSResourceGroupDeployment> result = resourcesClient.FilterResourceGroupDeployments(options);

            Assert.Equal(deploymentName, result[0].DeploymentName);
            Assert.Equal(resourceGroupName, result[0].ResourceGroupName);
            Assert.Equal(DeploymentMode.Incremental, result[0].Mode);
            Assert.Equal("123", result[0].CorrelationId);
            Assert.Equal(new Uri("http://microsoft.com").ToString(), result[0].TemplateLink.Uri.ToString());
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void CancelsActiveDeployment()
        {
            var result = new AzureOperationResponse<DeploymentExtended>()
            {
                Body = new DeploymentExtended(
                    name: deploymentName + 3,
                    properties: new DeploymentPropertiesExtended(
                        mode: DeploymentMode.Incremental,
                        templateLink: new TemplateLink()
                        {
                            Uri = "http://microsoft1.com"
                        },
                        provisioningState: "Running"))
            };

            deploymentsMock.Setup(f => f.GetWithHttpMessagesAsync(
                resourceGroupName,
                deploymentName + 3,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(Task.Factory.StartNew(() => result));

            deploymentsMock.Setup(f => f.CancelWithHttpMessagesAsync(
                resourceGroupName,
                deploymentName + 3,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(Task.Factory.StartNew(() => new Rest.Azure.AzureOperationResponse()));

            resourcesClient.CancelDeployment(
                new FilterDeploymentOptions(DeploymentScopeType.ResourceGroup)
                {
                    ResourceGroupName = resourceGroupName,
                    DeploymentName = deploymentName + 3
                });

            deploymentsMock.Verify(f => f.CancelWithHttpMessagesAsync(resourceGroupName, deploymentName + 3, null, new CancellationToken()), Times.Once());
        }

        //[Fact(Skip = "Test produces different outputs since hashtable order is not guaranteed.")]
        //[Trait(Category.AcceptanceType, Category.CheckIn)]
        //public void SerializeHashtableProperlyHandlesAllDataTypes()
        //{
        //    Hashtable hashtable = new Hashtable();
        //    var pass = "pass";
        //    hashtable.Add("key1", "string");
        //    hashtable.Add("key2", 1);
        //    hashtable.Add("key3", true);
        //    hashtable.Add("key4", new DateTime(2014, 05, 08));
        //    hashtable.Add("key5", null);
        //    hashtable.Add("key6", pass);

        //    string resultWithoutAddedLayer = resourcesClient.SerializeHashtable(hashtable, false);
        //    Assert.NotEmpty(resultWithoutAddedLayer);
        //    EqualsIgnoreWhitespace(@"{
        //        ""key5"": null,
        //        ""key2"": 1,
        //        ""key4"": ""2014-05-08T00:00:00"",
        //        ""key6"": ""pass"",
        //        ""key1"": ""string"",
        //        ""key3"": true
        //    }", resultWithoutAddedLayer);

        //    string resultWithAddedLayer = resourcesClient.SerializeHashtable(hashtable, true);
        //    Assert.NotEmpty(resultWithAddedLayer);
        //    EqualsIgnoreWhitespace(@"{
        //      ""key5"": {
        //        ""value"": null
        //      },
        //      ""key2"": {
        //        ""value"": 1
        //      },
        //      ""key4"": {
        //        ""value"": ""2014-05-08T00:00:00""
        //      },
        //      ""key6"": {
        //        ""value"": ""pass""
        //      },
        //      ""key1"": {
        //        ""value"": ""string""
        //      },
        //      ""key3"": {
        //        ""value"": true
        //      }
        //    }", resultWithAddedLayer);
        //}
    }
}
