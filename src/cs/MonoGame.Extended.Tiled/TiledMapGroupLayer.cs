using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame.Extended.Tiled
{
	public class TiledMapGroupLayer : TiledMapLayer
	{
        public TiledMapGroupLayer(TiledMap owner, string name, List<TiledMapLayer> layers, Vector2? offset = null, float opacity = 1, bool isVisible = true)
			: base(owner, name, offset, opacity, isVisible)
		{
            this.Layers = layers;

            foreach (TiledMapLayer layer in layers)
            {
                layer.ParentLayer = this;
            }
        }

        public List<TiledMapLayer> Layers { get; }
	}
}
