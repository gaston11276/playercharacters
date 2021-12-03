using System.Collections.Generic;
using System.Threading.Tasks;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

using fpGuid = Gaston11276.SimpleUi.UiPanel.fpGuid;
namespace Gaston11276.Playercharacters.Client
{
	class HudCharacters : Hud
	{
		protected List<fpVoid> onCharacterListOpenCallbacks = new List<fpVoid>();
		protected List<fpGuid> onCharacterListCloseCallbacks = new List<fpGuid>();

		private List<Character> characters;

		private HudComponentCharacterList uiCharacterList = new HudComponentCharacterList();
		private HudComponentNewCharacter uiNewCharacter = new HudComponentNewCharacter();

		private fpVoid OnCreateCallback;
		private fpGuid OnDeleteCallback;
		private fpGuid OnPlayCallback;

		public HudCharacters()
		{
			uiCharacterList.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);
			uiNewCharacter.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);
		}

		public void RegisterOnCharacterListOpenCallback(fpVoid OnOpen)
		{
			onCharacterListOpenCallbacks.Add(OnOpen);
		}

		public void RegisterOnCharacterListCloseCallback(UiPanel.fpGuid OnClose)
		{
			onCharacterListCloseCallbacks.Add(OnClose);
		}

		public void RegisterOnPlayCallback(fpGuid OnPlay)
		{
			OnPlayCallback = OnPlay;
		}

		private void OnCharacterListOpen()
		{
			foreach (fpVoid onCharacterListOpen in onCharacterListOpenCallbacks)
			{
				onCharacterListOpen();
			}
		}

		private void OnCharacterListClose()
		{
			foreach (UiPanel.fpGuid onCharacterListClose in onCharacterListCloseCallbacks)
			{
				onCharacterListClose(uiCharacterList.selectedCharacterId);
			}
		}

		private void OnPlay(System.Guid selectedCharacterId)
		{
			Close();
			OnPlayCallback(selectedCharacterId);
		}

		public void SetCharacterInfo(Character character)
		{
			uiNewCharacter.SetCharacterInfo(character);
		}

		public void ClearCharacterListEdit()
		{
			uiNewCharacter.ClearCharacterListEdit();
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
			OnCharacterListOpen();
		}

		protected override void OnClose()
		{
			base.OnClose();
			OnCharacterListClose();
		}

		public override void OnInputKey(int state, int keycode)
		{
			if (IsOpen())
			{
				base.OnInputKey(state, keycode);

				//if (uiCharacterList.selectedCharacterId != null && uiCharacterList.selectedCharacterId.GetHashCode() != 0) // Force player to select a character
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

			uiCharacterList.RegisterOnToggleCallback(ToggleNewCharacterMenu);

			uiCharacterList.CreateUi(uiMain);
			uiNewCharacter.CreateUi(uiMain);
		}

		private void ToggleNewCharacterMenu()
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

		public override void InitUi()
		{
			base.InitUi();
			uiCharacterList.SetUi();
			uiNewCharacter.SetUi();
		}

		public override async Task RefreshUi()
		{
			uiCharacterList.SetUi();
			uiNewCharacter.SetUi();
			await Delay(1);
		}
	}
}
