// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop.Implementation;

namespace Microsoft.JSInterop.Infrastructure;

internal sealed class JSFunctionReferenceJsonConverter : JsonConverter<IJSFunctionReference>
{
    private readonly JSRuntime _jsRuntime;

    public JSFunctionReferenceJsonConverter(JSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override bool CanConvert(Type typeToConvert)
        => typeToConvert == typeof(IJSFunctionReference) || typeToConvert == typeof(JSFunctionReference);

    public override IJSFunctionReference? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = JSObjectReferenceJsonWorker.ReadJSObjectReferenceIdentifier(ref reader);
        return new JSFunctionReference(_jsRuntime, id);
    }

    public override void Write(Utf8JsonWriter writer, IJSFunctionReference value, JsonSerializerOptions options)
    {
        JSObjectReferenceJsonWorker.WriteJSFunctionReference(writer, (JSFunctionReference)value);
    }
}
