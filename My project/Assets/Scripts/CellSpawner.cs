using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CellSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> firstCornerSpawnLocations;
    [SerializeField]
    private List<Transform> secondCornerSpawnLocations;
    [SerializeField]
    private List<Transform> thirdCornerSpawnLocations;
    [SerializeField]
    private List<Transform> fourthCornerSpawnLocations;
    [SerializedDictionary("Cell Type", "Prefab")]
    public AYellowpaper.SerializedCollections.SerializedDictionary<ActiveCell, Cell> cells;
    public List<CellButton> cellButtons = new(); 
    [SerializeField]
    public bool automaticSpawning;
    public bool spawnCooldown = false;
    private ActiveCell activeCellSpawing = ActiveCell.none;
    private bool isLaunching;

    private void Update()
    {
        activeCellSpawing = DetermineCellSpawnType();
        if (activeCellSpawing == ActiveCell.none) return;

        if (!automaticSpawning && Input.GetMouseButtonDown(0)) SpawnCell();


        if (isLaunching) LaunchCell();
    }

    private ActiveCell DetermineCellSpawnType()
    {
        ResetActiveCellButton();
        if (InputManager.instance.SpawnCell1Input)
        {
            if (activeCellSpawing == ActiveCell.basic) activeCellSpawing = ActiveCell.none;
            else activeCellSpawing = ActiveCell.basic;

            InputManager.instance.UseSpawnCell1Input();
        }
        else if (InputManager.instance.SpawnCell2Input)
        {
            if (activeCellSpawing == ActiveCell.ranged) activeCellSpawing = ActiveCell.none;
            else activeCellSpawing = ActiveCell.ranged;

            InputManager.instance.UseSpawnCell2Input();
        }
        else if (InputManager.instance.SpawnCell3Input)
        {
            if (activeCellSpawing == ActiveCell.bomb) activeCellSpawing = ActiveCell.none;
            else activeCellSpawing = ActiveCell.bomb;

            InputManager.instance.UseSpawnCell3Input();
        }
        else if (InputManager.instance.SpawnCell4Input)
        {
            if (activeCellSpawing == ActiveCell.spawner) activeCellSpawing = ActiveCell.none;
            else activeCellSpawing = ActiveCell.spawner;

            InputManager.instance.UseSpawnCell4Input();
        }
        ActivateCellButton();
        return activeCellSpawing;
    }

    private void SpawnCell()
    {
        int dnaCost = cells[activeCellSpawing].dnaCost;
        if (spawnCooldown || GameManager.instance.DNA < dnaCost) return; //placeholder for dnaCost

        Vector2 spawnPos = GetSpawnPosition();
        var cellObj = Instantiate(cells[activeCellSpawing], spawnPos, Quaternion.identity);
        var cell = cellObj.GetComponent<Cell>();
        CellManager.instance.AddCell(cell);
        AudioManager.PlaySFX("shsh");
        GameManager.instance.DNA -= dnaCost;
        GameManager.instance.UpdateDNAText();

        spawnCooldown = true;

        LaunchCell();

        StartCoroutine(Cooldown(cell.spawnCooldown));
    }

    private void LaunchCell()
    {
        var quadrant = GameManager.instance.GetScreenQuadrant();


    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = GameManager.instance.GetScreenQuadrant();
        Vector2 spawnPos = Vector2.zero;

        if (direction.x < 0)
        {
            if (direction.y < 0) spawnPos = DetermineSpawnLocation(thirdCornerSpawnLocations);
            else spawnPos = DetermineSpawnLocation(firstCornerSpawnLocations);
        }
        else
        {
            if (direction.y < 0) spawnPos = DetermineSpawnLocation(fourthCornerSpawnLocations);
            else spawnPos = DetermineSpawnLocation(secondCornerSpawnLocations);
        }

        Vector2 offset = Vector2.zero;
        offset.x = Random.Range(-0.2f, 0.2f);
        offset.y = Random.Range(-0.2f, 0.2f);

        spawnPos += offset;

        return spawnPos;
    }

    private Vector2 DetermineSpawnLocation(List<Transform> positions)
    {
        int chosenPosition = Random.Range(0, positions.Count);

        return positions[chosenPosition].position;
    }

    private IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);

        spawnCooldown = false;
    }

    void ResetActiveCellButton()
    {
        if (activeCellSpawing != ActiveCell.none)
        {
            cellButtons[(int)activeCellSpawing].Reset();
        }
    }

    void ActivateCellButton()
    {
        if (activeCellSpawing != ActiveCell.none)
        {
            cellButtons[(int)activeCellSpawing].Activate();
        }
    }
}

public enum ActiveCell
{
    basic,
    bomb,
    ranged,
    spawner,
    none
}
