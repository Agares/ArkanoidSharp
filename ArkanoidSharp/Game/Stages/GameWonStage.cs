using Agares.Engine;
using Agares.Engine.Stages;

namespace ArkanoidSharp.Game.Stages
{
	public class GameWonStage : Stage
	{
		public GameWonStage(IResourceManager resourceManager)
		{
			AddObject(new GameWonObject(resourceManager));
		}
	}
}