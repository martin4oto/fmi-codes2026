using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Cell:MonoBehaviour
{
    int HP;
    
    [SerializeField]
    internal bool isEnemy;
    
    internal bool alreadyTargetted;
    internal bool isShooting;

    [Header("Stats")]
    public float speed;
    public float range;
    public bool shouldStop;
    public int DMG;
    public int maxHP;
    public float spawnCooldown;    
    public float targettingRange;
    public string info;
    float wanderDelay;

    float timeToArive;
    float currentTime;
    bool hasMoveCommand;
    Vector3 moveStartPoint;
    Vector3 moveEndPoint;
    List<Node> path;
    int nodeIndex;
    bool pathMovement;
    Vector3 realEndPoint;
    protected Transform objectToFollow;
    float followTolerance = 0.25f;

    void Start()
    {
        HP = maxHP;
        alreadyTargetted = false;

        if (isEnemy)
        {
            EnemyDefault();
        }

        RotateByQuadrant();
    }
    protected void Update()
    {
        if (hasMoveCommand)
        {
            MoveStep();
        }

        if (objectToFollow != null)
        {
            if (Vector2.Distance(transform.position, objectToFollow.position) <= followTolerance)
            {
                Arrive(objectToFollow);
                objectToFollow = null;
            }
            else
            {
                Move(objectToFollow.position);
            }
        }

        if (wanderDelay > 0)
        {
            wanderDelay -= Time.deltaTime;
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
                    moveEndPoint = new Vector2(path[nodeIndex].x * PathManager.instance.gridOffset, path[nodeIndex].y * PathManager.instance.gridOffset);
                    float distance = Vector3.Distance(moveStartPoint, moveEndPoint);
                    currentTime = 0;
                    timeToArive = distance / speed;
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
                moveEndPoint = new Vector2(path[0].x*PathManager.instance.gridOffset, path[0].y*PathManager.instance.gridOffset);
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
            // death anim
            AudioManager.PlaySFX("pop");
            Remove();
        }
    }

    internal Cell FindTargetToFollow()
    {
        GameObject[] foes = FindFoe();
        if(foes.Length == 0) return null;

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

    internal Cell FindTargetToFollow(float _range)
    {
        GameObject[] foes = FindFoe();
        if(foes.Length == 0) return null;

        Cell almostValidCell = null;

        for(int i = 0; i<foes.Length; i++)
        {
            Cell current = foes[i].GetComponent<Cell>();
            float currentDistance = Vector2.Distance(current.transform.position, transform.position);

            if(currentDistance<_range)
            {
                if(!current.alreadyTargetted)
                {
                    return current;
                }
                else
                {
                    almostValidCell = current;
                }
            }
        }
        return almostValidCell;
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
        if(!isEnemy)
        {
            CellManager.instance.RemoveCell(this);
        }
        else
        {
            CellManager.instance.RemoveVirus(this);
            if (transform.childCount != 0)
            {
                for (int i = transform.childCount-1; i >= 0; i--)
                {
                    Cell childCell = transform.GetChild(i).GetComponent<Cell>();
                    if (childCell != null) childCell.Remove();
                }
            }
        }  
    }

    protected void StopMoving()
    {
        hasMoveCommand = false;
        objectToFollow = null;
    }

    public void TryToStopMoving()
    {
        if(isEnemy&&!hasATarget)return;

        GameObject[] foes = FindFoe();
        List<Cell> inRange = GetCellsInRange(foes, range);

        if(inRange.Count != 0 && shouldStop)
        {
            StopMoving();
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
        
        if(!isShooting)
        {
            if(isEnemy)
            {
                EnemyDefault();
            }
            else
            {
                Cell foe = FindTargetToFollow();
                if(foe == null)return;

                Follow(foe.transform);
            }
        }
    }

    void EnemyDefault()
    {
        Move(Vector3.zero);
        hasATarget = false;
    }

    bool hasATarget = false;
    public void TryToTargetCell()
    {
        if(hasATarget)return;

        Cell target = FindTargetToFollow(targettingRange);
        if(target != null)
        {
            Follow(target.transform);
            hasATarget = true;
        }
    }

    private void RotateByQuadrant()
    {
        float newRotation = 0f;
        Vector2 direction = GameManager.instance.GetScreenQuadrant();

        if (direction.x < 0)
        {
            if (direction.y < 0) newRotation = 35;
            else newRotation = -35;
        }
        else
        {
            if (direction.y < 0) newRotation = 135;
            else newRotation = -135;
        }

        transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }

    protected void Wander()
    {
        if (wanderDelay > 0) return;

        Vector2 destination = (Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Move(destination);
        wanderDelay = Random.Range(1f, 6f);
    }
}
