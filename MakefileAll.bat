@echo off

set msbuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
set config=Release
set platform=AnyCPU
set warnings=1591,1572,1573,1570,1000,1587
if "%CI%"=="True" (
    set logger=/l:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
)
set buildargs=/p:Configuration="%config%" /p:Platform="%platform%" /p:NoWarn="%warnings%" /v:minimal %logger%

cd source

echo Restoring NuGets...

nuget restore -MsbuildPath "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin"
dotnet restore

echo Building Xamvvm...

%msbuild% Xamvvm.Core/Xamvvm.Core.csproj %buildargs%
%msbuild% Xamvvm.Forms/Xamvvm.Forms.csproj %buildargs%
%msbuild% Xamvvm.Forms.RxUI/Xamvvm.Forms.RxUI.csproj %buildargs%
%msbuild% Xamvvm.Mock/Xamvvm.Mock.csproj %buildargs%

echo Packaging NuGets...

nuget pack Xamvvm.Core.nuspec
nuget pack Xamvvm.Forms.nuspec
nuget pack Xamvvm.Forms.RxUI.nuspec
nuget pack Xamvvm.Mock.nuspec

echo All done.
