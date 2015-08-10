﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace MonoGame.Extended.Tiled
{
    public class TiledLayer
    {
        public TiledLayer(TiledMap tiledMap, GraphicsDevice graphicsDevice, string name, int width, int height, int[] data)
        {
            Name = name;
            Width = width;
            Height = height;
            Properties = new TiledProperties();
            
            _tiledMap = tiledMap;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _tiles = CreateTiles(data);
        }

        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public TiledProperties Properties { get; private set; }

        private readonly TiledMap _tiledMap;
        private readonly TiledTile[] _tiles;
        private readonly SpriteBatch _spriteBatch;

        private TiledTile[] CreateTiles(int[] data)
        {
            var tiles = new TiledTile[data.Length];
            var index = 0;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    tiles[x + y * Width] = new TiledTile(data[index], x, y);
                    index++;
                }
            }

            return tiles;
        }

        public void Draw(Camera2D camera)
        {
            var renderOrderFunction = GetRenderOrderFunction();

            _spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());

            foreach (var tile in renderOrderFunction())
            {
                var region = _tiledMap.GetTileRegion(tile.Id);

                if (region != null)
                {
                    // not exactly sure why we need to compensate 1 pixel here. Could be a bug in MonoGame?
                    var tx = tile.X * (_tiledMap.TileWidth - 1);
                    var ty = tile.Y * (_tiledMap.TileHeight - 1);
                        
                    _spriteBatch.Draw(region, new Rectangle(tx, ty, region.Width, region.Height), Color.White);
                }
            }

            _spriteBatch.End();
        }
        
        public TiledTile GetTile(int x, int y)
        {
            return _tiles[x + y * Width];
        }

        private Func<IEnumerable<TiledTile>> GetRenderOrderFunction()
        {
            switch (_tiledMap.RenderOrder)
            {
                case TiledMapRenderOrder.LeftDown:
                    return GetTilesLeftDown;
                case TiledMapRenderOrder.LeftUp:
                    return GetTilesLeftUp;
                case TiledMapRenderOrder.RightDown:
                    return GetTilesRightDown;
                case TiledMapRenderOrder.RightUp:
                    return GetTilesRightUp;
            }

            throw new NotSupportedException(string.Format("{0} is not supported", _tiledMap.RenderOrder));
        }

        private IEnumerable<TiledTile> GetTilesRightDown()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                    yield return GetTile(x, y);
            }
        }

        private IEnumerable<TiledTile> GetTilesRightUp()
        {
            for (var y = Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < Width; x++)
                    yield return GetTile(x, y);
            }
        }

        private IEnumerable<TiledTile> GetTilesLeftDown()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = Width - 1; x >= 0; x--)
                    yield return GetTile(x, y);
            }
        }
        
        private IEnumerable<TiledTile> GetTilesLeftUp()
        {
            for (var y = Height - 1; y >= 0; y--)
            {
                for (var x = Width - 1; x >= 0; x--)
                    yield return GetTile(x, y);
            }
        }
    }
}