namespace Gaston11276.Playercharacters.Client
{
	class UiAnimations : UiAppearance
	{
		public UiAnimations()
		{
			cameraMode = CameraMode.Front;
		}

		public override void SetUi()
		{ }

		public override void CreateContent()
		{
			uiHeader.SetText("Animations");
		}

		public void ApplyToPed()
		{ }
	}
}
