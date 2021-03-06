---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/get-pnpgroupmember
schema: 2.0.0
title: Get-PnPGroupMember
---

# Get-PnPGroupMember

## SYNOPSIS
Retrieves all members of a group

## SYNTAX

```powershell
Get-PnPGroupMember -Group <GroupPipeBind> [-User String]
```

## DESCRIPTION
This command will return all the users or a specific user that are members of the provided SharePoint Group

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPGroupMember -Group 'Marketing Site Members'
```

Returns all the users that are a member of the group 'Marketing Site Members' in the current sitecollection

### EXAMPLE 2
```powershell
Get-PnPGroupMember -Group "Marketing Site Members" -User "manager@domain.com"
```

Will return a user if the user specific is a member of the specified group.

### EXAMPLE 3
```powershell
Get-PnPGroup | Get-PnPGroupMember
```

Returns all the users that are a member of any of the groups in the current sitecollection

### EXAMPLE 4
```powershell
Get-PnPGroup | ? Title -Like 'Marketing*' | Get-PnPGroupMember
```

Returns all the users that are a member of any of the groups of which their name starts with the word 'Marketing' in the current sitecollection

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
A group object, an ID or a name of a group

```yaml
Type: GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)