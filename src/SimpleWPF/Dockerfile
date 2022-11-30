FROM mcr.microsoft.com/dotnet/sdk:6.0-nanoserver-1809 AS build
WORKDIR /source
COPY ["src/SimpleWPF/SimpleWPF.csproj", "src/SimpleWPF/"]
RUN dotnet restore "src/SimpleWPF/SimpleWPF.csproj"
COPY . .
WORKDIR "/source/src/SimpleWPF"
RUN dotnet build "SimpleWPF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleWPF.csproj" -c Release -o /app/publish

ENTRYPOINT ["tail", "-f", "/dev/null"]

