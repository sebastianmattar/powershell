﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "UnifiedAuditLog")]
    [OfficeManagementApiPermissionCheck(OfficeManagementApiPermission.ActivityFeed_Read)]
    [PnPManagementShellScopes("ActivityFeed.Read")]
    public class GetUnifiedAuditLog : PnPOfficeManagementApiCmdlet
    {
        private const string ParameterSet_LogsByDate = "Logs by date";

        [Parameter(Mandatory = false)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LogsByDate)]
        public Enums.AuditContentType ContentType = Enums.AuditContentType.SharePoint;

        [Parameter(
            Mandatory = false,
            ParameterSetName = ParameterSet_LogsByDate)]
        public DateTime StartTime = DateTime.MinValue;

        [Parameter(
            Mandatory = false,
            ParameterSetName = ParameterSet_LogsByDate)]
        public DateTime EndTime = DateTime.MaxValue;

        /// <summary>
        /// Returns the Content Type to query in a string format which is recognized by the Office 365 Management API
        /// </summary>
        protected string ContentTypeString
        {
            get
            {
                switch (ContentType)
                {
                    case Enums.AuditContentType.AzureActiveDirectory: return "Audit.AzureActiveDirectory";
                    case Enums.AuditContentType.SharePoint: return "Audit.SharePoint";
                    case Enums.AuditContentType.Exchange: return "Audit.Exchange";
                    case Enums.AuditContentType.DLP: return "DLP.All";
                    case Enums.AuditContentType.General: default: return "Audit.General";
                }
            }
        }

        /// <summary>
        /// Base URL to the Office 365 Management API being used in this cmdlet
        /// </summary>
        protected string ApiUrl => $"{ApiRootUrl}activity/feed";

        private IEnumerable<ManagementApiSubscription> GetSubscriptions()
        {
            var url = $"{ApiUrl}/subscriptions/list";
            return GraphHelper.GetAsync<IEnumerable<ManagementApiSubscription>>(HttpClient, url, AccessToken).GetAwaiter().GetResult();
        }

        private void EnsureSubscription(string contentType)
        {
            var subscriptions = GetSubscriptions();
            var subscription = subscriptions.FirstOrDefault(s => s.ContentType == contentType);
            if (subscription == null)
            {
                subscription = GraphHelper.PostAsync<ManagementApiSubscription>(HttpClient, $"{ApiUrl}/subscriptions/start?contentType={contentType}&PublisherIdentifier={Token.TenantId}", AccessToken).GetAwaiter().GetResult();
                if (!subscription.Status.Equals("enabled", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Cannot enable subscription for {contentType}");
                }
            }
        }

        protected override void ExecuteCmdlet()
        {
            EnsureSubscription(ContentTypeString);

            var url = $"{ApiUrl}/subscriptions/content?contentType={ContentTypeString}&PublisherIdentifier=${Token.TenantId}";
            if (StartTime != DateTime.MinValue)
            {
                url += $"&startTime={StartTime:yyyy-MM-ddThh:mm:ss}";
            }
            if (EndTime != DateTime.MaxValue)
            {
                url += $"&endTime={EndTime:yyyy-MM-ddThh:mm:ss}";
            }

            List<ManagementApiSubscriptionContent> subscriptionContents = new List<ManagementApiSubscriptionContent>();
            var subscriptionResponse = GraphHelper.GetResponseAsync(HttpClient, url, AccessToken).GetAwaiter().GetResult();
            if (subscriptionResponse.IsSuccessStatusCode)
            {
                var content = subscriptionResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                subscriptionContents.AddRange(System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ManagementApiSubscriptionContent>>(content, new System.Text.Json.JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                while (subscriptionResponse.Headers.Contains("NextPageUri"))
                {
                    subscriptionResponse = GraphHelper.GetResponseAsync(HttpClient, subscriptionResponse.Headers.GetValues("NextPageUri").First(), AccessToken).GetAwaiter().GetResult();
                    if (subscriptionResponse.IsSuccessStatusCode)
                    {
                        content = subscriptionResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        subscriptionContents.AddRange(System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ManagementApiSubscriptionContent>>(content));
                    }
                }
            }
            if (subscriptionContents.Any())
            {
                foreach (var content in subscriptionContents)
                {

                    var logs = GraphHelper.GetAsync<IEnumerable<ManagementApiUnifiedLogRecord>>(HttpClient, content.ContentUri, AccessToken, false).GetAwaiter().GetResult();
                    WriteObject(logs, true);
                }
            }
        }
    }
}