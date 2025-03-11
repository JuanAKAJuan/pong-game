namespace Ping_Pong
{
    class Ball : GameObject
    {
        private float _dx;
        public float Dx
        {
            get => _dx;
            set => _dx = value;
        }

        private float _dy;
        public float Dy
        {
            get => _dy;
            set => _dy = value;
        }
    }
}
