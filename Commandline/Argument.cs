using System;
using Dialogue.Logging;

namespace Dialogue.Commandline;

public class Argument {
	/// <summary>
	/// The name of the argument, as it appears on the commandline (omitting the key prefix "--")
	/// </summary>
	public string Name { get; private set; }
	
	/// <summary>
	/// The data type of this argument.
	/// </summary>
	public ArgumentType Type { get; private set; }

	/// <summary>
	/// An optional description of what this argument controls
	/// </summary>
	public string? Description { get; private set; }

	/// <summary>
	/// An optional default value for this argument to assume if no value is provided on the commandline. Leaving this blank indicates a required argument.
	/// </summary>
	string? DefaultValue;

	/// <summary>
	/// The value for this argument specified by the user
	/// </summary>
	string? ProvidedValue;

	public Argument(string name, ArgumentType type, string? description = null, string? defaultValue = null) {
		Name = name.ToLower();
		Type = type;
		Description = description;
		DefaultValue = defaultValue;
	}

	/// <summary>
	/// Checks whether the specified value meets this argument's schema and sets the value if it does. When faced with type mismatches, emits warnings for optional arguments and exceptions for required ones. Protects against overwrites.
	/// </summary>
	/// <param name="value">The value to set</param>
	public void SetAndValidateValue(string value) {
		// Overwrite protection
		if (ProvidedValue != null) {
			Logger.Log($"Argument {Name} already has a value \"{ProvidedValue}\" but received another value \"{value}\"! The earlier value takes precedence.", MessageSource.Commandline, MessageSeverity.Warning);
			return;
		}

		// Validation
		switch (Type) {
			case ArgumentType.Integer:
				if (!int.TryParse(value, out int _)) {
					if (DefaultValue == null) {
						throw new ArgumentException($"Required argument {Name} is of type Integer but received a value \"{value}\" not matching that type!");
					}
					else {
						Logger.Log($"Optional argument {Name} is of type Integer but received a value \"{value}\" not matching that type! Assuming default \"{DefaultValue}\".", MessageSource.Commandline, MessageSeverity.Warning);
						return;
					}
				}
				break;
			case ArgumentType.Boolean:
				value = value.ToLower().Trim();

				if (!(value == "true" || value == "false")) {
					if (DefaultValue == null) 
						throw new ArgumentException($"Required argument {Name} is of type Boolean but received a value \"{value}\" not matching that type!");

					Logger.Log($"Optional argument {Name} is of type Boolean but received a value \"{value}\" not matching that type! Assuming default \"{DefaultValue}\".", MessageSource.Commandline, MessageSeverity.Warning);
					return;
				}
				break;
			case ArgumentType.String:
				// Included to not fall into the "no validator" default, but there are no special rules to validating a string
				break;
			case ArgumentType.LogLevel:
				value = value.ToLower().Trim();
				string levelIndex = "";

				switch (value) {
					case "debug":
						levelIndex = "0";
						break;
					case "info":
						levelIndex = "1";
						break;
					case "warn":
						levelIndex = "2";
						break;
					case "error":
						levelIndex = "3";
						break;
					case "fatal":
						levelIndex = "4";
						break;
					default:
						if (DefaultValue == null) 
						throw new ArgumentException($"Required argument {Name} is of type LogLevel but received a value \"{value}\" not matching that type!");

						Logger.Log($"Optional argument {Name} is of type LogLevel but received a value \"{value}\" not matching that type! Assuming default \"{DefaultValue}\".", MessageSource.Commandline, MessageSeverity.Warning);
						return;
				}

				value = levelIndex;
				break;
			default:
				if (DefaultValue == null) 
					throw new ArgumentException($"Required argument {Name} uses the type of \"{Type.ToString()}\", which does not have a validator!");

				Logger.Log($"Optional argument {Name} uses the type of \"{Type.ToString()}\", which does not have a validator! Assuming default \"{DefaultValue}\".", MessageSource.Commandline, MessageSeverity.Warning);
				return;
		}

		// If validation passed, assign the value
		ProvidedValue = value;
	}

	public string GetValueOrDefault() {
		// Use the user-provided value if present
		if (ProvidedValue != null) return ProvidedValue;
		// Otherwise fall back to default
		if (DefaultValue != null) return DefaultValue;
		// And if there is no default, throw an ArgumentException
		throw new ArgumentException($"Attempted to access argument {Name}, but it wasn't manually set and has no default!");
	}
}