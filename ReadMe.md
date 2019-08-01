# Tempestas
Api test project that uses free weather apis to return information about weather. Upon request service will issue three request to implemented Third-party apis and return information from first to respond.
Service will measure time of request precessing and write it to log.

### Prerequisites
* Browser
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)
  
IDE of your choice:
* [JetBrains Rider](https://www.jetbrains.com/rider/download/#section=windows)
* [Visual Studio 2019](https://www.visualstudio.com/downloads/)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. Restore required packages by running using IDE or by running:
     ```
     dotnet restore
     ```
     
  3. Build solution using IDE or by running:
     ```
     dotnet build
     ```
  4. launchSettings.json holds setting to start project at https://localhost:5001/sagger, which will be serve swagger ui for convenient testing application work. Time taken to process request and api that did it is included in returned model, so there is no need to look in logs.
  
  ## Technologies
* .NET Core 2.2
* ASP.NET Core 2.2
* [Serilog](https://serilog.net/)
* [MediatR](https://github.com/jbogard/MediatR/wiki)
Library leverages mediator pattern for solving the problem of decoupling the sending of messages from handling messages. In this solution mainly controls CQRS logic.
* [Nswager](https://github.com/RicoSuter/NSwag/wiki)     