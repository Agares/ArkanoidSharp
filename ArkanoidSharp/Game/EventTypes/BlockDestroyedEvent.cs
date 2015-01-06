using Agares.Engine.Events;

namespace ArkanoidSharp.Game.EventTypes
{
	public struct BlockDestroyedEvent : IEvent
	{
		public BlockGroup BlockGroup { get; private set; }

		public BlockDestroyedEvent(BlockGroup blockGroup) : this()
		{
			BlockGroup = blockGroup;
		}
	}
}