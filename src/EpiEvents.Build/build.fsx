#r @"../../packages/FAKE.4.58.6/tools/FakeLib.dll"

open Fake

let company = "Maris Krivtezs"
let authors = [company]
let projectName = "EpiEvents.Core"
let projectDescription = "An in-process messaging style event handling for Episerver"
let projectSummary = projectDescription
let releaseNotes = "Initial release"
let copyright = "Copyright © Maris Krivtezs 2017"
let assemblyVersion = "1.0.0"

let solutionPath = "../../EpiEvents.sln"
let buildDir = "../EpiEvents.Core/bin"
let packagingRoot = "../../packaging/"
let packagingDir = packagingRoot @@ "core"
let assemblyInfoPath = "../EpiEvents.Core/Properties/AssemblyInfo.cs"

let buildMode = getBuildParamOrDefault "buildMode" "Release"

MSBuildDefaults <- {
    MSBuildDefaults with
        ToolsVersion = Some "14.0"
        Verbosity = Some MSBuildVerbosity.Minimal }

Target "Clean" (fun _ ->
    CleanDirs [buildDir; packagingRoot; packagingDir]
)

open Fake.AssemblyInfoFile

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo assemblyInfoPath
      [ Attribute.Product projectName
        Attribute.Version assemblyVersion
        Attribute.FileVersion assemblyVersion
        Attribute.ComVisible false
        Attribute.Copyright copyright
        Attribute.Company company
        Attribute.Description projectDescription
        Attribute.Title projectName]
)

let setParams defaults = {
    defaults with
        ToolsVersion = Some("14.0")
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
    let net45Dir = packagingDir @@ "lib/net45/"

    CleanDirs [net45Dir]

    CopyFile net45Dir (buildDir @@ "Release/EpiEvents.Core.dll")
    CopyFile net45Dir (buildDir @@ "Release/EpiEvents.Core.pdb")

    NuGet (fun p ->
        {p with 
            Authors = authors
            Project = projectName
            Description = projectDescription
            OutputPath = packagingRoot
            Summary = projectSummary
            WorkingDir = packagingDir
            Version = assemblyVersion
            ReleaseNotes = releaseNotes
            Publish = false
            }) "core.nuspec"
)

Target "Default" DoNothing

Target "CreatePackages" DoNothing

"Clean"
   ==> "AssemblyInfo"
   ==> "BuildApp"

"BuildApp"
   ==> "CreateCorePackage"
   ==> "CreatePackages"

RunTargetOrDefault "Default"
