# Calculator-Service
HTTP/RESTbased 'Calculator Service’ capable of some basic arithmetic operations, like add, subtract, square, etc. along with a history service keeping track of requests sharing a common an identifier.

## How to run the applications
The applications are developed with NET.Core 6. You can download the SDK from here. 
https://dotnet.microsoft.com/es-es/download/dotnet/6.0

After cloning the repository you have to execute the following commands from within the created folder 
### CalculatorService.Server
#### Deploy
```bash
dotnet publish CalculatorService.Server/CalculatorService.Server.WebAPI -o application/CalculatorService.Server
```
### Run
```bash
./application/CalculatorService.Server/CalculatorService.Server.WebAPI.exe
```
You can see how start the application.
### CalculatorService.Client
#### Deploy
```bash
dotnet publish CalculatorService.Client/CalculatorService.Client -o application/CalculatorService.Client
```
### Run
```bash
./application/CalculatorService.Client/CalculatorService.Client.exe
```
You can see:
**Usage CalculatorService-Client.exe Operation [values] [User-id]
Operation: add, sub, mult, div, sqrt, journal**

## Log
The log is created in a folder called **Log** that is created wherever the CalculatorService.Server application **is run**.



