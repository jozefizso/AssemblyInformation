@echo off
setlocal

@if "%VS120COMNTOOLS%"=="" goto error_no_VS120COMNTOOLSDIR

call "%VS120COMNTOOLS%vsvars32.bat"

cd /d %~dp0
cd..
set TEMP_BUILD_LOG=BuildLog.txt
Echo Building Assembly Information
msbuild.exe /p:Configuration=Release;Platform=x86 AssemblyInformation.sln >%TEMP_BUILD_LOG%
if errorlevel 1 (
    echo "Failed to build AssemblyInformation in Release (x86) mode."
    type %TEMP_BUILD_LOG%
    if not defined NOPAUSE pause
    exit /b 1
)

msbuild.exe /p:Configuration=Release;Platform=x64 AssemblyInformation.sln >%TEMP_BUILD_LOG%
if errorlevel 1 (
    echo "Failed to build AssemblyInformation in Release (x64) mode."
    type %TEMP_BUILD_LOG%
    if not defined NOPAUSE pause
    exit /b 2
)

Echo Cleaning Up Temporary files

del .\bin\Release\AssemblyInformation64.exp>nul 2>nul
del .\bin\Release\AssemblyInformation64.ilk>nul 2>nul
del .\bin\Release\AssemblyInformation64.lib>nul 2>nul
del .\bin\Release\AssemblyInformation64.pdb>nul 2>nul
del .\bin\Release\AssemblyInformation64.exe.config>nul 2>nul
del .\bin\Release\AssemblyInformation32.exp>nul 2>nul
del .\bin\Release\AssemblyInformation32.ilk>nul 2>nul
del .\bin\Release\AssemblyInformation32.lib>nul 2>nul
del .\bin\Release\AssemblyInformation32.pdb>nul 2>nul
del .\bin\Release\AssemblyInformation32.exe.config>nul 2>nul

del %TEMP_BUILD_LOG%>nul 2>nul

rd /S /Q obj>nul 2>nul
Echo Completed Building Assembly Information
if not defined NOPAUSE pause
exit /B 0

:error_no_VS120COMNTOOLSDIR
echo ERROR: Cannot determine the location of the VS Common Tools folder (%%VS120COMNTOOLS%%).
goto end

:end
