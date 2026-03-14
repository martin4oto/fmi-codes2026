using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public List<Cell> cells;
    public List<Cell> viruses;

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
        if(currentTimer >= retargetTimer)
        {
            RetargetCells();
            currentTimer = 0;
        }
        currentTimer += Time.deltaTime;
    }

    public void RemoveVirus(Cell virus)
    {
        viruses.Remove(virus);

        if (viruses.Count == 0)
        {
            WaveManager.instance.StartCoroutine(WaveManager.instance.StartNextWave());;
        }
        
        Destroy(virus.gameObject);
    }
    public void RemoveCell(Cell cell)
    {
        cells.Remove(cell);

        Destroy(cell.gameObject);
    }

    public void AddCell(Cell cell)
    {
        cell.isEnemy = false;
        cells.Add(cell);
    }

    public void AddVirus(Cell cell)
    {
        cell.isEnemy = true;
        viruses.Add(cell);
    }

    void RetargetCells()
    {
        foreach(Cell cell in cells)
        {
            cell.TryToStopMoving();
        }

        foreach(Cell cell in viruses)
        {
            cell.TryToStopMoving();
        }
    }
}
