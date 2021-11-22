using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using NFive.SDK.Client.Extensions;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Extensions;


using Gaston11276.Playercharacters.Shared.Models;


namespace Gaston11276.Playercharacters.Client.Models
{
	public class Character : IdentityModel, ICharacter
	{
		public string Forename { get; set; }
		public string Middlename { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
		public short Gender { get; set; }
		public bool Alive { get; set; }
		public int Health { get; set; }
		public int Armor { get; set; }
		public int Ssn { get; set; }
		public Position Position { get; set; }
		public string Model { get; set; }
		public string AnimationSet { get; set; }
		public Guid PedHeadBlendDataId { get; set; }
		public PedHeadBlendData PedHeadBlendData { get; set; }
		public Guid PedFaceFeaturesId{ get; set; }
		public PedFaceFeatures PedFaceFeatures { get; set; }
		public Guid PedHeadOverlaysId{ get; set; }
		public PedHeadOverlays PedHeadOverlays { get; set; }
		public Guid PedDecorationsId { get; set; }
		public PedDecorations PedDecorations { get; set; }
		public Guid PedComponentsId { get; set; }
		public PedComponents PedComponents { get; set; }
		public int PedHairColor { get; set; }
		public int PedSecondHairColor { get; set; }
		public PedEyeColor PedEyeColor { get; set; }
		public Guid PedPropsId { get; set; }
		public PedProps PedProps { get; set; }


		public DateTime? LastPlayed { get; set; }
		public Guid UserId { get; set; }

		[JsonIgnore] public string FullName => $"{this.Forename} {this.Middlename} {this.Surname}".Replace("  ", " ");

		[JsonIgnore] public PedHash ModelHash => (PedHash)Convert.ToUInt32(this.Model);

		public void RenderCustom(ILogger logger)
		{
			// Only for free mode models
			if (this.ModelHash != PedHash.FreemodeMale01 && this.ModelHash != PedHash.FreemodeFemale01) return;

			var player = Game.Player.Character.Handle;

			// https://gtaforums.com/topic/858970-all-gtao-face-ids-pedset_ped_head_blend_data-explained/
			// https://wiki.gt-mp.net/index.php/Character_Components
			// https://wiki.gt-mp.net/index.php?title=Hair_Colors

			//API.SetPedHeadBlendData(player, this.PedHeadBlendData.Parent1, this.PedHeadBlendData.Parent2, 0, this.PedHeadBlendData.Parent1, this.PedHeadBlendData.Parent2, 0, this.PedHeadBlendData.ShapeMix, this.PedHeadBlendData.SkinMix, 0f, true);

			//API.SetPedHairColor(player, this.Appearance.HairColorId, this.Appearance.HairHighlightColor);
			//API.SetPedHeadOverlay(player, (int)PedHeadOverlayType.Blemishes, this.PedHeadOverlay.Ageing.Index, this.PedHeadOverlay.Ageing.Opacity);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Beard, this.Appearance.Beard.Index, this.Appearance.Beard.Opacity);
			//API.SetPedEyeColor(player, this.Appearance.EyeColorId);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Eyebrows, this.Appearance.Eyebrows.Index, this.Appearance.Eyebrows.Opacity);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Makeup, this.Appearance.Makeup.Index, this.Appearance.Makeup.Opacity);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Lipstick, this.Appearance.Lipstick.Index, this.Appearance.Lipstick.Opacity);
			//API.SetPedHeadOverlayColor(player, (int)FeatureTypes.Beard, (int)this.Appearance.Beard.ColorType, this.Appearance.Beard.ColorId, this.Appearance.Beard.SecondColorId);
			//API.SetPedHeadOverlayColor(player, (int)FeatureTypes.Eyebrows, (int)this.Appearance.Eyebrows.ColorType, this.Appearance.Eyebrows.ColorId, this.Appearance.Eyebrows.SecondColorId);
			//API.SetPedHeadOverlayColor(player, (int)FeatureTypes.Makeup, (int)this.Appearance.Makeup.ColorType, this.Appearance.Makeup.ColorId, this.Appearance.Makeup.SecondColorId);
			//API.SetPedHeadOverlayColor(player, (int)FeatureTypes.Lipstick, (int)this.Appearance.Lipstick.ColorType, this.Appearance.Lipstick.ColorId, this.Appearance.Lipstick.SecondColorId);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Blush, this.Appearance.Blush.Index, this.Appearance.Blush.Opacity);
			//API.SetPedHeadOverlayColor(player, (int)FeatureTypes.Blush, (int)this.Appearance.Blush.ColorType, this.Appearance.Blush.ColorId, this.Appearance.Blush.SecondColorId);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Complexion, this.Appearance.Complexion.Index, this.Appearance.Complexion.Opacity);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.SunDamage, this.Appearance.SunDamage.Index, this.Appearance.SunDamage.Opacity);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.MolesAndFreckles, this.Appearance.MolesAndFreckles.Index, this.Appearance.MolesAndFreckles.Opacity);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Chest, this.Appearance.Chest.Index, this.Appearance.Chest.Opacity);
			//API.SetPedHeadOverlayColor(player, (int)FeatureTypes.Chest, (int)this.Appearance.Chest.ColorType, this.Appearance.Chest.ColorId, this.Appearance.Chest.SecondColorId);
			//API.SetPedHeadOverlay(player, (int)FeatureTypes.Blemishes, this.Appearance.Blemishes.Index, this.Appearance.Blemishes.Opacity);

			/*
			API.SetPedFaceFeature(player, 0, this.PedFaceFeatures.NoseWidth);
			API.SetPedFaceFeature(player, 1, this.PedFaceFeatures.NosePeakHeight);
			API.SetPedFaceFeature(player, 2, this.PedFaceFeatures.NosePeakLength);
			API.SetPedFaceFeature(player, 3, this.PedFaceFeatures.NoseBoneHeight);
			API.SetPedFaceFeature(player, 4, this.PedFaceFeatures.NosePeakLowering);
			API.SetPedFaceFeature(player, 5, this.PedFaceFeatures.NoseBoneTwist);
			API.SetPedFaceFeature(player, 6, this.PedFaceFeatures.EyeBrowHeight);
			API.SetPedFaceFeature(player, 7, this.PedFaceFeatures.EyeBrowForward);
			API.SetPedFaceFeature(player, 8, this.PedFaceFeatures.CheekBoneHeight);
			API.SetPedFaceFeature(player, 9, this.PedFaceFeatures.CheekBoneWidth);
			API.SetPedFaceFeature(player, 10, this.PedFaceFeatures.CheekWidth);
			API.SetPedFaceFeature(player, 11, this.PedFaceFeatures.EyesOpening);
			API.SetPedFaceFeature(player, 12, this.PedFaceFeatures.LipThickness);
			API.SetPedFaceFeature(player, 13, this.PedFaceFeatures.JawBoneWidth);
			API.SetPedFaceFeature(player, 14, this.PedFaceFeatures.JawBoneLength);
			API.SetPedFaceFeature(player, 15, this.PedFaceFeatures. ChinBoneLowering);
			API.SetPedFaceFeature(player, 16, this.PedFaceFeatures.ChinBoneLength);
			API.SetPedFaceFeature(player, 17, this.PedFaceFeatures.ChinBoneWidth);
			API.SetPedFaceFeature(player, 18, this.PedFaceFeatures.ChinDimple);
			API.SetPedFaceFeature(player, 19, this.PedFaceFeatures.NeckThickness);

			API.SetPedDecoration(player, (uint)this.PedDecorations.ChestTop.Collection, (uint)this.PedDecorations.ChestTop.Overlay);
			*/
		}

		public async Task Render(ILogger logger)
		{
			// Apparently this _must_ be called
			Game.Player.Character.Style.SetDefaultClothes();

			Game.Player.Character.Position = this.Position.ToVector3().ToCitVector3();
			Game.Player.Character.Armor = this.Armor;

			API.RequestClipSet(this.AnimationSet);
			await BaseScript.Delay(100); // Required to load
			Game.Player.Character.MovementAnimationSet = this.AnimationSet;
			/*
			Game.Player.Character.Style[PedComponents.Face].SetVariation(this.Apparel.Face.Index, this.Apparel.Face.Texture);
			Game.Player.Character.Style[PedComponents.Head].SetVariation(this.Apparel.Head.Index, this.Apparel.Head.Texture);

			// Temporary network visibility fix workaround
			Game.Player.Character.Style[PedComponents.Hair].SetVariation(1, 1);

			Game.Player.Character.Style[PedComponents.Hair].SetVariation(this.Apparel.Hair.Index, this.Apparel.Hair.Texture);

			Game.Player.Character.Style[PedComponents.Torso].SetVariation(this.Apparel.Torso.Index, this.Apparel.Torso.Texture);
			Game.Player.Character.Style[PedComponents.Legs].SetVariation(this.Apparel.Legs.Index, this.Apparel.Legs.Texture);
			Game.Player.Character.Style[PedComponents.Hands].SetVariation(this.Apparel.Hands.Index, this.Apparel.Hands.Texture);
			Game.Player.Character.Style[PedComponents.Shoes].SetVariation(this.Apparel.Shoes.Index, this.Apparel.Shoes.Texture);
			Game.Player.Character.Style[PedComponents.Special1].SetVariation(this.Apparel.Special1.Index, this.Apparel.Special1.Texture);
			Game.Player.Character.Style[PedComponents.Special2].SetVariation(this.Apparel.Special2.Index, this.Apparel.Special2.Texture);
			Game.Player.Character.Style[PedComponents.Special3].SetVariation(this.Apparel.Special3.Index, this.Apparel.Special3.Texture);
			Game.Player.Character.Style[PedComponents.Textures].SetVariation(this.Apparel.Textures.Index, this.Apparel.Textures.Texture);
			Game.Player.Character.Style[PedComponents.Torso2].SetVariation(this.Apparel.Torso2.Index, this.Apparel.Torso2.Texture);

			Game.Player.Character.Style[PedProps.Hats].SetVariation(this.Apparel.Hat.Index, this.Apparel.Hat.Texture);
			Game.Player.Character.Style[PedProps.Glasses].SetVariation(this.Apparel.Glasses.Index, this.Apparel.Glasses.Texture);
			Game.Player.Character.Style[PedProps.EarPieces].SetVariation(this.Apparel.EarPiece.Index, this.Apparel.EarPiece.Texture);
			Game.Player.Character.Style[PedProps.Unknown3].SetVariation(this.Apparel.Unknown3.Index, this.Apparel.Unknown3.Texture);
			Game.Player.Character.Style[PedProps.Unknown4].SetVariation(this.Apparel.Unknown4.Index, this.Apparel.Unknown4.Texture);
			Game.Player.Character.Style[PedProps.Unknown5].SetVariation(this.Apparel.Unknown5.Index, this.Apparel.Unknown5.Texture);
			Game.Player.Character.Style[PedProps.Watches].SetVariation(this.Apparel.Watch.Index, this.Apparel.Watch.Texture);
			Game.Player.Character.Style[PedProps.Wristbands].SetVariation(this.Apparel.Wristband.Index, this.Apparel.Wristband.Texture);
			Game.Player.Character.Style[PedProps.Unknown8].SetVariation(this.Apparel.Unknown8.Index, this.Apparel.Unknown8.Texture);
			Game.Player.Character.Style[PedProps.Unknown9].SetVariation(this.Apparel.Unknown9.Index, this.Apparel.Unknown9.Texture);
			*/
		}
	}
}
