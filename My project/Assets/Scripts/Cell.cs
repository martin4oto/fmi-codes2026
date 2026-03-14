using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;
using NUnit.Framework;
using System.Net.Security;

public class Cell:MonoBehaviour
{
    int HP;
    internal bool isEnemy;
    internal bool alreadyTargetted;
    internal bool isShooting = true;

    [Header("Stats")]
    public float speed;
    public float range;
    public bool shouldStop;
    public int DMG;
    public int maxHP;

    float timeToArive;
    float currentTime;
    bool hasMoveCommand;
    Vector3 moveStartPoint;
    Vector3 moveEndPoint;
    List<Node> path;
    int nodeIndex;
    bool pathMovement;
    Vector3 realEndPoint;
    public bool TEST;
    public bool TEST2;
    Vector2 coords;
    public Transform objectToFollow;
    float followTolerance = 0.25f;
    public GameObject fasdasd;

    void Start()
    {
        alreadyTargetted = false;
    }
    protected void Update()
    {
        if (TEST)
        {
            TEST = false;
            Move(coords);
        }
        if (TEST2)
        {
            TEST2 = false;
            Follow(fasdasd.transform);
        }

        if (hasMoveCommand)
        {
            MoveStep();
        }
        else if (objectToFollow != null)
        {
            if (Vector2.Distance(transform.position, objectToFollow.position) <= followTolerance)
            {
                Arrive(objectToFollow);
                objectToFollow = null;          
            }else{
                Move(objectToFollow.position);
            }
        }
    }
    void MoveStep()
    {
        currentTime += Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, timeToArive);
        if (timeToArive != 0) transform.position = Vector3.Lerp(moveStartPoint, moveEndPoint, currentTime/timeToArive);

        if (currentTime >= timeToArive)
        {
            if (pathMovement && nodeIndex != path.Count-1)
            {
                if (WallRaycast(realEndPoint)){
                    nodeIndex++;
                    moveStartPoint = transform.position;
                    moveEndPoint = new Vector2(path[nodeIndex].x, path[nodeIndex].y);
                    float distance = Vector3.Distance(moveStartPoint, moveEndPoint);
                    currentTime = 0;
                    timeToArive = distance/speed;
                }
                else
                {
                    StraightMove(realEndPoint);
                }
            }else{
                hasMoveCommand = false;
                Arrive();
            }
        }
    }
    public virtual void Move(Vector2 _position)
    {
        if(_position == null) return;

        if (!WallRaycast(_position)){
            StraightMove(_position);
        }
        else
        {
           PathMove(_position);
        }   
    }
    void StraightMove(Vector2 _position)
    {
        hasMoveCommand = true;
        moveStartPoint = transform.position;
        moveEndPoint = _position;
        currentTime = 0;
        timeToArive = Vector3.Distance(moveStartPoint, moveEndPoint)/speed;
        pathMovement = false;
    }
    void PathMove(Vector2 _position)
    {
        Node start = PathManager.instance.GetNearestNodeFromPosition(transform.position);
        Node end = PathManager.instance.GetNearestNodeFromPosition(_position);
        if (start != PathManager.instance.nullNode && end != PathManager.instance.nullNode)
        {
            path = PathManager.instance.GetPath(start, end);
            if (path != null)
            {
                hasMoveCommand = true;
                pathMovement = true;
                moveStartPoint = transform.position;
                moveEndPoint = new Vector2(path[0].x, path[0].y);
                currentTime = 0;
                timeToArive = Vector3.Distance(moveStartPoint, moveEndPoint)/speed;
                nodeIndex = 0;
                realEndPoint = _position;
            }
        }
    }
    public virtual void Follow(Transform _objectToFollow)
    {
        objectToFollow = _objectToFollow;
        Move(objectToFollow.position);
    }

    public virtual void Arrive()
    {
        
    }

    public virtual void Arrive(Transform foe)
    {
        
    }
    bool WallRaycast(Vector2 _end)
    {
        // Returns true if a wall is found between this object and the given point
        float distance = Vector2.Distance(transform.position, _end);
        Vector2 direction = (_end - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Wall"));
        return hit.collider != null;
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
        
        if(HP<=0)
        {
            Remove();
        }
    }

    internal Cell FindTargetToFollow()
    {
        GameObject[] foes = FindFoe();
        if(foes.Length == 0)return null;

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
        hasMoveCommand = false;
        objectToFollow = null;
    }

    public void TryToStopMoving()
    {
        GameObject[] foes = FindFoe();
        List<Cell> inRange = GetCellsInRange(foes, range);

        if(inRange.Count != 0 && shouldStop)
        {
            StopMoving();
            isShooting = true;
        }
        else if(isShooting)
        {
            isShooting = false;
            Cell foe = FindTargetToFollow();
            Follow(foe.transform);
        }
    }
}
