using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class GameController : MonoBehaviour
{
    TilesData tilesData;
    List<TilesBase> tileList;
    [SerializeField] GameObject tileDebugUI;
    private UIDocument  tileDebugUIDocument;
    List<TilesBase> tileRandomList;
    int tileCount;

    // Start is called before the first frame update
    void Awake()
    {
        tilesData = new TilesData();
        tileList  = tilesData.GetTiles();
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (tileRandomList.Count <= tileCount)
                return;

            var labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
            labelElement.text = "";

            if (tileRandomList[tileCount].tileType == TileType.Number)
            {
                Suits suits = (Suits)tileRandomList[tileCount];
                if(suits.suitsType == SuitsType.Characters)
                {
                    labelElement = tileDebugUIDocument.rootVisualElement.Q<Label>("TileLabel");
                    labelElement.text = "Characters" + suits.number.ToString();
                }
                else if(suits.suitsType == SuitsType.Circles)
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
            else if (tileRandomList[tileCount].tileType == TileType.Honours)
            {
                HonoursBase honoursBase = (HonoursBase)tileRandomList[tileCount];
                if (honoursBase.honoursType == HonoursType.ThreeYuan)
                {
                    YuanHonours yuanHonours = (YuanHonours)tileRandomList[tileCount];
                    if(yuanHonours.yuanType == YuanType.White)
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
                else if(honoursBase.honoursType == HonoursType.WindTiles)
                {
                    WindHonours windHonours = (WindHonours)tileRandomList[tileCount];
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
            }

            labelElement.text = labelElement.text + "::: tile count : " + tileCount.ToString();
            tileCount++;
        }
    }
}
