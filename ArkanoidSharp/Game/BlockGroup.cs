using System.Collections.ObjectModel;
using Agares.Engine;
using Agares.Engine.Events;
using Agares.Engine.Events.EventTypes;
using Agares.Engine.Geometry;

namespace ArkanoidSharp.Game
{
	public class BlockGroup : IEventHandler<DrawingEvent>
	{
		private const string TextureId = "block";
		private readonly Texture _blockTexture;
		public ObservableCollection<Block> Blocks { get; private set; }

		public BlockGroup(IResourceManager resourceManager)
		{
			_blockTexture = resourceManager.LoadTexture(TextureId);
			Blocks = new ObservableCollection<Block>();

			for (int i = 0; i < 70; i += 70)
			{
				for (int j = 0; j < 50; j += 30)
				{
					var block = new Block(new Point(i, j), this);
					Blocks.Add(block);
				}
			}
		}

		public void RemoveBlock(Block block)
		{
			Blocks.Remove(block);
		}

		public void HandleEvent(DrawingEvent @event)
		{
			foreach (var block in Blocks)
			{
				@event.Renderer.DrawTexture(_blockTexture, Rectangle.FromPositionAndSize(0, 0, _blockTexture.Width, _blockTexture.Height), block.BoundingRectangle);
			}
		}
	}
}