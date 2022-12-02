using System.Text.Json;
using StringContent = Statiq.Common.StringContent;

namespace Statiq.Extensions.WebFingerAlias.Content;

internal sealed class JsonContent : StringContent
{
    private const string JsonMediaType = "application/json";
    public JsonContent(string content) 
        : base(content, JsonMediaType)
    {
    }
    
    public JsonContent(JsonElement jsonElement) 
        : base(jsonElement.GetRawText(), JsonMediaType)
    {
    }
}