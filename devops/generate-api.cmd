@REM %1 - $(ProjectDir)

CheckFileHasModified\CheckFileHasModified.exe %1\..\api.yaml
IF %ERRORLEVEL% EQU 0 GOTO EXIT

YamlToNSwag\YamlToNSwag.exe %1\..\api.yaml %1\..\api.nswag
IF %ERRORLEVEL% NEQ 0 GOTO EXIT

call nswag run %1\..\api.nswag /runtime:Net60
IF %ERRORLEVEL% NEQ 0 GOTO EXIT

CheckFileHasModified\CheckFileHasModified.exe %1\..\api.yaml -s

:EXIT
exit /b %ERRORLEVEL%