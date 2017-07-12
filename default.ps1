properties {
    # build variables
    $configuration = "Release"	# build configuration
    $script:version = "1.0.0"
    $script:nugetVersion = "1.0.0"
    $script:runCoverity = $false

    # directories
    $base_dir = . resolve-path .\
    $project_dir = "$base_dir\src\BCLExtensions\"
    $test_project_dir = "$base_dir\src\BCLExtensions.Tests\"
    $build_output_dir = "$base_dir\src\BCLExtensions\bin\$configuration\"
    $test_results_dir = "$base_dir\TestResults\"
    $build_packages_dir = "$base_dir\BuildPackages\"
    $package_dir = "$base_dir\Package\"
    $archive_dir = "$package_dir" + "Archive"

    # files
    $sln_file = "$base_dir\src\BCLExtensions.sln"

}

Include ".\build\utils.ps1"

task default

task BootstrapNuget {
    BootstrapNuget "nuget" $build_packages_dir
}

task RestoreNuGetPackages {
    exec { dotnet restore $sln_file }
}

task InstallCoveralls -depends BootstrapNuget {
    InstallNugetPackage "coveralls.net" 0.6.0 $build_packages_dir
}

task InstallOpenCover -depends BootstrapNuget {
    InstallNugetPackage "OpenCover" 4.6.519 $build_packages_dir
}

task InstallCoverity -depends BootstrapNuget {
    InstallNugetPackage "PublishCoverity" 0.9.0 $build_packages_dir
}

task InstallGitVersion -depends BootstrapNuget {
    InstallNugetPackage "GitVersion.CommandLine" 3.6.5 $build_packages_dir
}

task GitVersion -depends InstallGitVersion {
    $gitVersion = GetGitVersionPath $build_packages_dir
    exec { & $gitVersion /output buildserver /updateassemblyinfo }
    $json = (& $gitVersion) | ConvertFrom-Json
    $script:version = $json.MajorMinorPatch
    $script:nugetVersion = $json.NuGetVersionV2
    ProjectVersion "$project_dir" $script:nugetVersion
}

task AppVeyorEnvironmentSettings {
    if(Test-Path Env:\GitVersion_ClassicVersion) {
        $script:version = $env:GitVersion_ClassicVersion
        Write-Output "version set to $script:version"
    }
    elseif (Test-Path Env:\APPVEYOR_BUILD_VERSION) {
        $script:version = $env:APPVEYOR_BUILD_VERSION
        Write-Output "version set to $script:version"
    }
    if(Test-Path Env:\GitVersion_NuGetVersionV2) {
        $script:nugetVersion = $env:GitVersion_NuGetVersionV2
        Write-Output "nuget version set to $script:nugetVersion"
    }
    elseif (Test-Path Env:\APPVEYOR_BUILD_VERSION) {
        $script:nugetVersion = $env:APPVEYOR_BUILD_VERSION
        Write-Output "nuget version set to $script:nugetVersion"
    }
}

task clean {
    if (Test-Path $package_dir) {
      Remove-Item $package_dir -r
    }
    if (Test-Path $test_results_dir) {
      Remove-Item $test_results_dir -r
    }
    if (Test-Path $build_packages_dir) {
      Remove-Item $build_packages_dir -r
    }
    dotnet clean $sln_file
}

task build {
    exec { dotnet build -c $configuration $sln_file }
}

task setup-coverity-local {
  $env:APPVEYOR_BUILD_FOLDER = "."
  $env:APPVEYOR_BUILD_VERSION = $script:version
  $env:APPVEYOR_REPO_NAME = "csmacnz/BCLExtensions"
  "You should have set the COVERITY_TOKEN and COVERITY_EMAIL environment variable already"
  $env:APPVEYOR_SCHEDULED_BUILD = "True"
}

task test-coverity -depends setup-coverity-local, coverity

task coverity -depends InstallCoverity -precondition { return $env:APPVEYOR_SCHEDULED_BUILD -eq "True" }{
  $coverityFileName = "BCLExtensions.coverity.$script:nugetVersion.zip"
  $PublishCoverity = GetCoverityPath $build_packages_dir

  & cov-build --dir cov-int msbuild "/t:Clean;Build" "/p:Configuration=$configuration" $sln_file

  & $PublishCoverity compress -o $coverityFileName

  & $PublishCoverity publish -t $env:COVERITY_TOKEN -e $env:COVERITY_EMAIL -z $coverityFileName -d "AppVeyor scheduled build ($env:APPVEYOR_BUILD_VERSION)." --codeVersion $script:nugetVersion
}

task coverage -depends build, coverage-only

task coverage-only -depends InstallOpenCover {
    
    mkdir $test_results_dir

    $opencover = GetOpenCoverPath $build_packages_dir
    exec { 
        Set-Location $test_project_dir
        & $opencover -oldstyle -register:user -target:dotnet.exe "-targetargs:xunit -configuration Debug" -filter:"+[BCLExtensions*]*" -output:"$test_results_dir\BCLExtensionsCoverage.xml"
        Set-Location $base_dir
    }
}

task test-coveralls -depends coverage, InstallCoveralls {
    $coveralls = GetCoverallsPath $build_packages_dir
    exec { & $coveralls --opencover -i "$test_results_dir\BCLExtensionsCoverage.xml" --dryrun -o "$test_results_dir\coverallsTestOutput.json" --repoToken "NOTAREALTOKEN" }
}

task coveralls -depends InstallCoveralls -precondition { return -not $env:APPVEYOR_PULL_REQUEST_NUMBER }{
    $coveralls = GetCoverallsPath $build_packages_dir
    exec { & $coveralls --opencover -i "$test_results_dir\BCLExtensionsCoverage.xml" --treatUploadErrorsAsWarnings }
}

task codecov {
    (New-Object System.Net.WebClient).DownloadFile("https://codecov.io/bash", ".\CodecovUploader.sh")
    .\CodecovUploader.sh -t $env:CODECOV_TOKEN -f "$test_results_dir\BCLExtensionsCoverage.xml"
}

task archive -depends build, archive-only

task archive-only {
    $archive_filename = "BCLExtensions.$script:nugetVersion.zip"

    mkdir $archive_dir

    Copy-Item "$build_output_dir\*" "$archive_dir" -Recurse

    Add-Type -assembly "system.io.compression.filesystem"

    [io.compression.zipfile]::CreateFromDirectory("$archive_dir", "$package_dir\$archive_filename")
}

task pack -depends build, pack-only

task pack-only {
    dotnet pack -c $configuration -o $package_dir ".\src\BCLExtensions\BCLExtensions.csproj"
}

task postbuild -depends pack, archive, coverage-only, codecov, coveralls

task appveyor-install -depends GitVersion, RestoreNuGetPackages

task appveyor-build -depends AppVeyorEnvironmentSettings, build

task appveyor-test -depends AppVeyorEnvironmentSettings, postbuild, coverity