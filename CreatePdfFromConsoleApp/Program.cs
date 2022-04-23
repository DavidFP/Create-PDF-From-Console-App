using System;
using PuppeteerSharp;
using PuppeteerSharp.Media;

string outputFilePath = @"C:\Users\David\source\repos\CreatePdfFromConsoleApp\CreatePdfFromConsoleApp\outputfiles\myFile.pdf";
string htmlTemplatePath = @"C:\Users\David\source\repos\CreatePdfFromConsoleApp\CreatePdfFromConsoleApp\template.html";
using var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync();

await using var browser = await Puppeteer.LaunchAsync(
    new LaunchOptions
    {
        Headless = true,
        ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
    }
);

await using var page = await browser.NewPageAsync();

await page.GoToAsync(htmlTemplatePath);
var name = "David";
await page.EvaluateFunctionAsync("replaceUsername", name);
await page.PdfAsync(outputFilePath, new PdfOptions
{
    PrintBackground = true,
    Format = PaperFormat.A4,
    DisplayHeaderFooter = true,
    MarginOptions = new MarginOptions
    {
        Top = "40px",
        Right = "20px",
        Left = "20px",
        Bottom = "40px"
    },
    FooterTemplate = $"<div style='font-size:5px !important; color:#808080;'>Pdf Generated at {DateTime.Now.ToString("G")}</div>",
    HeaderTemplate = $"<div style='font-size:5px !important; color:#808080;'>Test document {DateTime.Now.ToString("G")}</div>"
});