---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/set-pnpteamifyprompthidden
schema: 2.0.0
title: set-pnpteamifyprompthidden
---

# Set-PnPTeamifyPromptHidden

## SYNOPSIS
Hides the teamify prompt for a site

## SYNTAX

```
Set-PnPTeamifyPromptHidden [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet hides the teamify prompt for the current site

## EXAMPLES

### Example 1
```powershell
Set-PnPTeamifyPromptHidden
```

This example hides the teamify prompt for the currently connected to site.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
