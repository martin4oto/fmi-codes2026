using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bomb:Cell
{
    public float bombUpdateTimer;
    public float bombRange;
    public int bombDamage;
    float currentTimer = 0;
    void Start()
    {
        Transform foe = FindTargetToFollow().transform;

        Move(foe.position);
        
        Debug.Log("start");
    }

    void Update()
    {
        base.Update();


        if(currentTimer>=bombUpdateTimer)
        {
            GameObject[] foes = FindFoe();
            TryToExplode(foes);

            currentTimer = 0;
        }
        currentTimer += Time.deltaTime;
    }

    void TryToExplode(GameObject[] foes)
    {
        List<GameObject> FoesInRange = new List<GameObject>();

        for(int i = 0; i<foes.Length; i++)
        {
            Vector3 foePosition = foes[i].transform.position;

            if(Vector3.Distance(foePosition, transform.position) < bombRange)
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
        Debug.Log("boom");

        for(int i = 0; i<foesInRange.Count; i++)
        {
            Cell foeCellScript = foesInRange[i].GetComponent<Cell>();

            foeCellScript.TakeDamage(bombDamage); 
        }
    }

    public override void Arrive(Transform foe)
    {
        base.Arrive(foe);
        transform.parent = foe;
        transform.localPosition = Vector3.zero;
    }

    public override void Arrive()
    {
        GameObject[] foes = FindFoe();
        TryToExplode(foes);
    }
}
