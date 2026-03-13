using UnityEngine;

public class BasicCell:Cell
{
    Cell foe;
    public int DMG;
    void Start()
    {
        foe = FindTargetToFollow();

        Move(foe.transform.position);
        
        Debug.Log("start");
    }

    public override void Arrive()
    {
        base.Arrive();
        foe.TakeDamage(DMG);

        Remove();
    }
}
