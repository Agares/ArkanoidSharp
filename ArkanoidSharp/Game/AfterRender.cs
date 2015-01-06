using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;

namespace ArkanoidSharp.Game
{
	public class AfterRender : IEventHandler<DrawingEvent>
	{
		[EventPriority(EventPriority.High)]
		public void HandleEvent(DrawingEvent @event)
		{
			@event.Renderer.Present();
		} 
	}
}