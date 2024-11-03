using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Item : BaseObject
    {
        #region hằng số vị trí mặc đinh
        private const int x_default = -20;
        private const int y_default = -20;
        #endregion  hằng số vị trí mặc đinh

        // biến kiểm tra vật phẩm có được hiển
        private bool isOn;
        // loại vật phẩm
        private ItemType itemType;

        // constructor
        public Item()
        {
            // khởi tạo vị trí mặc định
            this.RectX = x_default;
            // khởi tạo vị trí mặc định
            this.RectY = y_default;
            // khởi tạo kích thước mặc định
            this.RectWidth = this.RectHeight = Common.tankSize;
            // khởi tạo vật phẩm không hiển thị
            isOn = false;
        }

        // hiển thị vật phẩm ra giao diện chơi
        public void CreateItem(Point itemPoint)
        {
            // tạo vật phẩm và bắt đầu chạy thời gian hiển thị vật phẩm
            // tìm vị trí cho item
            this.RectX = itemPoint.X;
            this.RectY = itemPoint.Y;
            // hiển thị vật phẩm
            Random rand = new Random();
            // chọn ngẫu nhiên loại vật phẩm
            switch (rand.Next(0, 4))
            {
                case 0:
                    // loại vật phẩm là máu
                    this.ItemType = ItemType.eItemHeart;
                    this.LoadImage(Common.path + @"\Images\icon_heart.png");
                    break;
                case 1:
                    // loại vật phẩm là khiên
                    this.ItemType = ItemType.eItemShield;
                    this.LoadImage(Common.path + @"\Images\icon_shield.png");
                    break;
                case 2:
                    // loại vật phẩm là bom
                    this.ItemType = ItemType.eItemGrenade;
                    this.LoadImage(Common.path + @"\Images\icon_grenade.png");
                    break;
                case 3:
                    // loại vật phẩm là tên lửa
                    this.ItemType = ItemType.eItemRocket;
                    this.LoadImage(Common.path + @"\Images\icon_rocket.png");
                    break;
            }
            // hiển thị vật phẩm trống 
            rand = null;
        }

        #region properties
        // getter, setter
        public ItemType ItemType
        {
            // lấy loại vật phẩm
            get
            {
                return itemType;
            }
            // thiết lập loại vật phẩm
            set
            {
                itemType = value;
            }
        }
        // getter, setter
        public bool IsOn
        {
            // lấy trạng thái hiển thị vật phẩm
            get
            {
                return isOn;
            }
            // thiết lập trạng thái hiển thị vật phẩm
            set
            {
                isOn = value;
            }
        }
        #endregion properties

    }
}
