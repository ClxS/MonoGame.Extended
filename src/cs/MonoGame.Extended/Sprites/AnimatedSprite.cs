using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Sprites
{
    public class AnimatedSprite : Sprite
    {
        private readonly SpriteSheet _spriteSheet;
        private SpriteSheetAnimation _currentAnimation;

        public AnimatedSprite(SpriteSheet spriteSheet, string playAnimation = null)
            : base(spriteSheet.TextureAtlas[0])
        {
            _spriteSheet = spriteSheet;

            if (playAnimation != null)
                Play(playAnimation);
        }

        public SpriteSheetAnimation Play(string name, Action onCompleted = null)
        {
            if (this._currentAnimation != null && !this._currentAnimation.IsComplete &&
                this._currentAnimation.Name == name)
            {
                return this._currentAnimation;
            }

            if (!this._spriteSheet.Cycles.TryGetValue(name, out SpriteSheetAnimationCycle cycle))
            {
                return this._currentAnimation;
            }

            (TextureRegion2D, bool IsCellMirrored)[] keyFrames = cycle.Frames.Select(f => (this._spriteSheet.TextureAtlas[f.Index], f.IsCellMirrored))
                .ToArray();
            this._currentAnimation = new SpriteSheetAnimation(
                name,
                keyFrames,
                cycle.FrameDuration,
                cycle.IsLooping,
                cycle.IsReversed,
                cycle.IsPingPong,
                cycle.IsMirrored);

            if (this._currentAnimation != null)
                this._currentAnimation.OnCompleted = onCompleted;

            return _currentAnimation;
        }

        public void Update(float deltaTime)
        {
            if (_currentAnimation != null && !_currentAnimation.IsComplete)
            {
                _currentAnimation.Update(deltaTime);
                TextureRegion = _currentAnimation.CurrentFrame;
                Effect = _currentAnimation.IsCurrentCellMirrored
                    ? SpriteEffects.FlipHorizontally
                    : SpriteEffects.None;
            }
        }

        public void Update(GameTime gameTime)
        {
            Update(gameTime.GetElapsedSeconds());
        }
    }
}
