using System.Collections.Generic;

namespace Dialogue.Commandline;

public static class ArgumentManager {
	const string KEY_PREFIX = "--";

	static Dictionary<string, string> keywordArgs = new ();

	public static void Parse(in string[] args) {
		// Go through the arguments one by one        
		for (int i = 0; i < args.Length; i++) {
			string arg = args[i];
			
			// Is it a key?
			if (arg.StartsWith(KEY_PREFIX)) {}
				// If it's the last arg, it's bound to be a switch
				if (i == args.Length - 1) {

				}

				// If not, look ahead
					// If the next arg is a value, assign it to this key and fastforward i
					// If not, this is a switch

			// If not, what the hell just happened?
			else {
				Logging.Logger.Log($"Key-less value at argument #{i}. Double check the arguments. Hint: The key prefix is \"{KEY_PREFIX}\".", Logging.MessageSource.Commandline, Logging.MessageSeverity.Warning);
			}
		}
	}

	static void RegisterArgument(string key, string value) {

	}
}