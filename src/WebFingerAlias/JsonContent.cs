using System.Text.Json;
using StringContent = Statiq.Common.StringContent;

namespace Statiq.Extensions;

internal sealed class JsonContent : StringContent
{
    private const string ContentType = "application/json";
    public JsonContent(string content) 
        : base(content, ContentType)
    {
    }
    
    public JsonContent(JsonElement jsonElement) 
        : base(jsonElement.GetRawText(), ContentType)
    {
    }
}