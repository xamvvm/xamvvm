@echo off

set msbuild="C:\Program Files (x86)\MSBuild\14.0\bin\msbuild.exe"
set config=Release
set platform=AnyCPU
set warnings=;1591;
if "%CI%"=="True" (
    set logger=/l:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
)
set buildargs=/p:Configuration="%config%" /p:Platform="%platform%" /p:NoWarn="%warnings%" /v:minimal %logger%

echo Restoring NuGets...

nuget restore source/Xamvvm.Forms/packages.config -solutiondirectory source/
nuget restore source/Xamvvm.Forms.RxUI/packages.config -solutiondirectory source/

echo Building Xamvvm...

%msbuild% source/Xamvvm.Core/Xamvvm.Core.csproj %buildargs%
%msbuild% source/Xamvvm.Forms/Xamvvm.Forms.csproj %buildargs%
%msbuild% source/Xamvvm.Forms.RxUI/Xamvvm.Forms.RxUI.csproj %buildargs%
%msbuild% source/Xamvvm.Mock/Xamvvm.Mock.csproj %buildargs%

echo Generating symbols with Gitlink...

GitLink.exe %~dp0 -u https://github.com/xamvvm/xamvvm

echo Packaging NuGets...

nuget pack source/Xamvvm.Core.nuspec
nuget pack source/Xamvvm.Forms.nuspec
nuget pack source/Xamvvm.Forms.RxUI.nuspec
nuget pack source/Xamvvm.Mock.nuspec

echo All done.
