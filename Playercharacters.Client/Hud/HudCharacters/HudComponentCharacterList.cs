using System;
using System.Collections.Generic;
using Gaston11276.Playercharacters.Client.Models;
using Gaston11276.SimpleUi;
using fpGuid = Gaston11276.SimpleUi.UiPanel.fpGuid;

namespace Gaston11276.Playercharacters.Client
{
	public class HudComponentCharacterList : HudComponent
	{
		UiPanel uiColumnCharacterName = new UiPanel();

		private Textbox uiCharactersButtonPlay = new Textbox();
		private Textbox uiCharactersButtonDelete = new Textbox();

		public Guid selectedCharacterId;

		fpGuid OnPlayCallback;
		fpGuid OnDeleteCallback;
		fpVoid OnToggleNewCharacter;

		public HudComponentCharacterList()
		{ }


		public void RegisterOnPlayCallback(fpGuid OnPlayCallback)
		{
			this.OnPlayCallback = OnPlayCallback;
		}

		public void RegisterOnDeleteCallback(fpGuid OnDeleteCallback)
		{
			this.OnDeleteCallback = OnDeleteCallback;
		}

		public void RegisterOnLoadCharactersCallback(fpVoid OnLoad)
		{

		}

		public void RegisterOnToggleCallback(fpVoid OnToggleNewCharacter)
		{
			this.OnToggleNewCharacter = OnToggleNewCharacter;
		}

		public void OnCharacterSelected(Guid selectedCharacterId)
		{
			this.selectedCharacterId = selectedCharacterId;
		}

		public void UpdateCharacterList(List<Character> characters)
		{
			uiCharactersButtonPlay.Disable();
			uiCharactersButtonDelete.Disable();
			uiColumnCharacterName.Clear();
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
				inputsOnMouseButton.Add(characterEntry.OnMouseButton);
				uiColumnCharacterName.AddElement(characterEntry);
			}
			contentFrame.Refresh();
		}

		protected override void CreateColumns()
		{
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			uiCenterPanel.SetHDimension(Dimension.Max);
			contentFrame.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnCharacterName);
		}

		protected override void CreateContent()
		{
			uiHeader.SetText("Characters");
			uiAppearanceMain.SetHDimension(Dimension.Absolute);
			uiAppearanceMain.SetWidth(0.25f);

			UiPanel uiCharactersButtons = new UiPanel();
			uiCharactersButtons.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiCharactersButtons.SetPadding(new UiRectangle(0f));
			uiCharactersButtons.SetBackgroundColor(0, 0, 0, 0);
			contentFrame.AddElement(uiCharactersButtons);

			UiPanel uiCharactersButtonsLeft = new UiPanel();
			uiCharactersButtonsLeft.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonsLeft.SetHDimension(Dimension.Max);
			uiCharactersButtonsLeft.SetGravity(HGravity.Left);
			uiCharactersButtons.AddElement(uiCharactersButtonsLeft);

			UiPanel uiCharactersButtonsRight = new UiPanel();
			uiCharactersButtonsRight.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonsRight.SetHDimension(Dimension.Max);
			uiCharactersButtonsRight.SetGravity(HGravity.Right);
			uiCharactersButtons.AddElement(uiCharactersButtonsRight);

			uiCharactersButtonPlay = new Textbox();
			uiCharactersButtonPlay.SetText("Play");
			uiCharactersButtonPlay.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonPlay.SetProperties(UiElement.CANFOCUS);
			uiCharactersButtonPlay.RegisterOnDisableCallback(uiCharactersButtonPlay.OnDisabled);
			uiCharactersButtonPlay.RegisterOffDisableCallback(uiCharactersButtonPlay.OffDisabled);
			uiCharactersButtonPlay.SetFlags(UiElement.DISABLED);
			uiCharactersButtonPlay.RegisterOnLMBRelease(OnPlay);
			inputsOnMouseMove.Add(uiCharactersButtonPlay.OnCursorMove);
			inputsOnMouseButton.Add(uiCharactersButtonPlay.OnMouseButton);
			uiCharactersButtonsLeft.AddElement(uiCharactersButtonPlay);

			uiCharactersButtonDelete = new Textbox();
			uiCharactersButtonDelete.SetText("Delete");
			uiCharactersButtonDelete.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonDelete.SetMargin(new UiRectangle(-0.004f, 0f, 0f, 0f));
			uiCharactersButtonDelete.SetProperties(UiElement.CANFOCUS);
			uiCharactersButtonDelete.RegisterOnDisableCallback(uiCharactersButtonDelete.OnDisabled);
			uiCharactersButtonDelete.RegisterOffDisableCallback(uiCharactersButtonDelete.OffDisabled);
			uiCharactersButtonDelete.SetFlags(UiElement.DISABLED);
			uiCharactersButtonDelete.RegisterOnLMBRelease(OnDelete);
			inputsOnMouseMove.Add(uiCharactersButtonDelete.OnCursorMove);
			inputsOnMouseButton.Add(uiCharactersButtonDelete.OnMouseButton);
			uiCharactersButtonsLeft.AddElement(uiCharactersButtonDelete);

			Textbox uiCharactersButtonNew = new Textbox();
			uiCharactersButtonNew.SetText("New");
			uiCharactersButtonNew.SetPadding(new UiRectangle(defaultPadding));
			uiCharactersButtonNew.SetMargin(new UiRectangle(-0.004f, 0f, 0f, 0f));
			uiCharactersButtonNew.SetProperties(UiElement.CANFOCUS);
			uiCharactersButtonNew.RegisterOnLMBRelease(OnToggleNew);
			inputsOnMouseMove.Add(uiCharactersButtonNew.OnCursorMove);
			inputsOnMouseButton.Add(uiCharactersButtonNew.OnMouseButton);
			uiCharactersButtonsRight.AddElement(uiCharactersButtonNew);
		}

		private void OnPlay()
		{
			OnPlayCallback(selectedCharacterId);
		}

		private void OnDelete()
		{
			OnDeleteCallback(selectedCharacterId);
		}

		public void OnToggleNew()
		{
			OnToggleNewCharacter();
		}
	}
}
