using Microsoft.Xna.Framework;

namespace MonoGame.Extended.Tiled
{
    public abstract class TiledMapLayer
    {
        public string Name { get; }

        public bool IsVisible { get; set; }

        public float Opacity { get; set; }

        public Vector2 Offset { get; set; }

        public TiledMapProperties Properties { get; }

        public TiledMap Owner { get; }

        public TiledMapLayer ParentLayer { get; set; }

        protected TiledMapLayer(TiledMap owner, string name, Vector2? offset = null, float opacity = 1.0f, bool isVisible = true)
        {
            Owner = owner;
            Name = name;
            Offset = offset ?? Vector2.Zero;
            Opacity = opacity;
            IsVisible = isVisible;
            Properties = new TiledMapProperties();
        }
    }
}
