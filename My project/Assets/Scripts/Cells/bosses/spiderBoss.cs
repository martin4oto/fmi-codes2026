using System.Collections.Generic;
using UnityEngine;

public class spiderBoss:Cell
{
    [Header("BossStats")]
    public GameObject projectile;
    public float[] deathPercentages;
    public int waitBetweeenMovingTime;
    public int shootSpeed;

    [SerializeField]
    private Animator animator;
    
    int waitTimer;
    int behaviourStage;

    int BossStage;
    bool bossMoving = false;

    public override void BossLogic()
    {
        if(DeathConditions())
        {
            return;
        }

        AttackLogic();

        if(waitTimer<=waitBetweeenMovingTime)
        {
            waitTimer++;
            return;
        }

        if(bossMoving)return;

        if(behaviourStage%2 == 0)
        {
            Vector3 position = transform.position;
            position.x *= -1;

            Move(position);
            bossMoving = true;
            animator.SetTrigger("hasJumped");
        }
        else
        {
            Vector3 position = transform.position;
            position.y *= -1;

            Move(position);
            bossMoving = true;
            animator.SetTrigger("hasJumped");
        }
    }

    public override void Arrive()
    {
        bossMoving = false;
        base.Arrive();
        behaviourStage++;
        waitTimer = 0;
    }

    public bool DeathConditions()
    {
        float HPPercentage = (float)HP/maxHP;

        if(HPPercentage < deathPercentages[BossStage])
        {
            BossStage++;
            CellManager.instance.HideBoss();
        }

        return false;
    }

    int shootingStage;
    int shootTimer;
    void AttackLogic()
    {
        if(shootTimer>shootSpeed)
        {
            if(shootingStage%2 == 0)
            {
                GameObject[] foes = FindFoe();
                WaveAttackPrepare(foes);
            }
            else
            {
                shootAtBrain();
            }

            shootTimer = 0;
            shootingStage++;
        }
        shootTimer++;
    }

    void WaveAttackPrepare(GameObject[] foes)
    {
        List<GameObject> FoesInRange = new List<GameObject>();

        for(int i = 0; i<foes.Length; i++)
        {
            Vector3 foePosition = foes[i].transform.position;

            if(Vector3.Distance(foePosition, transform.position) < range)
            {
                FoesInRange.Add(foes[i]);
            }
        }

        if(FoesInRange.Count > 0)
        {
            WaveAttack(FoesInRange);
        }
    }

    void WaveAttack(List<GameObject> foesInRange)
    {
        //choose a clip for the boss attack
        AudioManager.PlaySFX("bomb");

        for(int i = 0; i<foesInRange.Count; i++)
        {
            Cell foeCellScript = foesInRange[i].GetComponent<Cell>();

            foeCellScript.TakeDamage(DMG); 
        }
    }

    void shootAtBrain()
    {
        Cell projectileCell = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Cell>();

        CellManager.instance.AddVirus(projectileCell);
    }
}
