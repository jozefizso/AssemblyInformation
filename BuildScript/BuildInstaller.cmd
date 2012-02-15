@echo off
setlocal
call "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\bin\vcvars32.bat">nul 2>nul
call "C:\Program Files\Microsoft Visual Studio 10.0\VC\bin\vcvars32.bat">nul 2>nul
cls
cd /d %~dp0
cd..
SET NOPAUSE=TRUE
call BuildScript\BuildAssemblyInformation.cmd
if errorlevel 1 (
	echo Failed building AssemblyInformation binaries. Aborting.
	pause
	exit /b 1
)

call BuildScript\BuildShellExtension.cmd
if errorlevel 1 (
	echo Failed building AssemblyInformation Shell Extensions binaries. Aborting.
	pause
	exit /b 1
)
rd /s /q Setup >nul 2>nul
verify >nul
set TEMP_BUILD_LOG=BuildLog.txt
Echo Building 64 bit Installer
devenv AssemblyInformationSetup.sln /build Release /project AssemblyInformationSetupx86 >%TEMP_BUILD_LOG%
if errorlevel 1 (
	echo Failed to create 64 bit Installer
	type %TEMP_BUILD_LOG%
	pause
	exit /b 1
)
Echo Building 32 bit Installer
devenv AssemblyInformationSetup.sln /build Release /project AssemblyInformationSetupx64 >%TEMP_BUILD_LOG%
if errorlevel 1 (
	echo Failed to create 32 bit Installer
	type %TEMP_BUILD_LOG%
	pause
	exit /b 1
)

del %TEMP_BUILD_LOG%>nul 2>nul
rd /S /Q bin>nul 2>nul
rd /S /Q obj>nul 2>nul
Echo Build Completed
pause


