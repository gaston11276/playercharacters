using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class PedDecorations	:IdentityModel, IPedDecorations
	{
		public PedDecoration ChestTop { get; set; }
		public PedDecoration ChestTopLeft { get; set; }
		public PedDecoration ChestTopRight { get; set; }	
		public PedDecoration ChestUpper { get; set; }
		public PedDecoration ChestUpperLeft { get; set; }
		public PedDecoration ChestUpperRight { get; set; }
		public PedDecoration ChestMiddle { get; set; }
		public PedDecoration ChestMiddleLeft { get; set; }
		public PedDecoration ChestMiddleRight { get; set; }
		public PedDecoration ChestLower { get; set; }
		public PedDecoration ChestLowerLeft { get; set; }
		public PedDecoration ChestLowerRight { get; set; }
		public PedDecoration SideUpperLeft { get; set; }
		public PedDecoration SideUpperRight { get; set; }
		public PedDecoration SideMiddleLeft { get; set; }
		public PedDecoration SideMiddleRight { get; set; }
		public PedDecoration SideLowerLeft { get; set; }
		public PedDecoration SideLowerRight { get; set; }
		public PedDecoration NeckFront { get; set; }
		public PedDecoration NeckBack { get; set; }
		public PedDecoration NeckLeft { get; set; }
		public PedDecoration NeckRight { get; set; }
		public PedDecoration BackTop { get; set; }
		public PedDecoration BackTopLeft { get; set; }
		public PedDecoration BackTopRight { get; set; }
		public PedDecoration BackUpper { get; set; }
		public PedDecoration BackUpperLeft { get; set; }
		public PedDecoration BackUpperRight { get; set; }
		public PedDecoration BackMiddle { get; set; }
		public PedDecoration BackMiddleLeft { get; set; }
		public PedDecoration BackMiddleRight { get; set; }
		public PedDecoration BackLower { get; set; }
		public PedDecoration BackLowerLeft { get; set; }
		public PedDecoration BackLowerRight { get; set; }
		public PedDecoration ArmSideUpperLeft { get; set; }
		public PedDecoration ArmSideUpperRight { get; set; }
		public PedDecoration ArmSideMiddleLeft { get; set; }
		public PedDecoration ArmSideMiddleRight { get; set; }
		public PedDecoration ArmSideLowerLeft { get; set; }
		public PedDecoration ArmSideLowerRight { get; set; }
		public PedDecoration ArmFrontUpperLeft { get; set; }
		public PedDecoration ArmFrontUpperRight { get; set; }
		public PedDecoration ArmFrontMiddleLeft { get; set; }
		public PedDecoration ArmFrontMiddleRight { get; set; }
		public PedDecoration ArmFrontLowerLeft { get; set; }
		public PedDecoration ArmFrontLowerRight { get; set; }
		public PedDecoration ArmInsideUpperLeft { get; set; }
		public PedDecoration ArmInsideUpperRight { get; set; }
		public PedDecoration ArmInsideMiddleLeft { get; set; }
		public PedDecoration ArmInsideMiddleRight { get; set; }
		public PedDecoration ArmInsideLowerLeft { get; set; }
		public PedDecoration ArmInsideLowerRight { get; set; }
		public PedDecoration ArmBackUpperLeft { get; set; }
		public PedDecoration ArmBackUpperRight { get; set; }
		public PedDecoration ArmBackMiddleLeft { get; set; }
		public PedDecoration ArmBackMiddleRight { get; set; }
		public PedDecoration ArmBackLowerLeft { get; set; }
		public PedDecoration ArmBackLowerRight { get; set; }
		public PedDecoration LegSideUpperLeft { get; set; }
		public PedDecoration LegSideUpperRight { get; set; }
		public PedDecoration LegSideMiddleLeft { get; set; }
		public PedDecoration LegSideMiddleRight { get; set; }
		public PedDecoration LegSideLowerLeft { get; set; }
		public PedDecoration LegSideLowerRight { get; set; }
		public PedDecoration LegFrontUpperLeft { get; set; }
		public PedDecoration LegFrontUpperRight { get; set; }
		public PedDecoration LegFrontMiddleLeft { get; set; }
		public PedDecoration LegFrontMiddleRight { get; set; }
		public PedDecoration LegFrontLowerLeft { get; set; }
		public PedDecoration LegFrontLowerRight { get; set; }
		public PedDecoration LegBackUpperLeft { get; set; }
		public PedDecoration LegBackUpperRight { get; set; }
		public PedDecoration LegBackMiddleLeft { get; set; }
		public PedDecoration LegBackMiddleRight { get; set; }
		public PedDecoration LegBackLowerLeft { get; set; }
		public PedDecoration LegBackLowerRight { get; set; }
		public PedDecoration Face { get; set; }

		public PedDecorations()
		{
			ChestTop = new PedDecoration();
			ChestTopLeft = new PedDecoration();
			ChestTopRight = new PedDecoration();
			ChestTopRight = new PedDecoration();
			ChestUpper = new PedDecoration();
			ChestUpperLeft = new PedDecoration();
			ChestUpperRight = new PedDecoration();
			ChestMiddle = new PedDecoration();
			ChestMiddleLeft = new PedDecoration();
			ChestMiddleRight = new PedDecoration();
			ChestLower = new PedDecoration();
			ChestLowerLeft = new PedDecoration();
			ChestLowerRight = new PedDecoration();
			SideUpperLeft = new PedDecoration();
			SideUpperRight = new PedDecoration();
			SideMiddleLeft = new PedDecoration();
			SideMiddleRight = new PedDecoration();
			SideLowerLeft = new PedDecoration();
			SideLowerRight = new PedDecoration();
			NeckFront = new PedDecoration();
			NeckBack = new PedDecoration();
			NeckLeft = new PedDecoration();
			NeckRight = new PedDecoration();
			BackTop = new PedDecoration();
			BackTopLeft = new PedDecoration();
			BackTopRight = new PedDecoration();
			BackUpper = new PedDecoration();
			BackUpperLeft = new PedDecoration();
			BackUpperRight = new PedDecoration();
			BackMiddle = new PedDecoration();
			BackMiddleLeft = new PedDecoration();
			BackMiddleRight = new PedDecoration();
			BackLower = new PedDecoration();
			BackLowerLeft = new PedDecoration();
			BackLowerRight = new PedDecoration();
			ArmSideUpperLeft = new PedDecoration();
			ArmSideUpperRight = new PedDecoration();
			ArmSideMiddleLeft = new PedDecoration();
			ArmSideMiddleRight = new PedDecoration();
			ArmSideLowerLeft = new PedDecoration();
			ArmSideLowerRight = new PedDecoration();
			ArmFrontUpperLeft = new PedDecoration();
			ArmFrontUpperRight = new PedDecoration();
			ArmFrontMiddleLeft = new PedDecoration();
			ArmFrontMiddleRight = new PedDecoration();
			ArmFrontLowerLeft = new PedDecoration();
			ArmFrontLowerRight = new PedDecoration();
			ArmInsideUpperLeft = new PedDecoration();
			ArmInsideUpperRight = new PedDecoration();
			ArmInsideMiddleLeft = new PedDecoration();
			ArmInsideMiddleRight = new PedDecoration();
			ArmInsideLowerLeft = new PedDecoration();
			ArmInsideLowerRight = new PedDecoration();
			ArmBackUpperLeft = new PedDecoration();
			ArmBackUpperRight = new PedDecoration();
			ArmBackMiddleLeft = new PedDecoration();
			ArmBackMiddleRight = new PedDecoration();
			ArmBackLowerLeft = new PedDecoration();
			ArmBackLowerRight = new PedDecoration();
			LegSideUpperLeft = new PedDecoration();
			LegSideUpperRight = new PedDecoration();
			LegSideMiddleLeft = new PedDecoration();
			LegSideMiddleRight = new PedDecoration();
			LegSideLowerLeft = new PedDecoration();
			LegSideLowerRight = new PedDecoration();
			LegFrontUpperLeft = new PedDecoration();
			LegFrontUpperRight = new PedDecoration();
			LegFrontMiddleLeft = new PedDecoration();
			LegFrontMiddleRight = new PedDecoration();
			LegFrontLowerLeft = new PedDecoration();
			LegFrontLowerRight = new PedDecoration();
			LegBackUpperLeft = new PedDecoration();
			LegBackUpperRight = new PedDecoration();
			LegBackMiddleLeft = new PedDecoration();
			LegBackMiddleRight = new PedDecoration();
			LegBackLowerLeft = new PedDecoration();
			LegBackLowerRight = new PedDecoration();
			Face = new PedDecoration();
		}
	}
}
