// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.ServiceFabricManagedClusters
{
    using System.Linq;
    using Microsoft.Rest;
    using Microsoft.Rest.Azure;
    using Models;

    /// <summary>
    /// Service Fabric Managed Clusters Management Client
    /// </summary>
    public partial class ServiceFabricManagedClustersManagementClient : Microsoft.Rest.ServiceClient<ServiceFabricManagedClustersManagementClient>, IServiceFabricManagedClustersManagementClient, IAzureClient
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri BaseUri { get; set; }
        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public Newtonsoft.Json.JsonSerializerSettings SerializationSettings { get; private set; }
        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public Newtonsoft.Json.JsonSerializerSettings DeserializationSettings { get; private set; }
        /// <summary>
        /// Credentials needed for the client to connect to Azure.
        /// </summary>
        public Microsoft.Rest.ServiceClientCredentials Credentials { get; private set; }

        /// <summary>
        /// The API version to use for this operation.
        /// </summary>
        public string ApiVersion { get; private set; }

        /// <summary>
        /// The ID of the target subscription.
        /// </summary>
        public string SubscriptionId { get; set;}

        /// <summary>
        /// The preferred language for the response.
        /// </summary>
        public string AcceptLanguage { get; set;}

        /// <summary>
        /// The retry timeout in seconds for Long Running Operations. Default
        /// /// value is 30.
        /// </summary>
        public int? LongRunningOperationRetryTimeout { get; set;}

        /// <summary>
        /// Whether a unique x-ms-client-request-id should be generated. When
        /// /// set to true a unique x-ms-client-request-id value is generated and
        /// /// included in each request. Default is true.
        /// </summary>
        public bool? GenerateClientRequestId { get; set;}

        /// <summary>
        /// Gets the IOperations
        /// </summary>
        public virtual IOperations Operations { get; private set; }
        /// <summary>
        /// Gets the IManagedClusterVersionOperations
        /// </summary>
        public virtual IManagedClusterVersionOperations ManagedClusterVersion { get; private set; }
        /// <summary>
        /// Gets the IOperationResultsOperations
        /// </summary>
        public virtual IOperationResultsOperations OperationResults { get; private set; }
        /// <summary>
        /// Gets the IOperationStatusOperations
        /// </summary>
        public virtual IOperationStatusOperations OperationStatus { get; private set; }
        /// <summary>
        /// Gets the IManagedUnsupportedVMSizesOperations
        /// </summary>
        public virtual IManagedUnsupportedVMSizesOperations ManagedUnsupportedVMSizes { get; private set; }
        /// <summary>
        /// Gets the IManagedClustersOperations
        /// </summary>
        public virtual IManagedClustersOperations ManagedClusters { get; private set; }
        /// <summary>
        /// Gets the IApplicationTypesOperations
        /// </summary>
        public virtual IApplicationTypesOperations ApplicationTypes { get; private set; }
        /// <summary>
        /// Gets the IApplicationTypeVersionsOperations
        /// </summary>
        public virtual IApplicationTypeVersionsOperations ApplicationTypeVersions { get; private set; }
        /// <summary>
        /// Gets the IApplicationsOperations
        /// </summary>
        public virtual IApplicationsOperations Applications { get; private set; }
        /// <summary>
        /// Gets the IServicesOperations
        /// </summary>
        public virtual IServicesOperations Services { get; private set; }
        /// <summary>
        /// Gets the IManagedApplyMaintenanceWindowOperations
        /// </summary>
        public virtual IManagedApplyMaintenanceWindowOperations ManagedApplyMaintenanceWindow { get; private set; }
        /// <summary>
        /// Gets the IManagedMaintenanceWindowStatusOperations
        /// </summary>
        public virtual IManagedMaintenanceWindowStatusOperations ManagedMaintenanceWindowStatus { get; private set; }
        /// <summary>
        /// Gets the IManagedAzResiliencyStatusOperations
        /// </summary>
        public virtual IManagedAzResiliencyStatusOperations ManagedAzResiliencyStatus { get; private set; }
        /// <summary>
        /// Gets the INodeTypesOperations
        /// </summary>
        public virtual INodeTypesOperations NodeTypes { get; private set; }
        /// <summary>
        /// Gets the INodeTypeSkusOperations
        /// </summary>
        public virtual INodeTypeSkusOperations NodeTypeSkus { get; private set; }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling ServiceFabricManagedClustersManagementClient.Dispose(). False: will not dispose provided httpClient</param>
        protected ServiceFabricManagedClustersManagementClient(System.Net.Http.HttpClient httpClient, bool disposeHttpClient) : base(httpClient, disposeHttpClient)
        {
            this.Initialize();
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected ServiceFabricManagedClustersManagementClient(params System.Net.Http.DelegatingHandler[] handlers) : base(handlers)
        {
            this.Initialize();
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient  class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected ServiceFabricManagedClustersManagementClient(System.Net.Http.HttpClientHandler rootHandler, params System.Net.Http.DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            this.Initialize();
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        protected ServiceFabricManagedClustersManagementClient(System.Uri baseUri, params System.Net.Http.DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            this.BaseUri = baseUri;
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        protected ServiceFabricManagedClustersManagementClient(System.Uri baseUri, System.Net.Http.HttpClientHandler rootHandler, params System.Net.Http.DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
        
            this.BaseUri = baseUri;
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public ServiceFabricManagedClustersManagementClient(Microsoft.Rest.ServiceClientCredentials credentials, params System.Net.Http.DelegatingHandler[] handlers) : this(handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
            
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name="credentials">
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling ServiceFabricManagedClustersManagementClient.Dispose(). False: will not dispose provided httpClient</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public ServiceFabricManagedClustersManagementClient(Microsoft.Rest.ServiceClientCredentials credentials, System.Net.Http.HttpClient httpClient, bool disposeHttpClient) : this(httpClient, disposeHttpClient)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
            
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name="credentials">
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public ServiceFabricManagedClustersManagementClient(Microsoft.Rest.ServiceClientCredentials credentials, System.Net.Http.HttpClientHandler rootHandler, params System.Net.Http.DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
            
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name="credentials">
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public ServiceFabricManagedClustersManagementClient(System.Uri baseUri, Microsoft.Rest.ServiceClientCredentials credentials, params System.Net.Http.DelegatingHandler[] handlers) : this(handlers) 
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            this.BaseUri = baseUri;
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
            
        }
        /// <summary>
        /// Initializes a new instance of the ServiceFabricManagedClustersManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name="credentials">
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public ServiceFabricManagedClustersManagementClient(System.Uri baseUri, Microsoft.Rest.ServiceClientCredentials credentials, System.Net.Http.HttpClientHandler rootHandler, params System.Net.Http.DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            this.BaseUri = baseUri;
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
            
        }
        /// <summary>
        /// An optional partial-method to perform custom initialization.
        /// </summary>
        partial void CustomInitialize();

        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            this.Operations = new Operations(this);
            this.ManagedClusterVersion = new ManagedClusterVersionOperations(this);
            this.OperationResults = new OperationResultsOperations(this);
            this.OperationStatus = new OperationStatusOperations(this);
            this.ManagedUnsupportedVMSizes = new ManagedUnsupportedVMSizesOperations(this);
            this.ManagedClusters = new ManagedClustersOperations(this);
            this.ApplicationTypes = new ApplicationTypesOperations(this);
            this.ApplicationTypeVersions = new ApplicationTypeVersionsOperations(this);
            this.Applications = new ApplicationsOperations(this);
            this.Services = new ServicesOperations(this);
            this.ManagedApplyMaintenanceWindow = new ManagedApplyMaintenanceWindowOperations(this);
            this.ManagedMaintenanceWindowStatus = new ManagedMaintenanceWindowStatusOperations(this);
            this.ManagedAzResiliencyStatus = new ManagedAzResiliencyStatusOperations(this);
            this.NodeTypes = new NodeTypesOperations(this);
            this.NodeTypeSkus = new NodeTypeSkusOperations(this);
            this.BaseUri = new System.Uri("https://management.azure.com");
            this.ApiVersion = "2025-03-01-preview";
            this.AcceptLanguage = "en-US";
            this.LongRunningOperationRetryTimeout = 30;
            this.GenerateClientRequestId = true;
            SerializationSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new Microsoft.Rest.Serialization.ReadOnlyJsonContractResolver(),
                Converters = new System.Collections.Generic.List<Newtonsoft.Json.JsonConverter>
                    {
                        new Microsoft.Rest.Serialization.Iso8601TimeSpanConverter()
                    }
            };
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.TransformationJsonConverter());
            DeserializationSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new Microsoft.Rest.Serialization.ReadOnlyJsonContractResolver(),
                Converters = new System.Collections.Generic.List<Newtonsoft.Json.JsonConverter>
                    {
                        new Microsoft.Rest.Serialization.Iso8601TimeSpanConverter()
                    }
            };
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicSerializeJsonConverter<ServiceResourceProperties>("serviceKind"));
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicDeserializeJsonConverter<ServiceResourceProperties>("serviceKind"));
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicSerializeJsonConverter<Partition>("partitionScheme"));
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicDeserializeJsonConverter<Partition>("partitionScheme"));
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicSerializeJsonConverter<ServicePlacementPolicy>("type"));
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicDeserializeJsonConverter<ServicePlacementPolicy>("type"));
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicSerializeJsonConverter<ScalingMechanism>("kind"));
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicDeserializeJsonConverter<ScalingMechanism>("kind"));
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicSerializeJsonConverter<ScalingTrigger>("kind"));
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicDeserializeJsonConverter<ScalingTrigger>("kind"));
            SerializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicSerializeJsonConverter<FaultSimulationContent>("faultKind"));
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.PolymorphicDeserializeJsonConverter<FaultSimulationContent>("faultKind"));
            CustomInitialize();
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Serialization.TransformationJsonConverter());
            DeserializationSettings.Converters.Add(new Microsoft.Rest.Azure.CloudErrorJsonConverter());
        }
    }
}