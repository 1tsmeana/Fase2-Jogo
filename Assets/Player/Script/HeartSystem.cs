using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HeartSystem : MonoBehaviour
{
    public int vida;
    public int vidaMaxima;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    public float tempoInvencivel = 0.5f; // Invencibilidade bem mais curta
    private bool invencivel = false;

    public SpriteRenderer personagemRenderer; // Sprite pra piscar

    private int personagemLayer;
    private int damageLayer;

    void Start()
    {
        personagemLayer = LayerMask.NameToLayer("Personagem");
        damageLayer = LayerMask.NameToLayer("Damage");
    }

    void Update()
    {
        HealthLogic();
    }

    void HealthLogic()
    {
        if (vida > vidaMaxima)
            vida = vidaMaxima;

        for (int i = 0; i < coracao.Length; i++)
        {
            coracao[i].sprite = (i < vida) ? cheio : vazio;
            coracao[i].enabled = (i < vidaMaxima);
        }
    }


public void TakeDamage(int dano)
{
    if (!invencivel)
    {
        vida -= dano;
        StartCoroutine(Invencibilidade());

        if (vida <= 0)
        {
            SceneManager.LoadScene("GameOver");  // Nome da sua cena de game over
        }
    }
}

    IEnumerator Invencibilidade()
    {
        invencivel = true;

        // Desativa colisão com objetos que causam dano
        Physics2D.IgnoreLayerCollision(personagemLayer, damageLayer, true);

        // Efeito visual de piscar
        for (float i = 0; i < tempoInvencivel; i += 0.3f)
        {
            personagemRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            personagemRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        // Reativa colisão
        Physics2D.IgnoreLayerCollision(personagemLayer, damageLayer, false);

        invencivel = false;
    }
}
