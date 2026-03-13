using UnityEngine;

public class Cell:MonoBehaviour
{
    internal bool isEnemy;

    public virtual void Move(Vector2 _position)
    {
        if(_position == null)return;
    }

    public virtual void Arrive()
    {
        
    }

    internal GameObject[] FindFoe()
    {
        if(isEnemy)
        {
            return FindCell();
        }
        else
        {
            return FindVirus();
        }
    }

    GameObject[] FindCell()
    {
        return GameObject.FindGameObjectsWithTag("Cell");
    }

    GameObject[] FindVirus()
    {
        return GameObject.FindGameObjectsWithTag("Virus");
    }

    public void TakeDamage(int DMG)
    {
        //TODO
    }
}
