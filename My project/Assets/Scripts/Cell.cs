using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;
using NUnit.Framework;

public class Cell:MonoBehaviour
{
    int HP;
    internal bool isEnemy;
    internal bool alreadyTargetted;
    internal bool isShooting = false;

    [Header("Stats")]
    public float speed;
    public float range;
    public bool canShoot;
    public int DMG;

    public float timeToArive;
    public float currentTime;
    public bool hasMoveCommand;
    public Vector3 moveStartPoint;
    public Vector3 moveEndPoint;

    void Start()
    {
        alreadyTargetted = false;
    }

    float currentShootingTimer = 0;
    protected void Update()
    {
        if (hasMoveCommand)
        {
            currentTime += Time.deltaTime;
            //currentTime = Mathf.Clamp(currentTime, 0, timeToArive);
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

    public virtual void Follow(Transform _objectToFollow)
    {
        //TODO
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
        HP-=DMG;
        //TODO
    }

    internal Cell FindTargetToFollow()
    {
        GameObject[] foes = FindFoe();

        foes = foes.OrderBy(foe => Vector3.Distance(transform.position, foe.transform.position)).ToArray();

        for(int i = 0; i<foes.Length; i++)
        {
            Cell current = foes[i].GetComponent<Cell>();

            if(!current.alreadyTargetted)
            {
                return current;
            }
        }
        return foes[0].GetComponent<Cell>();
    }

    List<Cell> GetCellsInRange(GameObject[] cells, float range)
    {
        List<Cell> current = new List<Cell>();

        foreach (GameObject g in cells)
        {
            float dist = Vector3.Distance(g.transform.position, transform.position);

            if (dist < range)
            {
                current.Add(g.GetComponent<Cell>());
            }
        }
        return current;
    }

    internal void Remove()
    {
        if(isEnemy)
        {
            CellManager.instance.RemoveCell(this);
        }
        else
        {
            CellManager.instance.RemoveVirus(this);
        }  
    }

    protected void StopMoving()
    {
        
    }

    public void TryToStopMoving()
    {
        if(!canShoot)return;

        GameObject[] foes = FindFoe();
        List<Cell> inRange = GetCellsInRange(foes, range);

        if(inRange.Count != 0)
        {
            StopMoving();
            isShooting = true;
        }
        else
        {
            isShooting = false;
            Cell foe = FindTargetToFollow();
            Follow(foe.transform);
        }
    }
}
