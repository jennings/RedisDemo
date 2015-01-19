@echo off
setlocal

set DIR=packages\Redis-64.2.8.17

%DIR%\redis-cli.exe %*
