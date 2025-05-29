// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;

namespace Microsoft.AspNetCore.Components;

/// <summary>
/// This is instance scoped per renderer
/// </summary>
internal class ComponentsActivitySource
{
    internal const string Name = "Microsoft.AspNetCore.Components";
    internal const string OnCircuitName = $"{Name}.CircuitStart";
    internal const string OnRouteName = $"{Name}.RouteChange";
    internal const string OnEventName = $"{Name}.HandleEvent";

    private ActivityContext _routeContext;

    private ActivitySource ActivitySource { get; } = new ActivitySource(Name);

    public Activity? LinkedActivity { get; set; }

    public Activity? StartRouteActivity(string componentType, string route)
    {
        var activity = ActivitySource.CreateActivity(OnRouteName, ActivityKind.Internal, parentId: null, null, null);
        if (activity is not null)
        {
            if (activity.IsAllDataRequested)
            {
                if (LinkedActivity != null)
                {
                    foreach (var tag in LinkedActivity.Tags)
                    {
                        activity.SetTag(tag.Key, tag.Value);
                    }
                }
                if (componentType != null)
                {
                    activity.SetTag("aspnetcore.components.type", componentType);
                }
                if (route != null)
                {
                    activity.SetTag("aspnetcore.components.route", route);
                }
                if (LinkedActivity != null)
                {
                    activity.AddLink(new ActivityLink(LinkedActivity.Context));
                }
            }

            activity.DisplayName = $"Route {route ?? "[unknown path]"} -> {componentType ?? "[unknown component]"}";
            activity.Start();
            _routeContext = activity.Context;
        }
        return activity;
    }

    public Activity? StartEventActivity(string? componentType, string? methodName, string? attributeName)
    {
        var activity = ActivitySource.CreateActivity(OnEventName, ActivityKind.Internal, parentId: null, null, null);
        if (activity is not null)
        {
            if (activity.IsAllDataRequested)
            {
                if (componentType != null)
                {
                    activity.SetTag("aspnetcore.components.type", componentType);
                }
                if (methodName != null)
                {
                    activity.SetTag("aspnetcore.components.method", methodName);
                }
                if (attributeName != null)
                {
                    activity.SetTag("aspnetcore.components.attribute.name", attributeName);
                }
                if (LinkedActivity != null)
                {
                    activity.AddLink(new ActivityLink(LinkedActivity.Context));
                }
                if (_routeContext != default)
                {
                    activity.AddLink(new ActivityLink(_routeContext));
                }
            }

            activity.DisplayName = $"Event {attributeName ?? "[unknown attribute]"} -> {componentType ?? "[unknown component]"}.{methodName ?? "[unknown method]"}";
            activity.Start();
        }
        return activity;
    }

    public static void FailEventActivity(Activity? activity, Exception ex)
    {
        if (activity != null && !activity.IsStopped)
        {
            activity.SetTag("error.type", ex.GetType().FullName);
            activity.SetStatus(ActivityStatusCode.Error);
            activity.Stop();
        }
    }

    public static async Task CaptureEventStopAsync(Task task, Activity? activity)
    {
        try
        {
            await task;
            activity?.Stop();
        }
        catch (Exception ex)
        {
            FailEventActivity(activity, ex);
        }
    }
}
