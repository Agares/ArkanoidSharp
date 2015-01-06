using Agares.Engine;
using Agares.Engine.Collisions;
using Agares.Engine.Events;
using Agares.Engine.Geometry;
using Agares.Engine.Stages;
using ArkanoidSharp.Game;
using ArkanoidSharp.Game.Stages;

namespace ArkanoidSharp
{
	internal class Program
	{
		private static void Main()
		{
			var boundingRectangle = Rectangle.FromPositionAndSize(0, 0, 800, 600);

			using (
				var window = Window.CreateCentered
					(
					"Och iś programire so hard",
					boundingRectangle.Width, 
					boundingRectangle.Height,
					WindowFlags.InputFocus | WindowFlags.MouseFocus
					)
				)
			{
				var renderer = new Renderer(window);

				var resourceManager = new FilesystemResourceManager(renderer);
				var eventManager = new EventManager();
				var stageManager = new StageManager(eventManager);
				var sdlEventEmitter = new SDLEventEmitter(eventManager);
				var collisionChecker = new CollisionChecker(eventManager);
				var gameLoop = new GameLoop(eventManager);
				var eventGroup = new SimpleEventGroup();
				var gameStage = new GameStage(eventManager, resourceManager, collisionChecker, boundingRectangle);
				var game = new Game.Game(renderer, eventManager, stageManager);

				eventGroup.AddEventHandler(sdlEventEmitter);
				eventGroup.AddEventHandler(game);
				eventGroup.AddEventHandler(new AfterRender());
				eventGroup.AddEventHandler(new BeforeRender());

				eventManager.AddGroup(eventGroup);
				stageManager.AddStage(gameStage);
				stageManager.AddStage(new GameWonStage(resourceManager));
				stageManager.AddStage(new GameLostStage(resourceManager));

				stageManager.TransitionTo<GameStage>();

				gameLoop.Run();
			}
		}
	}
}