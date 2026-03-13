using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    List<Cell> cells;
    List<Cell> viruses;

    static CellManager instance;
    void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    public void RemoveVirus(Cell virus)
    {
        //TODO
    }
    public void RemoveCell(Cell cell)
    {
        //TODO
    }
}
