@echo off
setlocal

set D="%~dp0\packages\Redis-64.2.8.17"

pushd "%D%"
dir
redis-server.exe redis.windows.conf
popd
