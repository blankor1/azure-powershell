// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.Storage.Models
{
    using static Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Extensions;

    /// <summary>Storage task execution report for a run instance.</summary>
    public partial class StorageTaskReportProperties
    {

        /// <summary>
        /// <c>AfterFromJson</c> will be called after the json deserialization has finished, allowing customization of the object
        /// before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="json">The JsonNode that should be deserialized into this object.</param>

        partial void AfterFromJson(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject json);

        /// <summary>
        /// <c>AfterToJson</c> will be called after the json serialization has finished, allowing customization of the <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject"
        /// /> before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="container">The JSON container that the serialization result will be placed in.</param>

        partial void AfterToJson(ref Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject container);

        /// <summary>
        /// <c>BeforeFromJson</c> will be called before the json deserialization has commenced, allowing complete customization of
        /// the object before it is deserialized.
        /// If you wish to disable the default deserialization entirely, return <c>true</c> in the <paramref name= "returnNow" />
        /// output parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="json">The JsonNode that should be deserialized into this object.</param>
        /// <param name="returnNow">Determines if the rest of the deserialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeFromJson(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject json, ref bool returnNow);

        /// <summary>
        /// <c>BeforeToJson</c> will be called before the json serialization has commenced, allowing complete customization of the
        /// object before it is serialized.
        /// If you wish to disable the default serialization entirely, return <c>true</c> in the <paramref name="returnNow" /> output
        /// parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="container">The JSON container that the serialization result will be placed in.</param>
        /// <param name="returnNow">Determines if the rest of the serialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeToJson(ref Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject container, ref bool returnNow);

        /// <summary>
        /// Deserializes a <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode"/> into an instance of Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IStorageTaskReportProperties.
        /// </summary>
        /// <param name="node">a <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode" /> to deserialize from.</param>
        /// <returns>
        /// an instance of Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IStorageTaskReportProperties.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IStorageTaskReportProperties FromJson(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode node)
        {
            return node is Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject json ? new StorageTaskReportProperties(json) : null;
        }

        /// <summary>
        /// Deserializes a Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject into a new instance of <see cref="StorageTaskReportProperties" />.
        /// </summary>
        /// <param name="json">A Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject instance to deserialize from.</param>
        internal StorageTaskReportProperties(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject json)
        {
            bool returnNow = false;
            BeforeFromJson(json, ref returnNow);
            if (returnNow)
            {
                return;
            }
            {_taskAssignmentId = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("taskAssignmentId"), out var __jsonTaskAssignmentId) ? (string)__jsonTaskAssignmentId : (string)_taskAssignmentId;}
            {_storageAccountId = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("storageAccountId"), out var __jsonStorageAccountId) ? (string)__jsonStorageAccountId : (string)_storageAccountId;}
            {_startTime = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("startTime"), out var __jsonStartTime) ? (string)__jsonStartTime : (string)_startTime;}
            {_finishTime = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("finishTime"), out var __jsonFinishTime) ? (string)__jsonFinishTime : (string)_finishTime;}
            {_objectsTargetedCount = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("objectsTargetedCount"), out var __jsonObjectsTargetedCount) ? (string)__jsonObjectsTargetedCount : (string)_objectsTargetedCount;}
            {_objectsOperatedOnCount = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("objectsOperatedOnCount"), out var __jsonObjectsOperatedOnCount) ? (string)__jsonObjectsOperatedOnCount : (string)_objectsOperatedOnCount;}
            {_objectFailedCount = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("objectFailedCount"), out var __jsonObjectFailedCount) ? (string)__jsonObjectFailedCount : (string)_objectFailedCount;}
            {_objectsSucceededCount = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("objectsSucceededCount"), out var __jsonObjectsSucceededCount) ? (string)__jsonObjectsSucceededCount : (string)_objectsSucceededCount;}
            {_runStatusError = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("runStatusError"), out var __jsonRunStatusError) ? (string)__jsonRunStatusError : (string)_runStatusError;}
            {_runStatusEnum = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("runStatusEnum"), out var __jsonRunStatusEnum) ? (string)__jsonRunStatusEnum : (string)_runStatusEnum;}
            {_summaryReportPath = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("summaryReportPath"), out var __jsonSummaryReportPath) ? (string)__jsonSummaryReportPath : (string)_summaryReportPath;}
            {_taskId = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("taskId"), out var __jsonTaskId) ? (string)__jsonTaskId : (string)_taskId;}
            {_taskVersion = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("taskVersion"), out var __jsonTaskVersion) ? (string)__jsonTaskVersion : (string)_taskVersion;}
            {_runResult = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString>("runResult"), out var __jsonRunResult) ? (string)__jsonRunResult : (string)_runResult;}
            AfterFromJson(json);
        }

        /// <summary>
        /// Serializes this instance of <see cref="StorageTaskReportProperties" /> into a <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode" />.
        /// </summary>
        /// <param name="container">The <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject"/> container to serialize this object into. If the caller
        /// passes in <c>null</c>, a new instance will be created and returned to the caller.</param>
        /// <param name="serializationMode">Allows the caller to choose the depth of the serialization. See <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode"/>.</param>
        /// <returns>
        /// a serialized instance of <see cref="StorageTaskReportProperties" /> as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode" />.
        /// </returns>
        public Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode ToJson(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject container, Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode serializationMode)
        {
            container = container ?? new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonObject();

            bool returnNow = false;
            BeforeToJson(ref container, ref returnNow);
            if (returnNow)
            {
                return container;
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._taskAssignmentId)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._taskAssignmentId.ToString()) : null, "taskAssignmentId" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._storageAccountId)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._storageAccountId.ToString()) : null, "storageAccountId" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._startTime)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._startTime.ToString()) : null, "startTime" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._finishTime)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._finishTime.ToString()) : null, "finishTime" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._objectsTargetedCount)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._objectsTargetedCount.ToString()) : null, "objectsTargetedCount" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._objectsOperatedOnCount)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._objectsOperatedOnCount.ToString()) : null, "objectsOperatedOnCount" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._objectFailedCount)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._objectFailedCount.ToString()) : null, "objectFailedCount" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._objectsSucceededCount)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._objectsSucceededCount.ToString()) : null, "objectsSucceededCount" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._runStatusError)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._runStatusError.ToString()) : null, "runStatusError" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._runStatusEnum)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._runStatusEnum.ToString()) : null, "runStatusEnum" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._summaryReportPath)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._summaryReportPath.ToString()) : null, "summaryReportPath" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._taskId)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._taskId.ToString()) : null, "taskId" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._taskVersion)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._taskVersion.ToString()) : null, "taskVersion" ,container.Add );
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.SerializationMode.IncludeRead))
            {
                AddIf( null != (((object)this._runResult)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Json.JsonString(this._runResult.ToString()) : null, "runResult" ,container.Add );
            }
            AfterToJson(ref container);
            return container;
        }
    }
}