

namespace Gaston11276.Playercharacters.Client
{
	class HudComponentAnimations : HudComponent
	{
		public HudComponentAnimations()
		{
			cameraMode = CameraMode.Front;
		}

		public override void SetUi()
		{
			
		}

		public override void CreateContent()
		{
			uiHeader.SetText("Animations");

			CreateApplyCancelButtons();
		}

		public void ApplyToPed()
		{ }
	}
}
