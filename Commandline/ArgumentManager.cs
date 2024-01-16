using System.Collections.Generic;

using Dialogue.Logging;

namespace Dialogue.Commandline;

public static class ArgumentManager {
	const string KEY_PREFIX = "--";

	static Dictionary<string, string> keywordArgs = new ();

	public static void Parse(in string[] args) {
		// Go through the arguments one by one        
		for (int i = 0; i < args.Length; i++) {
			string arg = args[i];
			
			// Is it a key?
			if (arg.StartsWith(KEY_PREFIX)) {
				// If it's the last arg, it's bound to be a switch
				if (i == args.Length - 1) {
					RegisterArgument(arg, "true");
				}
				// If not, look ahead
				else {
					string nextArg = args[i + 1];
					
					// If the next arg is already another key, this one is a switch
					if (nextArg.StartsWith("--")) {
						RegisterArgument(arg, "true");
					}
					// If it is a value, assign it to the current arg key and fastforward i
					else {
						RegisterArgument(arg, nextArg);
						i++;
					}
				}
			}
			// If not, issue a warning. This shouldn't happen unless this code is broken or the arguments are bad.
			else {
				Logger.Log($"Key-less value at argument #{i + 1} (\"{arg}\"). Double check the CLI arguments. Hint: The key prefix is \"{KEY_PREFIX}\".", MessageSource.Commandline, MessageSeverity.Warning);
			}
		}
	}

	static void RegisterArgument(string key, string value) {
		if (keywordArgs.ContainsKey(key)) {
			Logger.Log($"Failed to register keyword argument \"{key}\": \"{value}\". A value for this key has already been specified (\"{keywordArgs[key]}\"). The earlier value will take precedence.", MessageSource.Commandline, MessageSeverity.Warning);
			return;
		}

		keywordArgs.Add(key, value);
		Logger.Log($"Keyword argument registered: \"{key}\": \"{value}\"", MessageSource.Commandline);
	}
}