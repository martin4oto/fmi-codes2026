using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public GameObject bossPrefab;
    Cell boss;

    public List<Cell> cells;
    public List<Cell> viruses;
    public Transform bossSpawn;

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

    public void RemoveAll()
    {
        for (int i = 0; i < cells.Count; i++) 
        {
            cells[i].Remove();
        }
        for (int i = 0; i < viruses.Count; i++) 
        {
            viruses[i].Remove();
        }
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

    public GameObject SpawnBoss(Vector2 position)
    {
        Vector3 bossPosition = position;
        bossPosition.x = Mathf.Sign(position.x)*bossSpawn.position.x;
        bossPosition.y = Mathf.Sign(position.y)*bossSpawn.position.y;

        if(boss == null)
        {
            GameObject bossObject = Instantiate(bossPrefab, bossPosition, Quaternion.identity);

            boss = bossObject.GetComponent<Cell>();
            viruses.Add(boss);
            return bossObject;
        }
        else
        {
            boss.gameObject.SetActive(true);
            boss.transform.position = bossPosition;
            viruses.Add(boss);

            return boss.gameObject;
        }
    }

    public void HideBoss()
    {
        viruses.Remove(boss);

        if (viruses.Count == 0)
        {
            WaveManager.instance.StartCoroutine(WaveManager.instance.StartNextWave());;
        }

        boss.gameObject.SetActive(false);
    }

    void RetargetCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] == null){
                 cells.RemoveAt(i);
                 i--;
            }
        }
        for (int i = 0; i < viruses.Count; i++)
        {
            if (viruses[i] == null){
                 viruses.RemoveAt(i);
                 i--;
            }
        }

        foreach(Cell cell in cells)
        {
            cell.TryToStopMoving();
        }

        for(int i = 0; i<viruses.Count; i++)
        {
            viruses[i].TryToStopMoving();

            if(viruses[i].isBoss)
            {
                viruses[i].BossLogic();
            }
        }
    }
}
