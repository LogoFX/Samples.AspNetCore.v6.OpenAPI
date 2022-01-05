@REM %1 - $(ProjectDir)

CheckFileHasModified\CheckFileHasModified.exe %1\..\api.yaml
IF %ERRORLEVEL% NEQ 0 GOTO GENERATION

CheckFileHasModified\CheckFileHasModified.exe %1\..\Templates\Controller.liquid
IF %ERRORLEVEL% NEQ 0 GOTO GENERATION

GOTO EXIT

:GENERATION

YamlToNSwag\YamlToNSwag.exe %1\..\api.yaml %1\..\api.nswag
IF %ERRORLEVEL% NEQ 0 GOTO EXIT

call nswag run %1\..\api.nswag /runtime:Net60
IF %ERRORLEVEL% NEQ 0 GOTO EXIT

CheckFileHasModified\CheckFileHasModified.exe %1\..\api.yaml -s
CheckFileHasModified\CheckFileHasModified.exe %1\..\Templates\Controller.liquid -s

:EXIT
exit /b %ERRORLEVEL%