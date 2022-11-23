# Sample Project File

<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>$(MSBuildProjectName.Replace(" ", "_").Replace("-", "_"))</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <Compile Remove="Pipelines\Commands\Commons\**" />
    <Compile Remove="Services\Bases\**" />
    <Compile Remove="Services\Monitorings\**" />
    <EmbeddedResource Remove="Pipelines\Commands\Commons\**" />
    <EmbeddedResource Remove="Services\Bases\**" />
    <EmbeddedResource Remove="Services\Monitorings\**" />
    <None Remove="Pipelines\Commands\Commons\**" />
    <None Remove="Services\Bases\**" />
    <None Remove="Services\Monitorings\**" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="MarkNet.Core" Version="2.1.4" targetFramework="net7.0" />
  </ItemGroup>

</Project>
