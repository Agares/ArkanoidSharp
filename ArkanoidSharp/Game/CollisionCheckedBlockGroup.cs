using Agares.Engine.Collisions;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Stages;
using ArkanoidSharp.Game.EventTypes;

namespace ArkanoidSharp.Game
{
	public class CollisionCheckedBlockGroup : IEventHandler<DrawingEvent>, IStageObject
	{
		private readonly BlockGroup _blockGroup;

		public CollisionCheckedBlockGroup(BlockGroup blockGroup, CollisionChecker collisionChecker, Ball ball, IEventManager eventManager)
		{
			_blockGroup = blockGroup;

			_blockGroup.Blocks.CollectionChanged += (sender, args) =>
			{
				if (args.NewItems != null)
				{
					foreach (ICollidable added in args.NewItems)
					{
						collisionChecker.AddCollisionToHandle(ball, added);
					}
				}

				if (args.OldItems != null)
				{
					foreach (ICollidable removed in args.OldItems)
					{
						collisionChecker.RemoveAllCollisionsWith(removed);
					}
				}

				eventManager.Emit(new BlockDestroyedEvent(blockGroup));
			};

			foreach (var block in _blockGroup.Blocks)
			{
				collisionChecker.AddCollisionToHandle(ball, block);
			}
		}

		public void HandleEvent(DrawingEvent @event)
		{
			_blockGroup.HandleEvent(@event);
		}
	}
}