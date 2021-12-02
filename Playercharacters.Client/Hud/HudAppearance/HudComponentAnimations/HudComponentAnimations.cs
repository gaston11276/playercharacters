using System.Threading.Tasks;

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

		protected override void CreateContent()
		{
			uiHeader.SetText("Animations");

			CreateApplyCancelButtons();
		}

		public void ApplyToPed()
		{ }

		public async Task SetDefaults()
		{
			await Delay(10);
		}
	}
}
