@echo off

setlocal
set PATH=%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin;%PATH%
set PATH=%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin;%PATH%
set PATH=%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin;%PATH%

.paket\paket update

packages\build\FAKE\tools\FAKE.exe init.fsx %*

rd /s /q WebSharper.Vsix/bin
rd /s /q WebSharper.Vsix/obj
MSBuild.exe WebSharper.Vsix.sln /p:Configuration=Release /t:Rebuild
