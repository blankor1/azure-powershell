
# ----------------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# Code generated by Microsoft (R) AutoRest Code Generator.Changes may cause incorrect behavior and will be lost if the code
# is regenerated.
# ----------------------------------------------------------------------------------

<#
.Synopsis
Update a new Event Hub as a nested resource within a Namespace.
.Description
Update a new Event Hub as a nested resource within a Namespace.
.Example
Set-AzEventHub -Name myEventHub -ResourceGroupName myResourceGroup -NamespaceName myNamespace -ArchiveNameFormat "{Namespace}/{EventHub}/{PartitionId}/{Year}/{Month}/{Day}/{Hour}/{Minute}/{Second}" -BlobContainer container -CaptureEnabled -DestinationName EventHubArchive.AzureBlockBlob -Encoding Avro -IntervalInSeconds 600 -SizeLimitInBytes 11000000 -SkipEmptyArchive -StorageAccountResourceId "/subscriptions/subscriptionId/resourceGroups/myResourceGroup/providers/Microsoft.Storage/storageAccounts/myStorageAccount"
.Example
$eventhub = Get-AzEventHub -Name myEventHub -ResourceGroupName myResourceGroup -NamespaceName myNamespace
Set-AzEventHub -InputObject $eventhub -RetentionTimeInHour 72

.Outputs
Microsoft.Azure.PowerShell.Cmdlets.EventHub.Models.IEventhub
.Link
https://learn.microsoft.com/powershell/module/az.eventhub/set-azeventhub
#>
function Set-AzEventHub {
[OutputType([Microsoft.Azure.PowerShell.Cmdlets.EventHub.Models.IEventhub])]
[CmdletBinding(DefaultParameterSetName='UpdateExpanded', PositionalBinding=$false, SupportsShouldProcess, ConfirmImpact='Medium')]
param(
    [Parameter(Mandatory)]
    [Alias('EventHubName')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Path')]
    [System.String]
    # The Event Hub name
    ${Name},

    [Parameter(Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Path')]
    [System.String]
    # The Namespace name
    ${NamespaceName},

    [Parameter(Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Path')]
    [System.String]
    # Name of the resource group within the azure subscription.
    ${ResourceGroupName},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Path')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Runtime.DefaultInfo(Script='(Get-AzContext).Subscription.Id')]
    [System.String]
    # Subscription credentials that uniquely identify a Microsoft Azure subscription.
    # The subscription ID forms part of the URI for every service call.
    ${SubscriptionId},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Blob naming convention for archive, e.g.
    # {Namespace}/{EventHub}/{PartitionId}/{Year}/{Month}/{Day}/{Hour}/{Minute}/{Second}.
    # Here all the parameters (Namespace,EventHub ..
    # etc) are mandatory irrespective of order
    ${ArchiveNameFormat},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Blob container Name
    ${BlobContainer},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Management.Automation.SwitchParameter]
    # A value that indicates whether capture description is enabled.
    ${CaptureDescriptionEnabled},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.PSArgumentCompleterAttribute("Avro", "AvroDeflate")]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Enumerates the possible values for the encoding format of capture description.
    # Note: 'AvroDeflate' will be deprecated in New API Version
    ${CaptureDescriptionEncoding},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int32]
    # The time window allows you to set the frequency with which the capture to Azure Blobs will happen, value should between 60 to 900 seconds
    ${CaptureDescriptionIntervalInSecond},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int32]
    # The size window defines the amount of data built up in your Event Hub before an capture operation, value should be between 10485760 to 524288000 bytes
    ${CaptureDescriptionSizeLimitInByte},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Management.Automation.SwitchParameter]
    # A value that indicates whether to Skip Empty Archives
    ${CaptureDescriptionSkipEmptyArchive},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # The Azure Data Lake Store name for the captured events
    ${DataLakeAccountName},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # The destination folder path for the captured events
    ${DataLakeFolderPath},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Subscription Id of Azure Data Lake Store
    ${DataLakeSubscriptionId},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Name for capture destination
    ${DestinationName},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.PSArgumentCompleterAttribute("SystemAssigned", "UserAssigned")]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Type of Azure Active Directory Managed Identity.
    ${IdentityType},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # ARM ID of Managed User Identity.
    # This property is required is the type is UserAssignedIdentity.
    # If type is SystemAssigned, then the System Assigned Identity Associated with the namespace will be used.
    ${IdentityUserAssignedIdentity},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int64]
    # Number of days to retain the events for this Event Hub, value should be 1 to 7 days
    ${MessageRetentionInDay},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.PSArgumentCompleterAttribute("LogAppend", "Create")]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Denotes the type of timestamp the message will hold.Two types of timestamp types - "AppendTime" and "CreateTime".
    # AppendTime refers the time in which message got appended inside broker log.
    # CreateTime refers to the time in which the message was generated on source side and producers can set this timestamp while sending the message.
    # Default value is AppendTime.
    # If you are using AMQP protocol, CreateTime equals AppendTime and its behavior remains the same.
    ${MessageTimestampDescriptionTimestampType},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int64]
    # Number of partitions created for the Event Hub, allowed values are from 1 to 32 partitions.
    ${PartitionCount},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.PSArgumentCompleterAttribute("Delete", "Compact", "DeleteOrCompact")]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Enumerates the possible values for cleanup policy
    ${RetentionDescriptionCleanupPolicy},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int64]
    # The minimum time a message will remain ineligible for compaction in the log.
    # This value is used when cleanupPolicy is Compact or DeleteOrCompact.
    ${RetentionDescriptionMinCompactionLagTimeInMinute},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int64]
    # Number of hours to retain the events for this Event Hub.
    # This should be positive value upto namespace SKU max.
    # -1 is a special case where retention time is infinite, but the size of an entity is restricted and its size depends on namespace SKU type.
    ${RetentionDescriptionRetentionTimeInHour},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.Int32]
    # Number of hours to retain the tombstone markers of a compacted Event Hub.
    # This value is used when cleanupPolicy is Compact or DeleteOrCompact.
    # Consumer must complete reading the tombstone marker within this specified amount of time if consumer begins from starting offset to ensure they get a valid snapshot for the specific key described by the tombstone marker within the compacted Event Hub
    ${RetentionDescriptionTombstoneRetentionTimeInHour},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.PSArgumentCompleterAttribute("Active", "Disabled", "Restoring", "SendDisabled", "ReceiveDisabled", "Creating", "Deleting", "Renaming", "Unknown")]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Enumerates the possible values for the status of the Event Hub.
    ${Status},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Resource id of the storage account to be used to create the blobs
    ${StorageAccountResourceId},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Gets and Sets Metadata of User.
    ${UserMetadata},

    [Parameter(ParameterSetName='UpdateViaJsonFilePath', Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Path of Json file supplied to the Update operation
    ${JsonFilePath},

    [Parameter(ParameterSetName='UpdateViaJsonString', Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Body')]
    [System.String]
    # Json string supplied to the Update operation
    ${JsonString},

    [Parameter()]
    [Alias('AzureRMContext', 'AzureCredential')]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Azure')]
    [System.Management.Automation.PSObject]
    # The DefaultProfile parameter is not functional.
    # Use the SubscriptionId parameter when available if executing the cmdlet against a different subscription.
    ${DefaultProfile},

    [Parameter(DontShow)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Runtime')]
    [System.Management.Automation.SwitchParameter]
    # Wait for .NET debugger to attach
    ${Break},

    [Parameter(DontShow)]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Runtime')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Runtime.SendAsyncStep[]]
    # SendAsync Pipeline Steps to be appended to the front of the pipeline
    ${HttpPipelineAppend},

    [Parameter(DontShow)]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Runtime')]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Runtime.SendAsyncStep[]]
    # SendAsync Pipeline Steps to be prepended to the front of the pipeline
    ${HttpPipelinePrepend},

    [Parameter(DontShow)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Runtime')]
    [System.Uri]
    # The URI for the proxy server to use
    ${Proxy},

    [Parameter(DontShow)]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Runtime')]
    [System.Management.Automation.PSCredential]
    # Credentials for a proxy server to use for the remote call
    ${ProxyCredential},

    [Parameter(DontShow)]
    [Microsoft.Azure.PowerShell.Cmdlets.EventHub.Category('Runtime')]
    [System.Management.Automation.SwitchParameter]
    # Use the default credentials for the proxy
    ${ProxyUseDefaultCredentials}
)

begin {
    try {
        $outBuffer = $null
        if ($PSBoundParameters.TryGetValue('OutBuffer', [ref]$outBuffer)) {
            $PSBoundParameters['OutBuffer'] = 1
        }
        $parameterSet = $PSCmdlet.ParameterSetName
        
        $testPlayback = $false
        $PSBoundParameters['HttpPipelinePrepend'] | Foreach-Object { if ($_) { $testPlayback = $testPlayback -or ('Microsoft.Azure.PowerShell.Cmdlets.EventHub.Runtime.PipelineMock' -eq $_.Target.GetType().FullName -and 'Playback' -eq $_.Target.Mode) } }

        $mapping = @{
            UpdateExpanded = 'Az.EventHub.private\Set-AzEventHub_UpdateExpanded';
            UpdateViaJsonFilePath = 'Az.EventHub.private\Set-AzEventHub_UpdateViaJsonFilePath';
            UpdateViaJsonString = 'Az.EventHub.private\Set-AzEventHub_UpdateViaJsonString';
        }
        if (('UpdateExpanded', 'UpdateViaJsonFilePath', 'UpdateViaJsonString') -contains $parameterSet -and -not $PSBoundParameters.ContainsKey('SubscriptionId') ) {
            if ($testPlayback) {
                $PSBoundParameters['SubscriptionId'] = . (Join-Path $PSScriptRoot '..' 'utils' 'Get-SubscriptionIdTestSafe.ps1')
            } else {
                $PSBoundParameters['SubscriptionId'] = (Get-AzContext).Subscription.Id
            }
        }

        $wrappedCmd = $ExecutionContext.InvokeCommand.GetCommand(($mapping[$parameterSet]), [System.Management.Automation.CommandTypes]::Cmdlet)
        if ($wrappedCmd -eq $null) {
            $wrappedCmd = $ExecutionContext.InvokeCommand.GetCommand(($mapping[$parameterSet]), [System.Management.Automation.CommandTypes]::Function)
        }
        $scriptCmd = {& $wrappedCmd @PSBoundParameters}
        $steppablePipeline = $scriptCmd.GetSteppablePipeline($MyInvocation.CommandOrigin)
        $steppablePipeline.Begin($PSCmdlet)
    } catch {

        throw
    }
}

process {
    try {
        $steppablePipeline.Process($_)
    } catch {

        throw
    }

}
end {
    try {
        $steppablePipeline.End()

    } catch {

        throw
    }
} 
}
