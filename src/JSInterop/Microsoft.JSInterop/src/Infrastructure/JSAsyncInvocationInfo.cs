// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.JSInterop.Infrastructure;

public class JSAsyncInvocationInfo
{
    //public JSAsyncInvocationInfo(long taskId, long targetInstanceId, string identifier, string? argsJson, JSCallType callType, JSCallResultType resultType)
    //{
    //    TaskId = taskId;
    //    TargetInstanceId = targetInstanceId;
    //    Identifier = identifier;
    //    ArgsJson = argsJson;
    //    CallType = callType;
    //    ResultType = resultType;
    //}

    public long TaskId { get; set; }

    public long TargetInstanceId { get; set; }

    public string Identifier { get; set; }

    public string? ArgsJson { get; set; }

    public JSCallType CallType { get; set; }

    public JSCallResultType ResultType { get; set; }
}
