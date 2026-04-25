@echo off
echo ================================================
echo  SACCO RMS - Reset Database (load sample data)
echo ================================================
echo.
echo Deletes the existing database so that the 5 group
echo members and sample data load fresh on next launch.
echo.
pause
del /f /q "%~dp0bin\Debug\sacco_data.db" 2>nul
del /f /q "%~dp0bin\Release\sacco_data.db" 2>nul
del /f /q "%~dp0sacco_data.db" 2>nul
echo Done! Now press F5 in Visual Studio to run the app.
echo.
pause
