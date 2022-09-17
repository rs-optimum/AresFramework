# AresFramework
[![Build Status](https://dev.azure.com/AresFramework/AresFramework/_apis/build/status/rs-optimum.AresFramework?branchName=release)](https://dev.azure.com/AresFramework/AresFramework/_build/latest?definitionId=1&branchName=release)

Welcome to a plugin first approach for an RSPS using decent OO design principles.

## How to run
Download .net 6 sdk and runtime if necessary
Open the project using Rider or Visual Studio and the project should automatically start.

Create a folder inside of your user home directy called `.ares/`
Within there create other folders called `Plugins`, `Cache`, `Logs` and `Data`.

The `Data` folder should contain all of the database information for the login server and other 

And within `Plugins` contains folders relevant to the plugins

Please find a cache from: https://archive.openrs2.org/

### Web API
The `AresFramework.WebApi` project will interface with different parts of the system and combine many operations - such as logging in. 
This will also have the ability to retrieve player's information such as stats, inventories etc. However, most services will be able to call themselves. This will be especially useful for operations on a website

## Plugins
This Section is dedicated to Plugins

### Ignores
Create a file `ignores.yml` in the folder of a plugin in the `Plugins` folder. This file is specifically created to prevent the targeted plugin from 
doing specific operations such as mapping certain npc ids to options.

An example:
`.ares/Plugins/ExamplePlugin/ignores.yml`
```yaml
npcInteractionIgnores:
  - option: talk-to
    npcId: 1
  - option: pickpocket
    npcId: 2
```

Checkout the following `AresFramework.Plugin.Ignore/PluginIgnore.cs` for all the possibilities to prevent specific plugin actions.

### Adding a plugin production
When you build and release your plugin, you should create a folder with the plugin name; `AresFramework.Plugins.[NAME]` 
Place the `AresFramework.Plugins.[NAME].dll` along side other project references from release build. You don't need to include any DLL's that can be found in this project for faster scanning

### Local Development with debugging
To make plugin development easy, create a new Project called `AresFramework.Plugins.[NAME]` replacing `[NAME]` whatever you want.

Import local references to this project, specifically `AresFramework.Plugin.Module` and create a class
that extends `IPluginModule` with the following attribute `PluginModuleAttribute`

To debug this project you can add it as a reference to `AresFramework.GameEngine` and the game engine will automatically detect that plugin and initialize it without adding to the Plugins folder enabling you to debug

### Project Layout

Please note, `<EnableDynamicLoading>true</EnableDynamicLoading>` Is important for the `.csproj` file
along with internal references to have the following:
```xml
    <ExcludeAssets>runtime</ExcludeAssets>
    <Private>false</Private>
```
example:
```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <EnableDynamicLoading>true</EnableDynamicLoading>
    </PropertyGroup>

    <ItemGroup>
        <ProjectReference Include="..\AresFramework\AresFramework.Plugin.Loaders\AresFramework.Plugin.Loaders.csproj">
            <ExcludeAssets>runtime</ExcludeAssets>
            <Private>false</Private>
        </ProjectReference>
        <ProjectReference Include="..\AresFramework\AresFramework.Plugin.Module\AresFramework.Plugin.Module.csproj">
            <ExcludeAssets>runtime</ExcludeAssets>
            <Private>false</Private>
        </ProjectReference>
    </ItemGroup>

    <ItemGroup>
        <Reference Include="JetBrains.Annotations">
            <HintPath>..\..\..\.nuget\packages\jetbrains.annotations\2022.1.0\lib\netstandard2.0\JetBrains.Annotations.dll</HintPath>
        </Reference>
    </ItemGroup>

    <ItemGroup>
        <PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
        <PackageReference Include="Polly" Version="7.2.3" />
    </ItemGroup>

    <ItemGroup>
        <EmbeddedResource Include="Resource/pickpocket.json" />
    </ItemGroup>

</Project>
```

In this example; `Polly.dll` and `Newtonsoft.Json.dll` would go into the folder along with `AresFramework.Plugins.[NAME].dll`. Anything that GameEngine or any module on this project inherits will not need to be added to the PluginFolder.

This is an example of a working module project: https://github.com/rs-optimum/AresFramework.Plugins.Thieving
