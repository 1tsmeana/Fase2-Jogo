using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Personagem;
    public float minX;
    public float maxX; // Novo limite máximo
    public float timeLerp;

    private void FixedUpdate()
    {
        Vector3 newPosition = Personagem.position + new Vector3(0, 0, -10);
        newPosition.y = 0.1f;
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);

        // Limita o eixo X dentro do intervalo minX e maxX
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }
}
    