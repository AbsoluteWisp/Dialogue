using System;
using System.Collections.Generic;

using Dialogue.Logging;

namespace Dialogue.Commandline;

public static class ArgumentManager {
	const string NAME_PREFIX = "--";

	static readonly Dictionary<string, Argument> arguments = new () {
		{"loglevel", new Argument("LogLevel", ArgumentType.LogLevel, "Sets the minimum level of log messages that get displayed.", "2")}
	};

	/// <summary>
	/// Maps an array of CLI arguments to a dictionary of keyword arguments.
	/// </summary>
	/// <param name="args">The argument array</param>
	public static void Parse(in string[] args) {
		// Go through the arguments one by one        
		for (int i = 0; i < args.Length; i++) {
			string arg = args[i];
			
			// Is it a key?
			if (arg.StartsWith(NAME_PREFIX)) {
				// If it's the last arg, it's bound to be a switch
				if (i == args.Length - 1) {
					RegisterArgument(arg.Substring(NAME_PREFIX.Length), "true");
				}
				// If not, look ahead
				else {
					string nextArg = args[i + 1];
					
					// If the next arg is already another key, this one is a switch
					if (nextArg.StartsWith("--")) {
						RegisterArgument(arg.Substring(NAME_PREFIX.Length), "true");
					}
					// If it is a value, assign it to the current arg key and fastforward i
					else {
						RegisterArgument(arg.Substring(NAME_PREFIX.Length), nextArg);
						i++;
					}
				}
			}
			// If not, issue a warning. This shouldn't happen unless this code is broken or the arguments are bad.
			else {
				Logger.Log($"Name-less value at argument #{i + 1} (\"{arg}\")! Double check the CLI arguments. Hint: The name prefix is \"{NAME_PREFIX}\".", MessageSource.Commandline, MessageSeverity.Warning);
			}
		}
	}

	public static string GetArgument(string name) {
		if (!arguments.ContainsKey(name)) {
			throw new ArgumentException($"An attempt was made to read from nonexistent argument \"{name}\"");
		}
		return arguments[name].GetValueOrDefault();
	}

	static void RegisterArgument(string name, string value) {
		// Ignore if it's not a recognised argument
		if (!arguments.ContainsKey(name)) {
			Logger.Log($"Failed to register keyword argument \"{name}\"! No argument is defined with this name.", MessageSource.Commandline, MessageSeverity.Warning);
			return;
		}

		// If it's a recognised name, try to save its value. The Argument class takes care of validation and overwriting.
		arguments[name].SetAndValidateValue(value);
	}
}