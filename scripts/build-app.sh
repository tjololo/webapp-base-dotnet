#!/bin/bash
pushd /template/Tjololo.DI.Template

pushd /
dotnet add /template/Tjololo.DI.Template/Tjololo.DI.Template.csproj reference /customize/**/*.csproj || true
popd

dotnet build Tjololo.DI.Template.csproj --configuration Release --output /app/build
dotnet publish Tjololo.DI.Template.csproj --configuration Release --output /app/publish

popd