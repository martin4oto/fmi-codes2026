using UnityEngine;

public class Cell
{
    public virtual void Move(Vector2 _position)
    {
        if(_position == null)return;
    }

    public virtual void Arrive()
    {
        
    }
}
