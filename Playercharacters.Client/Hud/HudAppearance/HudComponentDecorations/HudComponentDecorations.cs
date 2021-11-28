using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class HudComponentDecorations : HudComponent
	{
		public List<DecorationTattooZoneGroup> m_available_tattoos = new List<DecorationTattooZoneGroup>();

		UiPanel uiPage01 = new UiPanel();
		UiPanel uiPage02 = new UiPanel();
		UiPanel uiPage03 = new UiPanel();
		UiPanel uiPage04 = new UiPanel();
		UiPanel uiPage05 = new UiPanel();
		UiPanel uiPage06 = new UiPanel();
		UiPanel uiPage07 = new UiPanel();

		UiEntryDecoration ChestTop;
		UiEntryDecoration ChestTopLeft;
		UiEntryDecoration ChestTopRight;
		UiEntryDecoration ChestUpper;
		UiEntryDecoration ChestUpperLeft;
		UiEntryDecoration ChestUpperRight;
		UiEntryDecoration ChestMiddle;
		UiEntryDecoration ChestMiddleLeft;
		UiEntryDecoration ChestMiddleRight;
		UiEntryDecoration ChestLower;
		UiEntryDecoration ChestLowerLeft;
		UiEntryDecoration ChestLowerRight;
		UiEntryDecoration SideUpperLeft;
		UiEntryDecoration SideUpperRight;
		UiEntryDecoration SideMiddleLeft;
		UiEntryDecoration SideMiddleRight;
		UiEntryDecoration SideLowerLeft;
		UiEntryDecoration SideLowerRight;
		UiEntryDecoration NeckFront;
		UiEntryDecoration NeckBack;
		UiEntryDecoration NeckLeft;
		UiEntryDecoration NeckRight;
		UiEntryDecoration BackTop;
		UiEntryDecoration BackTopLeft;
		UiEntryDecoration BackTopRight;
		UiEntryDecoration BackUpper;
		UiEntryDecoration BackUpperLeft;
		UiEntryDecoration BackUpperRight;
		UiEntryDecoration BackMiddle;
		UiEntryDecoration BackMiddleLeft;
		UiEntryDecoration BackMiddleRight;
		UiEntryDecoration BackLower;
		UiEntryDecoration BackLowerLeft;
		UiEntryDecoration BackLowerRight;
		UiEntryDecoration ArmSideUpperLeft;
		UiEntryDecoration ArmSideUpperRight;
		UiEntryDecoration ArmSideMiddleLeft;
		UiEntryDecoration ArmSideMiddleRight;
		UiEntryDecoration ArmSideLowerLeft;
		UiEntryDecoration ArmSideLowerRight;
		UiEntryDecoration ArmFrontUpperLeft;
		UiEntryDecoration ArmFrontUpperRight;
		UiEntryDecoration ArmFrontMiddleLeft;
		UiEntryDecoration ArmFrontMiddleRight;
		UiEntryDecoration ArmFrontLowerLeft;
		UiEntryDecoration ArmFrontLowerRight;
		UiEntryDecoration ArmInsideUpperLeft;
		UiEntryDecoration ArmInsideUpperRight;
		UiEntryDecoration ArmInsideMiddleLeft;
		UiEntryDecoration ArmInsideMiddleRight;
		UiEntryDecoration ArmInsideLowerLeft;
		UiEntryDecoration ArmInsideLowerRight;
		UiEntryDecoration ArmBackUpperLeft;
		UiEntryDecoration ArmBackUpperRight;
		UiEntryDecoration ArmBackMiddleLeft;
		UiEntryDecoration ArmBackMiddleRight;
		UiEntryDecoration ArmBackLowerLeft;
		UiEntryDecoration ArmBackLowerRight;
		UiEntryDecoration LegSideUpperLeft;
		UiEntryDecoration LegSideUpperRight;
		UiEntryDecoration LegSideMiddleLeft;
		UiEntryDecoration LegSideMiddleRight;
		UiEntryDecoration LegSideLowerLeft;
		UiEntryDecoration LegSideLowerRight;
		UiEntryDecoration LegFrontUpperLeft;
		UiEntryDecoration LegFrontUpperRight;
		UiEntryDecoration LegFrontMiddleLeft;
		UiEntryDecoration LegFrontMiddleRight;
		UiEntryDecoration LegFrontLowerLeft;
		UiEntryDecoration LegFrontLowerRight;
		UiEntryDecoration LegBackUpperLeft;
		UiEntryDecoration LegBackUpperRight;
		UiEntryDecoration LegBackMiddleLeft;
		UiEntryDecoration LegBackMiddleRight;
		UiEntryDecoration LegBackLowerLeft;
		UiEntryDecoration LegBackLowerRight;
		UiEntryDecoration Face;
		UiEntryDecoration Unknown;

		public HudComponentDecorations()
		{
			cameraMode = CameraMode.Front;

			List<DecorationTattoo> tmp_list = new List<DecorationTattoo>();
			Tattoos.CreateTattoos(ref tmp_list);

			// Create groups
			for (int i = 0; i < (int)DecorationZone.NumberOfZones; i++)
			{
				m_available_tattoos.Add(new DecorationTattooZoneGroup());
			}

			// Copy tattoos from tmp list to their respective group
			for (int i = 0; i < tmp_list.Count; i++)
			{
				m_available_tattoos[(int)tmp_list[i].zone].tattoos.Add(tmp_list[i]);
			}
		}

		public override async void SetUi()
		{
			await ChestTop.SetUi();
			await ChestTopLeft.SetUi();
			await ChestTopRight.SetUi();
			await ChestUpper.SetUi();
			await ChestUpperLeft.SetUi();
			await ChestUpperRight.SetUi();
			await ChestMiddle.SetUi();
			await ChestMiddleLeft.SetUi();
			await ChestMiddleRight.SetUi();
			await ChestLower.SetUi();
			await ChestLowerLeft.SetUi();
			await ChestLowerRight.SetUi();
			await SideUpperLeft.SetUi();
			await SideUpperRight.SetUi();
			await SideMiddleLeft.SetUi();
			await SideMiddleRight.SetUi();
			await SideLowerLeft.SetUi();
			await SideLowerRight.SetUi();
			await NeckFront.SetUi();
			await NeckBack.SetUi();
			await NeckLeft.SetUi();
			await NeckRight.SetUi();
			await BackTop.SetUi();
			await BackTopLeft.SetUi();
			await BackTopRight.SetUi();
			await BackUpper.SetUi();
			await BackUpperLeft.SetUi();
			await BackUpperRight.SetUi();
			await BackMiddle.SetUi();
			await BackMiddleLeft.SetUi();
			await BackMiddleRight.SetUi();
			await BackLower.SetUi();
			await BackLowerLeft.SetUi();
			await BackLowerRight.SetUi();
			await ArmSideUpperLeft.SetUi();
			await ArmSideUpperRight.SetUi();
			await ArmSideMiddleLeft.SetUi();
			await ArmSideMiddleRight.SetUi();
			await ArmSideLowerLeft.SetUi();
			await ArmSideLowerRight.SetUi();
			await ArmFrontUpperLeft.SetUi();
			await ArmFrontUpperRight.SetUi();
			await ArmFrontMiddleLeft.SetUi();
			await ArmFrontMiddleRight.SetUi();
			await ArmFrontLowerLeft.SetUi();
			await ArmFrontLowerRight.SetUi();
			await ArmInsideUpperLeft.SetUi();
			await ArmInsideUpperRight.SetUi();
			await ArmInsideMiddleLeft.SetUi();
			await ArmInsideMiddleRight.SetUi();
			await ArmInsideLowerLeft.SetUi();
			await ArmInsideLowerRight.SetUi();
			await ArmBackUpperLeft.SetUi();
			await ArmBackUpperRight.SetUi();
			await ArmBackMiddleLeft.SetUi();
			await ArmBackMiddleRight.SetUi();
			await ArmBackLowerLeft.SetUi();
			await ArmBackLowerRight.SetUi();
			await LegSideUpperLeft.SetUi();
			await LegSideUpperRight.SetUi();
			await LegSideMiddleLeft.SetUi();
			await LegSideMiddleRight.SetUi();
			await LegSideLowerLeft.SetUi();
			await LegSideLowerRight.SetUi();
			await LegFrontUpperLeft.SetUi();
			await LegFrontUpperRight.SetUi();
			await LegFrontMiddleLeft.SetUi();
			await LegFrontMiddleRight.SetUi();
			await LegFrontLowerLeft.SetUi();
			await LegFrontLowerRight.SetUi();
			await LegBackUpperLeft.SetUi();
			await LegBackUpperRight.SetUi();
			await LegBackMiddleLeft.SetUi();
			await LegBackMiddleRight.SetUi();
			await LegBackLowerLeft.SetUi();
			await LegBackLowerRight.SetUi();
			await Face.SetUi();
			await Unknown.SetUi();
		}

		private UiEntryDecoration CreateTattooEntry(UiPanel page, DecorationZone zone, string label)
		{
			cameraMode = CameraMode.Face;
			float defaultPadding = 0.0025f;

			UiPanel columnZoneLabels = (UiPanel)page.GetElement(0);
			UiPanel columnName = (UiPanel)page.GetElement(1);
			UiPanel columnIndex = (UiPanel)page.GetElement(2);
			UiPanel columnDecrease = (UiPanel)page.GetElement(3);
			UiPanel columnIncrease = (UiPanel)page.GetElement(4);

			UiEntryDecoration entry = new UiEntryDecoration();
			entry.SetDelay(Delay);
			entry.zone = zone;

			entry.uiZoneLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiZoneLabel.SetText(label);
			entry.uiZoneLabel.SetFlags(UiElement.TRANSPARENT);
			columnZoneLabels.AddElement(entry.uiZoneLabel);

			entry.uiTattooName.SetPadding(new UiRectangle(defaultPadding));
			entry.uiTattooName.SetText("TattooName");
			entry.uiTattooName.SetFlags(UiElement.TRANSPARENT);
			columnName.AddElement(entry.uiTattooName);

			entry.uiTattooIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiTattooIndex.SetFlags(UiElement.TRANSPARENT);
			columnIndex.AddElement(entry.uiTattooIndex);

			entry.btnDecrease.SetText("-");
			entry.btnDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnDecrease.RegisterOnLMBRelease(entry.Decrease);
			inputsOnMouseMove.Add(entry.btnDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnDecrease.OnMouseButton);
			columnDecrease.AddElement(entry.btnDecrease);

			entry.btnIncrease.SetText("+");
			entry.btnIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIncrease.RegisterOnLMBRelease(entry.Increase);
			inputsOnMouseMove.Add(entry.btnIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIncrease.OnMouseButton);
			columnIncrease.AddElement(entry.btnIncrease);

			entry.SetIndex = SetTattooIndex;
			entry.GetIndex = GetTattooIndex;
			entry.GetIndexMax = GetNumberOfTattoos;
			entry.SetCamera = SetCamera;
			entry.GetName = GetName;

			return entry;
		}

		
		private void SetCamera(DecorationZone zone)
		{
			float distance = 0f;
			float height = 0f;
			float rotation = 0f;
			GetCameraPosition(zone, ref distance, ref height, ref rotation);
			this.camera.SetCamera(distance, height, rotation);
		}

		private void GetCameraPosition(DecorationZone zone, ref float distance, ref float height, ref float rotation)
		{
			switch (zone)
			{
				case DecorationZone.ChestTop:
					distance = 1.0f;
					height = 0.5f;
					rotation = 180.0f;
					break;
				case DecorationZone.ChestTopLeft:
					distance = 1.0f;
					height = 0.5f;
					rotation = -170.0f;
					break;
				case DecorationZone.ChestTopRight:
					distance = 1.0f;
					height = 0.5f;
					rotation = 170.0f;
					break;
				case DecorationZone.ChestUpper:
					distance = 1.0f;
					height = 0.4f;
					rotation = 180.0f;
					break;
				case DecorationZone.ChestUpperLeft:
					distance = 1.0f;
					height = 0.4f;
					rotation = -170.0f;
					break;
				case DecorationZone.ChestUpperRight:
					distance = 1.0f;
					height = 0.4f;
					rotation = 170.0f;
					break;
				case DecorationZone.ChestMiddle:
					distance = 1.0f;
					height = 0.3f;
					rotation = 180.0f;
					break;
				case DecorationZone.ChestMiddleLeft:
					distance = 1.0f;
					height = 0.3f;
					rotation = -170.0f;
					break;
				case DecorationZone.ChestMiddleRight:
					distance = 1.0f;
					height = 0.3f;
					rotation = 170.0f;
					break;
				case DecorationZone.ChestLower:
					distance = 1.0f;
					height = 0.2f;
					rotation = 180.0f;
					break;
				case DecorationZone.ChestLowerLeft:
					distance = 1.0f;
					height = 0.2f;
					rotation = -170.0f;
					break;
				case DecorationZone.ChestLowerRight:
					distance = 1.0f;
					height = 0.2f;
					rotation = 170.0f;
					break;
				case DecorationZone.SideUpperLeft:
					distance = 1.0f;
					height = 0.0f;
					rotation = -90.0f;
					break;
				case DecorationZone.SideUpperRight:
					distance = 1.0f;
					height = 0.0f;
					rotation = 90.0f;
					break;
				case DecorationZone.SideMiddleLeft:
					distance = 1.0f;
					height = 0.0f;
					rotation = -90.0f;
					break;

				case DecorationZone.SideMiddleRight:
					distance = 1.0f;
					height = 0.0f;
					rotation = 90.0f;
					break;

				case DecorationZone.SideLowerLeft:
					distance = 1.0f;
					height = 0.0f;
					rotation = -90.0f;
					break;

				case DecorationZone.SideLowerRight:
					distance = 1.0f;
					height = 0.0f;
					rotation = 90.0f;
					break;

				case DecorationZone.NeckFront:
					distance = 1.0f;
					height = 0.5f;
					rotation = 180.0f;
					break;

				case DecorationZone.NeckBack:
					distance = 1.0f;
					height = 0.6f;
					rotation = 0.0f;
					break;

				case DecorationZone.NeckLeft:
					distance = 1.0f;
					height = 0.5f;
					rotation = -90.0f;
					break;

				case DecorationZone.NeckRight:
					distance = 1.0f;
					height = 0.5f;
					rotation = 90.0f;
					break;

				case DecorationZone.BackTop:
					distance = 1.0f;
					height = 0.6f;
					rotation = 0.0f;
					break;

				case DecorationZone.BackTopLeft:
					distance = 1.0f;
					height = 0.6f;
					rotation = -10.0f;
					break;

				case DecorationZone.BackTopRight:
					distance = 1.0f;
					height = 0.6f;
					rotation = 10.0f;
					break;

				case DecorationZone.BackUpper:
					distance = 1.0f;
					height = 0.4f;
					rotation = 0.0f;
					break;
				case DecorationZone.BackUpperLeft:
					distance = 1.0f;
					height = 0.4f;
					rotation = -10.0f;
					break;
				case DecorationZone.BackUpperRight:
					distance = 1.0f;
					height = 0.4f;
					rotation = 10.0f;
					break;

				case DecorationZone.BackMiddle:
					distance = 1.0f;
					height = 0.3f;
					rotation = 0.0f;
					break;
				case DecorationZone.BackMiddleLeft:
					distance = 1.0f;
					height = 0.3f;
					rotation = -10.0f;
					break;
				case DecorationZone.BackMiddleRight:
					distance = 1.0f;
					height = 0.3f;
					rotation = 10.0f;
					break;

				case DecorationZone.BackLower:
					distance = 1.0f;
					height = 0.3f;
					rotation = 0.0f;
					break;
				case DecorationZone.BackLowerLeft:
					distance = 1.0f;
					height = 0.3f;
					rotation = -10.0f;
					break;
				case DecorationZone.BackLowerRight:
					distance = 1.0f;
					height = 0.3f;
					rotation = 10.0f;
					break;


				case DecorationZone.ArmSideUpperLeft:
					distance = 1.0f;
					height = 0.4f;
					rotation = -90.0f;
					break;

				case DecorationZone.ArmSideUpperRight:
					distance = 1.0f;
					height = 0.4f;
					rotation = 90.0f;
					break;

				case DecorationZone.ArmSideMiddleLeft:
					distance = 1.0f;
					height = 0.3f;
					rotation = -90.0f;
					break;

				case DecorationZone.ArmSideMiddleRight:
					distance = 1.0f;
					height = 0.3f;
					rotation = 90.0f;
					break;

				case DecorationZone.ArmSideLowerLeft:
					distance = 1.0f;
					height = 0.2f;
					rotation = -90.0f;
					break;
				case DecorationZone.ArmSideLowerRight:
					distance = 1.0f;
					height = 0.2f;
					rotation = 90.0f;
					break;


				case DecorationZone.ArmFrontUpperLeft:
					distance = 1.0f;
					height = 0.4f;
					rotation = -160.0f;
					break;

				case DecorationZone.ArmFrontUpperRight:
					distance = 1.0f;
					height = 0.4f;
					rotation = 160.0f;
					break;

				case DecorationZone.ArmFrontMiddleLeft:
					distance = 1.0f;
					height = 0.3f;
					rotation = -160.0f;
					break;

				case DecorationZone.ArmFrontMiddleRight:
					distance = 1.0f;
					height = 0.3f;
					rotation = 160.0f;
					break;

				case DecorationZone.ArmFrontLowerLeft:
					distance = 1.0f;
					height = 0.2f;
					rotation = -160.0f;
					break;
				case DecorationZone.ArmFrontLowerRight:
					distance = 1.0f;
					height = 0.2f;
					rotation = 160.0f;
					break;



				case DecorationZone.ArmBackUpperLeft:
					distance = 1.0f;
					height = 0.4f;
					rotation = -20.0f;
					break;

				case DecorationZone.ArmBackUpperRight:
					distance = 1.0f;
					height = 0.4f;
					rotation = 20.0f;
					break;

				case DecorationZone.ArmBackMiddleLeft:
					distance = 1.0f;
					height = 0.3f;
					rotation = -20.0f;
					break;

				case DecorationZone.ArmBackMiddleRight:
					distance = 1.0f;
					height = 0.3f;
					rotation = 20.0f;
					break;

				case DecorationZone.ArmBackLowerLeft:
					distance = 1.0f;
					height = 0.2f;
					rotation = -20.0f;
					break;
				case DecorationZone.ArmBackLowerRight:
					distance = 1.0f;
					height = 0.2f;
					rotation = 20.0f;
					break;

				case DecorationZone.ArmInsideUpperLeft:
					distance = 1.0f;
					height = 0.0f;
					rotation = 0.0f;
					break;

				case DecorationZone.ArmInsideUpperRight:
					distance = 1.0f;
					height = 0.0f;
					rotation = 0.0f;
					break;

				case DecorationZone.ArmInsideMiddleLeft:
					distance = 1.0f;
					height = 0.0f;
					rotation = 0.0f;
					break;

				case DecorationZone.ArmInsideMiddleRight:
					distance = 1.0f;
					height = 0.0f;
					rotation = 0.0f;
					break;

				case DecorationZone.ArmInsideLowerLeft:
					distance = 1.0f;
					height = 0.0f;
					rotation = 0.0f;
					break;
				case DecorationZone.ArmInsideLowerRight:
					distance = 1.0f;
					height = 0.0f;
					rotation = -90.0f;
					break;



				case DecorationZone.LegSideUpperLeft:
					distance = 1.0f;
					height = -0.1f;
					rotation = -90.0f;
					break;

				case DecorationZone.LegSideUpperRight:
					distance = 1.0f;
					height = -0.1f;
					rotation = 90.0f;
					break;
				case DecorationZone.LegSideMiddleLeft:
					distance = 1.0f;
					height = -0.2f;
					rotation = -90.0f;
					break;

				case DecorationZone.LegSideMiddleRight:
					distance = 1.0f;
					height = -0.2f;
					rotation = 90.0f;
					break;
				case DecorationZone.LegSideLowerLeft:
					distance = 1.0f;
					height = -0.3f;
					rotation = -90.0f;
					break;

				case DecorationZone.LegSideLowerRight:
					distance = 1.0f;
					height = -0.3f;
					rotation = 90.0f;
					break;

				case DecorationZone.LegFrontUpperLeft:
					distance = 1.0f;
					height = -0.1f;
					rotation = -160.0f;
					break;

				case DecorationZone.LegFrontUpperRight:
					distance = 1.0f;
					height = -0.1f;
					rotation = 160.0f;
					break;
				case DecorationZone.LegFrontMiddleLeft:
					distance = 1.0f;
					height = -0.2f;
					rotation = -160.0f;
					break;
				case DecorationZone.LegFrontMiddleRight:
					distance = 1.0f;
					height = -0.2f;
					rotation = 160.0f;
					break;
				case DecorationZone.LegFrontLowerLeft:
					distance = 1.0f;
					height = -0.3f;
					rotation = -160.0f;
					break;
				case DecorationZone.LegFrontLowerRight:
					distance = 1.0f;
					height = -0.3f;
					rotation = 160.0f;
					break;
				case DecorationZone.LegBackUpperLeft:
					distance = 1.0f;
					height = -0.1f;
					rotation = -20.0f;
					break;
				case DecorationZone.LegBackUpperRight:
					distance = 1.0f;
					height = -0.1f;
					rotation = 20.0f;
					break;
				case DecorationZone.LegBackMiddleLeft:
					distance = 1.0f;
					height = -0.2f;
					rotation = -20.0f;
					break;
				case DecorationZone.LegBackMiddleRight:
					distance = 1.0f;
					height = -0.2f;
					rotation = 20.0f;
					break;
				case DecorationZone.LegBackLowerLeft:
					distance = 1.0f;
					height = -0.3f;
					rotation = -20.0f;
					break;
				case DecorationZone.LegBackLowerRight:
					distance = 1.0f;
					height = -0.3f;
					rotation = 20.0f;
					break;
				//case DecorationZone.Face:
				//	distance = 0.5f;
				//	height = 0.6f;
				//	rotation = 180.0f;
				//	break;
				case DecorationZone.Unknown:
					distance = 2.0f;
					height = 0.0f;
					rotation = 180f;
					break;
				default:
					distance = 3f;
					height = 0.0f;
					rotation = 0.0f;
					break;
			}
		}

		public override void CreateColumns()
		{
			UiPanel uiPages = new UiPanel();
			uiPages.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiPages);
			uiPages.SetMoveFlags(UiElement.HIDDEN);

			// Buttons
			UiPanel uiPagesButtons = new UiPanel();
			uiPagesButtons.SetPadding(new UiRectangle(defaultPadding));
			uiPagesButtons.SetFlags(UiElement.TRANSPARENT);
			contentFrame.AddElement(uiPagesButtons);

			Textbox uiButtonPrev = new Textbox();
			uiButtonPrev.SetText("Prev");
			uiButtonPrev.SetPadding(new UiRectangle(defaultPadding));
			uiButtonPrev.SetProperties(UiElement.CANFOCUS);
			uiButtonPrev.RegisterOnLMBRelease(uiPages.MoveFlagsUp);
			uiPages.RegisterOnFirst(uiButtonPrev.Disable);

			inputsOnMouseMove.Add(uiButtonPrev.OnCursorMove);
			inputsOnMouseButton.Add(uiButtonPrev.OnMouseButton);
			uiPagesButtons.AddElement(uiButtonPrev);

			Textbox uiButtonNext= new Textbox();
			uiButtonNext.SetText("Next");
			uiButtonNext.SetPadding(new UiRectangle(defaultPadding));
			uiButtonNext.SetProperties(UiElement.CANFOCUS);
			uiButtonNext.RegisterOnLMBRelease(uiPages.MoveFlagsDown);
			uiButtonNext.RegisterOnLMBRelease(uiButtonPrev.Enable);
			uiPages.RegisterOnLast(uiButtonNext.Disable);
			uiButtonPrev.RegisterOnLMBRelease(uiButtonNext.Enable);
			inputsOnMouseMove.Add(uiButtonNext.OnCursorMove);
			inputsOnMouseButton.Add(uiButtonNext.OnMouseButton);
			uiPagesButtons.AddElement(uiButtonNext);
			
			uiPage01.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage01);
			uiPages.AddElement(uiPage01);

			uiPage02.SetFlags(UiElement.HIDDEN);
			uiPage02.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage02);
			uiPages.AddElement(uiPage02);

			uiPage03.SetFlags(UiElement.HIDDEN);
			uiPage03.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage03);
			uiPages.AddElement(uiPage03);

			uiPage04.SetFlags(UiElement.HIDDEN);
			uiPage04.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage04);
			uiPages.AddElement(uiPage04);

			uiPage05.SetFlags(UiElement.HIDDEN);
			uiPage05.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage05);
			uiPages.AddElement(uiPage05);

			uiPage06.SetFlags(UiElement.HIDDEN);
			uiPage06.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage06);
			uiPages.AddElement(uiPage06);

			uiPage07.SetFlags(UiElement.HIDDEN);
			uiPage07.SetPadding(new UiRectangle(defaultPadding));
			CreatePageColumns(uiPage07);
			uiPages.AddElement(uiPage07);
		}

		protected void CreatePageColumns(UiPanel uiPage)
		{
			CreateColumn(uiPage, HGravity.Left);
			CreateColumn(uiPage, HGravity.Left);
			CreateColumn(uiPage, HGravity.Right);
			CreateColumn(uiPage, HGravity.Center);
			CreateColumn(uiPage, HGravity.Center);
		}

		protected void CreateColumn(UiPanel uiPanel, HGravity gravity, string columnHeader = null)
		{
			UiPanel uiColumn = new UiPanel();
			uiColumn.SetOrientation(Orientation.Vertical);
			uiColumn.SetPadding(new UiRectangle(defaultPadding));
			uiColumn.SetGravity(gravity);
			uiColumn.SetFlags(UiElement.TRANSPARENT);
			uiPanel.AddElement(uiColumn);

			if (columnHeader != null)
			{
				Textbox header = new Textbox();
				header.SetPadding(new UiRectangle(defaultPadding));
				header.SetText(columnHeader);
				header.SetFlags(UiElement.TRANSPARENT);
				if (columnHeader.Length == 0)
				{
					header.SetTextFlags(UiElement.TRANSPARENT);
				}
				uiColumn.AddElement(header);
			}
		}

		public override void CreateContent()
		{
			uiHeader.SetText("Tattoos");
			ChestTop = CreateTattooEntry(uiPage01, DecorationZone.ChestTop,  "Chest Top");
			ChestTopLeft = CreateTattooEntry(uiPage01, DecorationZone.ChestTopLeft, "Chest Top Left");
			ChestTopRight = CreateTattooEntry(uiPage01, DecorationZone.ChestTopRight, "ChestTopRight");
			ChestUpper = CreateTattooEntry(uiPage01, DecorationZone.ChestUpper, "ChestUpper");
			ChestUpperLeft = CreateTattooEntry(uiPage01, DecorationZone.ChestUpperLeft, "ChestUpperLeft");
			ChestUpperRight = CreateTattooEntry(uiPage01, DecorationZone.ChestUpperRight, "ChestUpperRight");
			ChestMiddle = CreateTattooEntry(uiPage01, DecorationZone.ChestMiddle, "ChestMiddle");
			ChestMiddleLeft = CreateTattooEntry(uiPage01, DecorationZone.ChestMiddleLeft, "ChestMiddleLeft");
			ChestMiddleRight = CreateTattooEntry(uiPage01, DecorationZone.ChestMiddleRight, "ChestMiddleRight");
			ChestLower = CreateTattooEntry(uiPage01, DecorationZone.ChestLower, "ChestLower");
			ChestLowerLeft = CreateTattooEntry(uiPage01, DecorationZone.ChestLowerLeft, "ChestLowerLeft");
			ChestLowerRight = CreateTattooEntry(uiPage01, DecorationZone.ChestLowerRight, "ChestLowerRight");
			SideUpperLeft = CreateTattooEntry(uiPage02, DecorationZone.SideUpperLeft, "SideUpperLeft");
			SideUpperRight = CreateTattooEntry(uiPage02, DecorationZone.SideUpperRight, "SideUpperRight");
			SideMiddleLeft = CreateTattooEntry(uiPage02, DecorationZone.SideMiddleLeft, "SideMiddleLeft");
			SideMiddleRight = CreateTattooEntry(uiPage02, DecorationZone.SideMiddleRight, "SideMiddleRight");
			SideLowerLeft = CreateTattooEntry(uiPage02, DecorationZone.SideLowerLeft, "SideLowerLeft");
			SideLowerRight = CreateTattooEntry(uiPage02, DecorationZone.SideLowerRight, "SideLowerRight");
			NeckFront = CreateTattooEntry(uiPage03, DecorationZone.NeckFront, "NeckFront");
			NeckBack = CreateTattooEntry(uiPage03, DecorationZone.NeckBack, "NeckBack");
			NeckLeft = CreateTattooEntry(uiPage03, DecorationZone.NeckLeft, "NeckLeft");
			NeckRight = CreateTattooEntry(uiPage03, DecorationZone.NeckRight, "NeckRight");
			BackTop = CreateTattooEntry(uiPage04, DecorationZone.BackTop, "BackTop");
			BackTopLeft = CreateTattooEntry(uiPage04, DecorationZone.BackTopLeft, "BackTopLeft");
			BackTopRight = CreateTattooEntry(uiPage04, DecorationZone.BackTopRight, "BackTopRight");
			BackUpper = CreateTattooEntry(uiPage04, DecorationZone.BackUpper, "BackUpper");
			BackUpperLeft = CreateTattooEntry(uiPage04, DecorationZone.BackUpperLeft, "BackUpperLeft");
			BackUpperRight = CreateTattooEntry(uiPage04, DecorationZone.BackUpperRight, "BackUpperRight");
			BackMiddle = CreateTattooEntry(uiPage04, DecorationZone.BackMiddle, "BackMiddle");
			BackMiddleLeft = CreateTattooEntry(uiPage04, DecorationZone.BackMiddleLeft, "BackMiddleLeft");
			BackMiddleRight = CreateTattooEntry(uiPage04, DecorationZone.BackMiddleRight, "BackMiddleRight");
			BackLower = CreateTattooEntry(uiPage04, DecorationZone.BackLower, "BackLower");
			BackLowerLeft = CreateTattooEntry(uiPage04, DecorationZone.BackLowerLeft, "BackLowerLeft");
			BackLowerRight = CreateTattooEntry(uiPage04, DecorationZone.BackLowerRight, "BackLowerRight");
			ArmSideUpperLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmSideUpperLeft, "ArmSideUpperLeft");
			ArmSideUpperRight = CreateTattooEntry(uiPage05, DecorationZone.ArmSideUpperRight, "ArmSideUpperRight");
			ArmSideMiddleLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmSideMiddleLeft, "ArmSideMiddleLeft");
			ArmSideMiddleRight = CreateTattooEntry(uiPage05, DecorationZone.ArmSideMiddleRight, "ArmSideMiddleRight");
			ArmSideLowerLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmSideLowerLeft, "ArmSideLowerLeft");
			ArmSideLowerRight = CreateTattooEntry(uiPage05, DecorationZone.ArmSideLowerRight, "ArmSideLowerRight");
			ArmFrontUpperLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmFrontUpperLeft, "ArmFrontUpperLeft");
			ArmFrontUpperRight = CreateTattooEntry(uiPage05, DecorationZone.ArmFrontUpperRight, "ArmFrontUpperRight");
			ArmFrontMiddleLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmFrontMiddleLeft, "ArmFrontMiddleLeft");
			ArmFrontMiddleRight = CreateTattooEntry(uiPage05, DecorationZone.ArmFrontMiddleRight, "ArmFrontMiddleRight");
			ArmFrontLowerLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmFrontLowerLeft, "ArmFrontLowerLeft");
			ArmFrontLowerRight = CreateTattooEntry(uiPage05, DecorationZone.ArmFrontLowerRight, "ArmFrontLowerRight");
			ArmInsideUpperLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmInsideUpperLeft, "ArmInsideUpperLeft");
			ArmInsideUpperRight = CreateTattooEntry(uiPage05, DecorationZone.ArmInsideUpperRight, "ArmInsideUpperRight");
			ArmInsideMiddleLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmInsideMiddleLeft, "ArmInsideMiddleLeft");
			ArmInsideMiddleRight = CreateTattooEntry(uiPage05, DecorationZone.ArmInsideMiddleRight, "ArmInsideMiddleRight");
			ArmInsideLowerLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmInsideLowerLeft, "ArmInsideLowerLeft");
			ArmInsideLowerRight = CreateTattooEntry(uiPage05, DecorationZone.ArmInsideLowerRight, "ArmInsideLowerRight");
			ArmBackUpperLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmBackUpperLeft, "ArmBackUpperLeft");
			ArmBackUpperRight = CreateTattooEntry(uiPage05, DecorationZone.ArmBackUpperRight, "ArmBackUpperRight");
			ArmBackMiddleLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmBackMiddleLeft, "ArmBackMiddleLeft");
			ArmBackMiddleRight = CreateTattooEntry(uiPage05, DecorationZone.ArmBackMiddleRight, "ArmBackMiddleRight");
			ArmBackLowerLeft = CreateTattooEntry(uiPage05, DecorationZone.ArmBackLowerLeft, "ArmBackLowerLeft");
			ArmBackLowerRight = CreateTattooEntry(uiPage05, DecorationZone.ArmBackLowerRight, "ArmBackLowerRight");
			LegSideUpperLeft = CreateTattooEntry(uiPage06, DecorationZone.LegSideUpperLeft, "LegSideUpperLeft");
			LegSideUpperRight = CreateTattooEntry(uiPage06, DecorationZone.LegSideUpperRight, "LegSideUpperRight");
			LegSideMiddleLeft = CreateTattooEntry(uiPage06, DecorationZone.LegSideMiddleLeft, "LegSideMiddleLeft");
			LegSideMiddleRight = CreateTattooEntry(uiPage06, DecorationZone.LegSideMiddleRight, "LegSideMiddleRight");
			LegSideLowerLeft = CreateTattooEntry(uiPage06, DecorationZone.LegSideLowerLeft, "LegSideLowerLeft");
			LegSideLowerRight = CreateTattooEntry(uiPage06, DecorationZone.LegSideLowerRight, "LegSideLowerRight");
			LegFrontUpperLeft = CreateTattooEntry(uiPage06, DecorationZone.LegFrontUpperLeft, "LegFrontUpperLeft");
			LegFrontUpperRight = CreateTattooEntry(uiPage06, DecorationZone.LegFrontUpperRight, "LegFrontUpperRight");
			LegFrontMiddleLeft = CreateTattooEntry(uiPage06, DecorationZone.LegFrontMiddleLeft, "LegFrontMiddleLeft");
			LegFrontMiddleRight = CreateTattooEntry(uiPage06, DecorationZone.LegFrontMiddleRight, "LegFrontMiddleRight");
			LegFrontLowerLeft = CreateTattooEntry(uiPage06, DecorationZone.LegFrontLowerLeft, "LegFrontLowerLeft");
			LegFrontLowerRight = CreateTattooEntry(uiPage06, DecorationZone.LegFrontLowerRight, "LegFrontLowerRight");
			LegBackUpperLeft = CreateTattooEntry(uiPage06, DecorationZone.LegBackUpperLeft, "LegBackUpperLeft");
			LegBackUpperRight = CreateTattooEntry(uiPage06, DecorationZone.LegBackUpperRight, "LegBackUpperRight");
			LegBackMiddleLeft = CreateTattooEntry(uiPage06, DecorationZone.LegBackMiddleLeft, "LegBackMiddleLeft");
			LegBackMiddleRight = CreateTattooEntry(uiPage06, DecorationZone.LegBackMiddleRight, "LegBackMiddleRight");
			LegBackLowerLeft = CreateTattooEntry(uiPage06, DecorationZone.LegBackLowerLeft, "LegBackLowerLeft");
			LegBackLowerRight = CreateTattooEntry(uiPage06, DecorationZone.LegBackLowerRight, "LegBackLowerRight");
			Face = CreateTattooEntry(uiPage07, DecorationZone.Face, "Face");
			Unknown = CreateTattooEntry(uiPage07, DecorationZone.Unknown, "Unknown");

			CreateApplyCancelButtons();
		}

		private string GetName(DecorationZone zone, int index)
		{
			if (index < 0)
			{
				return "None";
			}

			if ((int)zone < m_available_tattoos.Count)
			{
				if (index < m_available_tattoos[(int)zone].tattoos.Count)
				{
					return m_available_tattoos[(int)zone].tattoos[index].overlay_str;
				}
			}
			return "Missing";
		}

		private int GetNumberOfTattoos(DecorationZone zone)
		{
			return m_available_tattoos[(int)zone].tattoos.Count;
		}

		private int GetTattooIndex(DecorationZone zone)
		{
			switch (zone)
			{
				case DecorationZone.ChestTop:
					return character.PedDecorations.ChestTop.Index;
				case DecorationZone.ChestTopLeft:
					return character.PedDecorations.ChestTopLeft.Index;
				case DecorationZone.ChestTopRight:
					return character.PedDecorations.ChestTopRight.Index;
				case DecorationZone.ChestUpper:
					return character.PedDecorations.ChestUpper.Index;
				case DecorationZone.ChestUpperLeft:
					return character.PedDecorations.ChestUpperLeft.Index;
				case DecorationZone.ChestUpperRight:
					return character.PedDecorations.ChestUpperRight.Index;
				case DecorationZone.ChestMiddle:
					return character.PedDecorations.ChestMiddle.Index;
				case DecorationZone.ChestMiddleLeft:
					return character.PedDecorations.ChestMiddleLeft.Index;
				case DecorationZone.ChestMiddleRight:
					return character.PedDecorations.ChestMiddleRight.Index;
				case DecorationZone.ChestLower:
					return character.PedDecorations.ChestLower.Index;
				case DecorationZone.ChestLowerLeft:
					return character.PedDecorations.ChestLowerLeft.Index;
				case DecorationZone.ChestLowerRight:
					return character.PedDecorations.ChestLowerRight.Index;
				case DecorationZone.SideUpperLeft:
					return character.PedDecorations.SideUpperLeft.Index;
				case DecorationZone.SideUpperRight:
					return character.PedDecorations.SideUpperRight.Index;
				case DecorationZone.SideMiddleLeft:
					return character.PedDecorations.SideMiddleLeft.Index;
				case DecorationZone.SideMiddleRight:
					return character.PedDecorations.SideMiddleRight.Index;
				case DecorationZone.SideLowerLeft:
					return character.PedDecorations.SideLowerLeft.Index;
				case DecorationZone.SideLowerRight:
					return character.PedDecorations.SideLowerRight.Index;
				case DecorationZone.NeckFront:
					return character.PedDecorations.NeckFront.Index;
				case DecorationZone.NeckBack:
					return character.PedDecorations.NeckBack.Index;
				case DecorationZone.NeckLeft:
					return character.PedDecorations.NeckLeft.Index;
				case DecorationZone.NeckRight:
					return character.PedDecorations.NeckRight.Index;
				case DecorationZone.BackTop:
					return character.PedDecorations.BackTop.Index;
				case DecorationZone.BackTopLeft:
					return character.PedDecorations.BackTopLeft.Index;
				case DecorationZone.BackTopRight:
					return character.PedDecorations.BackTopRight.Index;
				case DecorationZone.BackUpper:
					return character.PedDecorations.BackUpper.Index;
				case DecorationZone.BackUpperLeft:
					return character.PedDecorations.BackUpperLeft.Index;
				case DecorationZone.BackUpperRight:
					return character.PedDecorations.BackUpperRight.Index;
				case DecorationZone.BackMiddle:
					return character.PedDecorations.BackMiddle.Index;
				case DecorationZone.BackMiddleLeft:
					return character.PedDecorations.BackMiddleLeft.Index;
				case DecorationZone.BackMiddleRight:
					return character.PedDecorations.BackMiddleRight.Index;
				case DecorationZone.BackLower:
					return character.PedDecorations.BackLower.Index;
				case DecorationZone.BackLowerLeft:
					return character.PedDecorations.BackLowerLeft.Index;
				case DecorationZone.BackLowerRight:
					return character.PedDecorations.BackLowerRight.Index;
				case DecorationZone.ArmSideUpperLeft:
					return character.PedDecorations.ArmSideUpperLeft.Index;
				case DecorationZone.ArmSideUpperRight:
					return character.PedDecorations.ArmSideUpperRight.Index;
				case DecorationZone.ArmSideMiddleLeft:
					return character.PedDecorations.ArmSideMiddleLeft.Index;
				case DecorationZone.ArmSideMiddleRight:
					return character.PedDecorations.ArmSideMiddleRight.Index;
				case DecorationZone.ArmSideLowerLeft:
					return character.PedDecorations.ArmSideLowerLeft.Index;
				case DecorationZone.ArmSideLowerRight:
					return character.PedDecorations.ArmSideLowerRight.Index;
				case DecorationZone.ArmFrontUpperLeft:
					return character.PedDecorations.ArmFrontUpperLeft.Index;
				case DecorationZone.ArmFrontUpperRight:
					return character.PedDecorations.ArmFrontUpperRight.Index;
				case DecorationZone.ArmFrontMiddleLeft:
					return character.PedDecorations.ArmFrontMiddleLeft.Index;
				case DecorationZone.ArmFrontMiddleRight:
					return character.PedDecorations.ArmFrontMiddleRight.Index;
				case DecorationZone.ArmFrontLowerLeft:
					return character.PedDecorations.ArmFrontLowerLeft.Index;
				case DecorationZone.ArmFrontLowerRight:
					return character.PedDecorations.ArmFrontLowerRight.Index;
				case DecorationZone.ArmInsideUpperLeft:
					return character.PedDecorations.ArmInsideUpperLeft.Index;
				case DecorationZone.ArmInsideUpperRight:
					return character.PedDecorations.ArmInsideUpperRight.Index;
				case DecorationZone.ArmInsideMiddleLeft:
					return character.PedDecorations.ArmInsideMiddleLeft.Index;
				case DecorationZone.ArmInsideMiddleRight:
					return character.PedDecorations.ArmInsideMiddleRight.Index;
				case DecorationZone.ArmInsideLowerLeft:
					return character.PedDecorations.ArmInsideLowerLeft.Index;
				case DecorationZone.ArmInsideLowerRight:
					return character.PedDecorations.ArmInsideLowerRight.Index;
				case DecorationZone.ArmBackUpperLeft:
					return character.PedDecorations.ArmBackUpperLeft.Index;
				case DecorationZone.ArmBackUpperRight:
					return character.PedDecorations.ArmBackUpperRight.Index;
				case DecorationZone.ArmBackMiddleLeft:
					return character.PedDecorations.ArmBackMiddleLeft.Index;
				case DecorationZone.ArmBackMiddleRight:
					return character.PedDecorations.ArmBackMiddleRight.Index;
				case DecorationZone.ArmBackLowerLeft:
					return character.PedDecorations.ArmBackLowerLeft.Index;
				case DecorationZone.ArmBackLowerRight:
					return character.PedDecorations.ArmBackLowerRight.Index;
				case DecorationZone.LegSideUpperLeft:
					return character.PedDecorations.LegSideUpperLeft.Index;
				case DecorationZone.LegSideUpperRight:
					return character.PedDecorations.LegSideUpperRight.Index;
				case DecorationZone.LegSideMiddleLeft:
					return character.PedDecorations.LegSideMiddleLeft.Index;
				case DecorationZone.LegSideMiddleRight:
					return character.PedDecorations.LegSideMiddleRight.Index;
				case DecorationZone.LegSideLowerLeft:
					return character.PedDecorations.LegSideLowerLeft.Index;
				case DecorationZone.LegSideLowerRight:
					return character.PedDecorations.LegSideLowerRight.Index;
				case DecorationZone.LegFrontUpperLeft:
					return character.PedDecorations.LegFrontUpperLeft.Index;
				case DecorationZone.LegFrontUpperRight:
					return character.PedDecorations.LegFrontUpperRight.Index;
				case DecorationZone.LegFrontMiddleLeft:
					return character.PedDecorations.LegFrontMiddleLeft.Index;
				case DecorationZone.LegFrontMiddleRight:
					return character.PedDecorations.LegFrontMiddleRight.Index;
				case DecorationZone.LegFrontLowerLeft:
					return character.PedDecorations.LegFrontLowerLeft.Index;
				case DecorationZone.LegFrontLowerRight:
					return character.PedDecorations.LegFrontLowerRight.Index;
				case DecorationZone.LegBackUpperLeft:
					return character.PedDecorations.LegBackUpperLeft.Index;
				case DecorationZone.LegBackUpperRight:
					return character.PedDecorations.LegBackUpperRight.Index;
				case DecorationZone.LegBackMiddleLeft:
					return character.PedDecorations.LegBackMiddleLeft.Index;
				case DecorationZone.LegBackMiddleRight:
					return character.PedDecorations.LegBackMiddleRight.Index;
				case DecorationZone.LegBackLowerLeft:
					return character.PedDecorations.LegBackLowerLeft.Index;
				case DecorationZone.LegBackLowerRight:
					return character.PedDecorations.LegBackLowerRight.Index;
				case DecorationZone.Face:
					return character.PedDecorations.Face.Index;
				case DecorationZone.Unknown:
					return character.PedDecorations.Face.Index;

				default:
					return 0;
			}
		}

		private void SetTattooIndex(DecorationZone zone, int index)
		{
			switch (zone)
			{
				case DecorationZone.ChestTop:
					character.PedDecorations.ChestTop.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTop.Zone = zone;
						character.PedDecorations.ChestTop.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestTop.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestTop.Collection = 0;
						character.PedDecorations.ChestTop.Overlay = 0;
					}
					
					break;
				case DecorationZone.ChestTopLeft:
					character.PedDecorations.ChestTopLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestTopLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestTopLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestTopLeft.Collection = 0;
						character.PedDecorations.ChestTopLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ChestTopRight:
					character.PedDecorations.ChestTopRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestTopRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestTopRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestTopRight.Collection = 0;
						character.PedDecorations.ChestTopRight.Overlay = 0;
					}
					break;
				case DecorationZone.ChestUpper:
					character.PedDecorations.ChestUpper.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestUpper.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestUpper.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestUpper.Collection = 0;
						character.PedDecorations.ChestUpper.Overlay = 0;
					}
					break;
				case DecorationZone.ChestUpperLeft:
					character.PedDecorations.ChestUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestUpperLeft.Collection = 0;
						character.PedDecorations.ChestUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ChestUpperRight:
					character.PedDecorations.ChestUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestUpperRight.Collection = 0;
						character.PedDecorations.ChestUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.ChestMiddle:
					character.PedDecorations.ChestMiddle.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestMiddle.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestMiddle.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestMiddle.Collection = 0;
						character.PedDecorations.ChestMiddle.Overlay = 0;
					}
					break;
				case DecorationZone.ChestMiddleLeft:
					character.PedDecorations.ChestMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestMiddleLeft.Collection = 0;
						character.PedDecorations.ChestMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ChestMiddleRight:
					character.PedDecorations.ChestMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestMiddleRight.Collection = 0;
						character.PedDecorations.ChestMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.ChestLower:
					character.PedDecorations.ChestLower.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestLower.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestLower.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestLower.Collection = 0;
						character.PedDecorations.ChestLower.Overlay = 0;
					}
					break;
				case DecorationZone.ChestLowerLeft:
					character.PedDecorations.ChestLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestLowerLeft.Collection = 0;
						character.PedDecorations.ChestLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ChestLowerRight:
					character.PedDecorations.ChestLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ChestLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ChestLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ChestLowerRight.Collection = 0;
						character.PedDecorations.ChestLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.SideUpperLeft:
					character.PedDecorations.SideUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.SideUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.SideUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.SideUpperLeft.Collection = 0;
						character.PedDecorations.SideUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.SideUpperRight:
					character.PedDecorations.SideUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.SideUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.SideUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.SideUpperRight.Collection = 0;
						character.PedDecorations.SideUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.SideMiddleLeft:
					character.PedDecorations.SideMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.SideMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.SideMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.SideMiddleLeft.Collection = 0;
						character.PedDecorations.SideMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.SideMiddleRight:
					character.PedDecorations.SideMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.SideMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.SideMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.SideMiddleRight.Collection = 0;
						character.PedDecorations.SideMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.SideLowerLeft:
					character.PedDecorations.SideLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.SideLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.SideLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.SideLowerLeft.Collection = 0;
						character.PedDecorations.SideLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.SideLowerRight:
					character.PedDecorations.SideLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.SideLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.SideLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.SideLowerRight.Collection = 0;
						character.PedDecorations.SideLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.NeckFront:
					character.PedDecorations.NeckFront.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.NeckFront.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.NeckFront.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.NeckFront.Collection = 0;
						character.PedDecorations.NeckFront.Overlay = 0;
					}
					break;
				case DecorationZone.NeckBack:
					character.PedDecorations.NeckBack.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.NeckBack.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.NeckBack.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.NeckBack.Collection = 0;
						character.PedDecorations.NeckBack.Overlay = 0;
					}
					break;
				case DecorationZone.NeckLeft:
					character.PedDecorations.NeckLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.NeckLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.NeckLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.NeckLeft.Collection = 0;
						character.PedDecorations.NeckLeft.Overlay = 0;
					}
					break;
				case DecorationZone.NeckRight:
					character.PedDecorations.NeckRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.NeckRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.NeckRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.NeckRight.Collection = 0;
						character.PedDecorations.NeckRight.Overlay = 0;
					}
					break;
				case DecorationZone.BackTop:
					character.PedDecorations.BackTop.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackTop.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackTop.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackTop.Collection = 0;
						character.PedDecorations.BackTop.Overlay = 0;
					}
					break;
				case DecorationZone.BackTopLeft:
					character.PedDecorations.BackTopLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackTopLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackTopLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackTopLeft.Collection = 0;
						character.PedDecorations.BackTopLeft.Overlay = 0;
					}
					break;
				case DecorationZone.BackTopRight:
					character.PedDecorations.BackTopRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackTopRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackTopRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackTopRight.Collection = 0;
						character.PedDecorations.BackTopRight.Overlay = 0;
					}
					break;
				case DecorationZone.BackUpper:
					character.PedDecorations.BackUpper.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackUpper.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackUpper.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackUpper.Collection = 0;
						character.PedDecorations.BackUpper.Overlay = 0;
					}
					break;
				case DecorationZone.BackUpperLeft:
					character.PedDecorations.ChestTopLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackUpperLeft.Collection = 0;
						character.PedDecorations.BackUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.BackUpperRight:
					character.PedDecorations.BackUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackUpperRight.Collection = 0;
						character.PedDecorations.BackUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.BackMiddle:
					character.PedDecorations.BackMiddle.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackMiddle.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackMiddle.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackMiddle.Collection = 0;
						character.PedDecorations.BackMiddle.Overlay = 0;
					}
					break;
				case DecorationZone.BackMiddleLeft:
					character.PedDecorations.BackMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackMiddleLeft.Collection = 0;
						character.PedDecorations.BackMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.BackMiddleRight:
					character.PedDecorations.BackMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackMiddleRight.Collection = 0;
						character.PedDecorations.BackMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.BackLower:
					character.PedDecorations.BackLower.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackLower.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackLower.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackLower.Collection = 0;
						character.PedDecorations.BackLower.Overlay = 0;
					}
					break;
				case DecorationZone.BackLowerLeft:
					character.PedDecorations.BackLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackLowerLeft.Collection = 0;
						character.PedDecorations.BackLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.BackLowerRight:
					character.PedDecorations.BackLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.BackLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.BackLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.BackLowerRight.Collection = 0;
						character.PedDecorations.BackLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmSideUpperLeft:
					character.PedDecorations.ArmSideUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmSideUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmSideUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmSideUpperLeft.Collection = 0;
						character.PedDecorations.ArmSideUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmSideUpperRight:
					character.PedDecorations.ArmSideUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmSideUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmSideUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmSideUpperRight.Collection = 0;
						character.PedDecorations.ArmSideUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmSideMiddleLeft:
					character.PedDecorations.ArmSideMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmSideMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmSideMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmSideMiddleLeft.Collection = 0;
						character.PedDecorations.ArmSideMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmSideMiddleRight:
					character.PedDecorations.ArmSideMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmSideMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmSideMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmSideMiddleRight.Collection = 0;
						character.PedDecorations.ArmSideMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmSideLowerLeft:
					character.PedDecorations.ArmSideLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmSideLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmSideLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmSideLowerLeft.Collection = 0;
						character.PedDecorations.ArmSideLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmSideLowerRight:
					character.PedDecorations.ArmSideLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmSideLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmSideLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmSideLowerRight.Collection = 0;
						character.PedDecorations.ArmSideLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmFrontUpperLeft:
					character.PedDecorations.ArmFrontUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmFrontUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmFrontUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmFrontUpperLeft.Collection = 0;
						character.PedDecorations.ArmFrontUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmFrontUpperRight:
					character.PedDecorations.ArmFrontUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmFrontUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmFrontUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmFrontUpperRight.Collection = 0;
						character.PedDecorations.ArmFrontUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmFrontMiddleLeft:
					character.PedDecorations.ArmFrontMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmFrontMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmFrontMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmFrontMiddleLeft.Collection = 0;
						character.PedDecorations.ArmFrontMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmFrontMiddleRight:
					character.PedDecorations.ArmFrontMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmFrontMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmFrontMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmFrontMiddleRight.Collection = 0;
						character.PedDecorations.ArmFrontMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmFrontLowerLeft:
					character.PedDecorations.ArmFrontLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmFrontLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmFrontLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmFrontLowerLeft.Collection = 0;
						character.PedDecorations.ArmFrontLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmFrontLowerRight:
					character.PedDecorations.ArmFrontLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmFrontLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmFrontLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmFrontLowerRight.Collection = 0;
						character.PedDecorations.ArmFrontLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmInsideUpperLeft:
					character.PedDecorations.ArmInsideUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmInsideUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmInsideUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmInsideUpperLeft.Collection = 0;
						character.PedDecorations.ArmInsideUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmInsideUpperRight:
					character.PedDecorations.ArmInsideUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmInsideUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmInsideUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmInsideUpperRight.Collection = 0;
						character.PedDecorations.ArmInsideUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmInsideMiddleLeft:
					character.PedDecorations.ArmInsideMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmInsideMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmInsideMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmInsideMiddleLeft.Collection = 0;
						character.PedDecorations.ArmInsideMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmInsideMiddleRight:
					character.PedDecorations.ArmInsideMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmInsideMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmInsideMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmInsideMiddleRight.Collection = 0;
						character.PedDecorations.ArmInsideMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmInsideLowerLeft:
					character.PedDecorations.ArmInsideLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmInsideLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmInsideLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmInsideLowerLeft.Collection = 0;
						character.PedDecorations.ArmInsideLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmInsideLowerRight:
					character.PedDecorations.ArmInsideLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmInsideLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmInsideLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmInsideLowerRight.Collection = 0;
						character.PedDecorations.ArmInsideLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmBackUpperLeft:
					character.PedDecorations.ArmBackUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmBackUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmBackUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmBackUpperLeft.Collection = 0;
						character.PedDecorations.ArmBackUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmBackUpperRight:
					character.PedDecorations.ArmBackUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmBackUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmBackUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmBackUpperRight.Collection = 0;
						character.PedDecorations.ArmBackUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmBackMiddleLeft:
					character.PedDecorations.ArmBackMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmBackMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmBackMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmBackMiddleLeft.Collection = 0;
						character.PedDecorations.ArmBackMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmBackMiddleRight:
					character.PedDecorations.ArmBackMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmBackMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmBackMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmBackMiddleRight.Collection = 0;
						character.PedDecorations.ArmBackMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.ArmBackLowerLeft:
					character.PedDecorations.ArmBackLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmBackLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmBackLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmBackLowerLeft.Collection = 0;
						character.PedDecorations.ArmBackLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.ArmBackLowerRight:
					character.PedDecorations.ArmBackLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.ArmBackLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.ArmBackLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.ArmBackLowerRight.Collection = 0;
						character.PedDecorations.ArmBackLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegSideUpperLeft:
					character.PedDecorations.LegSideUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegSideUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegSideUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegSideUpperLeft.Collection = 0;
						character.PedDecorations.LegSideUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegSideUpperRight:
					character.PedDecorations.LegSideUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegSideUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegSideUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegSideUpperRight.Collection = 0;
						character.PedDecorations.LegSideUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegSideMiddleLeft:
					character.PedDecorations.LegSideMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegSideMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegSideMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegSideMiddleLeft.Collection = 0;
						character.PedDecorations.LegSideMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegSideMiddleRight:
					character.PedDecorations.LegSideMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegSideMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegSideMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegSideMiddleRight.Collection = 0;
						character.PedDecorations.LegSideMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegSideLowerLeft:
					character.PedDecorations.LegSideLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegSideLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegSideLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegSideLowerLeft.Collection = 0;
						character.PedDecorations.LegSideLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegSideLowerRight:
					character.PedDecorations.LegSideLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegSideLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegSideLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegSideLowerRight.Collection = 0;
						character.PedDecorations.LegSideLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegFrontUpperLeft:
					character.PedDecorations.LegFrontUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegFrontUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegFrontUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegFrontUpperLeft.Collection = 0;
						character.PedDecorations.LegFrontUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegFrontUpperRight:
					character.PedDecorations.LegFrontUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegFrontUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegFrontUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegFrontUpperRight.Collection = 0;
						character.PedDecorations.LegFrontUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegFrontMiddleLeft:
					character.PedDecorations.LegFrontMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegFrontMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegFrontMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegFrontMiddleLeft.Collection = 0;
						character.PedDecorations.LegFrontMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegFrontMiddleRight:
					character.PedDecorations.LegFrontMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegFrontMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegFrontMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegFrontMiddleRight.Collection = 0;
						character.PedDecorations.LegFrontMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegFrontLowerLeft:
					character.PedDecorations.LegFrontLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegFrontLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegFrontLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegFrontLowerLeft.Collection = 0;
						character.PedDecorations.LegFrontLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegFrontLowerRight:
					character.PedDecorations.LegFrontLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegFrontLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegFrontLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegFrontLowerRight.Collection = 0;
						character.PedDecorations.LegFrontLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegBackUpperLeft:
					character.PedDecorations.LegBackUpperLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegBackUpperLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegBackUpperLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegBackUpperLeft.Collection = 0;
						character.PedDecorations.LegBackUpperLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegBackUpperRight:
					character.PedDecorations.LegBackUpperRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegBackUpperRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegBackUpperRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegBackUpperRight.Collection = 0;
						character.PedDecorations.LegBackUpperRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegBackMiddleLeft:
					character.PedDecorations.LegBackMiddleLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegBackMiddleLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegBackMiddleLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegBackMiddleLeft.Collection = 0;
						character.PedDecorations.LegBackMiddleLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegBackMiddleRight:
					character.PedDecorations.LegBackMiddleRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegBackMiddleRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegBackMiddleRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegBackMiddleRight.Collection = 0;
						character.PedDecorations.LegBackMiddleRight.Overlay = 0;
					}
					break;
				case DecorationZone.LegBackLowerLeft:
					character.PedDecorations.LegBackLowerLeft.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegBackLowerLeft.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegBackLowerLeft.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegBackLowerLeft.Collection = 0;
						character.PedDecorations.LegBackLowerLeft.Overlay = 0;
					}
					break;
				case DecorationZone.LegBackLowerRight:
					character.PedDecorations.LegBackLowerRight.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.LegBackLowerRight.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.LegBackLowerRight.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.LegBackLowerRight.Collection = 0;
						character.PedDecorations.LegBackLowerRight.Overlay = 0;
					}
					break;
				case DecorationZone.Face:
				case DecorationZone.Unknown:
					character.PedDecorations.Face.Index = index;
					if (index >= 0)
					{
						//character.PedDecorations.ChestTopLeft.Zone = zone;
						character.PedDecorations.Face.Collection = (int)m_available_tattoos[(int)zone].tattoos[index].collection_hash;
						character.PedDecorations.Face.Overlay = (int)m_available_tattoos[(int)zone].tattoos[index].overlay_hash;
					}
					else
					{
						character.PedDecorations.Face.Collection = 0;
						character.PedDecorations.Face.Overlay = 0;
					}
					break;
				default:
					break;
			}
			ApplyToPed();
			
		}

		public void ApplyToPed()
		{
			ClearPedDecorations(Game.PlayerPed.Handle);
			ApplyPedDecorations();
		}

		public void ApplyPedDecorations()
		{
			for (int i = 0; i < (int)DecorationZone.NumberOfZones; i++)
			{
				ApplyZone((DecorationZone)i);
			}
		}

		private void ApplyZone(DecorationZone zone)
		{
			switch (zone)
			{
				case DecorationZone.ChestTop:
					if (character.PedDecorations.ChestTop.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestTop.Collection, (uint)character.PedDecorations.ChestTop.Overlay);
					}	
					break;
				case DecorationZone.ChestTopLeft:
					if (character.PedDecorations.ChestTopLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestTopLeft.Collection, (uint)character.PedDecorations.ChestTopLeft.Overlay);
					}
					break;
				case DecorationZone.ChestTopRight:
					if (character.PedDecorations.ChestTopRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestTopRight.Collection, (uint)character.PedDecorations.ChestTopRight.Overlay);
					}
					break;
				case DecorationZone.ChestUpper:
					if (character.PedDecorations.ChestUpper.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestUpper.Collection, (uint)character.PedDecorations.ChestUpper.Overlay);
					}
					break;
				case DecorationZone.ChestUpperLeft:
					if (character.PedDecorations.ChestUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestUpperLeft.Collection, (uint)character.PedDecorations.ChestUpperLeft.Overlay);
					}
					break;
				case DecorationZone.ChestUpperRight:
					if (character.PedDecorations.ChestUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestUpperRight.Collection, (uint)character.PedDecorations.ChestUpperRight.Overlay);
					}
					break;
				case DecorationZone.ChestMiddle:
					if (character.PedDecorations.ChestMiddle.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestMiddle.Collection, (uint)character.PedDecorations.ChestMiddle.Overlay);
					}
					break;
				case DecorationZone.ChestMiddleLeft:
					if (character.PedDecorations.ChestMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestMiddleLeft.Collection, (uint)character.PedDecorations.ChestMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.ChestMiddleRight:
					if (character.PedDecorations.ChestMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestMiddleRight.Collection, (uint)character.PedDecorations.ChestMiddleRight.Overlay);
					}
					break;
				case DecorationZone.ChestLower:
					if (character.PedDecorations.ChestLower.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestLower.Collection, (uint)character.PedDecorations.ChestLower.Overlay);
					}
					break;
				case DecorationZone.ChestLowerLeft:
					if (character.PedDecorations.ChestLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestLowerLeft.Collection, (uint)character.PedDecorations.ChestLowerLeft.Overlay);
					}
					break;
				case DecorationZone.ChestLowerRight:
					if (character.PedDecorations.ChestLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ChestLowerRight.Collection, (uint)character.PedDecorations.ChestLowerRight.Overlay);
					}
					break;
				case DecorationZone.SideUpperLeft:
					if (character.PedDecorations.SideUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.SideUpperLeft.Collection, (uint)character.PedDecorations.SideUpperLeft.Overlay);
					}
					break;
				case DecorationZone.SideUpperRight:
					if (character.PedDecorations.SideUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.SideUpperRight.Collection, (uint)character.PedDecorations.SideUpperRight.Overlay);
					}
					break;
				case DecorationZone.SideMiddleLeft:
					if (character.PedDecorations.SideMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.SideMiddleLeft.Collection, (uint)character.PedDecorations.SideMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.SideMiddleRight:
					if (character.PedDecorations.SideMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.SideMiddleRight.Collection, (uint)character.PedDecorations.SideMiddleRight.Overlay);
					}
					break;
				case DecorationZone.SideLowerLeft:
					if (character.PedDecorations.SideLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.SideLowerLeft.Collection, (uint)character.PedDecorations.SideLowerLeft.Overlay);
					}
					break;
				case DecorationZone.SideLowerRight:
					if (character.PedDecorations.SideLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.SideLowerRight.Collection, (uint)character.PedDecorations.SideLowerRight.Overlay);
					}
					break;
				case DecorationZone.NeckFront:
					if (character.PedDecorations.NeckFront.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.NeckFront.Collection, (uint)character.PedDecorations.NeckFront.Overlay);
					}
					break;
				case DecorationZone.NeckBack:
					if (character.PedDecorations.NeckBack.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.NeckBack.Collection, (uint)character.PedDecorations.NeckBack.Overlay);
					}
					break;
				case DecorationZone.NeckLeft:
					if (character.PedDecorations.NeckLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.NeckLeft.Collection, (uint)character.PedDecorations.NeckLeft.Overlay);
					}
					break;
				case DecorationZone.NeckRight:
					if (character.PedDecorations.NeckRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.NeckRight.Collection, (uint)character.PedDecorations.NeckRight.Overlay);
					}
					break;
				case DecorationZone.BackTop:
					if (character.PedDecorations.BackTop.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackTop.Collection, (uint)character.PedDecorations.BackTop.Overlay);
					}
					break;
				case DecorationZone.BackTopLeft:
					if (character.PedDecorations.BackTopLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackTopLeft.Collection, (uint)character.PedDecorations.BackTopLeft.Overlay);
					}
					break;
				case DecorationZone.BackTopRight:
					if (character.PedDecorations.BackTopRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackTopRight.Collection, (uint)character.PedDecorations.BackTopRight.Overlay);
					}
					break;
				case DecorationZone.BackUpper:
					if (character.PedDecorations.BackUpper.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackUpper.Collection, (uint)character.PedDecorations.BackUpper.Overlay);
					}
					break;
				case DecorationZone.BackUpperLeft:
					if (character.PedDecorations.BackUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackUpperLeft.Collection, (uint)character.PedDecorations.BackUpperLeft.Overlay);
					}
					break;
				case DecorationZone.BackUpperRight:
					if (character.PedDecorations.BackUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackUpperRight.Collection, (uint)character.PedDecorations.BackUpperRight.Overlay);
					}
					break;
				case DecorationZone.BackMiddle:
					if (character.PedDecorations.BackMiddle.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackMiddle.Collection, (uint)character.PedDecorations.BackMiddle.Overlay);
					}
					break;
				case DecorationZone.BackMiddleLeft:
					if (character.PedDecorations.BackMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackMiddleLeft.Collection, (uint)character.PedDecorations.BackMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.BackMiddleRight:
					if (character.PedDecorations.BackMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackMiddleRight.Collection, (uint)character.PedDecorations.BackMiddleRight.Overlay);
					}
					break;
				case DecorationZone.BackLower:
					if (character.PedDecorations.BackLower.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackLower.Collection, (uint)character.PedDecorations.BackLower.Overlay);
					}
					break;
				case DecorationZone.BackLowerLeft:
					if (character.PedDecorations.BackLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackLowerLeft.Collection, (uint)character.PedDecorations.BackLowerLeft.Overlay);
					}
					break;
				case DecorationZone.BackLowerRight:
					if (character.PedDecorations.BackLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.BackLowerRight.Collection, (uint)character.PedDecorations.BackLowerRight.Overlay);
					}
					break;
				case DecorationZone.ArmSideUpperLeft:
					if (character.PedDecorations.ArmSideUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmSideUpperLeft.Collection, (uint)character.PedDecorations.ArmSideUpperLeft.Overlay);
					}
					break;
				case DecorationZone.ArmSideUpperRight:
					if (character.PedDecorations.ArmSideUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmSideUpperRight.Collection, (uint)character.PedDecorations.ArmSideUpperRight.Overlay);
					}
					break;
				case DecorationZone.ArmSideMiddleLeft:
					if (character.PedDecorations.ArmSideMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmSideMiddleLeft.Collection, (uint)character.PedDecorations.ArmSideMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.ArmSideMiddleRight:
					if (character.PedDecorations.ArmSideMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmSideMiddleRight.Collection, (uint)character.PedDecorations.ArmSideMiddleRight.Overlay);
					}
					break;
				case DecorationZone.ArmSideLowerLeft:
					if (character.PedDecorations.ArmSideLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmSideLowerLeft.Collection, (uint)character.PedDecorations.ArmSideLowerLeft.Overlay);
					}
					break;
				case DecorationZone.ArmSideLowerRight:
					if (character.PedDecorations.ArmSideLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmSideLowerRight.Collection, (uint)character.PedDecorations.ArmSideLowerRight.Overlay);
					}
					break;
				case DecorationZone.ArmFrontUpperLeft:
					if (character.PedDecorations.ArmFrontUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmFrontUpperLeft.Collection, (uint)character.PedDecorations.ArmFrontUpperLeft.Overlay);
					}
					break;
				case DecorationZone.ArmFrontUpperRight:
					if (character.PedDecorations.ArmFrontUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmFrontUpperRight.Collection, (uint)character.PedDecorations.ArmFrontUpperRight.Overlay);
					}
					break;
				case DecorationZone.ArmFrontMiddleLeft:
					if (character.PedDecorations.ArmFrontMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmFrontMiddleLeft.Collection, (uint)character.PedDecorations.ArmFrontMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.ArmFrontMiddleRight:
					if (character.PedDecorations.ArmFrontMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmFrontMiddleRight.Collection, (uint)character.PedDecorations.ArmFrontMiddleRight.Overlay);
					}
					break;
				case DecorationZone.ArmFrontLowerLeft:
					if (character.PedDecorations.ArmFrontLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmFrontLowerLeft.Collection, (uint)character.PedDecorations.ArmFrontLowerLeft.Overlay);
					}
					break;
				case DecorationZone.ArmFrontLowerRight:
					if (character.PedDecorations.ArmFrontLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmFrontLowerRight.Collection, (uint)character.PedDecorations.ArmFrontLowerRight.Overlay);
					}
					break;
				case DecorationZone.ArmInsideUpperLeft:
					if (character.PedDecorations.ArmInsideUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmInsideUpperLeft.Collection, (uint)character.PedDecorations.ArmInsideUpperLeft.Overlay);
					}
					break;
				case DecorationZone.ArmInsideUpperRight:
					if (character.PedDecorations.ArmInsideUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmInsideUpperRight.Collection, (uint)character.PedDecorations.ArmInsideUpperRight.Overlay);
					}
					break;
				case DecorationZone.ArmInsideMiddleLeft:
					if (character.PedDecorations.ArmInsideMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmInsideMiddleLeft.Collection, (uint)character.PedDecorations.ArmInsideMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.ArmInsideMiddleRight:
					if (character.PedDecorations.ArmInsideMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmInsideMiddleRight.Collection, (uint)character.PedDecorations.ArmInsideMiddleRight.Overlay);
					}
					break;
				case DecorationZone.ArmInsideLowerLeft:
					if (character.PedDecorations.ArmInsideLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmInsideLowerLeft.Collection, (uint)character.PedDecorations.ArmInsideLowerLeft.Overlay);
					}
					break;
				case DecorationZone.ArmInsideLowerRight:
					if (character.PedDecorations.ArmInsideLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmInsideLowerRight.Collection, (uint)character.PedDecorations.ArmInsideLowerRight.Overlay);
					}
					break;
				case DecorationZone.ArmBackUpperLeft:
					if (character.PedDecorations.ArmBackUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmBackUpperLeft.Collection, (uint)character.PedDecorations.ArmBackUpperLeft.Overlay);
					}
					break;
				case DecorationZone.ArmBackUpperRight:
					if (character.PedDecorations.ArmBackUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmBackUpperRight.Collection, (uint)character.PedDecorations.ArmBackUpperRight.Overlay);
					}
					break;
				case DecorationZone.ArmBackMiddleLeft:
					if (character.PedDecorations.ChestTopLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmBackMiddleLeft.Collection, (uint)character.PedDecorations.ArmBackMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.ArmBackMiddleRight:
					if (character.PedDecorations.ChestTopLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmBackMiddleRight.Collection, (uint)character.PedDecorations.ArmBackMiddleRight.Overlay);
					}
					break;
				case DecorationZone.ArmBackLowerLeft:
					if (character.PedDecorations.ArmBackLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmBackLowerLeft.Collection, (uint)character.PedDecorations.ArmBackLowerLeft.Overlay);
					}
					break;
				case DecorationZone.ArmBackLowerRight:
					if (character.PedDecorations.ArmBackLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.ArmBackLowerRight.Collection, (uint)character.PedDecorations.ArmBackLowerRight.Overlay);
					}
					break;
				case DecorationZone.LegSideUpperLeft:
					if (character.PedDecorations.LegSideUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegSideUpperLeft.Collection, (uint)character.PedDecorations.LegSideUpperLeft.Overlay);
					}
					break;
				case DecorationZone.LegSideUpperRight:
					if (character.PedDecorations.ChestTopLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegSideUpperRight.Collection, (uint)character.PedDecorations.LegSideUpperRight.Overlay);
					}
					break;
				case DecorationZone.LegSideMiddleLeft:
					if (character.PedDecorations.LegSideMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegSideMiddleLeft.Collection, (uint)character.PedDecorations.LegSideMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.LegSideMiddleRight:
					if (character.PedDecorations.ChestTopLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegSideMiddleRight.Collection, (uint)character.PedDecorations.LegSideMiddleRight.Overlay);
					}
					break;
				case DecorationZone.LegSideLowerLeft:
					if (character.PedDecorations.LegSideLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegSideLowerLeft.Collection, (uint)character.PedDecorations.LegSideLowerLeft.Overlay);
					}
					break;
				case DecorationZone.LegSideLowerRight:
					if (character.PedDecorations.LegSideLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegSideLowerRight.Collection, (uint)character.PedDecorations.LegSideLowerRight.Overlay);
					}
					break;
				case DecorationZone.LegFrontUpperLeft:
					if (character.PedDecorations.LegFrontUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegFrontUpperLeft.Collection, (uint)character.PedDecorations.LegFrontUpperLeft.Overlay);
					}
					break;
				case DecorationZone.LegFrontUpperRight:
					if (character.PedDecorations.LegFrontUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegFrontUpperRight.Collection, (uint)character.PedDecorations.LegFrontUpperRight.Overlay);
					}
					break;
				case DecorationZone.LegFrontMiddleLeft:
					if (character.PedDecorations.LegFrontMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegFrontMiddleLeft.Collection, (uint)character.PedDecorations.LegFrontMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.LegFrontMiddleRight:
					if (character.PedDecorations.LegFrontMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegFrontMiddleRight.Collection, (uint)character.PedDecorations.LegFrontMiddleRight.Overlay);
					}
					break;
				case DecorationZone.LegFrontLowerLeft:
					if (character.PedDecorations.LegFrontLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegFrontLowerLeft.Collection, (uint)character.PedDecorations.LegFrontLowerLeft.Overlay);
					}
					break;
				case DecorationZone.LegFrontLowerRight:
					if (character.PedDecorations.LegFrontLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegFrontLowerRight.Collection, (uint)character.PedDecorations.LegFrontLowerRight.Overlay);
					}
					break;
				case DecorationZone.LegBackUpperLeft:
					if (character.PedDecorations.LegBackUpperLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegBackUpperLeft.Collection, (uint)character.PedDecorations.LegBackUpperLeft.Overlay);
					}
					break;
				case DecorationZone.LegBackUpperRight:
					if (character.PedDecorations.LegBackUpperRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegBackUpperRight.Collection, (uint)character.PedDecorations.LegBackUpperRight.Overlay);
					}
					break;
				case DecorationZone.LegBackMiddleLeft:
					if (character.PedDecorations.LegBackMiddleLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegBackMiddleLeft.Collection, (uint)character.PedDecorations.LegBackMiddleLeft.Overlay);
					}
					break;
				case DecorationZone.LegBackMiddleRight:
					if (character.PedDecorations.LegBackMiddleRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegBackMiddleRight.Collection, (uint)character.PedDecorations.LegBackMiddleRight.Overlay);
					}
					break;
				case DecorationZone.LegBackLowerLeft:
					if (character.PedDecorations.LegBackLowerLeft.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegBackLowerLeft.Collection, (uint)character.PedDecorations.LegBackLowerLeft.Overlay);
					}
					break;
				case DecorationZone.LegBackLowerRight:
					if (character.PedDecorations.LegBackLowerRight.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.LegBackLowerRight.Collection, (uint)character.PedDecorations.LegBackLowerRight.Overlay);
					}
					break;
				case DecorationZone.Face:
				case DecorationZone.Unknown:
					if (character.PedDecorations.Face.Index >= 0)
					{
						SetPedDecoration(Game.PlayerPed.Handle, (uint)character.PedDecorations.Face.Collection, (uint)character.PedDecorations.Face.Overlay);
					}
					break;
				default:
					break;
			}
		}
	}
}
