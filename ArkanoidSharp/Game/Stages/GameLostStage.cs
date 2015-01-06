using Agares.Engine;
using Agares.Engine.Stages;

namespace ArkanoidSharp.Game.Stages
{
	public class GameLostStage : Stage
	{
		public GameLostStage(IResourceManager resourceManager)
		{
			AddObject(new GameLostObject(resourceManager));
		}
	}
}