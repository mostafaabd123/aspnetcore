// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using static Microsoft.AspNetCore.Internal.LinkerFlags;

namespace Microsoft.JSInterop;

/// <summary>
/// Represents a reference to a JavaScript function.
/// </summary>
public interface IJSFunctionReference : IAsyncDisposable
{
    /// <summary>
    /// Invokes the referenced JavaScript function asynchronously.
    /// <para>
    /// <see cref="JSRuntime"/> will apply timeouts to this operation based on the value configured in <see cref="JSRuntime.DefaultAsyncTimeout"/>. To dispatch a call with a different, or no timeout,
    /// consider using <see cref="InvokeAsync{TValue}(CancellationToken, object[])" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
    /// <param name="args">JSON-serializable arguments.</param>
    /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value.</returns>
    ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(params object?[]? args);

    /// <summary>
    /// Invokes the referenced JavaScript function asynchronously.
    /// </summary>
    /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
    /// <param name="cancellationToken">
    /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
    /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
    /// </param>
    /// <param name="args">JSON-serializable arguments.</param>
    /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value.</returns>
    ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(CancellationToken cancellationToken, params object?[]? args);

    /// <summary>
    /// Invokes the referenced JavaScript constructor function asynchronously. The function is invoked with the <c>new</c> operator.
    /// </summary>
    /// <param name="args">JSON-serializable arguments.</param>
    /// <returns>An <see cref="IJSObjectReference"/> instance that represents the created JS object.</returns>
    ValueTask<IJSObjectReference> InvokeNewAsync(params object?[]? args);

    /// <summary>
    /// Invokes the referenced JavaScript constructor function asynchronously. The function is invoked with the <c>new</c> operator.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
    /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
    /// </param>
    /// <param name="args">JSON-serializable arguments.</param>
    /// <returns>An <see cref="IJSObjectReference"/> instance that represents the created JS object.</returns>
    ValueTask<IJSObjectReference> InvokeNewAsync(CancellationToken cancellationToken, params object?[]? args);
}
