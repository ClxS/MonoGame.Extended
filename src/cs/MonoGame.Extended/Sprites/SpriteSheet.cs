using System.Collections.Generic;
using System.Linq;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Sprites
{
    public class SpriteSheet
    {
        public SpriteSheet()
        {
            Cycles = new Dictionary<string, SpriteSheetAnimationCycle>();
        }

        public TextureAtlas TextureAtlas { get; set; }

        public Dictionary<string, SpriteSheetAnimationCycle> Cycles { get; set; }
    }
}
