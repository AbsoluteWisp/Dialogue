using System;

using Dialogue.Commandline;
using Dialogue.Logging;

namespace Dialogue;

public class Dialogue {
	const string branch = "argparser";
	const string version = "0.2.0";

	public static void Main(string[] args) {
		try {
			Logger.Log($"Dialogue {version} ({branch})", MessageSource.Core, MessageSeverity.Info);
			Logger.Log("Help and source code: https://github.com/AbsoluteWisp/Dialogue", MessageSource.Core, MessageSeverity.Info);

			ArgumentManager.Parse(args);
		}
		catch (Exception e) {
			Logger.Log(e, MessageSource.Unknown, MessageSeverity.Fatal);
			Environment.Exit(1);
		}
	}
}