#load nuget:?package=Cake.Recipe&version=3.0.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "StatiqExtensions.WebFingerAlias",
    solutionFilePath: "./src/WebFingerAlias.sln",
    masterBranchName: "main",
    repositoryOwner: "nils-org",
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true,
    preferredBuildProviderType: BuildProviderType.GitHubActions,
    preferredBuildAgentOperatingSystem: PlatformFamily.Linux);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);
ToolSettings.SetToolPreprocessorDirectives(
    reSharperTools: "#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2022.3.0-eap09&prerelease");

Build.RunDotNetCore();
