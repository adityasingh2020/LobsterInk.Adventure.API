#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Step 1: Copy the solution and projects
COPY *.sln .
COPY ./*/*.csproj ./
RUN ls -1 *.csproj | sed -e 's/\.csproj$//' | xargs -I % sh -c 'mkdir -p %; mv %.csproj %'

# Step 2: Restore the nuget dependencies
RUN dotnet restore

# Step 3: Copy the code
COPY . .

# Step 4: Build the solution and (optionally) do the static analysis

RUN dotnet build /app/LobsterInk.Adventure.API/LobsterInk.Adventure.API.csproj -c Release -o /app/build

FROM build AS publish
WORKDIR /app
RUN dotnet test /app/LobsterInk.Adventure.Tests.Unit/LobsterInk.Adventure.Tests.Unit.csproj -c Release --logger:trx
RUN dotnet publish /app/LobsterInk.Adventure.API/LobsterInk.Adventure.API.csproj -c Release -o /app/publish --no-restore


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LobsterInk.Adventure.API.dll"]


