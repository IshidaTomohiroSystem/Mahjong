using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    List<TilesBase> tileList = new List<TilesBase>();
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    /// <summary>
    /// �v����z�ɉ�����
    /// </summary>
    /// <param name="tilesBase"></param>
    public void SetTile(TilesBase tilesBase)
    {
        tileList.Add(tilesBase);
    }

    /// <summary>
    /// ��z�̃f�[�^���擾
    /// </summary>
    public List<TilesBase> GetTiles()
    {
        return tileList;
    }

    public void SortTiles()
    {
        tileList.Sort(TilesBase.CompareTiles);
    }
}
