using UnityEngine;

public class CellSpawnAnimations : MonoBehaviour
{
    [SerializeField] private float deploySpeed;
    [SerializeField] private float deployLength;
    private Vector2 finalPosition;
    private Cell cell;

    public void SetFinalPosition(Vector2 pos) 
    {
        finalPosition = pos;
    }

    void Update()
    {
        deployLength -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, deploySpeed * Time.deltaTime);
    }
}
