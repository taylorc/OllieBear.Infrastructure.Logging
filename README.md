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
    "LoggingFileConfiguration": {
      "NumberOfFilesRetained": 10,
	  "FileSizeLimitBytes": 450000,
	  "FilePath": "F:\\Logs\\Log01.log",
	  "Format": "[{Application}],{Level},{Message}"
    }
  }

```