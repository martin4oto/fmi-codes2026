using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> upSpawnLocations;
    [SerializeField]
    private List<Transform> downSpawnLocations;
    [SerializeField]
    private List<Transform> leftSpawnLocations;
    [SerializeField]
    private List<Transform> rightSpawnLocations;
    [SerializedDictionary("Cell Type", "Prefab")]
    public SerializedDictionary<string, Cell> cells;

    private void Update()
    {
        SpawnCell();

        if (InputManager.instance.TestInput)
        {
            Debug.Log(InputManager.instance.MouseRelativeToBrainPosition);
            Debug.Log(CalculateSpawnDirection());
            InputManager.instance.UseTestInput();
        }
    }

    private void SpawnCell()
    {
        
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = CalculateSpawnDirection();
        Vector2 spawnPos = CalculateSpawnDirection();

        if (direction.x < 0) spawnPos = DetermineSpawnLocation(leftSpawnLocations);
        else if (direction.x > 0) spawnPos = DetermineSpawnLocation(rightSpawnLocations);
        else if (direction.y < 0) spawnPos = DetermineSpawnLocation(downSpawnLocations);
        else spawnPos = DetermineSpawnLocation(upSpawnLocations);

        return spawnPos;
    }

    private Vector2 CalculateSpawnDirection()
    {
        Vector2 direction = Vector2.zero;
        Vector2 mousePos = InputManager.instance.MouseRelativeToBrainPosition;

        if (Mathf.Abs(mousePos.x) >= Mathf.Abs(mousePos.y * 2)) direction.x = Mathf.Sign(mousePos.x);
        else direction.y = Mathf.Sign(mousePos.y);

        return direction;
    }

    private Vector2 DetermineSpawnLocation(List<Transform> positions)
    {
        int chosenPosition = UnityEngine.Random.RandomRange(0, positions.Count);

        return positions[chosenPosition].position;
    }
}
