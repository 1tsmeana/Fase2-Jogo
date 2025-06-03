using UnityEngine;
using System.Collections;

public class GameOverBlink : MonoBehaviour
{
    public float blinkInterval = 0.5f; // meio segundo ligado, meio segundo apagado

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
