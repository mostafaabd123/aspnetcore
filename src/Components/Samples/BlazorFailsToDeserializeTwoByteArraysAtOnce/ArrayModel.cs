// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BlazorFailsToDeserializeTwoByteArraysAtOnce;

public class ArrayModel
{
    public byte[] A { get; set; }
    public byte[] B { get; set; }
    public byte[] C { get; set; }
}
