
### Open Source Packages Used
* [MediatR](https://github.com/jbogard/MediatR) - implements CQRS and dispatching
* [Automapper](https://github.com/AutoMapper/AutoMapper) - mapping to/from entities and dtos.
* [FluentValidation](https://github.com/FluentValidation/FluentValidation) - cleaner validations
* [FluentAssertions](https://github.com/fluentassertions/fluentassertions) - for unit tests
* [Nswag](https://github.com/RicoSuter/NSwag) - creates Open API docs/endpoints

---

### Testing Api Calls

* In Visual Studio Code, install the extension VS Rest Client (repo is [here](https://github.com/Huachao/vscode-restclient)) .
* Go to the root test directory under **Rest Scripts** and open the file in VS Code named **ApiCalls.http**.  
* Be sure BerkeGaming.Api is set as the startup project and hit F5.
* To execute a request, go back to ApiCalls.http in VS Code and click "Send Request" above each HTTP request.

---

### Open Api Document
* Start Api project and navigate to http://localhost:5000/api

---

### Solution Structure

* Based on the [Clean Architecture Solution Template](https://github.com/jasontaylordev/CleanArchitecture).


#### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

#### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

#### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.