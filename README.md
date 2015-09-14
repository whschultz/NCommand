# NCommand for .NET

NCommand is an interactive command parser for .NET. Commands can be marked through the `Command-Attribute` or with a `Fluent-Api`. NCommand's aim is to easily expose services and repositories for interactive processing through Console, REST, LINQPad or any other frontend. To get started have a look at the example section below.

NCommand consists of two packages:

- [NuGet package NCommand](https://www.nuget.org/packages/NCommand/) is the core package. Use this package to enable your services or repositories for use with NCommand frontends.
- [NuGet package NCommand.Console](https://www.nuget.org/packages/NCommand.Console/) is a frontend package for NCommand. NCommand enabled libraries can easily be exposed to the console.
- More frontend packages are under construction.

Modes:

Execution / Interactive multicommand mode: If command returns success or exception, command runs in execution mode and returns. Otherwise interactive mode is started in multicommand mode. 

    // Executes and exits after completion:
	MyCommands.exe copy /sourcePath:[path] /destinationPath:[path]
    
	// Change to interactive multicommand mode:
	MyCommands.exe copy /sourcePath:[path]

Features: 

- Interactive mode if parameters are missing.
- Autodetect commands.
- CommandAttribute and FluentApi.

Todo:

- Implement FluentApi
- Support for enums
- Async
- Alternate name for parameters
- Improve validation
- Provider for REST
- Provider for LINQPad


## Examples

Example with `CommandAttribute`: To get started create a new console project and add NuGet packages [NCommand](https://www.nuget.org/packages/NCommand/) and [NCommand.Console](https://www.nuget.org/packages/NCommand.Console/). To enable any repository or service for use through console, add a class that wraps the needed functionality:

	namespace Commands
	{
	    public class ExampleCommands
	    {	
	        [Command(description: "Copy resources.")]
	        public bool Copy( 
				[Argument(description:"Source path.")] string sourcePath,
				[Argument(description:"Destination path.")] string destinationPath
				[Argument(defaultValue: false)] bool overwrite 
			)
	        {
				// do something
	            return true;
	        }
		}
	}	

Add following code to your console main:

    static void Main(string[] args)
    {
        NCommander.App.RunConsole(args);
    }

The command `Copy` can be executed with following command line calls: 

    MyCommands.exe copy /sourcePath:[path] /destinationPath:[path]
    Output: true/false

    MyCommands.exe copy /sourcePath:[path] 
	Interactive mode prompts for parameter sourcePath.
	Output: true/false

    MyCommands.exe
	A list of all commands (in this case only copy) is shown.

