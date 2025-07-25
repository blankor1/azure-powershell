// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.
namespace Microsoft.Azure.Management.ServiceFabricManagedClusters
{
    using Microsoft.Rest.Azure;
    using Models;

    /// <summary>
    /// Extension methods for ManagedMaintenanceWindowStatusOperations
    /// </summary>
    public static partial class ManagedMaintenanceWindowStatusOperationsExtensions
    {
        /// <summary>
        /// Action to get Maintenance Window Status of the Service Fabric Managed
        /// Clusters.
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The name of the resource group. The name is case insensitive.
        /// </param>
        /// <param name='clusterName'>
        /// The name of the cluster resource.
        /// </param>
        public static ManagedMaintenanceWindowStatus Get(this IManagedMaintenanceWindowStatusOperations operations, string resourceGroupName, string clusterName)
        {
                return ((IManagedMaintenanceWindowStatusOperations)operations).GetAsync(resourceGroupName, clusterName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Action to get Maintenance Window Status of the Service Fabric Managed
        /// Clusters.
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The name of the resource group. The name is case insensitive.
        /// </param>
        /// <param name='clusterName'>
        /// The name of the cluster resource.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<ManagedMaintenanceWindowStatus> GetAsync(this IManagedMaintenanceWindowStatusOperations operations, string resourceGroupName, string clusterName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.GetWithHttpMessagesAsync(resourceGroupName, clusterName, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
