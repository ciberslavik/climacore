dotnet publish -r linux-arm
pscp -r source \ClimaDaemon\bin\Debug\net5.0\linux-arm\ root@10.0.10.146:/opt/daemon