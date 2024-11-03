using SuperTank.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SuperTank.General;

namespace SuperTank
{
    class EnemyTank : Tank
    {
        // loại xe tăng địch
        private EnemyTankType enemyType;
        // khoảng cách giữa xe tăng địch và xe tăng player
        private int enemyTankDistance;
        public EnemyTank()
        {
            // khởi tạo các giá trị mặc định
            // giá trị mặc định của xe tăng địch
            bmpEffect = new Bitmap(Common.path + @"\Images\effect2.png");
        }

        // kiểm tra xe tăng đich va chạm xe tăng player
        public bool IsPlayerTankCollision(PlayerTank playerTank)
        {
            // kiểm tra va chạm giữa xe tăng địch và xe tăng player
            if (this.IsObjectCollision(playerTank.Rect))
                // nếu va chạm thì trả về true
                return true;
            // nếu không va chạm thì trả về false
            return false;
        }

        // kiểm tra xe tăng địch va chạm với xe tăng địch đồng minh
        public bool IsAlliedTanksCollision(List<EnemyTank> alliedTanks)
        {
            // kiểm tra va chạm giữa xe tăng địch và xe tăng đồng minh
            foreach (EnemyTank enemyTank in alliedTanks)
            {
                // nếu va chạm thì trả về true
                if (this.IsObjectCollision(enemyTank.Rect))
                    return true;
            }
            // nếu không va chạm thì trả về false
            return false;
        }

        #region bộ não xử lí cách di chuyển của xe tăng địch
        // xử lí di chuyển của xe tăng type = normal
        public bool HandleMoveNormal(List<Wall> walls, PlayerTank playerTank, List<EnemyTank> alliedTanks)
        {
            // kiểm tra xe tăng địch có va chạm tường
            bool isWallCollision;
            // kiểm tra xe tăng địch có va chạm xe tăng player
            bool isPlayerTankCollision;
            // kiểm tra xe tăng địch có va chạm với xe tăng địch đồng minh
            bool isAlliedTanksCollision;
            // kiểm tra xe tăng địch có va chạm với tường hay ko
            isWallCollision = this.IsWallCollision(walls, this.directionTank);
            // kiểm tra xe tăng địch có va chạm xe tăng player
            isPlayerTankCollision = this.IsPlayerTankCollision(playerTank);
            // kiểm tra xe tăng địch có va chạm với xe tắng địch đồng minh
            isAlliedTanksCollision = this.IsAlliedTanksCollision(alliedTanks);
            // nếu va chạm tường, player hoặc xe tăng đồng minh của địch thì xử lí đổi hướng
            if (isWallCollision || isAlliedTanksCollision || isPlayerTankCollision)
            {
                Random rand = new Random();
                // random ngẫu nhiên hướng di chuyển (0: left; 1:right; 2: up; 3: down)
                switch (rand.Next(0, 4))
                {
                    // di chuyển theo hướng trái
                    case 0:
                        // hướng di chuyển của xe tăng
                        Left = true;
                        // các hướng còn lại bằng false
                        Right = Up = Down = false;
                        break;
                    // di chuyển theo hướng phải
                    case 1:
                        // hướng di chuyển của xe tăng
                        Right = true;
                        // các hướng còn lại bằng false
                        Left = Up = Down = false;
                        break;
                    // di chuyển theo hướng lên
                    case 2:
                        // hướng di chuyển của xe tăng
                        Up = true;
                        // các hướng còn lại bằng false
                        Left = Right = Down = false;
                        break;
                    // di chuyển theo hướng xuống
                    case 3:
                        // hướng di chuyển của xe tăng
                        Down = true;
                        // các hướng còn lại bằng false
                        Left = Right = Up = false;
                        break;
                }
                // xoay frame của xe tăng
                this.RotateFrame();
                //giải phóng bộ nhớ
                rand = null;
                return false;
            }
            // nếu không va chạm với tường, player hoặc xe tăng đồng minh thì xe tăng địch được phép di chuyển
            else
            {
                // xe tăng được phép di chuyển khi không va chạm gì
                this.IsMove = true;
                return true;
            }
            
        }


        // xử lí di chuyển của xe tăng type = medium
        bool isPriority = false;
        public bool HandleMoveMedium(List<Wall> walls, PlayerTank playerTank, List<EnemyTank> alliedTanks)
        {
            // kiểm tra xe tăng địch có va chạm tường
            bool isWallCollision;
            // kiểm tra xe tăng địch có va chạm xe tăng player
            bool isPlayerTankCollision;
            // kiểm tra xe tăng địch có va chạm với xe tắng địch đồng minh
            bool isAlliedTanksCollision;
            bool flag;
            flag = false;

            isWallCollision = this.IsWallCollision(walls, this.directionTank);
            isPlayerTankCollision = this.IsPlayerTankCollision(playerTank);
            isAlliedTanksCollision = this.IsAlliedTanksCollision(alliedTanks);
            // nếu va chạm tường hoặc xe tăng đồng minh của địch thì xử lí đổi hướng
            if ((isWallCollision || isAlliedTanksCollision || isPlayerTankCollision) && isPriority == false)
            {
                Random rand = new Random();
                // random ngẫu nhiên hướng di chuyển (0: left; 1:right; 2: up; 3: down)
                switch (rand.Next(0, 4))
                {
                    case 0:
                        Left = true;
                        Right = Up = Down = false;
                        break;
                    case 1:
                        Right = true;
                        Left = Up = Down = false;
                        break;
                    case 2:
                        Up = true;
                        Left = Right = Down = false;
                        break;
                    case 3:
                        Down = true;
                        Left = Right = Up = false;
                        break;
                }
                this.RotateFrame();
                rand = null;
                return false;
            }
            else
            //nếu xe không va chạm với tường hoặc xe tăng đông minh thì xử lí di chuyển
            if (playerTank.RectX != 17 * Common.STEP || playerTank.RectY != 36 * Common.STEP)
            {
                // nếu xe tăng địch ở cùng hàng với xe tăng player
                if (this.Rect.Top + this.Rect.Height / 2 > playerTank.Rect.Top &&
                   this.Rect.Top + this.Rect.Height / 2 < playerTank.Rect.Bottom &&
                   this.RectX > playerTank.RectX)
                {
                    // hướng di chuyển của xe tăng
                    Left = true;
                    // các hướng còn lại bằng false
                    Down = Up = Right = false;
                    // cờ hiệu
                    flag = true;
                }
                else
                // nếu xe tăng địch ở cùng cột với xe tăng player
                   if (this.Rect.Top + this.Rect.Height / 2 > playerTank.Rect.Top &&
                   this.Rect.Top + this.Rect.Height / 2 < playerTank.Rect.Bottom &&
                   this.RectX < playerTank.RectX)
                {
                    // hướng di chuyển của xe tăng
                    Right = true;
                    // các hướng còn lại bằng false
                    Down = Up = Left = false;
                    // cờ hiệu
                    flag = true;
                }
                else
                // nếu xe tăng địch ở trên xe tăng player
                   if (this.Rect.Left + this.Rect.Width / 2 > playerTank.Rect.Left &&
                   this.Rect.Left + this.Rect.Width / 2 < playerTank.Rect.Right &&
                   this.RectY > playerTank.RectY)
                {
                    // hướng di chuyển của xe tăng
                    Up = true;
                    // các hướng còn lại bằng false
                    Left = Down = Right = false;
                    // cờ hiệu
                    flag = true;
                }
                else
                // nếu xe tăng địch ở dưới xe tăng player
                   if (this.Rect.Left + this.Rect.Width / 2 > playerTank.Rect.Left &&
                   this.Rect.Left + this.Rect.Width / 2 < playerTank.Rect.Right &&
                   this.RectY < playerTank.RectY)
                {
                    // hướng di chuyển của xe tăng
                    Down = true;
                    // các hướng còn lại bằng false
                    Left = Up = Right = false;
                    // cờ hiệu
                    flag = true;
                }
                // nếu xe tăng địch ở cùng hàng hoặc cùng cột với xe tăng player thì xe tăng địch di chuyển theo hướng của xe tăng player
                if (flag)
                {
                    // xoay frame của xe tăng
                    isPriority = true;
                    //giải phóng bộ nhớ
                    flag = false;
                    // xe tăng được phép di chuyển khi không va chạm gì
                    this.RotateFrame();
                    //giải phóng bộ nhớ
                    this.isMove = false;
                    //giải phóng bộ nhớ
                    return false;
                }
                // nếu xe tăng địch không ở cùng hàng hoặc cùng cột với xe tăng player thì xe tăng địch di chuyển ngẫu nhiên
                else
                {
                    // nếu xe tăng địch không va chạm với tường hoặc xe tăng đồng minh thì xe tăng địch được phép di chuyển
                    if (isPriority == true)
                    {
                        // xe tăng được phép di chuyển khi không va chạm gì
                        isPriority = false;
                        return false;
                    }
                    //nếu xe tăng địch va chạm với tường hoặc xe tăng đồng minh thì xe tăng địch được phép di chuyển
                    else
                    {
                        // xe tăng được phép di chuyển khi không va chạm gì
                        this.isMove = true;
                        return true;
                    }
                }
            }
            // nếu xe tăng player ở vị trí (17, 36) thì xe tăng địch di chuyển ngẫu nhiên
            else
            {
                
                isPriority = false;
                this.isMove = true;
                return true;
            }
        }
        #endregion bộ não xử lí cách di chuyển của xe tăng địch

        #region properties
        public EnemyTankType EnemyType
        {
            get
            {
                return enemyType;
            }

            set
            {
                enemyType = value;
            }
        }
        public int EnemyTankDistance
        {
            get
            {
                return enemyTankDistance;
            }

            set
            {
                enemyTankDistance = value;
            }
        }
        #endregion properties
    }
}
