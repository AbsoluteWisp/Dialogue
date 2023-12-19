using System;
using System.Collections.Generic;

namespace Dialogue.Logging;

public static class Logger {
	static readonly Dictionary<MessageSeverity, ConsoleColor> severityColors = new() {
		{ MessageSeverity.Debug, ConsoleColor.DarkMagenta },
		{ MessageSeverity.Info, ConsoleColor.White },
		{ MessageSeverity.Warning, ConsoleColor.Yellow },
		{ MessageSeverity.Error, ConsoleColor.Red },
		{ MessageSeverity.Fatal, ConsoleColor.DarkRed }
	};

	static readonly Dictionary<MessageSeverity, string> severityNames = new() {
		{ MessageSeverity.Debug,   "debug" },
		{ MessageSeverity.Info,    "info " },
		{ MessageSeverity.Warning, "warn " },
		{ MessageSeverity.Error,   "error" },
		{ MessageSeverity.Fatal,   "fatal" }
	};

	static readonly Dictionary<MessageSource, string> sourceNames = new() {
		{ MessageSource.Unknown, "nosource" },
		{ MessageSource.Core,    "core    " }
	};
	
	public static void Log(string message, MessageSource source = MessageSource.Unknown, MessageSeverity severity = MessageSeverity.Debug) {
		string line = $"< {severityNames[severity]} |{sourceNames[source]} | {message}";
		
		ColorWriteLine(severityColors[severity], line.TrimEnd());
	}

	public static void Log(Exception e, MessageSource source = MessageSource.Unknown, MessageSeverity severity = MessageSeverity.Error) {
		var entry = $"An instance of {e.GetType().FullName} was thrown: \"{e.Message}\"\n";

		if (e.Data.Count == 0) {
			entry += "No additional data was provided.\n";	
		}
		else {
			entry += "There was additional data associated with the exception:\n";

			foreach (var k in e.Data.Keys) {
				var v = e.Data[k];

				if (v != null) 
					entry += $"{k.ToString()}: {v.ToString()}";
				else
					entry += $"{k.ToString()}: NULL";
				
			}
		}

		Log(entry, source, severity);
	}

	static void ColorWrite(ConsoleColor color, string text) {
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.Write(text);
		Console.ForegroundColor = oldColor;		
	}

	static void ColorWriteLine(ConsoleColor color, string text) {
		ColorWrite(color, text + '\n');
	}
}