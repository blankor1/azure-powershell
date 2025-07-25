
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
Create an in-memory object for InitialAgentPoolConfiguration.
.Description
Create an in-memory object for InitialAgentPoolConfiguration.
.Example
New-AzNetworkCloudInitialAgentPoolConfigurationObject -Count <Int64> -Mode <AgentPoolMode> -Name <String> -VMSkuName <String> -AdministratorConfigurationAdminUsername <String> -AdministratorConfigurationSshPublicKey <ISshPublicKey[]>  -AgentOptionHugepagesCount <Int64> -AgentOptionHugepagesSize <HugepagesSize> -AttachedNetworkConfigurationL2Network <IL2NetworkAttachmentConfiguration[]> -AttachedNetworkConfigurationL3Network <IL3NetworkAttachmentConfiguration[]> -AttachedNetworkConfigurationTrunkedNetwork <ITrunkedNetworkAttachmentConfiguration[]> -AvailabilityZone <String[]> -Label <IKubernetesLabel[]> -Taint <IKubernetesLabel[]> -UpgradeSettingMaxSurge <String>

.Outputs
Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.InitialAgentPoolConfiguration
.Notes
COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.

ADMINISTRATORCONFIGURATIONSSHPUBLICKEY <ISshPublicKey[]>: The SSH configuration for the operating systems that run the nodes in the Kubernetes cluster. In some cases, specification of public keys may be required to produce a working environment.
  KeyData <String>: The SSH public key data.

ATTACHEDNETWORKCONFIGURATIONL2NETWORK <IL2NetworkAttachmentConfiguration[]>: The list of Layer 2 Networks and related configuration for attachment.
  NetworkId <String>: The resource ID of the network that is being configured for attachment.
  [PluginType <KubernetesPluginType?>]: The indicator of how this network will be utilized by the Kubernetes cluster.

ATTACHEDNETWORKCONFIGURATIONL3NETWORK <IL3NetworkAttachmentConfiguration[]>: The list of Layer 3 Networks and related configuration for attachment.
  NetworkId <String>: The resource ID of the network that is being configured for attachment.
  [IpamEnabled <L3NetworkConfigurationIpamEnabled?>]: The indication of whether this network will or will not perform IP address management and allocate IP addresses when attached.
  [PluginType <KubernetesPluginType?>]: The indicator of how this network will be utilized by the Kubernetes cluster.

ATTACHEDNETWORKCONFIGURATIONTRUNKEDNETWORK <ITrunkedNetworkAttachmentConfiguration[]>: The list of Trunked Networks and related configuration for attachment.
  NetworkId <String>: The resource ID of the network that is being configured for attachment.
  [PluginType <KubernetesPluginType?>]: The indicator of how this network will be utilized by the Kubernetes cluster.

LABEL <IKubernetesLabel[]>: The labels applied to the nodes in this agent pool.
  Key <String>: The name of the label or taint.
  Value <String>: The value of the label or taint.

TAINT <IKubernetesLabel[]>: The taints applied to the nodes in this agent pool.
  Key <String>: The name of the label or taint.
  Value <String>: The value of the label or taint.
.Link
https://learn.microsoft.com/powershell/module/Az.NetworkCloud/new-AzNetworkCloudInitialAgentPoolConfigurationObject
#>
function New-AzNetworkCloudInitialAgentPoolConfigurationObject {
[OutputType([Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.InitialAgentPoolConfiguration])]
[CmdletBinding(PositionalBinding=$false)]
param(
    [Parameter(Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.Int64]
    # The number of virtual machines that use this configuration.
    ${Count},

    [Parameter(Mandatory)]
    [ArgumentCompleter([Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Support.AgentPoolMode])]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Support.AgentPoolMode]
    # The selection of how this agent pool is utilized, either as a system pool or a user pool.
    # System pools run the features and critical services for the Kubernetes Cluster, while user pools are dedicated to user workloads.
    # Every Kubernetes cluster must contain at least one system node pool with at least one node.
    ${Mode},

    [Parameter(Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.String]
    # The name that will be used for the agent pool resource representing this agent pool.
    ${Name},

    [Parameter(Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.String]
    # The name of the VM SKU that determines the size of resources allocated for node VMs.
    ${VMSkuName},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.String]
    # The user name for the administrator that will be applied to the operating systems that run Kubernetes nodes.
    # If not supplied, a user name will be chosen by the service.
    ${AdministratorConfigurationAdminUsername},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.ISshPublicKey[]]
    # The SSH configuration for the operating systems that run the nodes in the Kubernetes cluster.
    # In some cases, specification of public keys may be required to produce a working environment.
    # To construct, see NOTES section for ADMINISTRATORCONFIGURATIONSSHPUBLICKEY properties and create a hash table.
    ${AdministratorConfigurationSshPublicKey},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.Int64]
    # The number of hugepages to allocate.
    ${AgentOptionHugepagesCount},

    [Parameter()]
    [ArgumentCompleter([Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Support.HugepagesSize])]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Support.HugepagesSize]
    # The size of the hugepages to allocate.
    ${AgentOptionHugepagesSize},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.IL2NetworkAttachmentConfiguration[]]
    # The list of Layer 2 Networks and related configuration for attachment.
    # To construct, see NOTES section for ATTACHEDNETWORKCONFIGURATIONL2NETWORK properties and create a hash table.
    ${AttachedNetworkConfigurationL2Network},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.IL3NetworkAttachmentConfiguration[]]
    # The list of Layer 3 Networks and related configuration for attachment.
    # To construct, see NOTES section for ATTACHEDNETWORKCONFIGURATIONL3NETWORK properties and create a hash table.
    ${AttachedNetworkConfigurationL3Network},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.ITrunkedNetworkAttachmentConfiguration[]]
    # The list of Trunked Networks and related configuration for attachment.
    # To construct, see NOTES section for ATTACHEDNETWORKCONFIGURATIONTRUNKEDNETWORK properties and create a hash table.
    ${AttachedNetworkConfigurationTrunkedNetwork},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.String[]]
    # The list of availability zones of the Network Cloud cluster used for the provisioning of nodes in this agent pool.
    # If not specified, all availability zones will be used.
    ${AvailabilityZone},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.IKubernetesLabel[]]
    # The labels applied to the nodes in this agent pool.
    # To construct, see NOTES section for LABEL properties and create a hash table.
    ${Label},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.IKubernetesLabel[]]
    # The taints applied to the nodes in this agent pool.
    # To construct, see NOTES section for TAINT properties and create a hash table.
    ${Taint},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.Int64]
    # The maximum time in seconds that is allowed for a node drain to complete before proceeding with the upgrade of the agent pool.
    # If not specified during creation, a value of 1800 seconds is used.
    ${UpgradeSettingDrainTimeout},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.String]
    # The maximum number or percentage of nodes that are surged during upgrade.
    # This can either be set to an integer (e.g.
    # '5') or a percentage (e.g.
    # '50%').
    # If a percentage is specified, it is the percentage of the total agent pool size at the time of the upgrade.
    # For percentages, fractional nodes are rounded up.
    # If not specified during creation, a value of 1 is used.
    # One of MaxSurge and MaxUnavailable must be greater than 0.
    ${UpgradeSettingMaxSurge},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Category('Body')]
    [System.String]
    # The maximum number or percentage of nodes that can be unavailable during upgrade.
    # This can either be set to an integer (e.g.
    # '5') or a percentage (e.g.
    # '50%').
    # If a percentage is specified, it is the percentage of the total agent pool size at the time of the upgrade.
    # For percentages, fractional nodes are rounded up.
    # If not specified during creation, a value of 0 is used.
    # One of MaxSurge and MaxUnavailable must be greater than 0.
    ${UpgradeSettingMaxUnavailable}
)

begin {
    try {
        $outBuffer = $null
        if ($PSBoundParameters.TryGetValue('OutBuffer', [ref]$outBuffer)) {
            $PSBoundParameters['OutBuffer'] = 1
        }
        $parameterSet = $PSCmdlet.ParameterSetName

        if ($null -eq [Microsoft.WindowsAzure.Commands.Utilities.Common.AzurePSCmdlet]::PowerShellVersion) {
            [Microsoft.WindowsAzure.Commands.Utilities.Common.AzurePSCmdlet]::PowerShellVersion = $PSVersionTable.PSVersion.ToString()
        }         
        $preTelemetryId = [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::TelemetryId
        if ($preTelemetryId -eq '') {
            [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::TelemetryId =(New-Guid).ToString()
            [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.module]::Instance.Telemetry.Invoke('Create', $MyInvocation, $parameterSet, $PSCmdlet)
        } else {
            $internalCalledCmdlets = [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::InternalCalledCmdlets
            if ($internalCalledCmdlets -eq '') {
                [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::InternalCalledCmdlets = $MyInvocation.MyCommand.Name
            } else {
                [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::InternalCalledCmdlets += ',' + $MyInvocation.MyCommand.Name
            }
            [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::TelemetryId = 'internal'
        }

        $mapping = @{
            __AllParameterSets = 'Az.NetworkCloud.custom\New-AzNetworkCloudInitialAgentPoolConfigurationObject';
        }
        $cmdInfo = Get-Command -Name $mapping[$parameterSet]
        [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Runtime.MessageAttributeHelper]::ProcessCustomAttributesAtRuntime($cmdInfo, $MyInvocation, $parameterSet, $PSCmdlet)
        if ($null -ne $MyInvocation.MyCommand -and [Microsoft.WindowsAzure.Commands.Utilities.Common.AzurePSCmdlet]::PromptedPreviewMessageCmdlets -notcontains $MyInvocation.MyCommand.Name -and [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Runtime.MessageAttributeHelper]::ContainsPreviewAttribute($cmdInfo, $MyInvocation)){
            [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Runtime.MessageAttributeHelper]::ProcessPreviewMessageAttributesAtRuntime($cmdInfo, $MyInvocation, $parameterSet, $PSCmdlet)
            [Microsoft.WindowsAzure.Commands.Utilities.Common.AzurePSCmdlet]::PromptedPreviewMessageCmdlets.Enqueue($MyInvocation.MyCommand.Name)
        }
        $wrappedCmd = $ExecutionContext.InvokeCommand.GetCommand(($mapping[$parameterSet]), [System.Management.Automation.CommandTypes]::Cmdlet)
        $scriptCmd = {& $wrappedCmd @PSBoundParameters}
        $steppablePipeline = $scriptCmd.GetSteppablePipeline($MyInvocation.CommandOrigin)
        $steppablePipeline.Begin($PSCmdlet)
    } catch {
        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::ClearTelemetryContext()
        throw
    }
}

process {
    try {
        $steppablePipeline.Process($_)
    } catch {
        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::ClearTelemetryContext()
        throw
    }

    finally {
        $backupTelemetryId = [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::TelemetryId
        $backupInternalCalledCmdlets = [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::InternalCalledCmdlets
        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::ClearTelemetryContext()
    }

}
end {
    try {
        $steppablePipeline.End()

        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::TelemetryId = $backupTelemetryId
        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::InternalCalledCmdlets = $backupInternalCalledCmdlets
        if ($preTelemetryId -eq '') {
            [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.module]::Instance.Telemetry.Invoke('Send', $MyInvocation, $parameterSet, $PSCmdlet)
            [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::ClearTelemetryContext()
        }
        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::TelemetryId = $preTelemetryId

    } catch {
        [Microsoft.WindowsAzure.Commands.Common.MetricHelper]::ClearTelemetryContext()
        throw
    }
} 
}
