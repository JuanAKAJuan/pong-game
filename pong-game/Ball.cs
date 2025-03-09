using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ping_Pong
{
    class Ball : GameObject
    {
        protected float _DX;
        public float DX
        {
            get { return _DX; }
            set { _DX = value; }
        }

        protected float m_DY;
        public float DY
        {
            get { return m_DY; }
            set { m_DY = value; }
        }
    }
}
