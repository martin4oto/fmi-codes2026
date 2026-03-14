using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    [SerializeField] private float blinkDecaySpeed = 1f;

    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    private MaterialPropertyBlock propertyBlock;
    private float blinkFactor;

    private void Start()
    {
        propertyBlock = new MaterialPropertyBlock();

        if (spriteRenderers.Count == 0) spriteRenderers.Add(GetComponent<SpriteRenderer>());
    }

    private void Update()
    {
        if (blinkFactor <= 0f) return;

        blinkFactor = Mathf.Lerp(blinkFactor, 0f, Time.deltaTime * blinkDecaySpeed);
        if (blinkDecaySpeed <= 0.01f) blinkFactor = 0f;

        ApplyBlinkFactor();
    }

    public void Blink()
    {
        blinkFactor = 1f;
        ApplyBlinkFactor();
    }

    private void ApplyBlinkFactor()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_BlinkFactor", blinkFactor);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}