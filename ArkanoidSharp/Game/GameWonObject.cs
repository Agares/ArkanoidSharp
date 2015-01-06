using Agares.Engine;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Stages;

namespace ArkanoidSharp.Game
{
	public class GameWonObject : IStageObject, IEventHandler<DrawingEvent>
	{
		const string TextureId = "gamewon";

		private readonly Texture _texture;

		public GameWonObject(IResourceManager resourceManager)
		{
			_texture = resourceManager.LoadTexture(TextureId);
		}

		public void HandleEvent(DrawingEvent @event)
		{
			@event.Renderer.DrawTexture(_texture);
		}
	}
}