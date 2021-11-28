
namespace Gaston11276.Playercharacters.Shared.Models
{
	public class PedDecoration
	{
		public PedDecorationType Type { get; set; }
		public DecorationZone Zone { get; set; } //Remove
		public int Index { get; set; }
		public int Collection { get; set; }
		public int Overlay { get; set; }

		public PedDecoration()
		{
			Index = -1;
		}
	}
}
