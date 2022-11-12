using System.Text.Json;
using Statiq.Common;

namespace Statiq.Extensions;

public sealed class StaticResponseModule : Module
{
    protected override Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context)
    {
        var staticResult = context.Get<object>(SettingKeys.StaticResult);
        if (staticResult == null)
        {
            context.LogTrace(null, $"{nameof(WebFingerAlias)} no static alias configured.");
            return Task.FromResult<IEnumerable<IDocument>>(Array.Empty<IDocument>());
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
                    throw new ArgumentException($"content of type {staticResult.GetType().Name} could not be parsed to json. {e.GetType().Name}: {e.Message}");
                }

                break;
        }
        
        context.LogTrace(null, $"{nameof(WebFingerAlias)} using static configured value.");

        return Task.FromResult<IEnumerable<IDocument>>(
            new []
            {
                new WebFingerDocument(content),
            });
    }
}
