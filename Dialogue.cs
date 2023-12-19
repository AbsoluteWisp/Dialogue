using System;

namespace Dialogue;

public class Dialogue {
	const string branch = "logging";
	const string version = "0.1.0";

	public static void Main(string[] args) {
		try {
			Logging.Logger.Log($"Dialogue {version} ({branch})", Logging.MessageSource.Core, Logging.MessageSeverity.Info);
			Logging.Logger.Log("Help and source code: https://github.com/Ghostling225/Dialogue", Logging.MessageSource.Core, Logging.MessageSeverity.Info);

			throw new ArithmeticException("Example arithmetic exception");
		}
		catch (Exception e) {
			Logging.Logger.Log(e, Logging.MessageSource.Unknown, Logging.MessageSeverity.Fatal);
		}
	}
}