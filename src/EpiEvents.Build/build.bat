@echo off
cls

"..\..\packages\FAKE.4.58.6\tools\Fake.exe" build.fsx "CreatePackages"
pause
