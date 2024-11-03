using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.General;
using System.Windows.Forms;

namespace SuperTank.Objects
{
    class PlayerTank : Tank
    {
        // số lượng sprite của xe tăng
        // sprite của xe tăng chạy từ 0 -> 3
        // sprite lớn nhất là 3


        // Số sprite trong ngữ cảnh lập trình game và đồ họa máy tính là
        // số lượng các hình ảnh hoặc đối tượng đồ họa được sử dụng trong
        // một khung cảnh hoặc trò chơi. Sprite là hình ảnh 2D đại diện cho
        // một đối tượng trong trò chơi, chẳng hạn như nhân vật, vật phẩm,
        // hiệu ứng hoặc các yếu tố khác trong môi trường. Các sprite thường
        // được vẽ riêng lẻ và sau đó được kết hợp với nhau để tạo ra các khung
        // cảnh phức tạp hơn hoặc để biểu diễn chuyển động và hoạt ảnh.
        private bool isShield;

        // số sprite của xe tăng
        private Bitmap bmpShield;

       
        public PlayerTank()
        {
            // khởi tạo các thông số mặc định của xe tăng player
            this.moveSpeed = 10;
            // tốc độ di chuyển của xe tăng player
            this.tankBulletSpeed = 20;
            // tốc độ đạn của xe tăng player
            this.energy = 100;
            // năng lượng của xe tăng player
            this.SetLocation();
            // vị trí của xe tăng player
            this.DirectionTank = Direction.eUp;
            // hướng di chuyển của xe tăng player (luôn hướng lên trên)
            this.SkinTank = Skin.eYellow;
            // màu sắc của xe tăng player
            bmpEffect = new Bitmap(Common.path + @"\Images\effect1.png");
            bmpShield = new Bitmap(Common.path + @"\Images\shield.png");
        }

        // cập nhật vị trí xe tăng player
        public void SetLocation()
        {
            int i = 17, j = 36;
            this.RectX = i * Common.STEP;
            this.RectY = j * Common.STEP;
        }

        // kiểm tra xe tăng player va chạm với xe tăng địch
        public bool IsEnemyTankCollisions(List<EnemyTank> enemyTanks)
        {
            foreach (EnemyTank enemyTank in enemyTanks)
            {
                if (this.IsObjectCollision(enemyTank.Rect))
                    return true;
            }
            return false;
        }

        // hiển thị xe tăng player
        public override void Show(Bitmap background)
        {
            // nếu xe tăng đang bật chế độ hoạt động sẽ hiển thị xe tăng, 
            // ngược lại hiện thị hiệu ứng xuất hiện
            if (IsActivate)
            {
                switch (directionTank)
                {
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
                // nếu xe tăng player đang ở chế độ được bảo vệ -> show vòng tròn bảo vệ
                if (this.isShield)
                {
                    Common.PaintObject(background, this.bmpShield, rect.X, rect.Y, 0, 0, 40, 40);
                }
                //nếu xe tăng được di chuyển bánh xe sẽ xoay
                if (this.isMove)
                {
                    frx_tank--;
                    if (frx_tank == -1)
                        frx_tank = MAX_NUMBER_SPRITE_TANK;
                }
            }
            else
            {
                // hiển thị hiệu ứng xuất hiện
                Common.PaintObject(background, this.bmpEffect, this.RectX, this.RectY,
                       frx_effect * this.RectWidth, fry_effect * this.RectHeight, this.RectWidth, this.RectHeight);
                frx_effect++;
                if (frx_effect == MAX_NUMBER_SPRITE_EFFECT)
                {
                    frx_effect = 0;
                    fry_effect++;
                    if (fry_effect == MAX_NUMBER_SPRITE_EFFECT)
                    {
                        fry_effect = 0;
                        // hiệu ứng kết thúc, bật lại hoạt động của xe
                        IsActivate = true;
                    }
                }
            }
        }

        #region properties
        // getter, setter
        public bool IsShield
        {
            // lấy loại vật phẩm
            get { return isShield; }
            // thiết lập loại vật phẩm
            set { isShield = value; }
        }
        #endregion properties
    }
}
