using Statiq.Common;

namespace Statiq.Extensions;

internal sealed class WebFingerDocument : Document<Document>
{
    public WebFingerDocument(IContentProvider content)
        : base(
            new NormalizedPath(".well-known/webfinger", PathKind.Relative),
            content)
    {
    }
}