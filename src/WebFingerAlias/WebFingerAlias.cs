using JetBrains.Annotations;
using Statiq.Common;
using Statiq.Core;

namespace Statiq.Extensions;

/// <summary>
/// This <see cref="IPipeline"/> writes a static file '/.well-known/webfinger'
/// to the output. This can be used to simply create an alias for an account in
/// the fediverse (like Mastodon) on a local server.
/// The content of the file needs to be json.
/// Use the settings-key <see cref="SettingKeys.StaticResult"/> to supply the content. 
/// </summary>
[PublicAPI]
public sealed class WebFingerAlias : Pipeline
{
    public WebFingerAlias()
    {
        OutputModules.Add(new StaticResponseModule());
        OutputModules.Add(new WriteFiles());
    }
}