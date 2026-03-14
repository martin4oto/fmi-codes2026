using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bomb:Cell
{
    public float bombUpdateTimer;
    float currentTimer = 0;
    bool readyToExplode = false;

    new void Update()
    {
        base.Update();
        if (readyToExplode){
            if(currentTimer>=bombUpdateTimer)
            {
                GameObject[] foes = FindFoe();
                TryToExplode(foes);

                currentTimer = 0;
            }
            currentTimer += Time.deltaTime;
        }
        else
        {
            Wander();
        }
    }

    void TryToExplode(GameObject[] foes)
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
            Explode(FoesInRange);
        }
    }

    void Explode(List<GameObject> foesInRange)
    {
        Debug.LogWarning("exasd");
        Remove();
        for(int i = 0; i<foesInRange.Count; i++)
        {
            Cell foeCellScript = foesInRange[i].GetComponent<Cell>();

            foeCellScript.TakeDamage(DMG); 
        }
    }

    public override void Arrive(Transform foe)
    {
        base.Arrive(foe);
        transform.parent = foe;
        transform.localPosition = Vector3.zero;
        currentTimer = 0;
        readyToExplode = true;
    }
}
