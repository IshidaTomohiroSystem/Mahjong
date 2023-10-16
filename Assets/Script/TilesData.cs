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
    /// 1í—Ş‚Ì”v‚Ì–‡” 4
    /// </summary> 
    readonly int TilesTypeNum = 4;

    /// <summary>
    /// ”š‚ª‚¢‚­‚Â‚Ü‚Å‚ ‚é‚© 9
    /// </summary>
    readonly int NumberMax = 9;
    
    /// <summary>
    /// ””v‚Ìí—Ş 3
    /// </summary>
    readonly int NumberType = 3;
    
    /// <summary>
    /// OŒ³”v‚Ìí—Ş 3
    /// </summary>
    readonly int YuanHonoursType = 3;

    /// <summary>
    /// •—”v‚Ìí—Ş 4
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

        // ””v
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

        // OŒ³”v
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

        // •—”v
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
}

/// <summary>
/// ””v
/// </summary>
public class Suits : TilesBase
{
    public SuitsType suitsType;
    public SuitsNumberType suitsNumberType;
    public int number;
}

/// <summary>
/// š”v base
/// </summary>
public class HonoursBase : TilesBase
{
    public HonoursType honoursType;
    public string honoursText;
}

/// <summary>
/// OŒ³”v
/// </summary>
public class YuanHonours : HonoursBase
{
    public YuanType yuanType;
}

/// <summary>
/// •—”v
/// </summary>
public class WindHonours : HonoursBase
{
    public WindType windType;
}


public enum TileType
{
    None,
    Number, // ”š
    Honours, // š
}

public enum SuitsType
{
    None,
    Characters, // ƒ}ƒ“ƒY
    Circles,    // ƒsƒ“ƒY
    Bamboo,     // ƒ\ƒEƒY
}

public enum SuitsNumberType
{
    None,
    Yao,            // 19”v
    OtherThanYao,   // 28”v
}

public enum HonoursType
{
    None,
    ThreeYuan,  // OŒ³”v
    WindTiles,  // •—”v
}

public enum YuanType
{
    None,
    White,  // ”’
    Green,  // ”­
    Center, // ’†
}

public enum WindType
{
    None,   
    East,   // “Œ
    West,   // ¼
    North,  // “ì
    South   // –k
}