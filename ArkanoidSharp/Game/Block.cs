using Agares.Engine.Collisions;
using Agares.Engine.Geometry;

namespace ArkanoidSharp.Game
{
	public class Block : ICollidable
	{
		private readonly Point _position;

		public BlockGroup Group { get; private set; }

		public Block(Point position, BlockGroup group)
		{
			_position = position;
			Group = group;
		}

		public Rectangle BoundingRectangle
		{
			get { return Rectangle.FromPositionAndSize(_position, new Vector(64, 24)); }
		}
	}
}