FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /home/app
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore
COPY . .
RUN dotnet test ./CakeCrafter.UnitTests/CakeCrafter.UnitTests.csproj
RUN dotnet publish ./CakeCrafter.API/CakeCrafter.API.csproj -o /publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /publish
COPY --from=build /publish ./
ENTRYPOINT [ "dotnet", "CakeCrafter.API.dll" ]