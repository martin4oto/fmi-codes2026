using UnityEngine;

public class BossProjectile:Cell
{
    void Start()
    {
        base.Start();
        Move(Vector3.zero);
    }

    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 4f, Space.Self);   
    }
}
