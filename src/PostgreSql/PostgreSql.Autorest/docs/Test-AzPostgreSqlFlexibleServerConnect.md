---
external help file:
Module Name: Az.PostgreSql
online version: https://learn.microsoft.com/powershell/module/az.postgresql/test-azpostgresqlflexibleserverconnect
schema: 2.0.0
---

# Test-AzPostgreSqlFlexibleServerConnect

## SYNOPSIS
Test out the connection to the database server

## SYNTAX

### Test (Default)
```
Test-AzPostgreSqlFlexibleServerConnect -Name <String> -ResourceGroupName <String>
 -AdministratorLoginPassword <SecureString> [-DatabaseName <String>] [-AdministratorUserName <String>]
 [-Timeout <Int32>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### TestAndQuery
```
Test-AzPostgreSqlFlexibleServerConnect -Name <String> -QueryText <String> -ResourceGroupName <String>
 -AdministratorLoginPassword <SecureString> [-DatabaseName <String>] [-AdministratorUserName <String>]
 [-Timeout <Int32>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### TestViaIdentity
```
Test-AzPostgreSqlFlexibleServerConnect -AdministratorLoginPassword <SecureString>
 -InputObject <IPostgreSqlIdentity> [-DatabaseName <String>] [-AdministratorUserName <String>]
 [-Timeout <Int32>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### TestViaIdentityAndQuery
```
Test-AzPostgreSqlFlexibleServerConnect -QueryText <String> -AdministratorLoginPassword <SecureString>
 -InputObject <IPostgreSqlIdentity> [-DatabaseName <String>] [-AdministratorUserName <String>]
 [-Timeout <Int32>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Test out the connection to the database server

## EXAMPLES

### Example 1: Test connection by name
```powershell
$password = ConvertTo-SecureString <YourPassword> -AsPlainText
Test-AzPostgreSqlFlexibleServerConnect -ResourceGroupName PowershellPostgreSqlTest -Name postgresql-test -AdministratorLoginPassword $password
```

```output
The connection testing to postgresql-test.database.azure.com was successful!
```

Test connection by the resource group and the server name

### Example 2: Test connection by identity
```powershell
$password = ConvertTo-SecureString <YourPassword> -AsPlainText
Get-AzPostgreSqlFlexibleServer -ResourceGroupName PowershellPostgreSqlTest -ServerName postgresql-test | Test-AzPostgreSqlFlexibleServerConnect -AdministratorLoginPassword $password
```

```output
The connection testing to postgresql-test.database.azure.com was successful!
```

Test connection by the identity

### Example 3: Test query by name
```powershell
$password = ConvertTo-SecureString <YourPassword> -AsPlainText
Test-AzPostgreSqlFlexibleServerConnect -ResourceGroupName PowershellPostgreSqlTest -Name postgresql-test -AdministratorLoginPassword $password -QueryText "SELECT * FROM test"
```

```output
col
-----
1
2
3
```

Test a query by the resource group and the server name

### Example 4: Test connection by identity
```powershell
Get-AzPostgreSqlFlexibleServer -ResourceGroupName PowershellPostgreSqlTest -ServerName postgresql-test | Test-AzPostgreSqlFlexibleServerConnect -QueryText "SELECT * FROM test" -AdministratorLoginPassword $password
```

```output
col
-----
1
2
3
```

Test a query by the identity

## PARAMETERS

### -AdministratorLoginPassword
The password of the administrator.
Minimum 8 characters and maximum 128 characters.
Password must contain characters from three of the following categories: English uppercase letters, English lowercase letters, numbers, and non-alphanumeric characters.

```yaml
Type: System.Security.SecureString
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AdministratorUserName
Administrator username for the server.
Once set, it cannot be changed.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName
The database name to connect.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with Azure.

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
The server to connect.
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.PostgreSql.Models.IPostgreSqlIdentity
Parameter Sets: TestViaIdentity, TestViaIdentityAndQuery
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
The name of the server to connect.

```yaml
Type: System.String
Parameter Sets: Test, TestAndQuery
Aliases: ServerName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -QueryText
The query for the database to test

```yaml
Type: System.String
Parameter Sets: TestAndQuery, TestViaIdentityAndQuery
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The name of the resource group that contains the resource, You can obtain this value from the Azure Resource Manager API or the portal.

```yaml
Type: System.String
Parameter Sets: Test, TestAndQuery
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timeout
The timeout in seconds for query execution.
Valid range is 1-31536000 seconds.

```yaml
Type: System.Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.PowerShell.Cmdlets.PostgreSql.Models.IPostgreSqlIdentity

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

