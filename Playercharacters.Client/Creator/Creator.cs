using System.Collections.Generic;
using CitizenFX.Core;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	class Creator : Hud
	{
		public delegate void OnCreate();
		public delegate void OnSelect(System.Guid Id);
		public delegate void OnDelete(System.Guid Id);
		public delegate void OnPlay(System.Guid Id);

		public delegate void OnLoadCharacters();

		private System.Guid selectedCharacterId;
		private List<Character> characters;

		private UiPanel uiCharactersList;

		private UiPanel uiNewCharacterPanel;
		private Editbox uiEditForename;
		private Editbox uiEditMiddlename;
		private Editbox uiEditLastname;
		private Editbox uiEditGender;
		private Editbox uiEditDateOfBirth;

		private Textbox uiCreateCharacterButton;
		private Textbox uiCharactersButtonPlay;
		private Textbox uiCharactersButtonDelete;

		private OnCreate OnCreateCallback;
		private OnSelect OnSelectCallback;
		private OnDelete OnDeleteCallback;
		private OnPlay OnPlayCallback;
		private OnLoadCharacters OnLoadCharactersCallback;

		public Creator()
		{
			uiCharactersList = new UiPanel();

			uiNewCharacterPanel = new UiPanel();
			uiEditForename = new Editbox();
			uiEditMiddlename = new Editbox();
			uiEditLastname = new Editbox();
			uiEditGender = new Editbox();
			uiEditDateOfBirth = new Editbox();
			uiCreateCharacterButton = new Textbox();
			uiCharactersButtonPlay = new Textbox();
			uiCharactersButtonDelete = new Textbox();
		}

		public void GetCharacterInfo(ref Character character)
		{
			character.Forename = uiEditForename.GetText();
			character.Middlename = uiEditMiddlename.GetText();
			character.Surname = uiEditLastname.GetText();

			string genderText = uiEditGender.GetText();
			if (genderText.StartsWith("F"))
			{
				character.Gender = 1;
				character.Model = ((uint)PedHash.FreemodeFemale01).ToString();
			}
			else
			{
				character.Gender = 0;
				character.Model = ((uint)PedHash.FreemodeMale01).ToString();
			}

			character.AnimationSet = "move_m@drunk@verydrunk";

			System.DateTime birthdate = new System.DateTime(1977, 05, 20);
			character.DateOfBirth = birthdate;
		}

		public void ClearCreatorEdit()
		{
			uiEditForename.ClearText();
			uiEditMiddlename.ClearText();
			uiEditLastname.ClearText();
			uiEditGender.ClearText();
			uiEditDateOfBirth.ClearText();
		}

		public void SetCharacters(List<Character> characters)
		{
			this.characters = characters;
			UpdateCharacterList();
		}

		public void UpdateCharacterList()
		{
			uiCharactersButtonPlay.Disable();
			uiCharactersButtonDelete.Disable();
			uiCharactersList.Clear();
			foreach (Character character in characters)
			{
				Textbox characterEntry = new Textbox();
				characterEntry.Id = character.Id;
				characterEntry.SetText($"{character.Forename} {character.Middlename} {character.Surname}");
				characterEntry.SetPadding(new UiRectangle(defaultPadding));
				characterEntry.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
				characterEntry.RegisterOnSelectCallback(uiCharactersButtonPlay.Enable);
				characterEntry.RegisterOnSelectIdCallback(OnCharacterSelected);
				characterEntry.RegisterOffSelectCallback(uiCharactersButtonPlay.Disable);
				characterEntry.RegisterOnSelectCallback(uiCharactersButtonDelete.Enable);
				characterEntry.RegisterOffSelectCallback(uiCharactersButtonDelete.Disable);
				inputsOnMouseMove.Add(characterEntry.OnCursorMove);
				inputsOnLeftMouseButton.Add(characterEntry.OnLeftMouseButton);
				uiCharactersList.AddElement(characterEntry);
			}
			uiMain.FirstBuild();
		}

		public void RegisterLoadCharactersCallback(OnLoadCharacters OnLoadCharacters)
		{
			this.OnLoadCharactersCallback = OnLoadCharacters;
		}

		public void RegisterOnCreateCallback(OnCreate OnCreate)
		{
			this.OnCreateCallback = OnCreate;
		}

		public void RegisterOnSelectCallback(OnSelect OnSelect)
		{
			this.OnSelectCallback = OnSelect;
		}

		public void RegisterOnDeleteCallback(OnDelete OnDelete)
		{
			this.OnDeleteCallback = OnDelete;
		}

		public void RegisterOnPlayCallback(OnPlay OnPlay)
		{
			this.OnPlayCallback = OnPlay;
		}

		void LoadCharacters()
		{
			OnLoadCharactersCallback();
		}

		public void OnCharacterSelected(System.Guid selectedCharacterId)
		{
			this.selectedCharacterId = selectedCharacterId;
		}

		private async void OnPlay0()
		{
			OnPlayCallback(selectedCharacterId);
		}

		private void OnCreate0()
		{
			OnCreateCallback();
		}

		private void OnSelect0(System.Guid selectedCharacterId)
		{
			this.selectedCharacterId = selectedCharacterId;
			OnSelectCallback(selectedCharacterId);
		}

		private void OnDelete0()
		{
			OnDeleteCallback(selectedCharacterId);
		}

		private void OnTextChanged()
		{
			if (uiEditForename.GetText().Length > 0 && uiEditLastname.GetText().Length > 0)
			{
				uiCreateCharacterButton.ClearFlags(UiElement.DISABLED);
			}
			else
			{
				uiCreateCharacterButton.SetFlags(UiElement.DISABLED);
			}
		}

		public override void CreateUi()
		{
			base.CreateUi();
			
			////////////////////////////////////
			// New Character
			uiNewCharacterPanel.name = "NewCharacterPanel";
			uiNewCharacterPanel.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterPanel.SetOrientation(Orientation.Vertical);
			uiNewCharacterPanel.SetHDimension(Dimension.Absolute);
			uiNewCharacterPanel.SetFlags(UiElement.DISABLED);
			uiNewCharacterPanel.SetGravity(VGravity.Top);
			uiNewCharacterPanel.SetWidth(.4f);
			uiNewCharacterPanel.SetHeight(.4f);
			uiNewCharacterPanel.SetAlignment(HAlignment.Right);
			uiNewCharacterPanel.SetAlignment(VAlignment.Top);
			uiNewCharacterPanel.SetProperties(UiElement.FLOATING | UiElement.COLLISION_PARENT | UiElement.SELECTABLE | UiElement.MOVABLE | UiElement.RESIZEABLE);
			inputsOnMouseMove.Add(uiNewCharacterPanel.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiNewCharacterPanel.OnLeftMouseButton);
			uiMain.AddElement(uiNewCharacterPanel);

			UiPanel uiNewCharacterHeaderPanel = new UiPanel();
			uiNewCharacterHeaderPanel.SetHDimension(Dimension.Max);
			uiNewCharacterHeaderPanel.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterPanel.AddElement(uiNewCharacterHeaderPanel);

			Textbox headerNewCharacter = new Textbox();
			headerNewCharacter.SetText("New Character");
			headerNewCharacter.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterHeaderPanel.AddElement(headerNewCharacter);

			UiPanel uiNewCharacterEditPanel = new UiPanel();
			uiNewCharacterEditPanel.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiNewCharacterEditPanel.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterPanel.AddElement(uiNewCharacterEditPanel);

			UiPanel uiNewCharacterEditPanelLeft = new UiPanel();
			uiNewCharacterEditPanelLeft.SetOrientation(Orientation.Vertical);
			uiNewCharacterEditPanelLeft.SetGravity(HGravity.Left);
			uiNewCharacterEditPanelLeft.SetMargin(new UiRectangle(0f, 0f, 0.002f, 0f));
			uiNewCharacterEditPanelLeft.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanel.AddElement(uiNewCharacterEditPanelLeft);

			UiPanel uiNewCharacterEditPanelRight = new UiPanel();
			uiNewCharacterEditPanelRight.SetOrientation(Orientation.Vertical);
			uiNewCharacterEditPanelRight.SetHDimension(Dimension.Max);
			uiNewCharacterEditPanelRight.SetGravity(HGravity.Left);
			uiNewCharacterEditPanelRight.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelRight.SetMargin(new UiRectangle(-0.002f, 0f, 0f, 0f));
			uiNewCharacterEditPanel.AddElement(uiNewCharacterEditPanelRight);

			Textbox uiLabelForename = new Textbox();
			uiLabelForename.SetText("Forename");
			uiLabelForename.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelForename);
			Textbox uiLabelMiddlename = new Textbox();
			uiLabelMiddlename.SetText("Middle name");
			uiLabelMiddlename.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelMiddlename);
			Textbox uiLabelLastname = new Textbox();
			uiLabelLastname.SetText("Lastname");
			uiLabelLastname.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelLastname);
			Textbox uiLabelGender = new Textbox();
			uiLabelGender.SetText("Gender");
			uiLabelGender.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelGender);
			Textbox uiLabelAge = new Textbox();
			uiLabelAge.SetText("Date of birth");
			uiLabelAge.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelAge);

			uiEditForename = new Editbox();
			uiEditForename.SetText("");
			uiEditForename.SetPadding(new UiRectangle(defaultPadding));
			uiEditForename.SetHDimension(Dimension.Max);
			uiEditForename.SetGravity(HGravity.Left);
			uiEditForename.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditForename.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditForename.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiEditForename.OnLeftMouseButton);
			inputsOnKey.Add(uiEditForename.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditForename);

			uiEditMiddlename = new Editbox();
			uiEditMiddlename.SetText("");
			uiEditMiddlename.SetPadding(new UiRectangle(defaultPadding));
			uiEditMiddlename.SetHDimension(Dimension.Max);
			uiEditMiddlename.SetGravity(HGravity.Left);
			uiEditMiddlename.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditMiddlename.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditMiddlename.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiEditMiddlename.OnLeftMouseButton);
			inputsOnKey.Add(uiEditMiddlename.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditMiddlename);

			uiEditLastname = new Editbox();
			uiEditLastname.SetText("");
			uiEditLastname.SetPadding(new UiRectangle(defaultPadding));
			uiEditLastname.SetHDimension(Dimension.Max);
			uiEditLastname.SetGravity(HGravity.Left);
			uiEditLastname.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditLastname.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditLastname.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiEditLastname.OnLeftMouseButton);
			inputsOnKey.Add(uiEditLastname.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditLastname);

			uiEditGender = new Editbox();
			uiEditGender.SetText("");
			uiEditGender.SetPadding(new UiRectangle(defaultPadding));
			uiEditGender.SetHDimension(Dimension.Max);
			uiEditGender.SetGravity(HGravity.Left);
			uiEditGender.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditGender.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditGender.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiEditGender.OnLeftMouseButton);
			inputsOnKey.Add(uiEditGender.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditGender);

			uiEditDateOfBirth = new Editbox();
			uiEditDateOfBirth.SetText("1976-05-15");
			uiEditDateOfBirth.SetPadding(new UiRectangle(defaultPadding));
			uiEditDateOfBirth.SetHDimension(Dimension.Max);
			uiEditDateOfBirth.SetGravity(HGravity.Left);
			uiNewCharacterEditPanelRight.AddElement(uiEditDateOfBirth);

			UiPanel uiNewCharacterButtonsPanel = new UiPanel();
			uiNewCharacterButtonsPanel.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiNewCharacterButtonsPanel.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterButtonsPanel.SetHDimension(Dimension.Max);
			uiNewCharacterButtonsPanel.SetGravity(HGravity.Left);
			uiNewCharacterPanel.AddElement(uiNewCharacterButtonsPanel);

			uiCreateCharacterButton = new Textbox();
			uiCreateCharacterButton.SetText("Create");
			uiCreateCharacterButton.SetPadding(new UiRectangle(defaultPadding));
			uiCreateCharacterButton.SetFlags(UiElement.DISABLED);
			uiCreateCharacterButton.SetProperties(UiElement.CANFOCUS);
			uiCreateCharacterButton.RegisterOnDisableCallback(uiCreateCharacterButton.OnDisable);
			uiCreateCharacterButton.RegisterOffDisableCallback(uiCreateCharacterButton.OffDisable);
			uiCreateCharacterButton.RegisterOnLMBRelease(OnCreate0);
			inputsOnMouseMove.Add(uiCreateCharacterButton.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiCreateCharacterButton.OnLeftMouseButton);
			uiNewCharacterButtonsPanel.AddElement(uiCreateCharacterButton);



			////////////////////////////////////
			// Characters
			UiPanel uiCharactersPanel = new UiPanel();
			uiCharactersPanel.name = "CharactersPanel ";
			uiCharactersPanel.SetOrientation(Orientation.Vertical);
			uiCharactersPanel.SetHDimension(Dimension.Absolute);
			uiCharactersPanel.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersPanel.SetGravity(VGravity.Top);
			uiCharactersPanel.SetWidth(.4f);
			uiCharactersPanel.SetHeight(.4f);
			uiCharactersPanel.SetPosition(0.0f, 0.0f);
			uiCharactersPanel.SetProperties(UiElement.FLOATING | UiElement.COLLISION_PARENT | UiElement.MOVABLE | UiElement.RESIZEABLE);
			uiCharactersPanel.RegisterOnShowCallback(LoadCharacters);
			inputsOnMouseMove.Add(uiCharactersPanel.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiCharactersPanel.OnLeftMouseButton);
			uiMain.AddElement(uiCharactersPanel);

			UiPanel uiCharactersHeaderPanel = new UiPanel();
			uiCharactersHeaderPanel.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersHeaderPanel.SetHDimension(Dimension.Max);
			uiCharactersPanel.AddElement(uiCharactersHeaderPanel);
			Textbox uiCharactersHeader = new Textbox();
			uiCharactersHeader.SetText("Characters");
			uiCharactersHeader.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersHeaderPanel.AddElement(uiCharactersHeader);

			uiCharactersList = new UiPanel();
			uiCharactersList.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiCharactersList.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersList.SetHDimension(Dimension.Max);
			uiCharactersList.SetOrientation(Orientation.Vertical);
			uiCharactersPanel.AddElement(uiCharactersList);

			UiPanel uiCharactersButtons = new UiPanel();
			uiCharactersButtons.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiCharactersButtons.SetPadding(new UiRectangle(0f));
			uiCharactersButtons.SetBackgroundColor(0, 0, 0, 0);
			uiCharactersPanel.AddElement(uiCharactersButtons);

			UiPanel uiCharactersButtonsLeft = new UiPanel();
			uiCharactersButtonsLeft.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonsLeft.SetHDimension(Dimension.Max);
			uiCharactersButtonsLeft.SetGravity(HGravity.Left);
			uiCharactersButtons.AddElement(uiCharactersButtonsLeft);

			UiPanel uiCharactersButtonsRight= new UiPanel();
			uiCharactersButtonsRight.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonsRight.SetHDimension(Dimension.Max);
			uiCharactersButtonsRight.SetGravity(HGravity.Right);
			uiCharactersButtons.AddElement(uiCharactersButtonsRight);

			uiCharactersButtonPlay = new Textbox();
			uiCharactersButtonPlay.SetText("Play");
			uiCharactersButtonPlay.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonPlay.SetProperties(UiElement.CANFOCUS);
			uiCharactersButtonPlay.RegisterOnDisableCallback(uiCharactersButtonPlay.OnDisable);
			uiCharactersButtonPlay.RegisterOffDisableCallback(uiCharactersButtonPlay.OffDisable);
			uiCharactersButtonPlay.SetFlags(UiElement.DISABLED);
			uiCharactersButtonPlay.RegisterOnLMBRelease(OnPlay0);
			inputsOnMouseMove.Add(uiCharactersButtonPlay.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiCharactersButtonPlay.OnLeftMouseButton);
			uiCharactersButtonsLeft.AddElement(uiCharactersButtonPlay);

			uiCharactersButtonDelete = new Textbox();
			uiCharactersButtonDelete.SetText("Delete");
			uiCharactersButtonDelete.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonDelete.SetMargin(new UiRectangle(-0.004f, 0f, 0f, 0f));
			uiCharactersButtonDelete.SetProperties(UiElement.CANFOCUS);
			uiCharactersButtonDelete.RegisterOnDisableCallback(uiCharactersButtonDelete.OnDisable);
			uiCharactersButtonDelete.RegisterOffDisableCallback(uiCharactersButtonDelete.OffDisable);
			uiCharactersButtonDelete.SetFlags(UiElement.DISABLED);
			uiCharactersButtonDelete.RegisterOnLMBRelease(OnDelete0);
			inputsOnMouseMove.Add(uiCharactersButtonDelete.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiCharactersButtonDelete.OnLeftMouseButton);
			uiCharactersButtonsLeft.AddElement(uiCharactersButtonDelete);

			Textbox uiCharactersButtonNew = new Textbox();
			uiCharactersButtonNew.SetText("New");
			uiCharactersButtonNew.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonNew.SetMargin(new UiRectangle(-0.004f, 0f, 0f, 0f));
			uiCharactersButtonNew.SetProperties(UiElement.CANFOCUS);
			uiCharactersButtonNew.RegisterOnLMBRelease(ToggleCreator);
			inputsOnMouseMove.Add(uiCharactersButtonNew.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiCharactersButtonNew.OnLeftMouseButton);
			uiCharactersButtonsRight.AddElement(uiCharactersButtonNew);
		}

		private void ToggleCreator()
		{
			if ((uiNewCharacterPanel.GetFlags() & UiElementListbox.DISABLED) != 0)
			{
				uiNewCharacterPanel.ClearFlags(UiElementListbox.DISABLED);
			}
			else
			{
				uiNewCharacterPanel.SetFlags(UiElementListbox.DISABLED);
			}
		}
	}
}
