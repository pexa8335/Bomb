using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.Objects;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Tank : BaseObject
    {
        #region số frame lớn nhất
        protected const int MAX_NUMBER_SPRITE_TANK = 7;
        protected const int MAX_NUMBER_SPRITE_EFFECT = 6;
        #endregion
        #region Số làm việc với frame (tank: có 8 frame 0-7; effect: có 6 frame 0-5)
        protected int frx_tank = 7;
        protected int frx_effect = 0;
        protected int fry_effect = 0;
        #endregion
        protected int moveSpeed;
        protected int tankBulletSpeed;
        protected int energy;
        private BulletType bulletType;
        protected Skin skinTank;
        protected bool isMove;
        private bool isActivate;
        protected bool left, right, up, down;
        protected Direction directionTank;
        private List<Bullet> bullets;
        protected Bitmap bmpEffect;
        // contructor
        public Tank()
        {
            this.isActivate = false;
            this.RectWidth = Common.tankSize;
            this.RectHeight = Common.tankSize;
            this.Bullets = new List<Bullet>();
            this.BulletType = BulletType.eTriangleBullet;
        }

        // hiển thị xe tăng
        public override void Show(Bitmap background)
        {
            // nếu xe tăng đang bật chế độ hoạt động sẽ hiển thị xe tăng, 
            // ngược lại hiện thị hiệu ứng xuất hiện
            if (IsActivate)
            {
                switch (directionTank)// dựaa vào để quyết định cách hiển thị xe tăng theo các hướng
                {
                    //Up down left right: lên xuống trái phải.
                    case Direction.eUp:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                               (int)skinTank * Common.tankSize, frx_tank * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                    case Direction.eDown:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                               (MAX_NUMBER_SPRITE_TANK - (int)skinTank) * Common.tankSize, frx_tank * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                    case Direction.eLeft:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                                 frx_tank * Common.tankSize, (MAX_NUMBER_SPRITE_TANK - (int)skinTank) * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                    case Direction.eRight:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                            frx_tank * Common.tankSize, (int)skinTank * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                }
                // nếu xe tăng được di chuyển bánh xe sẽ xoay
                if (this.isMove)
                {
                    frx_tank--;
                    if (frx_tank == -1)
                        frx_tank = MAX_NUMBER_SPRITE_TANK;
                }
                /* Khi xe tăng di chuyển (isMove là true), frx_tank giảm dần, chuyển sang khung hình khác để tạo hiệu ứng xoay của bánh xe.
Khi frx_tank giảm về -1, giá trị sẽ được đặt lại thành MAX_NUMBER_SPRITE_TANK để lặp lại hoạt ảnh.*/

            }
            else
            {
                /*Nếu IsActivate là false, phương thức sẽ hiển thị hiệu ứng xuất hiện thay vì xe tăng. bmpEffect là bitmap của hiệu ứng xuất hiện.*/
                Common.PaintObject(background, this.bmpEffect, this.RectX, this.RectY,
                       frx_effect * this.RectWidth, fry_effect * this.RectHeight, this.RectWidth, this.RectHeight);


                /*Vị trí hiển thị dựa trên frx_effect và fry_effect, hai biến này sẽ thay đổi để lặp qua từng khung hình của hiệu ứng.*/
                frx_effect++;
                if (frx_effect == MAX_NUMBER_SPRITE_EFFECT)
                {
                    frx_effect = 0;
                    fry_effect++;

                    /* Khi frx_effect đạt MAX_NUMBER_SPRITE_EFFECT, nó sẽ được đặt lại thành 0, 
                     * và fry_effect sẽ tăng lên để chuyển sang dòng tiếp theo của ảnh hiệu ứng.*/
                    if (fry_effect == MAX_NUMBER_SPRITE_EFFECT)
                    {
                        fry_effect = 0;
                        /*Khi cả frx_effect và fry_effect đạt giới hạn, điều này có nghĩa hiệu ứng xuất hiện đã hoàn thành. 
                         * Tại đây, IsActivate sẽ được đặt thành true để kích hoạt xe tăng.*/
                        IsActivate = true;
                    }
                }
            }
        }

        // xoay frame xe tăng
        public void RotateFrame()
        {
            // Nếu xe tăng hiện tại hướng xuống (eDown) nhưng nhận lệnh di chuyển sang trái, 
            // hoặc hướng lên mà di chuyển sang phải, 
            // hoặc hướng trái mà di chuyển lên, hoặc hướng phải mà di chuyển xuống, 
            // thì xe tăng cần xoay 90 độ để phù hợp.
            if ((left == true && this.DirectionTank == Direction.eDown) ||
                (right == true && this.DirectionTank == Direction.eUp) ||
                (up == true && this.DirectionTank == Direction.eLeft) ||
                (down == true && this.DirectionTank == Direction.eRight))
            {
                this.bmpObject.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            //xoay 270 độ đối với các trường hợp này
            else if ((left == true && this.DirectionTank == Direction.eUp) ||
               (right == true && this.DirectionTank == Direction.eDown) ||
               (up == true && this.DirectionTank == Direction.eRight) ||
               (down == true && this.DirectionTank == Direction.eLeft))
            {
                this.bmpObject.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            //xoay 180 độ với các case này
            else if ((left == true && this.DirectionTank == Direction.eRight) ||
               (right == true && this.DirectionTank == Direction.eLeft) ||
               (up == true && this.DirectionTank == Direction.eDown) ||
               (down == true && this.DirectionTank == Direction.eUp))
            {
                this.bmpObject.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            //không xoay vì các trường hợp còn lại cùng hướng
            else
            {
                this.bmpObject.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            }
            // cập nhật hướng của xe tăng
            if (left)
                directionTank = Direction.eLeft;
            else if (right)
                directionTank = Direction.eRight;
            else if (up)
                directionTank = Direction.eUp;
            else if (down)
                directionTank = Direction.eDown;
        }

        // tạo đạn cho xe tăng, và thiết lập các thuộc tính ban đầu của viên đạn như hình ảnh,
        // kích thước, năng lượng, tốc độ, và hướng di chuyển dựa trên hướng của xe tăng và loại đạn
        public void CreatBullet(string pathRoundBullet, string pathRocketBullet)
        {
            /* this.bullets.Count == 0 đảm bảo chỉ có một viên đạn tồn tại trong danh sách bullets tại một thời điểm 
             * xe tăng chỉ bắn được một viên đạn tại một thời điểm.
              Và this.IsActivate để đảm bảo xe tăng đã được kích hoạt trước khi tạo đạn.*/

            if (this.bullets.Count == 0 && this.IsActivate)
            {
                // đạn
                Bullet bullet;
                bullet = new Bullet();

                //đặt tốc độ cho viên đạn bằng với tốc độ đạn của xe tăng (TankBulletSpeed).
                bullet.SpeedBullet = this.TankBulletSpeed;

                // set loại bullet
                switch (this.bulletType)
                {
                    case BulletType.eTriangleBullet:
                        bullet.LoadImage(Common.path + pathRoundBullet);
                        // đạn tam giác có kích thước 8x8
                        bullet.RectWidth =8;
                        bullet.RectHeight = 8;
                        // năng lượng của đạn tam giác mặc định là 10
                        bullet.Power = 10;
                        break;
                    case BulletType.eRocketBullet:
                        bullet.LoadImage(Common.path + pathRocketBullet);
                        // đạn rocket có kích thước 12x12
                        bullet.RectWidth = 12;
                        bullet.RectHeight = 12;
                        // năng lượng của đạn rocket mặc định là 40
                        bullet.Power = 30;
                        break;
                }
                // hướng của xe tăng
                /*Direction.eLeft: Đạn di chuyển sang trái, xoay 270 độ. Vị trí RectX và RectY của viên đạn được tính toán để nó xuất phát từ mép trái của xe tăng.*/
                switch (directionTank)
                {
                    case Direction.eLeft:
                        bullet.DirectionBullet = Direction.eLeft;
                        bullet.BmpObject.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        bullet.RectX = this.RectX + bullet.RectWidth;
                        bullet.RectY = this.RectY + this.RectHeight / 2 - bullet.RectHeight / 2;
                        break;

                    /*Direction.eRight: Đạn di chuyển sang phải, xoay 90 độ. Viên đạn xuất phát từ mép phải của xe tăng.*/
                    case Direction.eRight:
                        bullet.DirectionBullet = Direction.eRight;
                        bullet.BmpObject.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        bullet.RectX = this.RectX + this.RectWidth - bullet.RectWidth;
                        bullet.RectY = this.RectY + this.RectHeight / 2 - bullet.RectHeight / 2;
                        break;

                    /*Direction.eUp: Đạn di chuyển lên trên, không xoay thêm. Viên đạn xuất phát từ đỉnh của xe tăng.*/
                    case Direction.eUp:
                        bullet.DirectionBullet = Direction.eUp;
                        bullet.RectY = this.RectY + bullet.RectHeight;
                        bullet.RectX = this.RectX + this.RectWidth / 2 - bullet.RectWidth / 2;
                        break;

                    /*Direction.eDown: Đạn di chuyển xuống, xoay 180 độ. Viên đạn xuất phát từ đáy của xe tăng*/
                    case Direction.eDown:
                        bullet.DirectionBullet = Direction.eDown;
                        bullet.BmpObject.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bullet.RectY = this.RectY + this.RectHeight - bullet.RectHeight;
                        bullet.RectX = this.RectX + this.RectWidth / 2 - bullet.RectWidth / 2;
                        break;
                }
                this.bullets.Add(bullet);
                bullet = null;
            }
        }

        // hủy một viên đạn
        public void RemoveOneBullet(int index)
        {
            this.bullets[index] = null;
            this.bullets.RemoveAt(index);
        }

        // di chuyển và hiển thị đạn xe tăng
        public void ShowBulletAndMove(Bitmap background)
        {
            for (int i = 0; i < this.Bullets.Count; i++)
            {
                this.Bullets[i].BulletMove();
                this.Bullets[i].Show(background);
            }
        }

        // kiểm tra va chạm của xe tăng với một đối tượng
        //code va chạm để nếu trả về true thì hai xe không đè được qua nhau
        public bool IsObjectCollision(Rectangle rectObj)
        {
            switch (this.directionTank)
            {
                //Góc trái của vật thể (this) = góc phải của vật thể khác thì nếu
                //1. Góc trên của vật thể (this) >= góc trên vật thể khác và góc trên của this bé hơn góc dưới của vật thể khác
                //2,3 các case còn lại
                //thì coi là va chạm -> không cho đi xuyên qua
                case Direction.eLeft:
                    if (this.Rect.Left == rectObj.Right)
                        if (this.Rect.Top >= rectObj.Top && this.Rect.Top < rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Top && this.Rect.Bottom <= rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Bottom && this.Rect.Top < rectObj.Top)
                        {
                            return true;
                        }
                    break;
                case Direction.eRight:
                    if (this.Rect.Right == rectObj.Left)
                        if (this.Rect.Top >= rectObj.Top && this.Rect.Top < rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Top && this.Rect.Bottom <= rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Bottom && this.Rect.Top < rectObj.Top)
                        {
                            return true;
                        }
                    break;
                case Direction.eUp:
                    if (this.Rect.Top == rectObj.Bottom)
                        if (this.Rect.Left < rectObj.Right && this.Rect.Left >= rectObj.Left ||
                            this.Rect.Right > rectObj.Left && this.Rect.Right <= rectObj.Right ||
                            this.Rect.Right > rectObj.Right && this.Rect.Left < rectObj.Left)
                        {
                            return true;
                        }
                    break;
                case Direction.eDown:
                    if (this.Rect.Bottom == rectObj.Top)
                        if (this.Rect.Left < rectObj.Right && this.Rect.Left >= rectObj.Left ||
                            this.Rect.Right > rectObj.Left && this.Rect.Right <= rectObj.Right ||
                            this.Rect.Right >= rectObj.Right && this.Rect.Left <= rectObj.Left)
                        {
                            return true;
                        }
                    break;
            }
            return false;
        }

        // kiểm tra xe tăng chạm tường
        public bool IsWallCollision(List<Wall> walls, Direction directionTank)
        {
            foreach (Wall wall in walls)
                // nếu không phải bụi cây thì xét va chạm là true -> không vượt qua được (cây là wall loại 4 - image wall.4)
                if (wall.WallNumber != 4)
                    if (IsObjectCollision(wall.Rect))
                        return true;
            return false;
        }

        // xe tăng di chuyển
        public void Move()
        {
            if (this.IsActivate)// Chỉ di chuyển nếu xe tăng đang kích hoạt
            {
                if (left)
                {
                    this.RectX -= this.MoveSpeed;
                }
                else if (right)
                {
                    this.RectX += this.MoveSpeed;
                }
                else if (up)
                {
                    this.RectY -= this.MoveSpeed;
                }
                else if (down)
                {
                    this.RectY += this.MoveSpeed;
                }
            }
        }

        #region properties
        //các thuộc tính: cung cấp giao diện cho các thuộc tính riêng tư của lớp Tank và cho phép truy xuất hoặc cập nhật giá trị của chúng.
        public int MoveSpeed
        {
            get
            {
                return moveSpeed;
            }

            set
            {
                moveSpeed = value;
            }
        }
        public int TankBulletSpeed
        {
            get
            {
                return tankBulletSpeed;
            }

            set
            {
                tankBulletSpeed = value;
            }
        }
        public int Energy
        {
            get
            {
                return energy;
            }

            set
            {
                energy = value;
            }
        }
        public bool Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
            }
        }
        public bool Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }
        public bool Up
        {
            get
            {
                return up;
            }

            set
            {
                up = value;
            }
        }
        public bool Down
        {
            get
            {
                return down;
            }

            set
            {
                down = value;
            }
        }
        public Direction DirectionTank
        {
            get
            {
                return directionTank;
            }

            set
            {
                directionTank = value;
            }
        }
        public bool IsMove
        {
            get
            {
                return isMove;
            }

            set
            {
                isMove = value;
            }
        }
        public List<Bullet> Bullets
        {
            get
            {
                return bullets;
            }

            set
            {
                bullets = value;
            }
        }
        public Skin SkinTank
        {
            get
            {
                return skinTank;
            }

            set
            {
                skinTank = value;
            }
        }

        public bool IsActivate
        {
            get
            {
                return isActivate;
            }

            set
            {
                isActivate = value;
            }
        }

        public BulletType BulletType {
            get
            {
                return bulletType;
            }
            set
            {
                bulletType = value;
            }
        }
        #endregion property
    }
}
