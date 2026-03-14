using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int DNA = 0;
    private float dnaGenerationPerSecond = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateDNA();
    }

    private void GenerateDNA()
    {
        StartCoroutine(GenerateDNACoroutine());
    }

    private IEnumerator GenerateDNACoroutine()
    {
        DNA++;
        yield return new WaitForSeconds(1f / dnaGenerationPerSecond);

        GenerateDNA();
    }

    public Vector2 GetScreenQuadrant()
    {
        Vector2 direction = Vector2.zero;
        Vector2 mousePos = InputManager.instance.MouseRelativeToBrainPosition;

        direction.x = Mathf.Sign(mousePos.x);
        direction.y = Mathf.Sign(mousePos.y);

        return direction;
    }
}
