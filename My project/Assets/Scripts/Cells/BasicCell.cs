using System.Collections.Generic;
using UnityEngine;

public class BasicCell:Cell
{
    public float maxShootingTimer;
    Cell foe;

    float currentShootingTimer = 0;
    void Update()
    {
        base.Update();
        if(isShooting)
        {
            currentShootingTimer+=Time.deltaTime;
            if(currentShootingTimer>=maxShootingTimer)
            {
                GameObject[] foes = FindFoe();
                TryToShoot(foes);
                currentShootingTimer = 0;
            }
        }
        else
        {
            currentShootingTimer = maxShootingTimer + 1;
        }
    }
    void TryToShoot(GameObject[] foes)
    {
        List<GameObject> FoesInRange = new List<GameObject>();

        for(int i = 0; i<foes.Length; i++)
        {
            Vector3 foePosition = foes[i].transform.position;

            if(Vector3.Distance(foePosition, transform.position) < range)
            {
                FoesInRange.Add(foes[i]);
            }
        }

        if(FoesInRange.Count > 0)
        {
            Shoot(FoesInRange);
        }
    }
    void Shoot(List<GameObject> foesInRange)
    {
        for(int i = 0; i<foesInRange.Count; i++)
        {
            Cell foeCellScript = foesInRange[i].GetComponent<Cell>();
            Debug.Log("shoot");

            foeCellScript.TakeDamage(DMG); 
        }
    }
}
