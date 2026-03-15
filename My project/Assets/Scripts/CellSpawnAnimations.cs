using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CellSpawnAnimations : MonoBehaviour
{
    [SerializeField] private float deploySpeed;
    [SerializeField] private float deployLength;
    private bool isSpawning;
    private Vector2 finalPosition;

    void Update()
    {
        while (isSpawning)
        {
            transform.position += Vector3.Lerp(transform.position, finalPosition, deploySpeed * Time.deltaTime);
        }
    }

    public void PlaySpawnAnimationTo(Vector2 position)
    {
        Debug.Log(transform.position);
        Debug.Log(position);

        isSpawning = true;
        finalPosition = position;

        StartCoroutine(AnimationDuration(deployLength));
    }

    private IEnumerator AnimationDuration(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        isSpawning = false;
    }
}
