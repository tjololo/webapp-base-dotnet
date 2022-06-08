FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["template/Tjololo.DI.Template/Tjololo.DI.Template.csproj", "template/Tjololo.DI.Template/"]
COPY scripts/ scripts/
RUN dotnet restore "template/Tjololo.DI.Template/Tjololo.DI.Template.csproj" && \
    chmod +x scripts/build-app.sh
COPY template/ template/
WORKDIR "/customize"
