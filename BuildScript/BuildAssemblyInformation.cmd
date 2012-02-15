@echo off
setlocal
call "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\bin\vcvars32.bat">nul 2>nul
call "C:\Program Files\Microsoft Visual Studio 10.0\VC\bin\vcvars32.bat">nul 2>nul

cd /d %~dp0
cd..
set TEMP_BUILD_LOG=BuildLog.txt
Echo Building Assembly Information
msbuild /p:configuration=Release AssemblyInformation.sln >%TEMP_BUILD_LOG%
if errorlevel 1 (
	echo Failed to build AssemblyInformation in Release Mode.
	type %TEMP_BUILD_LOG%
	if not defined NOPAUSE pause
	exit /b 1
)

Echo Cleaning Up Temporary files

del .\bin\Release\AssemblyInformationx64.exp>nul 2>nul
del .\bin\Release\AssemblyInformationx64.ilk>nul 2>nul
del .\bin\Release\AssemblyInformationx64.lib>nul 2>nul
del .\bin\Release\AssemblyInformationx64.pdb>nul 2>nul
del .\bin\Release\AssemblyInformationx64.exe.config>nul 2>nul
del .\bin\Release\AssemblyInformation.exp>nul 2>nul
del .\bin\Release\AssemblyInformation.ilk>nul 2>nul
del .\bin\Release\AssemblyInformation.lib>nul 2>nul
del .\bin\Release\AssemblyInformation.pdb>nul 2>nul
del .\bin\Release\AssemblyInformation.exe.config>nul 2>nul

del %TEMP_BUILD_LOG%>nul 2>nul

rd /S /Q obj>nul 2>nul
Echo Completed Building Assembly Information
if not defined NOPAUSE pause


