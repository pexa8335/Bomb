using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.General;
using SuperTank.Objects;
using System.IO;

namespace SuperTank.Objects
{
    class EnemyTankManagement
    {
        // danh sách xe tăng địch
        private List<EnemyTank> enemyTanks;
        // danh sách thông số xe tăng địch
        private List<EnemyTankParameter> enemyTankParameters;
        // số lượng xe tăng địch cần tiêu diệt
        private int numberEnemyTankDestroy;

        public EnemyTankManagement()
        {
            // khởi tạo danh sách xe tăng địch
            EnemyTanks = new List<EnemyTank>();
            // khởi tạo danh sách thông số xe tăng địch
            EnemyTankParameters = new List<EnemyTankParameter>();
        }

        // load danh sách địch
        public void Init_EnemyTankManagement(string path)
        {
            this.numberEnemyTankDestroy = 0;
            // đọc thông số tất cả xe tăng địch
            this.ReadEnemyTankParameters(path);

            // tạo xe tăng địch
            this.CreateListEnemyTank();
        }

        // giải phóng danh sách địch
        public void EnemyTanksClear()
        {
            // giải phóng danh sách xe tăng địch
            this.enemyTankParameters.Clear();
            // giải phóng danh sách thông số xe tăng địch
            this.enemyTanks.Clear();
        }

        #region Các hàm khởi tạo xe tăng địch nội bộ
        // đọc thông số xe tăng địch vào danh sách
        private void ReadEnemyTankParameters(string path)
        {
            // đọc từ file
            string s = "";
            // đọc từ file sd streamreader
            using (StreamReader reader = new StreamReader(path))
            {
                // đọc từng dòng trong file
                EnemyTankParameter enemyTankParameter;
                
                while ((s = reader.ReadLine()) != null)
                // tách thông số từng xe tăng địch
                {
                    // tách thông số từng xe tăng địch
                    string[] token = s.Split('#');
                    // tạo thông số xe tăng địch
                    enemyTankParameter = new EnemyTankParameter();
                    // thiết lập thông số xe tăng địch
                    enemyTankParameter.Type = int.Parse(token[0]);
                    // năng lượng của xe tăng địch
                    enemyTankParameter.Energy = int.Parse(token[1]);
                    // tốc độ di chuyển của xe tăng địch
                    enemyTankParameter.TankMoveSpeed = int.Parse(token[2]);
                    // tốc độ đạn của xe tăng địch
                    enemyTankParameter.TankBulletSpeed = int.Parse(token[3]);
                    // tọa độ xe tăng địch
                    enemyTankParameter.XEnemyTank = int.Parse(token[4]);
                    enemyTankParameter.YEnemyTank = int.Parse(token[5]);
                    // số lượng xe tăng địch
                    enemyTankParameter.maxNumberEnemyTank = int.Parse(token[6]);
                    this.EnemyTankParameters.Add(enemyTankParameter);
                }
                enemyTankParameter = null;
                s = null;
            }
            //Console.WriteLine("Số lượng địch: " + this.enemyTankParameters.Count);
        }
        // tạo danh sách xe tăng địch đưa và danh sách
        private void CreateListEnemyTank()
        {
            foreach (EnemyTankParameter enemyTankParameter in EnemyTankParameters)
            {
                // thêm từng xe tăng địch vào danh sách
                this.EnemyTanks.Add(this.CreateOneEnemyTank(enemyTankParameter));
                // đếm số lượng địch cần tiêu diệt
                numberEnemyTankDestroy += enemyTankParameter.maxNumberEnemyTank;
            }
            //Console.WriteLine("Số lượng địch cần tiêu diệt: " + numberEnemyTankDestroy);
            //Console.WriteLine("Đã tạo " + this.NumberEnemyTank() + " xe tăng địch");
        }
        // skin xe tăng địch thay đổi theo năng lượng địch
        public Skin SkinEnemyTank(EnemyTank enemyTank)
        {
            switch (enemyTank.Energy)
            {
                case 70:
                    // skin màu đỏ
                    return Skin.eRed;
                case 60:
                    // skin màu xanh cam
                    return Skin.eOrange;
                case 50:
                    // skin màu xanh dương
                    return Skin.eBlue;
                case 40:
                    // skin màu tím
                    return Skin.ePurple;
                case 30:
                    // skin màu hồng
                    return Skin.ePink;
                case 20:
                    // skin màu xanh lục
                    return Skin.eGreen;
                case 10:
                    // skin màu xanh sáng
                    return Skin.eLightBlue;
            }
            return Skin.eLightBlue;
        }
        // tạo một xe tăng địch
        private EnemyTank CreateOneEnemyTank(EnemyTankParameter enemyTankParameter)
        {
            // tạo một xe tăng địch
            EnemyTank enemyTank;
            // tạo một xe tăng địch
            enemyTank = new EnemyTank();
            // cập nhật thông số xe tăng địch
            this.UpdateParameter(enemyTank, enemyTankParameter);
            // trả về xe tăng địch
            return enemyTank;
        }
        #endregion Các hàm khởi tạo xe tăng địch nội bộ

        // cập nhật tham số xe tăng địch
        public void UpdateParameter(EnemyTank enemyTank, EnemyTankParameter enemyTankParameter)
        {
            // thiết lập hướng di chuyển của xe tăng địch
            enemyTank.DirectionTank = Direction.eUp;
            // thiết lập xe tăng địch có thể di chuyển
            enemyTank.IsMove = true;
            // thiết lập loại xe tăng (0: normal; 1: medium)
            switch (enemyTankParameter.Type)
            {
                case 0:
                    // loại xe tăng địch
                    enemyTank.EnemyType = EnemyTankType.eNormal;
                    // load hình ảnh xe tăng địch
                    enemyTank.LoadImage(Common.path + @"\Images\tank0.png");
                    break;
                case 1:
                    // loại xe tăng địch
                    enemyTank.EnemyType = EnemyTankType.eMedium;
                    // load hình ảnh xe tăng địch
                    enemyTank.LoadImage(Common.path + @"\Images\tank1.png");
                    break;
            }
            // cập nhật thông số xe tăng địch

            // năng lượng của xe tăng địch
            enemyTank.Energy = enemyTankParameter.Energy;
            // tốc độ di chuyển của xe tăng địch
            enemyTank.MoveSpeed = enemyTankParameter.TankMoveSpeed;
            // tốc độ đạn của xe tăng địch
            enemyTank.TankBulletSpeed = enemyTankParameter.TankBulletSpeed;
            // update skin xe tăng địch khi biết năng lượng
            enemyTank.SkinTank = this.SkinEnemyTank(enemyTank);
            // tọa độ xe tăng địch
            enemyTank.RectX = enemyTankParameter.XEnemyTank * Common.STEP;
            // tọa độ xe tăng địch
            enemyTank.RectY = enemyTankParameter.YEnemyTank * Common.STEP;
        }

        // di chuyển toàn bộ danh sách xe tăng địch 
        public void MoveAllEnemyTank(List<Wall> walls, PlayerTank playerTank)
        {
            // di chuyển từng xe tăng địch
            bool isMove_local = false;
            // xử lí di chuyển xe tăng địch
            foreach (EnemyTank enemyTank in EnemyTanks) 
            {
                // xử lí di chuyển xe tăng địch
                switch (enemyTank.EnemyType)
                {
                    //nếu là xe tăng địch loại normal
                    case EnemyTankType.eNormal:
                        // nếu xe tăng địch đã qua xử lí là đã được phép di chuyển
                        isMove_local = enemyTank.HandleMoveNormal(walls, playerTank,this.EnemyTanks);
                        break;
                    //nếu là xe tăng địch loại medium
                    case EnemyTankType.eMedium:
                        // nếu xe tăng địch đã qua xử lí là đã được phép di chuyển
                        isMove_local = enemyTank.HandleMoveMedium(walls, playerTank, this.EnemyTanks);
                        break;
                }
                // di chuyển xe tăng địch khi đã qua xử lí là đã được phép di chuyển
                if (isMove_local)
                    enemyTank.Move();
            }
        }

        // hiển thị toàn bộ danh sách xe tăng địch
        public void ShowAllEnemyTank(Bitmap background)
        {
            // hiển thị từng xe tăng địch
            foreach (EnemyTank enemyTank in EnemyTanks)
            {
                //hiển thị xe tăng địch ở biến background 
                enemyTank.Show(background);
            }
        }

        // số lượng xe tăng địch
        public int NumberEnemyTank()
        {
            // trả về số lượng xe tăng địch
            return this.numberEnemyTankDestroy;
        }

        #region properties
        public List<EnemyTank> EnemyTanks
        {
            // lấy danh sách xe tăng địch
            get
            {
                // trả về danh sách xe tăng địch
                return enemyTanks;
            }

            set
            {
                // thiết lập danh sách xe tăng địch
                enemyTanks = value;
            }
        }
        // lấy danh sách thông số xe tăng địch
        public List<EnemyTankParameter> EnemyTankParameters
        {
            // lấy danh sách thông số xe tăng địch
            get
            {
                return enemyTankParameters;
            }
            // thiết lập danh sách thông số xe tăng địch
            set
            {
                enemyTankParameters = value;
            }
        }
        public int NumberEnemyTankDestroy
        {
            // thiết lập số lượng xe tăng địch cần tiêu diệt
            set { numberEnemyTankDestroy = 1; }
            // lấy số lượng xe tăng địch cần tiêu diệt
            get { return numberEnemyTankDestroy; }
        }
        #endregion properties
    }
}
