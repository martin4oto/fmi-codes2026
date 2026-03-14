using System.Collections.Generic;
using UnityEngine;

public class Duplicater : Cell
{
    public float maxShootingTimer;
    public float duplicateCooldown = 2f;
    public float displacementForce = 10f;

    float currentShootingTimer = 0;
    bool isDuplicateCooldownOn = false;
    float cooldownTimer = 2f;
    void Update()
    {
        base.Update();
        if (isShooting)
        {
            currentShootingTimer += Time.deltaTime;
            if (currentShootingTimer >= maxShootingTimer)
            {
                GameObject[] foes = FindFoe();
                TryToShoot(foes);
                currentShootingTimer = 0;
            }
        }
        else
        {
            currentShootingTimer = maxShootingTimer + 1;
        }

        if (!isDuplicateCooldownOn || cooldownTimer <= 0)
        {
            Duplicate();
        }
        else cooldownTimer -= Time.deltaTime;
    }
    void TryToShoot(GameObject[] foes)
    {
        List<GameObject> FoesInRange = new List<GameObject>();

        for (int i = 0; i < foes.Length; i++)
        {
            Vector3 foePosition = foes[i].transform.position;

            if (Vector3.Distance(foePosition, transform.position) < range)
            {
                FoesInRange.Add(foes[i]);
            }
        }

        if (FoesInRange.Count > 0)
        {
            Shoot(FoesInRange);
        }
    }
    void Shoot(List<GameObject> foesInRange)
    {
        for (int i = 0; i < foesInRange.Count; i++)
        {
            Cell foeCellScript = foesInRange[i].GetComponent<Cell>();
            Debug.Log("shoot");

            foeCellScript.TakeDamage(DMG);
        }
    }

    private void Duplicate()
    {
        Vector2 force = new Vector2(displacementForce, 0);
        var copy = Instantiate(this, transform.position, transform.rotation);
        GetComponent<Rigidbody>().AddForce(force);
        copy.GetComponent<Rigidbody>().AddForce(-force);

        isDuplicateCooldownOn = true;
        cooldownTimer = duplicateCooldown;
    }
}
