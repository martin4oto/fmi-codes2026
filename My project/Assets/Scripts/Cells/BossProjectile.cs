using UnityEngine;

public class BossProjectile:Cell
{
    void Start()
    {
        base.Start();
        Move(Vector3.zero);
    }
}
