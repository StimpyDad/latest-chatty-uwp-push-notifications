# FROM microsoft/dotnet
# WORKDIR /dotnetapp
# COPY project.json .
# RUN dotnet restore
# COPY . .
# RUN dotnet publish -c Release -o out
# ENTRYPOINT ["dotnet", "out/dotnetapp.dll"]
FROM microsoft/dotnet:nanoserver
# ENV DOTNET_VERSION 1.1.0

# ENV DOTNET_DOWNLOAD_URL https://dotnetcli.blob.core.windows.net/dotnet/release/1.1.0/Binaries/$DOTNET_VERSION/dotnet-win-x64.$DOTNET_VERSION.zip

# RUN powershell -NoProfile -Command $ErrorActionPreference = 'Stop'; Invoke-WebRequest %DOTNET_DOWNLOAD_URL% -OutFile dotnet.zip; Expand-Archive dotnet.zip -DestinationPath '%ProgramFiles%\dotnet'; Remove-Item -Force dotnet.zip

# RUN setx /M PATH "%PATH%;%ProgramFiles%\dotnet"
#RUN powershell -NoProfile -Command New-NetFirewallRule -DisplayName "Allow Inbound Port 4000" -Direction Inbound –LocalPort 4000 -Protocol TCP -Action Allow

WORKDIR /dotnetapp
COPY bin/Release/netcoreapp1.0/publish/ .
COPY appsettings.json .
ENTRYPOINT ["dotnet", "dotnetapp.dll"]