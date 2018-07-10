# vprc-infrastructure-logging

Logging module for infrastructure
This module contains the logging infrastructure package. 
To load it, simply include the following line in the bootstrapper code:
```
builder.RegisterModule<InfrastructureLoggingIoCModule>();
```

Include the following in the appsettings.json:
```
"ApplicationName": "<APPLICATION_NAME>"
```
