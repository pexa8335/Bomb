using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Bullet : BaseObject
    {
        // hướng di chuyển của viên đạn
        private Direction directionBullet;
        // tốc độ di chuyển của viên đạn
        private int speedBullet;
        // sức mạnh của viên đạn
        private int power;
            
        // viên đạn di chuyển
        public void BulletMove()
        {
            // di chuyển theo hướng của viên đạn
            switch (directionBullet)
            {
                // di chuyển theo hướng trái
                case Direction.eLeft:
                    // giảm tọa độ x của viên đạn
                    this.RectX -= speedBullet;
                    break;
                // di chuyển theo hướng phải
                case Direction.eRight:
                    // tăng tọa độ x của viên đạn
                    this.RectX += speedBullet;
                    break;
                // di chuyển theo hướng lên
                case Direction.eUp:
                    // giảm tọa độ y của viên đạn
                    this.RectY -= speedBullet;
                    break;
                // di chuyển theo hướng xuống
                case Direction.eDown:
                    // tăng tọa độ y của viên đạn
                    this.RectY += speedBullet;
                    break;
            }
        }

        #region properties
        // các thuộc tính
        public Direction DirectionBullet
        {
            // lấy hướng di chuyển của viên đạn
            get
            {
                return directionBullet;
            }
            // thiết lập hướng di chuyển của viên đạn
            set
            {
                directionBullet = value;
            }
        }
        // tốc độ di chuyển của viên đạn
        public int SpeedBullet
        {
            // lấy tốc độ di chuyển của viên đạn
            get
            {
                return speedBullet;
            }
            // thiết lập tốc độ di chuyển của viên đạn
            set
            {
                speedBullet = value;
            }
        }
        // sức mạnh của viên đạn
        public int Power 
        {
            // lấy sức mạnh của viên đạn
            get
            {
                return power;
            }
            // thiết lập sức mạnh của viên đạn
            set
            {
                power = value;
            }
        }
        #endregion
    }
}
