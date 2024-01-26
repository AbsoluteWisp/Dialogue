# Changelog
Learn more about the philosophy and format of this file in [VERSIONING](./VERSIONING.md)

## \[EDGE\] ArgParser - to be version 0.2.0

### Added
`+` New namespace created: `Dialogue.Commandline`  
`+` New message source created: `Commandline`  
`+` `ArgumentManager` static class with methods for handling command line arguments  
`+` `Argument` class to describe the metadata and state of an argument (default and provided values, description, name, etc.)  
`+` A prepopulated list of `Argument`s (for now only a "minimum log level to show" argument).  
`+` `Parse` static method for loading an array of command line arguments into a dictionary of keyword arguments (`string` -> `Argument`)  
`+` `GetArgument` static method retrieves the value of an argument, with defaults available for optional arguments.

### Changed
`/` After catching a fatal exception, the program will immediately exit with a non-zero exit code (`1`) as opposed to coming out of the catch block and exiting with a misleading "success" code (`0`)  
`/` Updated the repository link because of new GitHub username (Ghostling225 -> AbsoluteWisp)

### Fixed
`*` Fixed source file indents using spaces instead of tabs

## Logging - version 0.1.0

### Added
`+` Source files are now organised into namespaces. Directory structure reflects C# namespaces.  
`+` New namespace created: `Dialogue.Logging`  
`+` Message severity enumeration describing severity of a given log message  
`+` Message source enumeration describing the source of a given log message in terms of major components  
`+` Logger static class for console logging  
`+` Logger method for printing text messages  
`+` Logger method for printing exceptions