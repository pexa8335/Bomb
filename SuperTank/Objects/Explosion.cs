using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Explosion : BaseObject
    {
        #region hằng số frame lớn nhất
        //số frame (khung hình) tối đa của 1 vụ nổ gây ra bởi 1 bullet
        //ảnh của vụ nổ chia thành nhiều small frame, các hằng số này xác định số frame của 1 vụ nổ
        private const int maxFrameX = 5;
        private const int maxFrameY = 2;
        #endregion hằng số frame lớn nhất

        //xác định vị trí khung hình hiện tại trên tọa độ Oxy.
        //xác định frame nào được hiển thị khi vẽ vụ nổ
        #region các thông số chuyển frame
        private int framex = 0;
        private int framey = 0;
        #endregion các thông số chuyển frame

        private ExplosionSize explosionSize;
        private bool isExplosion;

        // hiển thị vụ nổ
        //vẽ hình ảnh vụ nổ lên Bitmap bmpBack
        public override void Show(Bitmap bmpBack)
        {
            //bmpObject là hình ảnh vụ nổ, rectX, rectY là tọa độ vụ nổ
            //this.framex * thisrectWidth để cho biết số frame cho 1 vụ nổ
            //this.rectWidth, Height là kích thước 1 frame.
            Common.PaintObject(bmpBack, this.bmpObject, this.RectX, this.RectY,
                this.framex * this.RectWidth, this.framey * this.RectHeight, this.RectWidth, this.RectHeight);

            //Vẽ xong 1 frame thì tăng framex để tới frame tiếp theo.
            /*Thứ tự chạy:
            (0, 0)-> (1, 0)-> (2, 0)-> (3, 0)-> (4, 0)  // Hết hàng 0
             Reset framex = 0, tăng framey
            (0, 1)-> (1, 1)-> (2, 1)-> (3, 1)-> (4, 1)  // Hết hàng 1
            Kết thúc vì framey = maxFrameY = 2*/
            this.framex++;
            if (framex == maxFrameX)
            {
                framex = 0;
                framey++;
                if (framey == maxFrameY)
                    this.isExplosion = false;
            }
        }

        #region properties
        public bool IsExplosion
        {
            get
            {
                return isExplosion;
            }

            set
            {
                isExplosion = value;
            }
        }
        public ExplosionSize ExplosionSize
        {
            get
            {
                return explosionSize;
            }

            set
            {
                explosionSize = value;
            }
        }
        #endregion properties
    }
}
