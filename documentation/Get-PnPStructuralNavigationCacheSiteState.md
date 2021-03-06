---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/get-pnpstructuralnavigationcachesitestate
schema: 2.0.0
title: Get-PnPStructuralNavigationCacheSiteState
---

# Get-PnPStructuralNavigationCacheSiteState

## SYNOPSIS
Get the structural navigation caching state for a site collection.

## SYNTAX

```
Get-PnPStructuralNavigationCacheSiteState [-SiteUrl <String>]
```

## DESCRIPTION
The Get-PnPStructuralNavigationCacheSiteState cmdlet can be used to determine if structural navigation caching is enabled or disabled for a site collection. If the SiteUrl parameter has not been specified the currently connected to site will be used. [Learn more](https://support.office.com/article/structural-navigation-and-performance-f163053f-8eca-4b9c-b973-36b395093b43). 

## EXAMPLES

### Example 1
```powershell
Get-PnPStructuralNavigationCacheSiteState -SiteUrl "https://contoso.sharepoint.com/sites/product/" 
```

This example checks if structural navigation caching is enabled for the entire site collection https://contoso.sharepoint.com/sites/product/. If caching is enabled, then it will return True. If caching is disabled, then it will return False. 

## PARAMETERS

### -SiteUrl
Specifies the absolute URL for the site collection's root web being checked for its caching state. 

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)