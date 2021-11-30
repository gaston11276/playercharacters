using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

using fpGuid = Gaston11276.SimpleUi.UiPanel.fpGuid;
namespace Gaston11276.Playercharacters.Client
{
	class HudCharacters : Hud
	{
		protected List<fpVoid> onCreatorOpenCallbacks = new List<fpVoid>();
		protected List<fpGuid> onCreatorCloseCallbacks = new List<fpGuid>();

		private List<Character> characters;

		private HudComponentCharacterList uiCharacterList = new HudComponentCharacterList();
		private HudComponentNewCharacter uiNewCharacter = new HudComponentNewCharacter();

		private fpVoid OnCreateCallback;
		private fpGuid OnDeleteCallback;

		public HudCharacters()
		{
			uiCharacterList.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);
			uiNewCharacter.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);
		}

		public void RegisterOnCreatorOpenCallback(fpVoid OnOpen)
		{
			onCreatorOpenCallbacks.Add(OnOpen);
		}

		public void RegisterOnCreatorCloseCallback(UiPanel.fpGuid OnClose)
		{
			onCreatorCloseCallbacks.Add(OnClose);
		}

		private void OnCreatorOpen()
		{
			foreach (fpVoid onCreatorOpen in onCreatorOpenCallbacks)
			{
				onCreatorOpen();
			}
		}

		private void OnCreatorClose()
		{
			foreach (UiPanel.fpGuid onCreatorClose in onCreatorCloseCallbacks)
			{
				onCreatorClose(uiCharacterList.selectedCharacterId);
			}
		}

		public void GetCharacterInfo(ref Character character)
		{
			uiNewCharacter.GetCharacterInfo(ref character);
		}

		public void ClearCreatorEdit()
		{
			uiNewCharacter.ClearCreatorEdit();
		}

		public void SetCharacters(List<Character> characters)
		{
			this.characters = characters;
			uiCharacterList.UpdateCharacterList(characters);
		}

		public void RegisterOnCreateCallback(fpVoid OnCreate)
		{
			this.OnCreateCallback = OnCreate;
		}

		public void RegisterOnDeleteCallback(fpGuid OnDelete)
		{
			this.OnDeleteCallback = OnDelete;
		}


		public void UpdateCharacterList()
		{
			uiCharacterList.UpdateCharacterList(characters);
		}

		protected override void OnOpen()
		{
			uiCharacterList.Open();
			base.OnOpen();
			OnCreatorOpen();
		}

		protected override void OnClose()
		{
			base.OnClose();
			OnCreatorClose();
		}

		public override void OnInputKey(int state, int keycode)
		{
			if (IsOpen())
			{
				base.OnInputKey(state, keycode);

				if (uiCharacterList.selectedCharacterId != null && uiCharacterList.selectedCharacterId.GetHashCode() != 0) // Force player to select a character
				{
					if (state == 3 && keycode == 27)// Escape
					{
						Close();
					}

					if (state == 3 && keycode == hotkey)	// Default F1
					{
						Close();
					}
				}
				
			}
		}

		private void OnPlay()
		{
			Close();
		}

		private void OnCreate()
		{
			OnCreateCallback();
		}

		private void OnDelete(System.Guid selectedCharacterId)
		{
			OnDeleteCallback(uiCharacterList.selectedCharacterId);
		}

		public override void CreateUi()
		{
			base.CreateUi();

			uiCharacterList.SetLogger(Logger);
			uiNewCharacter.SetLogger(Logger);
			
			uiNewCharacter.RegisterOnCreateCallback(OnCreate);
			uiCharacterList.RegisterOnPlayCallback(OnPlay);
			uiCharacterList.RegisterOnDeleteCallback(OnDelete);

			uiCharacterList.RegisterOnToggleCallback(ToggleCreator);

			uiCharacterList.CreateUi(uiMain);
			uiNewCharacter.CreateUi(uiMain);
		}

		private void ToggleCreator()
		{
			if (uiNewCharacter.IsOpen())
			{
				uiNewCharacter.Close();
			}
			else
			{
				uiNewCharacter.Open();
			}
		}

		public override void SetDelay(fpDelay Delay)
		{
			this.Delay = Delay;
			uiCharacterList.SetDelay(Delay);
			uiNewCharacter.SetDelay(Delay);
		}

		public override async Task RefreshUi()
		{
			uiCharacterList.SetUi();
			uiNewCharacter.SetUi();
			await Delay(1);
		}
	}
}
