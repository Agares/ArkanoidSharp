using Agares.Engine;
using Agares.Engine.Collisions;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Geometry;
using Agares.Engine.Stages;
using Agares.Engine.Utilities;
using ArkanoidSharp.Game.EventTypes;

namespace ArkanoidSharp.Game
{
	public class Ball : ICollidable, IEventHandler<LoopEvent>, IEventHandler<DrawingEvent>, IStageObject
	{
		private const string TextureId = "ball";

		private readonly Texture _ballTexture;
		private readonly Rectangle _gameBoundingRectangle;
		private readonly IEventManager _eventManager;

		public PointDouble Position { get; set; }
		public VectorDouble Speed { get; set; }

		public VectorDouble DefaultSpeed
		{
			get { return new VectorDouble(0.5, 0.5); }
		}

		public Rectangle BoundingRectangle
		{
			get { return Rectangle.FromPositionAndSize((int)Position.X, (int)Position.Y, 24, 24); }
		}

		public Ball(IResourceManager resourceManager, Rectangle gameBoundingRectangle, IEventManager eventManager)
		{
			Position = new PointDouble(300, 300);
			Speed = DefaultSpeed;

			_ballTexture = resourceManager.LoadTexture(TextureId);
			_gameBoundingRectangle = gameBoundingRectangle;
			_eventManager = eventManager;
		}

		public void HandleEvent(LoopEvent @event)
		{
			Position += Speed*@event.Delta.TotalMilliseconds;

			EnsureOnBoard();
		}

		public void HandleEvent(DrawingEvent @event)
		{
			@event.Renderer.DrawTexture(_ballTexture, Rectangle.FromPositionAndSize(0, 0, 128, 128), BoundingRectangle);
		}

		private void EnsureOnBoard()
		{
			if (_gameBoundingRectangle.Contains(BoundingRectangle))
			{
				return;
			}

			HandleWallHit();
			HandleOutOfBoundsPosition();
		}

		private void HandleOutOfBoundsPosition()
		{
			var x = Position.X.Clamp(_gameBoundingRectangle.Left, _gameBoundingRectangle.Right - BoundingRectangle.Width);
			var y = Position.Y.Clamp(_gameBoundingRectangle.Top, _gameBoundingRectangle.Bottom - BoundingRectangle.Height);

			Position = new PointDouble(x, y);
		}

		private void HandleWallHit()
		{
			var direction = _gameBoundingRectangle.GetDirectionTo(BoundingRectangle);

			if (direction.HasFlag(Direction.Bottom))
			{
				_eventManager.Emit(new BallFallenEvent());
			}

			if (direction.HasFlag(Direction.Top))
			{
				NegateVerticalSpeed();
			}

			if (direction.HasFlag(Direction.Left) || direction.HasFlag(Direction.Right))
			{
				NegateHorizontalSpeed();
			}
		}

		public void NegateHorizontalSpeed()
		{
			Speed = Speed.NegateX();
		}

		public void NegateVerticalSpeed()
		{
			Speed = Speed.NegateY();
		}
	}
}