@echo off
cls

REM "..\..\packages\FAKE.4.58.6\tools\Fake.exe" build.fsx "BuildApp"
"..\..\packages\FAKE.4.58.6\tools\Fake.exe" build.fsx "CreatePackages"
pause
