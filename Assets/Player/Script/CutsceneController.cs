using System.Collections;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject personagem;
    public GameObject onibus;
    public GameObject pontoParada1;
    public GameObject pontoParada2;
    public GameObject pontoParadaOnibus;
    public GameObject pontoEntradaOnibus;
    public GameObject pontoSaidaOnibus;
    public Camera mainCamera;

    private Animator animatorPersonagem;

    private bool cutsceneAtiva = false;
    private CameraFollow cameraFollow;

    void Start()
    {
        animatorPersonagem = personagem.GetComponent<Animator>();
        cameraFollow = mainCamera.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cutsceneAtiva) return;

        if (collision.CompareTag("Personagem"))
        {
            Debug.Log("Cutscene iniciou");
            cutsceneAtiva = true;
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        Movimento movimentoScript = personagem.GetComponent<Movimento>();

        // Desativa script Movimento e câmera para controle manual
        movimentoScript.enabled = false;
        cameraFollow.enabled = false;

        // Primeira parada do personagem (parado)
        personagem.transform.position = new Vector2(pontoParada1.transform.position.x, pontoParada1.transform.position.y);
        animatorPersonagem.SetBool("Movendo", false);

        yield return new WaitForSeconds(0.5f);

        // Move a câmera até próximo do ônibus
        yield return StartCoroutine(MoverCamera(new Vector3(114.6441f, mainCamera.transform.position.y, mainCamera.transform.position.z), 2f));

        yield return new WaitForSeconds(0.5f);

        // Personagem anda até ponto 2
        animatorPersonagem.SetBool("Movendo", true);
        yield return StartCoroutine(MoverPersonagem(new Vector2(pontoParada2.transform.position.x, pontoParada2.transform.position.y), 2f));
        animatorPersonagem.SetBool("Movendo", false);

        yield return new WaitForSeconds(0.5f);

        // Ônibus entra na cena vindo do ponto de entrada
        onibus.transform.position = new Vector2(pontoEntradaOnibus.transform.position.x, pontoParadaOnibus.transform.position.y);
        yield return StartCoroutine(MoverOnibus(new Vector2(pontoParadaOnibus.transform.position.x, pontoParadaOnibus.transform.position.y), 2f));

        yield return new WaitForSeconds(1f);

        // Personagem some (entra no ônibus)
        personagem.SetActive(false);

        // Ônibus sai da cena indo pro ponto de saída
        yield return StartCoroutine(MoverOnibus(new Vector2(pontoSaidaOnibus.transform.position.x, pontoParadaOnibus.transform.position.y), 2f));

        Debug.Log("Cutscene finalizada");

        // Só reativa o movimento do personagem, não ativa a câmera
        movimentoScript.enabled = true;
        // cameraFollow permanece desativado
    }

    IEnumerator MoverCamera(Vector3 destino, float duracao)
    {
        Vector3 origem = mainCamera.transform.position;
        float tempo = 0;

        while (tempo < duracao)
        {
            mainCamera.transform.position = Vector3.Lerp(origem, destino, tempo / duracao);
            tempo += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = destino;
    }

    IEnumerator MoverPersonagem(Vector2 destino, float duracao)
    {
        Vector2 origem = personagem.transform.position;
        float tempo = 0;

        while (tempo < duracao)
        {
            personagem.transform.position = Vector2.Lerp(origem, destino, tempo / duracao);
            tempo += Time.deltaTime;
            yield return null;
        }
        personagem.transform.position = destino;
    }

    IEnumerator MoverOnibus(Vector2 destino, float duracao)
    {
        Vector2 origem = onibus.transform.position;
        float tempo = 0;

        while (tempo < duracao)
        {
            onibus.transform.position = Vector2.Lerp(origem, destino, tempo / duracao);
            tempo += Time.deltaTime;
            yield return null;
        }
        onibus.transform.position = destino;
    }
}