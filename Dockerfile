FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["template/Tjololo.DI.Template/Tjololo.DI.Template.csproj", "template/Tjololo.DI.Template/"]
RUN dotnet restore "template/Tjololo.DI.Template/Tjololo.DI.Template.csproj"
COPY template/ template/
COPY scripts/ scripts/
WORKDIR "/customize"
