# vprc-infrastructure-logging

Standardised logging infrastructure module for VPRC projects

## Implementations

### Autofac

#### Serilog
To load the Serilog logging module via Autofac, add the following module to the ContainerBuilder registration:
```C#
var containerBuilder = new ContainerBuilder();

...

builder.RegisterModule<InfrastructureLoggingIoCModule>();
```

### DependencyInjection

#### Serilog
To load the Serilog logging module via DependencyInjection, add the following to the ServicesCollection registration:
```C#
var services = new ServiceCollection();

...

Services.AddSerilogLogging();
```

## Logging Configuration Options
### Sample appsettings.json
```json
"LoggingConfigurationOptions": {
    "ApplicationName": "Test.Application",
    "ConsoleMinimumLogLevel": "Information",
    "LoggingFileConfigurations": [{
      "NumberOfFilesRetained": 10,
	  "FileSizeLimitBytes": 450000,
	  "FilePath": "F:\\Logs\\",
	  "Format": "[{Application}],{Level},{Message}",
      "MinimumLogLevel": "Verbose"
    }]
  }

```
## Log Levels
Log level must be one of the following:

- Verbose
- Debug
- Information
- Warning
- Error
- Fatal
Strings other than these entries will throw exceptions.
## Multiple Files
### Sample appsettings.json
```json
{
  "LoggingConfigurationOptions": {
    "ApplicationName": "Infrastructure.Logging.Sample.Host",
    "ConsoleMinimumLogLevel": "Debug",
    "LoggingFileConfigurations": [
      {
        "NumberOfFilesRetained": 15,
        "MinimumLogLevel": "Information",
        "FilePath": "D:\\Logs\\SpecificFolder1\\"
      },
      {
        "MinimumLogLevel": "Fatal",
        "FilePath": "D:\\Logs\\SpecificFolder2"
      },
      {
        "NumberOfFilesRetained": 1
      }
    ]
  }
}
```

## Disabling default ASP.NET Core console logging
You can disable the other configured logging in your projects by adding the following to your Program / Startup code:
```C#
	WebHost.CreateDefaultBuilder(args)
		...
		.ConfigureLogging(l => l.ClearProviders())
```
