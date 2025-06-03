using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [Header("Fundo")]
    public Transform fundo;       // referência ao fundo que vai se mover
    public float startX = 19.32f;
    public float endX = 68.7f;
    public float speed = 1f;

    [Header("UI Elements")]
    public Transform gameOverUI;  // GameOver imagem
    public Transform botaoUI;     // botão Restart
    public Transform cameraTransform;
    public Vector3 uiOffset = new Vector3(0, 0, 0);

    void Update()
    {
        // Move fundo em loop
        if (fundo != null)
        {
            fundo.position += Vector3.right * speed * Time.deltaTime;
            if (fundo.position.x >= endX)
                fundo.position = new Vector3(startX, fundo.position.y, fundo.position.z);
        }

        // Mantém GameOver e botão fixos na tela (seguindo a câmera)
        if (cameraTransform != null)
        {
            Vector3 basePos = cameraTransform.position + uiOffset;
            if (gameOverUI != null)
                gameOverUI.position = basePos;
            if (botaoUI != null)
                botaoUI.position = basePos + new Vector3(0, -2f, 0);  // exemplo: botão um pouco abaixo da imagem
        }
    }
}
