// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop.Infrastructure;
using static Microsoft.AspNetCore.Internal.LinkerFlags;

namespace Microsoft.JSInterop.Implementation;

/// <summary>
/// Implements functionality for <see cref="IJSFunctionReference"/>.
/// </summary>
public class JSFunctionReference : IJSFunctionReference
{
    private const string JSFunctionReferenceIdentifier = "";
    private readonly JSRuntime _jsRuntime;

    internal bool Disposed { get; set; }

    /// <summary>
    /// The unique identifier assigned to the referenced function instance.
    /// </summary>
    protected internal long Id { get; }

    /// <summary>
    /// Initializes a new <see cref="JSFunctionReference"/> instance.
    /// </summary>
    /// <param name="jsRuntime">The <see cref="JSRuntime"/> used for invoking JS interop calls.</param>
    /// <param name="id">The unique identifier.</param>
    protected internal JSFunctionReference(JSRuntime jsRuntime, long id)
    {
        _jsRuntime = jsRuntime;

        Id = id;
    }

    /// <inheritdoc />
    public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(object?[]? args)
    {
        ThrowIfDisposed();

        return _jsRuntime.InvokeAsync<TValue>(Id, JSFunctionReferenceIdentifier, JSCallType.FunctionCall, args);
    }

    /// <inheritdoc />
    public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(CancellationToken cancellationToken, object?[]? args)
    {
        ThrowIfDisposed();

        return _jsRuntime.InvokeAsync<TValue>(Id, JSFunctionReferenceIdentifier, JSCallType.FunctionCall, cancellationToken, args);
    }

    /// <inheritdoc />
    public ValueTask<IJSObjectReference> InvokeNewAsync(object?[]? args)
    {
        ThrowIfDisposed();

        return _jsRuntime.InvokeAsync<IJSObjectReference>(Id, JSFunctionReferenceIdentifier, JSCallType.NewCall, args);
    }

    /// <inheritdoc />
    public ValueTask<IJSObjectReference> InvokeNewAsync(CancellationToken cancellationToken, object?[]? args)
    {
        ThrowIfDisposed();

        return _jsRuntime.InvokeAsync<IJSObjectReference>(Id, JSFunctionReferenceIdentifier, JSCallType.NewCall, cancellationToken, args);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (!Disposed)
        {
            Disposed = true;

            await _jsRuntime.InvokeVoidAsync("DotNet.disposeJSObjectReferenceById", Id);
        }
    }

    /// <inheritdoc />
    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
    }
}
