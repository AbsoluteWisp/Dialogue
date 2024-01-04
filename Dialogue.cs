using System;

namespace Dialogue;

public class Dialogue {
	const string branch = "argparser";
	const string version = "0.2.0";

	public static void Main(string[] args) {
		try {
			Logging.Logger.Log($"Dialogue {version} ({branch})", Logging.MessageSource.Core, Logging.MessageSeverity.Info);
			Logging.Logger.Log("Help and source code: https://github.com/AbsoluteWisp/Dialogue", Logging.MessageSource.Core, Logging.MessageSeverity.Info);

			
		}
		catch (Exception e) {
			Logging.Logger.Log(e, Logging.MessageSource.Unknown, Logging.MessageSeverity.Fatal);
			Environment.Exit(1);
		}
	}
}