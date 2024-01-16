using System;
using Dialogue.Logging;

namespace Dialogue.Commandline;

public class Argument {
	/// <summary>
	/// The name of the argument, as it appears on the commandline (omitting the key prefix "--")
	/// </summary>
	public string Name;
	
	/// <summary>
	/// The data type of this argument.
	/// </summary>
	public ArgumentType Type;

	/// <summary>
	/// An optional description of what this argument controls
	/// </summary>
	public string? Description;

	/// <summary>
	/// An optional default value for this argument to assume if no value is provided on the commandline. Leaving this blank indicates a required argument.
	/// </summary>
	public string? DefaultValue;

	/// <summary>
	/// The value for this argument specified by the user
	/// </summary>
	public string? ProvidedValue;

	public Argument(string name, ArgumentType type, string? description = null, string? defaultValue = null) {
		Name = name;
		Type = type;
		Description = description;
		DefaultValue = defaultValue;
	}

	/// <summary>
	/// Checks whether the specified value matches this argument's type and assigns it if it does. In case of mismatches emits a warning for optional arguments or an exception for required arguments.
	/// </summary>
	/// <param name="providedValue">The value to set</param>
	public void SetAndValidateValue(string value) {
		// Validation
		switch (Type) {
			case ArgumentType.Integer:
				if (!int.TryParse(value, out int _)) {
					if (DefaultValue == null) {
						throw new ArgumentException($"Required argument {Name} is of type Integer but received a value \"{value}\" not matching that type!");
					}
					else {
						Logger.Log($"Optional argument {Name} is of type Integer but received a value \"{value}\" not matching that type!", MessageSource.Commandline, MessageSeverity.Warning);
						return;
					}
				}
				break;
			case ArgumentType.Boolean:
				if (!(value.ToLower() == "true" || value.ToLower() == "false")) {
					if (DefaultValue == null) {
						throw new ArgumentException($"Required argument {Name} is of type Boolean but received a value \"{value}\" not matching that type!");
					}
					else {
						Logger.Log($"Optional argument {Name} is of type Boolean but received a value \"{value}\" not matching that type!", MessageSource.Commandline, MessageSeverity.Warning);
						return;
					}
				}
				break;
			case ArgumentType.String:
				// Included for completeness, can't validate a string
				break;
		}

		// If validation passed, assign the value
		ProvidedValue = value;
	}
}