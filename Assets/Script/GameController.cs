using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    TilesData tilesData;
    List<TilesBase> tileList;
    // Start is called before the first frame update
    void Awake()
    {
        tilesData = new TilesData();
        tileList  = tilesData.GetTiles();
        Debug.Log(tileList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
