# SimpleWPF
[![Build And Test](https://github.com/dpanfilyonok/SimpleWPF/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/dpanfilyonok/SimpleWPF/actions/workflows/build-and-test.yml)

A desktop application to perform CRUD operations on a set of entities.

## Prerequisites

- .NET SDK 6.0 
- Docker

## Build 
Once the repo is created from the terminal run:
```shell
dotnet restore
dotnet build
```

## Run
```shell
docker compose up
dotnet run --project .\src\SimpleWPF\SimpleWPF.csproj
```

## Test
```shell
dotnet test
```

## Entities Diagram
![](docs/Entities.png)
