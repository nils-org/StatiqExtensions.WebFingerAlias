using System.Text.Json;
using Statiq.Common;
using Statiq.Extensions.WebFingerAlias.Content;

namespace Statiq.Extensions.WebFingerAlias;

public sealed class StaticResponseModule : Module
{
    protected override async Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context)
    {
        var staticResult = context.Get<object>(SettingKeys.StaticResult);
        if (staticResult == null)
        {
            context.LogTrace(null, "No static alias configured.");
            return Array.Empty<IDocument>();
        }

        JsonContent content;
        switch (staticResult)
        {
            case string stringResult:
                content = new JsonContent(stringResult);
                break;
            case JsonDocument jsonDocument:
                content = new JsonContent(jsonDocument.RootElement);
                break;
            default:
                try
                {
                    var json = JsonSerializer.Serialize(staticResult);
                    content = new JsonContent(json);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        $"content of type {staticResult.GetType().Name} could not be parsed to json. {e.GetType().Name}: {e.Message}");
                }

                break;
        }
        
        context.LogTrace(null, $"Using static configured value.");

        return await context.CreateDocument(
            NormalizedPath.AbsoluteRoot / ".well-known/webfinger", // Source is needed for input. 
            new NormalizedPath(".well-known/webfinger", PathKind.Relative),
            new Dictionary<string, object>
            {
                { "IsWebFingerDocument", true },
                { "ShouldOutput", true },
                { "ContentType", "Content" },
                { "MediaType", "application/json" },
                { "IncludeInSitemap", false },
                { Common.Keys.DestinationExtension, null! }, // really, no extension. I mean it.
                { Common.Keys.DestinationFileName, "webfinger" },
            },
            content).YieldAsync();
    }
}
