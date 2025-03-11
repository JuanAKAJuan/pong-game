using Microsoft.Xna.Framework;

namespace Ping_Pong
{
    internal class GameObject
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public Rectangle Rect => new((int)X, (int)Y, (int)Width, (int)Height);

        public object Visual { get; set; }
    }
}
