# LibLab.Interview.Lotr.SDK

LibLab.Interview.Lotr.SDK is a toolkit to access LOTR API and query movies information.

## Installation
Use the package manager [nuget](https://www.nuget.org/) to install LibLab.Interview.Lotr.SDK.

#### Package Manager Console
```bash
Install-Package LibLab.Interview.Lotr.SDK
```

#### .NET CLI Console
```bash
dotnet add package LibLab.Interview.Lotr.SDK
```

## Usage
### Register to DI container
```
services.AddLotrMovieService();
// or
services.AddLotrMovieService("validApiToken");
```

### Inject and use
```
private readonly ILOTRMovieService service;
...

// returns all movies from the API
var movies = await service.GetMoviesAsync();

```

## License

[MIT](https://choosealicense.com/licenses/mit/)