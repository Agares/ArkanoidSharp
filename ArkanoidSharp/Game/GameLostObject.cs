using Agares.Engine;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Stages;

namespace ArkanoidSharp.Game
{
	public class GameLostObject : IStageObject, IEventHandler<DrawingEvent>
	{
		private const string TextureId = "gameover";
		private readonly Texture _texture;

		public GameLostObject(IResourceManager resourceManager)
		{
			_texture = resourceManager.LoadTexture(TextureId);
		}

		public void HandleEvent(DrawingEvent @event)
		{
			@event.Renderer.DrawTexture(_texture);
		}
	}
}