---
external help file: Az.NeonPostgres-help.xml
Module Name: Az.NeonPostgres
online version: https://learn.microsoft.com/powershell/module/az.neonpostgres/remove-azneonpostgresbranch
schema: 2.0.0
---

# Remove-AzNeonPostgresBranch

## SYNOPSIS
Delete a Branch

## SYNTAX

### Delete (Default)
```
Remove-AzNeonPostgresBranch -Name <String> -OrganizationName <String> -ProjectName <String>
 -ResourceGroupName <String> [-SubscriptionId <String>] [-DefaultProfile <PSObject>] [-PassThru]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### DeleteViaIdentityProject
```
Remove-AzNeonPostgresBranch -Name <String> -ProjectInputObject <INeonPostgresIdentity>
 [-DefaultProfile <PSObject>] [-PassThru] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### DeleteViaIdentityOrganization
```
Remove-AzNeonPostgresBranch -Name <String> -ProjectName <String>
 -OrganizationInputObject <INeonPostgresIdentity> [-DefaultProfile <PSObject>] [-PassThru]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### DeleteViaIdentity
```
Remove-AzNeonPostgresBranch -InputObject <INeonPostgresIdentity> [-DefaultProfile <PSObject>] [-PassThru]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Delete a Branch

## EXAMPLES

### Example 1: Delete an existing branch within a Neon Postgres project
```powershell
Remove-AzNeonPostgresBranch -Name "br-damp-bird-a82olmcu" -OrganizationName "NeonDemoOrgPS1" -ProjectName "dawn-breeze-86932057" -ResourceGroupName "neonrg" -SubscriptionId "00000000-0000-0000-0000-000000000000"
```

Delete an existing branch within a Neon Postgres project.

## PARAMETERS

### -DefaultProfile
The DefaultProfile parameter is not functional.
Use the SubscriptionId parameter when available if executing the cmdlet against a different subscription.

```yaml
Type: System.Management.Automation.PSObject
Parameter Sets: (All)
Aliases: AzureRMContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Identity Parameter

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.NeonPostgres.Models.INeonPostgresIdentity
Parameter Sets: DeleteViaIdentity
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
The name of the Branch

```yaml
Type: System.String
Parameter Sets: Delete, DeleteViaIdentityProject, DeleteViaIdentityOrganization
Aliases: BranchName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrganizationInputObject
Identity Parameter

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.NeonPostgres.Models.INeonPostgresIdentity
Parameter Sets: DeleteViaIdentityOrganization
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -OrganizationName
Name of the Neon Organizations resource

```yaml
Type: System.String
Parameter Sets: Delete
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Returns true when the command succeeds

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProjectInputObject
Identity Parameter

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.NeonPostgres.Models.INeonPostgresIdentity
Parameter Sets: DeleteViaIdentityProject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ProjectName
The name of the Project

```yaml
Type: System.String
Parameter Sets: Delete, DeleteViaIdentityOrganization
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The name of the resource group.
The name is case insensitive.

```yaml
Type: System.String
Parameter Sets: Delete
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscriptionId
The ID of the target subscription.
The value must be an UUID.

```yaml
Type: System.String
Parameter Sets: Delete
Aliases:

Required: False
Position: Named
Default value: (Get-AzContext).Subscription.Id
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.PowerShell.Cmdlets.NeonPostgres.Models.INeonPostgresIdentity

## OUTPUTS

### System.Boolean

## NOTES

## RELATED LINKS
