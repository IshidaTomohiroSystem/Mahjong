using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.TextCore.Text;


public class TilesData
{
    public TilesData() 
    {
        InitializeTiles();
    }


    /// <summary>
    /// 1種類の牌の枚数 4
    /// </summary> 
    readonly int TilesTypeNum = 4;

    /// <summary>
    /// 数字がいくつまであるか 9
    /// </summary>
    readonly int NumberMax = 9;
    
    /// <summary>
    /// 数牌の種類 3
    /// </summary>
    readonly int NumberType = 3;
    
    /// <summary>
    /// 三元牌の種類 3
    /// </summary>
    readonly int YuanHonoursType = 3;

    /// <summary>
    /// 風牌の種類 4
    /// </summary>
    readonly int WindHonoursType = 4;

    int tilesMax;
    List<TilesBase> tileList = new List<TilesBase>();

    /// <summary>
    /// InitializeTiles
    /// </summary> 
    private void InitializeTiles()
    {
        tilesMax = (NumberType * NumberMax + YuanHonoursType + WindHonoursType) * TilesTypeNum;

        // 数牌
        for(int h = 0; h < NumberType; h++)
        {
            for (int i = 0; i < NumberMax; i++)
            {
                for (int j = 0; j < TilesTypeNum; j++)
                {
                    Suits suits = new Suits();
                    suits.number = i + 1;
                    suits.suitsType = (SuitsType)(h + 1);
                    if (i == 0 || i == 8)
                    {
                        suits.suitsNumberType = SuitsNumberType.Yao;
                    }
                    else
                    {
                        suits.suitsNumberType = SuitsNumberType.Yao;
                    }
                    tileList.Add(suits);
                }
            }
        }

        // 三元牌
        for (int i = 0; i < YuanHonoursType; i++)
        {
            for (int j = 0; j < TilesTypeNum; j++)
            {
                YuanHonours yuanHonours = new YuanHonours();
                yuanHonours.honoursType = HonoursType.ThreeYuan;
                yuanHonours.yuanType = (YuanType)(i + 1);
                if (yuanHonours.yuanType == YuanType.White)
                {
                    yuanHonours.honoursText = "white";
                }
                else if (yuanHonours.yuanType == YuanType.Green)
                {
                    yuanHonours.honoursText = "green";
                }
                else
                {
                    yuanHonours.honoursText = "center";
                }
                tileList.Add(yuanHonours);
            }
        }

        // 風牌
        for (int i = 0; i < WindHonoursType; i++)
        {
            for (int j = 0; j < TilesTypeNum; j++)
            {
                WindHonours windHonours = new WindHonours();
                windHonours.honoursText = "";
                windHonours.honoursType = HonoursType.WindTiles;
                windHonours.windType = (WindType)(i + 1);
                if (windHonours.windType == WindType.East)
                {
                    windHonours.honoursText = "east";
                }
                else if (windHonours.windType == WindType.West)
                {
                    windHonours.honoursText = "west";
                }
                else if(windHonours.windType == WindType.North)
                {
                    windHonours.honoursText = "north";
                }
                else
                {
                    windHonours.honoursText = "south";
                }
                tileList.Add(windHonours);
            }
        }
        
    }

    public List<TilesBase> GetTiles() { return tileList; }
}

/// <summary>
/// tiles base
/// </summary>
public class TilesBase
{
    public TileType tileType;
    public Sprite tileTexture;

    /// <summary>
    /// sort
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int CompareTiles(TilesBase a, TilesBase b)
    {
        if (typeof(Suits) == a.GetType() && typeof(Suits) == b.GetType())
        {
            Suits aSuits = (Suits)a;
            Suits bSuits = (Suits)b;
            if (aSuits.suitsType == bSuits.suitsType)
            {
                if (aSuits.number <= bSuits.number)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if ((int)aSuits.suitsType <= (int)bSuits.suitsType)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
        else if (typeof(Suits) == a.GetType() && typeof(YuanHonours) == b.GetType())
        {
            return -1;
        }
        else if (typeof(Suits) == a.GetType() && typeof(WindHonours) == b.GetType())
        {
            return -1;
        }
        else if (typeof(YuanHonours) == a.GetType() && typeof(Suits) == b.GetType())
        {
            return 1;
        }
        else if (typeof(YuanHonours) == a.GetType() && typeof(YuanHonours) == b.GetType())
        {
            YuanHonours aYuanHonours = (YuanHonours)a;
            YuanHonours bYuanHonours = (YuanHonours)b;
            if ((int)aYuanHonours.yuanType <= (int)bYuanHonours.yuanType)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        else if (typeof(YuanHonours) == a.GetType() && typeof(WindHonours) == b.GetType())
        {
            return -1;
        }
        else if (typeof(WindHonours) == a.GetType() && typeof(Suits) == b.GetType())
        {
            return 1;
        }
        else if (typeof(WindHonours) == a.GetType() && typeof(YuanHonours) == b.GetType())
        {
            return 1;
        }
        else if (typeof(WindHonours) == a.GetType() && typeof(WindHonours) == b.GetType())
        {
            WindHonours aWindHonours = (WindHonours)a;
            WindHonours bWindHonours = (WindHonours)b;
            if ((int)aWindHonours.windType <= (int)bWindHonours.windType)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 0;

        }
    }
}

/// <summary>
/// 数牌
/// </summary>
public class Suits : TilesBase
{
    public Suits() 
    {
        tileType = TileType.Number;
    }
    public SuitsType suitsType;
    public SuitsNumberType suitsNumberType;
    public int number;
}

/// <summary>
/// 字牌 base
/// </summary>
public class HonoursBase : TilesBase
{
    public HonoursBase()
    {
        tileType = TileType.Honours;
    }
    public HonoursType honoursType;
    public string honoursText;
}

/// <summary>
/// 三元牌
/// </summary>
public class YuanHonours : HonoursBase
{
    public YuanHonours()
    {
        honoursType = HonoursType.ThreeYuan;
    }
    public YuanType yuanType;
}

/// <summary>
/// 風牌
/// </summary>
public class WindHonours : HonoursBase
{
    public WindHonours ()
    {
        honoursType = HonoursType.WindTiles;
    }
    public WindType windType;
}


public enum TileType
{
    None,
    Number, // 数字
    Honours, // 字
}

public enum SuitsType
{
    None,
    Characters, // マンズ
    Circles,    // ピンズ
    Bamboo,     // ソウズ
}

public enum SuitsNumberType
{
    None,
    Yao,            // 19牌
    OtherThanYao,   // 28牌
}

public enum HonoursType
{
    None,
    ThreeYuan,  // 三元牌
    WindTiles,  // 風牌
}

public enum YuanType
{
    None,
    White,  // 白
    Green,  // 発
    Center, // 中
}

public enum WindType
{
    None,   
    East,   // 東
    South,  // 南
    West,   // 西
    North,  // 北
}
