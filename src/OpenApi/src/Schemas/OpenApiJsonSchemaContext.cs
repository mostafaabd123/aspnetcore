// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Microsoft.AspNetCore.OpenApi;

[JsonSerializable(typeof(OpenApiJsonSchema))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(JsonNode))]
internal sealed partial class OpenApiJsonSchemaContext : JsonSerializerContext { }
