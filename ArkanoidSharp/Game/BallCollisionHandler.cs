using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Geometry;

namespace ArkanoidSharp.Game
{
	public class BallCollisionHandler : IEventHandler<CollisionEvent>
	{
		public void HandleEvent(CollisionEvent @event)
		{
			if (!@event.Has<Ball>())
			{
				return;
			}

			var ball = @event.Get<Ball>();
			ball.NegateVerticalSpeed();

			if (@event.Has<Paddle>())
			{
				var paddle = @event.Get<Paddle>();
				HandleCollisionWithPaddle(ball, paddle);
			}
			else if (@event.Has<Block>())
			{
				var block = @event.Get<Block>();
				HandleCollisionWithBlock(ball, block);
			}
		}

		private void HandleCollisionWithBlock(Ball ball, Block block)
		{
			block.Group.RemoveBlock(block);
			ball.Position = new PointDouble(ball.Position.X, block.BoundingRectangle.Bottom + 1);
		}

		private void HandleCollisionWithPaddle(Ball ball, Paddle paddle)
		{
			var hitRelativePosition = ComputeCollisionPositionRelativeToPaddle(ball, paddle);

			double ySpeed;
			double xSpeed = ball.DefaultSpeed.X;

			if (hitRelativePosition <= 0.5)
			{
				ySpeed = -(ball.DefaultSpeed.Y - ball.DefaultSpeed.Y * hitRelativePosition);
				xSpeed = -xSpeed;
			}
			else
			{
				ySpeed = -(ball.DefaultSpeed.Y - ball.DefaultSpeed.Y * (1 - hitRelativePosition));
			}

			ball.Speed = new VectorDouble(xSpeed, ySpeed);
		}

		private double ComputeCollisionPositionRelativeToPaddle(Ball ball, Paddle paddle)
		{
			int hitX = (ball.BoundingRectangle.Left + ball.BoundingRectangle.Right)/2;
			int hitXRelativeToPaddle = hitX - paddle.BoundingRectangle.Left;
			return hitXRelativeToPaddle/(double) paddle.BoundingRectangle.Width;
		}
	}
}