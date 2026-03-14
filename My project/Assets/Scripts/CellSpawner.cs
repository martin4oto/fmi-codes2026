using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections.Generic;
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

    private ActiveCell activeCellSpawing;

    private void Update()
    {
        SpawnCell();

        if (InputManager.instance.TestInput)
        {
            Debug.Log(InputManager.instance.MouseRelativeToBrainPosition);
            Debug.Log(CalculateSpawnDirection());
            InputManager.instance.UseTestInput();
        }

        if (InputManager.instance.SpawnCell1Input)
        {
            activeCellSpawing = ActiveCell.basic;
        }
        else if (InputManager.instance.SpawnCell2Input)
        {
            activeCellSpawing = ActiveCell.ranged;
        }
        else if (InputManager.instance.SpawnCell3Input)
        {
            activeCellSpawing = ActiveCell.bomb;
        }
        else if (InputManager.instance.SpawnCell4Input)
        {
            activeCellSpawing = ActiveCell.spawner;
        }
    }

    private void SpawnCell()
    {
        Vector2 spawnPos = GetSpawnPosition();
        var cellObj = Instantiate(cells[activeCellSpawing], spawnPos, Quaternion.identity);
        var cell = cellObj.GetComponent<Cell>();
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = CalculateSpawnDirection();
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

        return spawnPos;
    }

    private Vector2 CalculateSpawnDirection()
    {
        Vector2 direction = Vector2.zero;
        Vector2 mousePos = InputManager.instance.MouseRelativeToBrainPosition;

        direction.x = Mathf.Sign(mousePos.x);
        direction.y = Mathf.Sign(mousePos.y);

        return direction;
    }

    private Vector2 DetermineSpawnLocation(List<Transform> positions)
    {
        int chosenPosition = Random.Range(0, positions.Count);

        return positions[chosenPosition].position;
    }
}

public enum ActiveCell
{
    basic,
    bomb,
    ranged,
    spawner
}
