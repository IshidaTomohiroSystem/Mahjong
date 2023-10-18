using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    TilesData tilesData;
    List<TilesBase> tileList;
    [SerializeField] GameObject tileDebugUI;
    private UIDocument  tileDebugUIDocument;
    List<TilesBase> tileRandomList;
    int tileCount;

    /// <summary>
    /// playerの数
    /// </summary>
    readonly int maxPlayerNum = 4;  
    /// <summary>
    /// 王牌の数
    /// </summary>
    readonly int deadWallNum = 14;  
    /// <summary>
    /// 配牌を配るとき何個ずつ牌をとっていくか
    /// </summary>
    readonly int startPickUpTileNum = 4;
    /// <summary>
    /// 配牌を配るとき何回とるか
    /// </summary>
    readonly int startPickUpTileCount = 3;  


    [SerializeField] GameObject playerObj;
    List<PlayerController> playerControllers = new List<PlayerController>();

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < maxPlayerNum; i++)
        {
            GameObject gameObject = Instantiate(playerObj);
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            playerControllers.Add(playerController);
        }

        InitializeWallTiles();
        tileCount = 0;
        Debug.Log(tileList.Count);

        // PrefabからUIを生成
        var tileDebugObject = Instantiate(tileDebugUI);

        // UIDocumentの参照を保存
        tileDebugUIDocument = tileDebugObject.GetComponent<UIDocument>();

        var labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
        labelElement.text = tileList.Count.ToString();

        // UIのBaseのVisual Elementを取得
        var baseElement = tileDebugUIDocument.rootVisualElement.Q<VisualElement>("Base");
        // UIのサイズを変更
        //baseElement.style.width = 150;
        //baseElement.style.height = 150;
        // UIの位置を変更
        //baseElement.transform.position = new Vector3(0, -200, 0);

        System.Random rand = new System.Random();
        tileRandomList = tileList.OrderBy(_ => rand.Next()).ToList();

        InitializePlayerTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < playerControllers.Count; i++)
            {
                playerControllers[i].SortTiles();
                DebugPlayerTiles(playerControllers[i].GetTiles());
            }
        }
    }

    /// <summary>
    /// プレイヤーの配牌作成
    /// </summary>
    void InitializePlayerTiles()
    {
        tileCount = 0;
        for (int i = 0; i < playerControllers.Count; i++)
        {
            for (int j = 0; j < startPickUpTileCount; j++)
            {
                for (int k = 0; k < startPickUpTileNum; k++)
                {
                    TilesBase tilesBase = GetTile(tileCount);
                    playerControllers[i].SetTile(tilesBase);
                    tileCount++;
                }

            }
        }
        for (int i = 0; i < playerControllers.Count; i++)
        {
            TilesBase tilesBase = GetTile(tileCount);
            playerControllers[i].SetTile(tilesBase);
            tileCount++;
        }

    }

    /// <summary>
    /// 山牌初期化
    /// </summary>
    void InitializeWallTiles()
    {
        tilesData = new TilesData();
        tileList = tilesData.GetTiles();
    }

    /// <summary>
    /// 山から1つ牌を取り出す
    /// </summary>
    /// <param name="tileNum"></param>
    /// <returns></returns>
    TilesBase GetTile(int tileNum)
    {
        if (tileRandomList.Count <= tileNum)
            return null;

        TilesBase tilesBase = tileRandomList[tileNum];

        return tileRandomList[tileNum];
    }


    void DebugGetTileData(TilesBase tilesBase)
    {
        if (tilesBase == null)
            return;

        var labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
        labelElement.text = "";

        if (typeof(Suits) == tilesBase.GetType())
        {
            Suits suits = (Suits)tilesBase;
            if (suits.suitsType == SuitsType.Characters)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "Characters" + suits.number.ToString();
            }
            else if (suits.suitsType == SuitsType.Circles)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "Circles" + suits.number.ToString();
            }
            else if (suits.suitsType == SuitsType.Bamboo)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "Bamboo" + suits.number.ToString();
            }
        }
        else if(typeof(YuanHonours) == tilesBase.GetType())
        {
            YuanHonours yuanHonours = (YuanHonours)tilesBase;
            if (yuanHonours.yuanType == YuanType.White)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "White";
            }
            else if (yuanHonours.yuanType == YuanType.Green)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "Green";
            }
            else if (yuanHonours.yuanType == YuanType.Center)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "Center";
            }
        }
        else if(typeof(WindHonours) == tilesBase.GetType())
        {
            WindHonours windHonours = (WindHonours)tilesBase;
            if (windHonours.windType == WindType.East)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "East";
            }
            else if (windHonours.windType == WindType.West)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "West";
            }
            else if (windHonours.windType == WindType.North)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "North";
            }

            else if (windHonours.windType == WindType.South)
            {
                labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                labelElement.text = "South";
            }
        }

        labelElement.text = labelElement.text + "::: tile count : " + tileCount.ToString();
    }

    void DebugPlayerTiles(List<TilesBase> tilesBases)
    {
        Debug.Log(tilesBases.Count);
        string resultString = "";

        for (int i = 0; i < tilesBases.Count; i++)
        {
            if (typeof(Suits) == tilesBases[i].GetType())
            {
                Suits suits = (Suits)tilesBases[i];
                if(suits.suitsType == SuitsType.Characters)
                {
                    resultString += "ch" + suits.number.ToString();
                }
                else if(suits.suitsType == SuitsType.Circles)
                {
                    resultString += "ci" + suits.number.ToString();
                }
                else if (suits.suitsType == SuitsType.Bamboo)
                {
                    resultString += "ba" + suits.number.ToString();
                }
            }
            else if (typeof(YuanHonours) == tilesBases[i].GetType())
            {
                YuanHonours yuanHonours = (YuanHonours)tilesBases[i];
                if(yuanHonours.yuanType == YuanType.White)
                {
                    resultString += "wh";
                }
                else if (yuanHonours.yuanType == YuanType.Green)
                {
                    resultString += "gr";
                }
                else if (yuanHonours.yuanType == YuanType.Center)
                {
                    resultString += "ce";
                }
            }
            else if (typeof(WindHonours) == tilesBases[i].GetType())
            {
                WindHonours windHonours = (WindHonours)tilesBases[i];
                if(windHonours.windType == WindType.East)
                {
                    resultString += "ea";
                }
                else if (windHonours.windType == WindType.West)
                {
                    resultString += "we";
                }
                else if (windHonours.windType == WindType.North)
                {
                    resultString += "no";
                }
                else if (windHonours.windType == WindType.South)
                {
                    resultString += "so";
                }
            }
        }

        Debug.Log(resultString);
    }
}
