using System.Xml.Serialization;

namespace MonoGame.Extended.Tiled;

public enum TileRenderSize
{
    [XmlEnum("tile")]
    Tile,

    [XmlEnum("grid")]
    Grid,
}
