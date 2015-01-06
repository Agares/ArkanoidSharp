using Agares.Engine;
using Agares.Engine.Collisions;
using Agares.Engine.Events;
using Agares.Engine.Geometry;
using Agares.Engine.Stages;

namespace ArkanoidSharp.Game.Stages
{
	public class GameStage : Stage
	{
		public GameStage(IEventManager eventManager, IResourceManager resourceManager, CollisionChecker collisionChecker, Rectangle boundingRectangle) : base()
		{
			var paddle = new Paddle(resourceManager, boundingRectangle);
			var ball = new Ball(resourceManager, boundingRectangle, eventManager);
			var blockGroup = new BlockGroup(resourceManager);
			var collisionCheckedBlockGroup = new CollisionCheckedBlockGroup(blockGroup, collisionChecker, ball, eventManager);

			collisionChecker.AddCollisionToHandle(paddle, ball);
			
			EventGroup.AddEventHandler(collisionChecker);
			EventGroup.AddEventHandler(new BallCollisionHandler());

			AddObject(paddle);
			AddObject(ball);
			AddObject(collisionCheckedBlockGroup);
		}
	}
}