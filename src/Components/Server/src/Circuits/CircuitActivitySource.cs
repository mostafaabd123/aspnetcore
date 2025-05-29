// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;

internal class CircuitActivitySource
{
    internal const string Name = "Microsoft.AspNetCore.Components";
    internal const string OnCircuitName = $"{Name}.CircuitStart";
    internal const string OnRouteName = $"{Name}.RouteChange";
    internal const string OnEventName = $"{Name}.HandleEvent";

    private ActivitySource ActivitySource { get; } = new ActivitySource(Name);

    public Activity? StartCircuitActivity(string circuitId, ActivityContext httpActivityContext)
    {
        var activity = ActivitySource.CreateActivity(OnCircuitName, ActivityKind.Internal, parentId: null, null, null);
        if (activity is not null)
        {
            if (activity.IsAllDataRequested)
            {
                if (circuitId != null)
                {
                    activity.SetTag("aspnetcore.components.circuit.id", circuitId);
                }
                if (httpActivityContext != default)
                {
                    activity.AddLink(new ActivityLink(httpActivityContext));
                }
            }
            activity.DisplayName = $"Circuit {circuitId ?? ""}";
            activity.Start();
        }
        return activity;
    }

    public void FailCircuitActivity(Activity? activity, Exception ex)
    {
        if (activity != null && !activity.IsStopped)
        {
            activity.SetTag("error.type", ex.GetType().FullName);
            activity.SetStatus(ActivityStatusCode.Error);
            activity.Stop();
        }
    }
}
