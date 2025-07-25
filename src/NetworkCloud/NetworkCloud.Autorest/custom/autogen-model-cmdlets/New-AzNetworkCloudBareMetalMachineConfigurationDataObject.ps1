
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
Create an in-memory object for BareMetalMachineConfigurationData.
.Description
Create an in-memory object for BareMetalMachineConfigurationData.

.Outputs
Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.BareMetalMachineConfigurationData
.Link
https://learn.microsoft.com/powershell/module/Az.NetworkCloud/new-AzNetworkCloudBareMetalMachineConfigurationDataObject
#>
function New-AzNetworkCloudBareMetalMachineConfigurationDataObject {
    [OutputType('Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.BareMetalMachineConfigurationData')]
    [CmdletBinding(PositionalBinding=$false)]
    Param(

        [Parameter(Mandatory, HelpMessage="The password of the administrator of the device used during initialization.")]
        [System.Security.SecureString]
        $BmcCredentialsPassword,
        [Parameter(Mandatory, HelpMessage="The username of the administrator of the device used during initialization.")]
        [string]
        $BmcCredentialsUsername,
        [Parameter(Mandatory, HelpMessage="The MAC address of the BMC for this machine.")]
        [string]
        $BmcMacAddress,
        [Parameter(Mandatory, HelpMessage="The MAC address associated with the PXE NIC card.")]
        [string]
        $BootMacAddress,
        [Parameter(HelpMessage="The free-form additional information about the machine, e.g. an asset tag.")]
        [string]
        $MachineDetail,
        [Parameter(HelpMessage="The user-provided name for the bare metal machine created from this specification.
        If not provided, the machine name will be generated programmatically.")]
        [string]
        $MachineName,
        [Parameter(Mandatory, HelpMessage="The slot the physical machine is in the rack based on the BOM configuration.")]
        [long]
        $RackSlot,
        [Parameter(Mandatory, HelpMessage="The serial number of the machine. Hardware suppliers may use an alternate value. For example, service tag.")]
        [string]
        $SerialNumber
    )

    process {
        $Object = [Microsoft.Azure.PowerShell.Cmdlets.NetworkCloud.Models.Api20250201.BareMetalMachineConfigurationData]::New()

        if ($PSBoundParameters.ContainsKey('BmcCredentialsPassword')) {
            $Object.BmcCredentialsPassword = $BmcCredentialsPassword
        }
        if ($PSBoundParameters.ContainsKey('BmcCredentialsUsername')) {
            $Object.BmcCredentialsUsername = $BmcCredentialsUsername
        }
        if ($PSBoundParameters.ContainsKey('BmcMacAddress')) {
            $Object.BmcMacAddress = $BmcMacAddress
        }
        if ($PSBoundParameters.ContainsKey('BootMacAddress')) {
            $Object.BootMacAddress = $BootMacAddress
        }
        if ($PSBoundParameters.ContainsKey('MachineDetail')) {
            $Object.MachineDetail = $MachineDetail
        }
        if ($PSBoundParameters.ContainsKey('MachineName')) {
            $Object.MachineName = $MachineName
        }
        if ($PSBoundParameters.ContainsKey('RackSlot')) {
            $Object.RackSlot = $RackSlot
        }
        if ($PSBoundParameters.ContainsKey('SerialNumber')) {
            $Object.SerialNumber = $SerialNumber
        }
        return $Object
    }
}

