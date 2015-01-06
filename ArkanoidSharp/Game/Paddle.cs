using Agares.Engine;
using Agares.Engine.Collisions;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Geometry;
using Agares.Engine.Stages;

namespace ArkanoidSharp.Game
{
	public class Paddle : ICollidable, IEventHandler<MouseMoveEvent>, IEventHandler<DrawingEvent>, IStageObject
	{
		private const string TextureId = "paddle";

		private readonly Texture _texture;
		private readonly Rectangle _boundingRectangle;

		private int _positionX;

		public Paddle(IResourceManager resourceManager, Rectangle boundingRectangle)
		{
			_texture = resourceManager.LoadTexture(TextureId);
			_boundingRectangle = boundingRectangle;
		}

		public Rectangle BoundingRectangle
		{
			get { return Rectangle.FromPositionAndSize(_positionX, _boundingRectangle.Height - 64, 128, 16); }
		}

		private void Move(int x)
		{
			_positionX = x;
		}

		public void HandleEvent(MouseMoveEvent @event)
		{
			Move(@event.Position.X);
		}

		public void HandleEvent(DrawingEvent @event)
		{
			@event.Renderer.DrawTexture(_texture, Rectangle.FromPositionAndSize(0, 0, 128, 16), BoundingRectangle);
		}
	}
}