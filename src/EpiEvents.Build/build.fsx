#r @"../../packages/FAKE.4.64.6/tools/FakeLib.dll"

open Fake

let company = "Maris Krivtezs"
let authors = [company]
let copyright = "Copyright Maris Krivtezs 2018"

let coreProjectName = "EpiEvents.Core"
let coreProjectDescription = "An in-process messaging style event handling for Episerver"
let coreProjectSummary = coreProjectDescription
let coreReleaseNotes = "Added approval events."
let coreAssemblyVersion = "1.2.0"

let commerceProjectName = "EpiEvents.Commerce"
let commerceProjectDescription = "An in-process messaging style event handling for Episerver Commerce"
let commerceProjectSummary = coreProjectDescription
let commerceReleaseNotes = "Added price and inventory update events."
let commerceAssemblyVersion = "1.0.0"

let solutionPath = "../../EpiEvents.sln"
let packagesDir = "../../packages/"
let packagingRoot = "../../packaging/"

let coreBuildDir = "../EpiEvents.Core/bin"
let corePackagingDir = packagingRoot @@ "core"
let coreAssemblyInfoPath = "../EpiEvents.Core/Properties/AssemblyInfo.cs"

let commerceBuildDir = "../EpiEvents.Commerce/bin"
let commercePackagingDir = packagingRoot @@ "commerce"
let commerceAssemblyInfoPath = "../EpiEvents.Commerce/Properties/AssemblyInfo.cs"

let PackageDependency packageName =
    packageName, GetPackageVersion packagesDir packageName

MSBuildDefaults <- {
    MSBuildDefaults with
        Verbosity = Some MSBuildVerbosity.Minimal }

Target "Clean" (fun _ ->
    CleanDirs [coreBuildDir; commerceBuildDir; packagingRoot; corePackagingDir; commercePackagingDir]
)

open Fake.AssemblyInfoFile

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo coreAssemblyInfoPath
      [ Attribute.Product coreProjectName
        Attribute.Version coreAssemblyVersion
        Attribute.FileVersion coreAssemblyVersion
        Attribute.ComVisible false
        Attribute.Copyright copyright
        Attribute.Company company
        Attribute.Description coreProjectDescription
        Attribute.Title coreProjectName]
    CreateCSharpAssemblyInfo commerceAssemblyInfoPath
      [ Attribute.Product commerceProjectName
        Attribute.Version commerceAssemblyVersion
        Attribute.FileVersion commerceAssemblyVersion
        Attribute.ComVisible false
        Attribute.Copyright copyright
        Attribute.Company company
        Attribute.Description commerceProjectDescription
        Attribute.Title commerceProjectName]
)

let buildMode = getBuildParamOrDefault "buildMode" "Release"

let setParams defaults = {
    defaults with
        Targets = ["Build"]
        Properties =
            [
                "Configuration", buildMode
            ]
    }

Target "BuildApp" (fun _ ->
    build setParams solutionPath
        |> DoNothing
)

Target "CreateCorePackage" (fun _ ->
    let net45Dir = corePackagingDir @@ "lib/net45/"

    CleanDirs [net45Dir]

    CopyFile net45Dir (coreBuildDir @@ "Release/EpiEvents.Core.dll")
    CopyFile net45Dir (coreBuildDir @@ "Release/EpiEvents.Core.pdb")

    NuGet (fun p ->
        {p with
            Authors = authors
            Project = coreProjectName
            Description = coreProjectDescription
            Copyright = copyright
            OutputPath = packagingRoot
            Summary = coreProjectSummary
            WorkingDir = corePackagingDir
            Version = coreAssemblyVersion
            ReleaseNotes = coreReleaseNotes
            Publish = false
            Dependencies =
                [
                PackageDependency "EPiServer.CMS.Core"
                PackageDependency "MediatR"
                PackageDependency "Optional"
                ]
            }) "core.nuspec"
)

Target "CreateCommercePackage" (fun _ ->
    let net45Dir = commercePackagingDir @@ "lib/net45/"

    CleanDirs [net45Dir]

    CopyFile net45Dir (commerceBuildDir @@ "Release/EpiEvents.Commerce.dll")
    CopyFile net45Dir (commerceBuildDir @@ "Release/EpiEvents.Commerce.pdb")

    NuGet (fun p ->
        {p with
            Authors = authors
            Project = commerceProjectName
            Description = commerceProjectDescription
            Copyright = copyright
            OutputPath = packagingRoot
            Summary = commerceProjectSummary
            WorkingDir = commercePackagingDir
            Version = commerceAssemblyVersion
            ReleaseNotes = commerceReleaseNotes
            Publish = false
            Dependencies =
                [
                coreProjectName, coreAssemblyVersion
                PackageDependency "EPiServer.CMS.Core"
                PackageDependency "EPiServer.Commerce.Core"
                PackageDependency "MediatR"
                PackageDependency "Optional"
                ]
            }) "core.nuspec"
)
Target "Default" DoNothing

Target "CreatePackages" DoNothing

"Clean"
   ==> "AssemblyInfo"
   ==> "BuildApp"

"BuildApp"
   ==> "CreateCorePackage"
   ==> "CreateCommercePackage"
   ==> "CreatePackages"

RunTargetOrDefault "Default"
