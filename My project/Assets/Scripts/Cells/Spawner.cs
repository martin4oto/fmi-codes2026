using UnityEngine;

public class Spawner:Cell
{
    public float maxShootingTimer;
    public GameObject basicEnemyPrefab;

    float currentShootingTimer = 0;
    new void Update()
    {
        base.Update();
        if(isShooting)
        {
            currentShootingTimer+=Time.deltaTime;
            if(currentShootingTimer>=maxShootingTimer)
            {
                Spawn();
                currentShootingTimer = 0;
            }
        }
        else
        {
            currentShootingTimer = maxShootingTimer + 1;
        }
    }

    void Spawn()
    {
        Cell newCell = Instantiate(basicEnemyPrefab, transform.position, Quaternion.identity).GetComponent<Cell>();

        CellManager.instance.AddCell(newCell);

        AudioManager.PlaySFX("shsh");
    }
}
