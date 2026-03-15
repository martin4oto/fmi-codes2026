using System.Collections.Generic;
using UnityEngine;

public class BossProjectile:Cell
{
    void Start()
    {
        base.Start();
        Move(Vector3.zero);
    }

    void Update()
    {
        base.Update();

        GameObject[] foes = FindFoe();
        List<Cell> inRange = GetCellsInRange(foes, range);

        if(inRange.Count != 0)
        {
            Explode(inRange);
        }
    }  

    void Explode(List<Cell> foesInRange)
    {
        Remove();
        Cell foeCellScript = foesInRange[0];

        foeCellScript.TakeDamage(DMG); 
    }
    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 4f, Space.Self);   
    }
}
