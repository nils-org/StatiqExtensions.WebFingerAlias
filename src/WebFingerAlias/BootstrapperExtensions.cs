using JetBrains.Annotations;
using Statiq.Common;
using System.Text.Json;
using Statiq.Extensions.WebFingerAlias;

// ReSharper disable once CheckNamespace
namespace Statiq;

[PublicAPI]
public static class BootstrapperExtensions
{
    /// <summary>
    /// Use this extension to modify the Inputs-<see cref="IPipeline"/>
    /// (e.g. of Statiq.Web or Statiq.Docs) and add a static file '/.well-known/webfinger'
    /// to the output. This can be used to simplify creating an alias for an account in
    /// the fediverse (like Mastodon) on a local server.
    /// The content of the file needs to be json.
    ///
    /// <para>
    /// Available SettingKeys are:
    /// <list type="bullet">
    ///   <item>
    ///     <term><see cref="SettingKeys.StaticResult"/></term>
    ///     <description>Use some static content.
    ///     The value for this key can be a (json)-<see cref="string"/>,
    ///     a <see cref="JsonDocument"/> or any other object that can be
    ///     serialized to json.
    ///     </description>
    ///   </item>
    /// </list>
    /// </para>
    ///
    /// <example>
    /// Use some static content, using the <see cref="SettingKeys.StaticResult"/> setting.
    /// <code>
    /// await Bootstrapper
    ///     .Factory
    ///     .CreateWeb(args)
    ///     .AddSetting(SettingKeys.StaticResult, new
    ///     {
    ///         subject = "acct:nils_andresen@mastodon.social",
    ///         aliases = new[]
    ///         {
    ///             "https://mastodon.social/@nils_andresen",
    ///             "https://mastodon.social/users/nils_andresen",
    ///         },
    ///         links = new object[]
    ///         {
    ///             new
    ///             {
    ///                 rel = "http://webfinger.net/rel/profile-page",
    ///                 type = "text/html",
    ///                 href = "https://mastodon.social/@nils_andresen",
    ///             },
    ///             new
    ///             {
    ///                 rel = "self",
    ///                 type = "application/activity+json",
    ///                 href = "https://mastodon.social/users/nils_andresen",
    ///             },
    ///             new
    ///             {
    ///                 rel = "http://ostatus.org/schema/1.0/subscribe",
    ///                 template = "https://mastodon.social/authorize_interaction?uri={uri}",
    ///             },
    ///         },
    ///     })
    ///     .WithWebFingerAlias()
    ///     .RunAsync();
    /// </code></example> 
    /// </summary>
    public static TBootstrapper WithWebFingerAlias<TBootstrapper>(this TBootstrapper bootstrapper)
        where TBootstrapper : IBootstrapper =>
        bootstrapper.ConfigureEngine(engine => 
            engine.Pipelines.Values
                .Where(p => p.GetType().Name == "Inputs")
                .ToList()
                .ForEach(pipeline =>
                {
                    engine.Logger?.LogTrace(null, $"Adding WebFingerAlias to pipeline: {pipeline.GetType().FullName}");
                    pipeline.InputModules.Add(new StaticResponseModule());
                })
        );
}