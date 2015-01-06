using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;

namespace ArkanoidSharp.Game
{
	public class BeforeRender : IEventHandler<DrawingEvent>
	{
		[EventPriority(EventPriority.Low)]
		public void HandleEvent(DrawingEvent @event)
		{
			@event.Renderer.Clear();
		}
	}
}