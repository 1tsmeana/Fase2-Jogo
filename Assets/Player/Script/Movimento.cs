using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int velocidade = 15;
    [SerializeField] private Transform PeDoPersonagem;
    [SerializeField] private LayerMask chaoLayer;

    private bool estaNoChao;
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private int MovendoHash = Animator.StringToHash("Movendo");
    private int SaltandoHash = Animator.StringToHash("Saltando");

    // 🔊 Áudio de pulo
    [SerializeField] private AudioClip somPulo;
    private AudioSource audioSource;

    // Bloqueio para cutscene
    public bool bloqueado = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (bloqueado)
        {
            animator.SetBool("Movendo", false);
            animator.SetBool("Saltando", false);
            horizontalInput = 0;
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.AddForce(Vector2.up * 600);
            audioSource.PlayOneShot(somPulo); // 🔊 toca o som de pulo
        }

        estaNoChao = Physics2D.OverlapCircle(PeDoPersonagem.position, 0.2f, chaoLayer);

        animator.SetBool("Movendo", horizontalInput != 0);
        animator.SetBool("Saltando", !estaNoChao);

        if (horizontalInput > 0)
            spriteRenderer.flipX = false;
        else if (horizontalInput < 0)
            spriteRenderer.flipX = true;
    }

    private void FixedUpdate()
    {
        if (bloqueado)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }
        rb.linearVelocity = new Vector2(horizontalInput * velocidade, rb.linearVelocity.y);
    }
}
