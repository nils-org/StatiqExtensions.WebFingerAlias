using System.Text.Json;

namespace Statiq.Extensions;

public static class SettingKeys
{
    /// <summary>
    /// Use this setting key for a static result.
    /// The value for this key can be a (json)-<see cref="string"/>,
    /// a <see cref="JsonDocument"/> or any other object that can be
    /// serialized to json.
    /// </summary>
    public const string StaticResult = "WebFingerStaticResult";
}