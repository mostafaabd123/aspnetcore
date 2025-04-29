// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BasicTestApp;
using Microsoft.AspNetCore.Components.E2ETest;
using Microsoft.AspNetCore.Components.E2ETest.Infrastructure;
using Microsoft.AspNetCore.Components.E2ETest.Infrastructure.ServerFixtures;
using Microsoft.AspNetCore.E2ETesting;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace Microsoft.AspNetCore.Components.E2ETests.Tests;

public class InteropMultipleByteArraysTransfer(
    BrowserFixture browserFixture,
    ToggleExecutionModeServerFixture<Program> serverFixture,
    ITestOutputHelper output)
    : ServerTestBase<ToggleExecutionModeServerFixture<Program>>(browserFixture, serverFixture, output)
{
    protected override void InitializeAsyncCore()
    {
        Navigate(ServerPathBase);
        Browser.MountTestComponent<InteropMultipleByteArrayTransfersComponent>();
    }

    [Fact]
    public void CanLoadOne()
    {
        var loadButton = Browser.Exists(By.Id("btn-load-one"));
        loadButton.Click();
        Browser.Exists(By.Id("success-one"));
    }

    [Fact]
    public void CanLoadTwo()
    {
        var loadButton = Browser.Exists(By.Id("btn-load-two"));
        loadButton.Click();
        Browser.Exists(By.Id("success-two"));
    }

    [Fact]
    public void CanLoadMany()
    {
        var loadButton = Browser.Exists(By.Id("btn-load-many"));
        loadButton.Click();
        Browser.Exists(By.Id("success-many"));
    }
}
