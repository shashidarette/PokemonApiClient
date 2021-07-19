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
The API is built using .net core 3.1 frameowrk in Visual Studio 2019 IDE.
The code can also be built and executed using Visual Studio Code by opening .code-workspace file inside folder [.vscode]

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
The solution file is available under webapi folder. It is structured as follows:
* Pokemon.Api
* Pokemon.Client
* PokemonTranslation.Client
* Pokemon.Interfaces
* Pokemon.DataModel
* Pokemon.Api.Tests

