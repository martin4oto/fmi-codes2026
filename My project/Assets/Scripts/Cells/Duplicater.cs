using System.Collections.Generic;
using UnityEngine;

public class Duplicater : Cell
{
    public float maxShootingTimer;
    Cell foe;

    float currentShootingTimer = 0;
    new void Update()
    {
        base.Update();

        if (currentShootingTimer < maxShootingTimer)
        {
            currentShootingTimer+=Time.deltaTime;
        }
        if(isShooting)
        {
            if(currentShootingTimer>=maxShootingTimer)
            {
                GameObject[] foes = FindFoe();
                TryToShoot(foes);
                currentShootingTimer = 0;
            }
        }else if (objectToFollow == null)
        {
            Wander();
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
            foeCellScript.TakeDamage(DMG); 
            AudioManager.PlaySFX("slap_pitched_down");
            squashAndStretch.Play();
        }
    }

    public void PowerUp()
    {
        base.maxHP = Mathf.CeilToInt(base.maxHP * 1.25f);
        base.HP = Mathf.CeilToInt(base.HP * 1.25f);
    }
}
