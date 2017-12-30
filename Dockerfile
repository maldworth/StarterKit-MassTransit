FROM microsoft/dotnet-framework:4.6.2
WORKDIR /app
COPY StarterKit.Service/bin/Release/net461 .
ENTRYPOINT ["StarterKit.Service.exe"]
