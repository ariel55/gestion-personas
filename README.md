# Gestion de Personas

El principal objetivo es construir una API REST con las operaciones CRUD necesarias
para gestionar el recurso Persona.


## Prerequisitos

- .NET Core 3 SDK [Windows](https://dotnet.microsoft.com/download)/[Linux](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-current).
- Visual Studio Code [Descargar](https://code.visualstudio.com/).
- Extensiones de Visual Studio Code.
  - [C#](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp).
  - [.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer).
  - [vscode-solution-explorer](https://marketplace.visualstudio.com/items?itemName=fernandoescolar.vscode-solution-explorer).

## Arquitectura

Usarémos una arquitectura sencilla, en la cual tendremos los siguientes componentes:

- Modelos. Clases POCO (Plain Old CLR Object).
- DataAccess. Libreria para el acceso a los datos que no depende del Destino. Implementa los siguientes patrones de diseño:

  - Patrón Repositorio [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html).
  - Unidad de Trabajo [Unit of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html).
- Services. Con estos dos proyecto se busca facilitar el uso de los conceptos de Segregación de Interfaces e Inversión de Dependencias  [SOLID](https://thatcsharpguy.com/posts/los-principios-solid/).
  - Contratos. Definición de Interfaces.
  - Implementación. Implementación de la Interfaz.
- WebAPI. Proyecto implementado en ASPNET Core que entregará mediante REST las frases del dia y del mes.

## Ejecucion

Para instalar los paquetes de nuget en el proyecto ejecute

    dotnet restore

Para Compilar un proyecto, en la carpeta donde se encuentre el .csproj de WebApi ejecute

    dotnet build

Para ejecutar un proyecto, en la carpeta donde se encuentre el .csproj WebApi ejecute

    dotnet run 

