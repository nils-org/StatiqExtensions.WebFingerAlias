

using Statiq.Extensions;

await Bootstrapper
    .Factory
    .CreateDefault(args)
    .AddSetting(SettingKeys.StaticResult, new
    {
        subject = "acct:nils_andresen@mastodon.social",
        aliases = new[]
        {
            "https://mastodon.social/@nils_andresen",
            "https://mastodon.social/users/nils_andresen",
        },
        links = new object[]
        {
            new
            {
                rel = "http://webfinger.net/rel/profile-page",
                type = "text/html",
                href = "https://mastodon.social/@nils_andresen",
            },
            new
            {
                rel = "self",
                type = "application/activity+json",
                href = "https://mastodon.social/users/nils_andresen",
            },
            new
            {
                rel = "http://ostatus.org/schema/1.0/subscribe",
                template = "https://mastodon.social/authorize_interaction?uri={uri}",
            },
        },
    })
    .AddPipeline(new WebFingerAlias())
    .RunAsync();