using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    List<Cell> cells;
    List<Cell> viruses;

    public static CellManager instance;
    void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    float currentTimer;
    public float retargetTimer = 0.2f;
    void Update()
    {
        if(currentTimer>=retargetTimer)
        {

            currentTimer = 0;
        }
        currentTimer += Time.deltaTime;
    }

    public void RemoveVirus(Cell virus)
    {
        //TODO
    }
    public void RemoveCell(Cell cell)
    {
        //TODO
    }

    void RetargetCells()
    {
        foreach(Cell cell in cells)
        {
            cell.TryToStopMoving();
        }
    }
}
