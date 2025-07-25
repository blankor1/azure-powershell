// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.ServiceFabricManagedClusters.Models
{
    using System.Linq;

    /// <summary>
    /// Node type fault simulation object with status.
    /// </summary>
    public partial class NodeTypeFaultSimulation
    {
        /// <summary>
        /// Initializes a new instance of the NodeTypeFaultSimulation class.
        /// </summary>
        public NodeTypeFaultSimulation()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the NodeTypeFaultSimulation class.
        /// </summary>

        /// <param name="nodeTypeName">Node type name.
        /// </param>

        /// <param name="status">Fault simulation status
        /// Possible values include: &#39;Starting&#39;, &#39;Active&#39;, &#39;Stopping&#39;, &#39;Done&#39;,
        /// &#39;StartFailed&#39;, &#39;StopFailed&#39;</param>

        /// <param name="operationId">Current or latest asynchronous operation identifier on the node type.
        /// </param>

        /// <param name="operationStatus">Current or latest asynchronous operation status on the node type
        /// Possible values include: &#39;Created&#39;, &#39;Started&#39;, &#39;Succeeded&#39;, &#39;Failed&#39;,
        /// &#39;Aborted&#39;, &#39;Canceled&#39;</param>
        public NodeTypeFaultSimulation(string nodeTypeName = default(string), string status = default(string), string operationId = default(string), string operationStatus = default(string))

        {
            this.NodeTypeName = nodeTypeName;
            this.Status = status;
            this.OperationId = operationId;
            this.OperationStatus = operationStatus;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets node type name.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "nodeTypeName")]
        public string NodeTypeName {get; set; }

        /// <summary>
        /// Gets or sets fault simulation status Possible values include: &#39;Starting&#39;, &#39;Active&#39;, &#39;Stopping&#39;, &#39;Done&#39;, &#39;StartFailed&#39;, &#39;StopFailed&#39;
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "status")]
        public string Status {get; set; }

        /// <summary>
        /// Gets or sets current or latest asynchronous operation identifier on the
        /// node type.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "operationId")]
        public string OperationId {get; set; }

        /// <summary>
        /// Gets current or latest asynchronous operation status on the node type Possible values include: &#39;Created&#39;, &#39;Started&#39;, &#39;Succeeded&#39;, &#39;Failed&#39;, &#39;Aborted&#39;, &#39;Canceled&#39;
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "operationStatus")]
        public string OperationStatus {get; private set; }
    }
}