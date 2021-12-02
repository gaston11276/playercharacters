using System;
using NFive.SDK.Core.Controllers;
using NFive.SDK.Core.Input;

namespace Gaston11276.Playercharacters.Shared
{
	public class Configuration : ControllerConfiguration
	{
		public SelectionScreenConfiguration SelectionScreen { get; set; } = new SelectionScreenConfiguration();
		public int MaximumCharacters { get; set; } = -1;

		public AutosaveConfiguration Autosave { get; set; } = new AutosaveConfiguration();

		public class AutosaveConfiguration
		{
			public TimeSpan CharacterInterval { get; set; } = TimeSpan.FromMinutes(5);

			public TimeSpan PositionInterval { get; set; } = TimeSpan.FromSeconds(2);
		}

		public class SelectionScreenConfiguration
		{
			public InputControl HotkeyCharacterList { get; set; } = InputControl.ReplayStartStopRecording; // Default to F1
			public InputControl HotkeyAppearanceMenu { get; set; } = InputControl.ReplayStartStopRecordingSecondary; // Default to F2
			public InputControl HotkeySpawnLocation{ get; set; } = InputControl.SelectCharacterMichael; // Default to F5
		}
	}
}
