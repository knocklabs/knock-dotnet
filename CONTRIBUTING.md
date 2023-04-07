# Contributing

TODO: This document captures only a few details. It needs to be expanded. Contributions are welcome!

## Getting started

1. Install [asdf](https://asdf-vm.com)
2. Install the asdf Dotnet plugin
    
    ```bash
    asdf plugin add dotnet https://github.com/hensou/asdf-dotnet.git
    ```
3. Run `asdf install` to install the version of .NET Core specified in the [.tool-versions](.tool-versions) file

## Running tests

Run `dotnet test`. You may see a number of warnings or errors as dotnet attempts to test the project across multiple SDK versions or architectures that are not available. The first test run for Version=v3.1 should succeed with something that looks like the following:

```bash
$ dotnet test
Test run for /Users/knock/src/knock-dotnet/KnockTests/bin/Debug/netcoreapp3.1/KnockTests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.7.1
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 10
     Passed: 10
 Total time: 2.7275 Seconds

...

[Errors down here are expected and can be ignored]
```

To supress errors temporarily while working, edit [`KnockTests/KnockTests.csproj`](KnockTests/KnockTests.csproj) and set the `TargetFrameworks` property to `netcoreapp3.1`. This will cause the tests to run only against the version of .NET Core specified in the `.tool-versions` file.

