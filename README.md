# Pokedex REST API
Pokedex REST API provides information about a given pokemon name. Current version of the API provides below information:
* name
* description
* habitat
* legendary status

# REST API overview
It provides 2 end points as below:
- /pokemon/{name}
	 - This end-point returns a pokemon JSON object with { name, description, habitat, isLegendary } fields.
	 
- /pokemon/translated/{name}
	- This end-point in addition to the pokemon JSON object, it translates pokemon's description with below rules.
		- It applies Yoda translation if the pokemon habitat is rare or has legendary status
		- It applied Shakeshpear translation if the pokemon is not legendary
		- If none of the above 2 rules are applicable it returns the pokemon with default description i.e. without any translation.

## Framework or Libraries
The API is built using .net core 3.1 frameowrk in Visual Studio 2019 IDE Version 16.10.3.
The code can also be built and executed using Visual Studio Code Version 1.58 by opening .code-workspace file inside folder [.vscode]

.net 3.1 SDK is required to build the source code.
Please refer below documentation to setup environment for .net core 3.1 on windows:
- https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=netcore31 &
- https://dotnet.microsoft.com/download/dotnet/3.1

For development: Below packages or resources are used:

- PokeApiNet v3.0.2 - A .Net wrapper for the Pokemon API at https://pokeapi.co.
	- More details can be found here: https://github.com/mtrdp642/PokeApiNet/releases
- For Yoda and Shakespeare translation, Funtranslation with below end-points is used:
	- https://funtranslations.com/api/yoda
	- https://funtranslations.com/shakespeare
- Newtonsoft.Json v12.0.3 is used manage and convert objects to Json.

For Testing, xUnit v2.4.0 is used with below packages:
- Microsoft.AspNetCore.Mvc.Testing v3.1.9- used to perform api tests.
- Moq v4.16.1 
	- Mocking framework for .NET to create mocks for core interfaces and objects.
- FluentAssertions v 5.10.3
	- Fluent API for asserting the results of unit tests.
	- To help handle asserts and object comparision efficiently.
   
## Solution Structure
The solution file is available under webapi folder. It has several projects structured as below:
- Pokemon.Api
	- Core web api project, with end-points implemented by *PokemonController*.
	- *PokemonController* uses IPokemonClient through DI.

* Pokemon.Client
	- contains *PokemonApiClient*, it is responsible to communicate with to communicate with Pokemon API using PokeApiNet v3.0.2.
	- it also interfaces with *IYodaTranslation* and *IShakespeareTranslation* translation clients.
	
* PokemonTranslation.Client
	- Contains PokemonYodaClient and PokemonShakespeareClient implemet relevant interface to perform respective translations using FunTranslation API
	- The clients can be improved to provide caching to speedup response times.

* Pokemon.Interfaces
	- contains the interface definitions for PokeAPI and translation client.
	- Used for dependency injection at the webapi level and mocking the object for unit testing.

* Pokemon.DataModel
	- contains data objects used to store pokemon information and translation responses from Fun Translations API.

* Pokemon.Api.Tests
	- Contains tests to verify the behaviour web api and pokemon.client components.
	- Moq is used to create mock behaviours for IPokemonClient
	- Test can be improved to mock the behavior of translation clients too.

# WebApi url
The REST API is publised as an app service on Azure and is available at the below location:
https://pokemonapi.azurewebsites.net/pokemon/mewtwo

# Build and Run Instructions
* Using git client, clone the source code using the repo link.
* Once source code is loaded, please install pre-requisites based on the development environment of choice.
* If you are using Visual Studio, once the solution is opened Pokemon.API project should be setup as StartUp project. Run webapi project from by pressing F5.
* If you are using Visual Studio code,  once the workspace is loaded and all relevant dependencies/extensions loaded. Run the application by pressing F5 or from the terminal type *dotnet run* command

* Items to be done for production
	* Add api documentation page at the root and exception/error pages
	* Error logging
	* Caching for translation requests
