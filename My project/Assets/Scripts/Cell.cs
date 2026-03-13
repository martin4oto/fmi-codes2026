using UnityEngine;

public class Cell:MonoBehaviour
{
    int HP;
    internal bool isEnemy;
    [Header("Stats")]
    public float speed;
    private float timeToArive;
    private float currentTime;
    private bool hasMoveCommand;
    private Vector3 moveStartPoint;
    private Vector3 moveEndPoint;
    void Update()
    {
        if (hasMoveCommand)
        {
            currentTime += Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0, timeToArive);
            transform.position = Vector3.Lerp(moveStartPoint, moveEndPoint, currentTime/timeToArive);

            if (currentTime >= timeToArive)
            {
                hasMoveCommand = false;
                Arrive();
            }
        }
    }
    public virtual void Move(Vector2 _position)
    {
        if(_position == null) return;

        hasMoveCommand = true;
        moveStartPoint = transform.position;
        moveEndPoint = _position;
        currentTime = 0;
        timeToArive = Vector3.Distance(moveStartPoint, moveEndPoint)/speed;
    }

    public virtual void Arrive()
    {
        
    }

    public virtual void Arrive(Transform foe)
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
