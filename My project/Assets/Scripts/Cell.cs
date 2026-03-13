using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class Cell:MonoBehaviour
{
    int HP;
    internal bool isEnemy;
    internal bool alreadyTargetted;

    [Header("Stats")]
    public float speed;
    public float timeToArive;
    public float currentTime;
    public bool hasMoveCommand;
    public Vector3 moveStartPoint;
    public Vector3 moveEndPoint;
    List<Node> path;
    int nodeIndex;
    bool pathMovement;
    Vector3 realEndPoint;
    public bool TEST;
    public Vector2 coords;

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
        if (hasMoveCommand)
        {
            MoveStep();
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
                StopMoving();
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
        //TODO
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
    }
}
