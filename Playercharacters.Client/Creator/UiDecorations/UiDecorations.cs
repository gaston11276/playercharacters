namespace Gaston11276.Playercharacters.Client
{
	public class UiDecorations : UiAppearance
	{
		public UiDecorations()
		{
			cameraMode = CameraMode.Front;
		}

		public override void SetUi()
		{ }

		public override void CreateContent()
		{
			uiHeader.SetText("Decorations");
		}

		public void ApplyToPed()
		{ }
	}
}
