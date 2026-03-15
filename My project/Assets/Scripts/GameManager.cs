using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int DNA = 0;
    [SerializeField]
    private float dnaGenerationPerSecond = 1f;
    public Transform worldParticles;

    [SerializeField]
    private TMP_Text DNAText;
    public List<TextMeshProUGUI> costLabels = new();
    public Color tooExpensiveColor;
    
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
        UpdateDNAText();
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

    public void UpdateDNAText()
    {
        DNAText.text = DNA.ToString();
        foreach (TextMeshProUGUI label in costLabels)
        {
            int cost = int.Parse(label.text);
            if (DNA >= cost)
            {
                label.color = Color.white;
            }
            else
            {
                label.color = tooExpensiveColor;
            }
        }
    }
}
