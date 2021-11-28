using JetBrains.Annotations;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	[PublicAPI]
	public interface IPedDecorations : IIdentityModel
	{
		PedDecoration ChestTop { get; set; }
		PedDecoration ChestTopLeft { get; set; }
		PedDecoration ChestTopRight { get; set; }
		PedDecoration ChestUpper { get; set; }
		PedDecoration ChestUpperLeft { get; set; }
		PedDecoration ChestUpperRight { get; set; }
		PedDecoration ChestMiddle { get; set; }
		PedDecoration ChestMiddleLeft { get; set; }
		PedDecoration ChestMiddleRight { get; set; }
		PedDecoration ChestLower { get; set; }
		PedDecoration ChestLowerLeft { get; set; }
		PedDecoration ChestLowerRight { get; set; }
		PedDecoration SideUpperLeft { get; set; }
		PedDecoration SideUpperRight { get; set; }
		PedDecoration SideMiddleLeft { get; set; }
		PedDecoration SideMiddleRight { get; set; }
		PedDecoration SideLowerLeft { get; set; }
		PedDecoration SideLowerRight { get; set; }
		PedDecoration NeckFront { get; set; }
		PedDecoration NeckBack { get; set; }
		PedDecoration NeckLeft { get; set; }
		PedDecoration NeckRight { get; set; }
		PedDecoration BackTop { get; set; }
		PedDecoration BackTopLeft { get; set; }
		PedDecoration BackTopRight { get; set; }
		PedDecoration BackUpper { get; set; }
		PedDecoration BackUpperLeft { get; set; }
		PedDecoration BackUpperRight { get; set; }
		PedDecoration BackMiddle { get; set; }
		PedDecoration BackMiddleLeft { get; set; }
		PedDecoration BackMiddleRight { get; set; }
		PedDecoration BackLower { get; set; }
		PedDecoration BackLowerLeft { get; set; }
		PedDecoration BackLowerRight { get; set; }
		PedDecoration ArmSideUpperLeft { get; set; }
		PedDecoration ArmSideUpperRight { get; set; }
		PedDecoration ArmSideMiddleLeft { get; set; }
		PedDecoration ArmSideMiddleRight { get; set; }
		PedDecoration ArmSideLowerLeft { get; set; }
		PedDecoration ArmSideLowerRight { get; set; }
		PedDecoration ArmFrontUpperLeft { get; set; }
		PedDecoration ArmFrontUpperRight { get; set; }
		PedDecoration ArmFrontMiddleLeft { get; set; }
		PedDecoration ArmFrontMiddleRight { get; set; }
		PedDecoration ArmFrontLowerLeft { get; set; }
		PedDecoration ArmFrontLowerRight { get; set; }
		PedDecoration ArmInsideUpperLeft { get; set; }
		PedDecoration ArmInsideUpperRight { get; set; }
		PedDecoration ArmInsideMiddleLeft { get; set; }
		PedDecoration ArmInsideMiddleRight { get; set; }
		PedDecoration ArmInsideLowerLeft { get; set; }
		PedDecoration ArmInsideLowerRight { get; set; }
		PedDecoration ArmBackUpperLeft { get; set; }
		PedDecoration ArmBackUpperRight { get; set; }
		PedDecoration ArmBackMiddleLeft { get; set; }
		PedDecoration ArmBackMiddleRight { get; set; }
		PedDecoration ArmBackLowerLeft { get; set; }
		PedDecoration ArmBackLowerRight { get; set; }
		PedDecoration LegSideUpperLeft { get; set; }
		PedDecoration LegSideUpperRight { get; set; }
		PedDecoration LegSideMiddleLeft { get; set; }
		PedDecoration LegSideMiddleRight { get; set; }
		PedDecoration LegSideLowerLeft { get; set; }
		PedDecoration LegSideLowerRight { get; set; }
		PedDecoration LegFrontUpperLeft { get; set; }
		PedDecoration LegFrontUpperRight { get; set; }
		PedDecoration LegFrontMiddleLeft { get; set; }
		PedDecoration LegFrontMiddleRight { get; set; }
		PedDecoration LegFrontLowerLeft { get; set; }
		PedDecoration LegFrontLowerRight { get; set; }
		PedDecoration LegBackUpperLeft { get; set; }
		PedDecoration LegBackUpperRight { get; set; }
		PedDecoration LegBackMiddleLeft { get; set; }
		PedDecoration LegBackMiddleRight { get; set; }
		PedDecoration LegBackLowerLeft { get; set; }
		PedDecoration LegBackLowerRight { get; set; }
		PedDecoration Face { get; set; }
	}
}
