using System.Linq;
using System.Threading;
using Agares.Engine;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Stages;
using ArkanoidSharp.Game.EventTypes;
using ArkanoidSharp.Game.Stages;

namespace ArkanoidSharp.Game
{
	public class Game : IEventHandler<LoopEvent>, IEventHandler<BallFallenEvent>, IEventHandler<BlockDestroyedEvent>
	{
		private readonly IRenderer _renderer;
		private readonly IEventManager _eventManager;
		private readonly IStageManager _stageManager;

		public Game(IRenderer renderer, IEventManager eventManager, IStageManager stageManager)
		{
			_renderer = renderer;
			_eventManager = eventManager;
			_stageManager = stageManager;
		}

		public void HandleEvent(LoopEvent @event)
		{
			_eventManager.Emit(new DrawingEvent(_renderer));

			Thread.Sleep(0);
		}

		public void HandleEvent(BallFallenEvent @event)
		{
			_stageManager.TransitionTo<GameLostStage>();
		}

		public void HandleEvent(BlockDestroyedEvent @event)
		{
			if (!@event.BlockGroup.Blocks.Any())
			{
				_stageManager.TransitionTo<GameWonStage>();
			}
		}
	}
}