# Changelog
Learn more about the philosophy and format of this file in [VERSIONING](./VERSIONING.md)

## \[EDGE\] ArgParser - to be version 0.2.0

### Added
`+` New namespace created: `Dialogue.Commandline`  
`+` New message source created: `Commandline`  
`^+` `ArgumentManager` static class with methods for handling command line arguments  
`^+` `Parse` static method for loading an array of command line arguments into a dictionary of keyword arguments  
`^+` `Validate` static method for checking the validity of the loaded dictionary (presence of required arguments, malformed arguments that don't match any supported option, invalid argument values), issuing errors for invalid required arguments and warnings for invalid optional arguments  
`^+` `GetArgumentOrDefault` retrieves the value of an argument if it's valid, or a default value otherwise.

### Changed
`/` After catching a fatal exception, the program will immediately exit with a non-zero exit code (`1`) as opposed to coming out of the catch block and exiting with a misleading "success" code (`0`)  
`/` Updated the repository link because of new GitHub username (Ghostling225 -> AbsoluteWisp)

## Logging - version 0.1.0

### Added
`+` Source files are now organised into namespaces. Directory structure reflects C# namespaces.  
`+` New namespace created: `Dialogue.Logging`  
`+` Message severity enumeration describing severity of a given log message  
`+` Message source enumeration describing the source of a given log message in terms of major components  
`+` Logger static class for console logging  
`+` Logger method for printing text messages  
`+` Logger method for printing exceptions