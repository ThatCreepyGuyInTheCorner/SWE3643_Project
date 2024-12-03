using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest2 : PageTest
{
    [Test]
    public async Task DemoTest()
    {
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();
        await page.GotoAsync("https://demo.playwright.dev/todomvc/#/");
        await page.GetByPlaceholder("What needs to be done?").ClickAsync();
        await page.GetByPlaceholder("What needs to be done?").FillAsync("Things");
        await page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");
        await Assertions.Expect(page.GetByTestId("todo-title")).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("body")).ToContainTextAsync("1 item leftAll Active Completed");
        await page.GetByLabel("Toggle Todo").CheckAsync();
        await Assertions.Expect(page.Locator("body"))
            .ToContainTextAsync("0 items leftAll Active CompletedClear completed");
        await page.GetByRole(AriaRole.Button, new() { Name = "Clear completed" }).ClickAsync();
        await Assertions.Expect(page.Locator("body")).ToMatchAriaSnapshotAsync(
            "- text: This is just a demo of TodoMVC for testing, not the\n- link \"real TodoMVC app.\"\n- heading \"todos\" [level=1]\n- textbox \"What needs to be done?\"\n- contentinfo:\n  - paragraph: Double-click to edit a todo\n  - paragraph:\n    - text: Created by\n    - link \"Remo H. Jansen\"\n  - paragraph:\n    - text: Part of\n    - link \"TodoMVC\"");
    }
}