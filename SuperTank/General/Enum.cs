using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.General
{
    //dùng mũi tên lên xuống trái phải
    public enum Direction
    {
        eLeft, eRight, eUp, eDown
    }
    public enum BulletType
    {
        eTriangleBullet, eRocketBullet
    }
    public enum ExplosionSize
    {
        eSmallExplosion, eBigExplosion
    }
    public enum EnemyTankType
    {
        // 0, 1
        eNormal, eMedium
    }
    public enum Skin
    {
        // 0, 1, 2, 3, 4, 5, 6, 7 đại diện cho lần lượt từng loại skin
       eGreen, eRed, eYellow, eBlue, ePurple, eLightBlue, eOrange, ePink
    }
    public enum ItemType
        //tên từng loại item
    {
        eItemHeart, eItemShield, eItemGrenade, eItemRocket
    }
    public enum InforStyle
    {
        eGameOver, eGameNext, eGameWin
    }
}
