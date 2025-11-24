using UnityEngine;
using UnityEngine.InputSystem; 

public class ControleMotinho : MonoBehaviour
{
    [Header("Configurações da Moto")]
    public float velocidade = 10000f;
    public float velocidadeVirar = 500f;
    
    [Header("Configurações de Drift")]
    public float multiplicadorGiroDrift = 1.5f; 
    [Range(0, 1)] 
    public float aderenciaNormal = 0.95f; 
    [Range(0, 1)] 
    public float aderenciaDrift = 0.2f;   

    [Header("Efeitos Visuais")]

    public Animator animatorPiloto;

    [Header("Áudio")]
    public AudioSource audioMotor;       
    public AudioSource audioBuzina; 
    public AudioClip somBuzinaClip;

    [Tooltip("Tom mínimo do motor (parado)")]
    public float pitchMinimo = 1.0f; 
    [Tooltip("Tom máximo do motor (velocidade máxima)")]
    public float pitchMaximo = 3.0f;

    private Rigidbody rb;
    private MyInputActions controles; 
    private Vector2 entradaMovimento;
    private bool estaDriftando; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controles = new MyInputActions();

        controles.Player.Movimento.performed += ctx => entradaMovimento = ctx.ReadValue<Vector2>();
        controles.Player.Movimento.canceled += ctx => entradaMovimento = Vector2.zero;

        controles.Player.Drift.performed += ctx => estaDriftando = true;
        controles.Player.Drift.canceled += ctx => estaDriftando = false;
    
        controles.Player.Buzina.performed += ctx => TocarBuzina();
    }

    void OnEnable() => controles.Enable();
    void OnDisable() => controles.Disable();

    void Update()
    {
        AtualizarAnimacao();
        AtualizarSomMotor();
    }

    void FixedUpdate()
    {
        MoverMoto();
        ControlarAderencia();
    }

    void MoverMoto()
    {
        float acelerar = entradaMovimento.y;
        float virar = entradaMovimento.x;

        float giroAtual = estaDriftando ? velocidadeVirar * multiplicadorGiroDrift : velocidadeVirar;

        rb.AddForce(transform.forward * acelerar * velocidade * Time.fixedDeltaTime);
        
        rb.AddTorque(transform.up * virar * giroAtual * Time.fixedDeltaTime);
    }

    void ControlarAderencia()
    {
        Vector3 velocidadeLocal = transform.InverseTransformDirection(rb.linearVelocity);

        float aderenciaAtual = estaDriftando ? aderenciaDrift : aderenciaNormal;
        
        velocidadeLocal.x = Mathf.Lerp(velocidadeLocal.x, 0f, aderenciaAtual);

        rb.linearVelocity = transform.TransformDirection(velocidadeLocal);
    }
    void AtualizarAnimacao()
    {
        if (animatorPiloto != null)
        {
            float inputFrenteTras = entradaMovimento.y;
            animatorPiloto.SetFloat("InputVertical", inputFrenteTras, 0.1f, Time.deltaTime);
        }
    }

    void AtualizarSomMotor()
    {
        if (audioMotor == null) return;

        float velocidadeAtual = rb.linearVelocity.magnitude;
        
        float velocidadeTopEstimada = 30f; 

        float porcentagemVelocidade = Mathf.Clamp01(velocidadeAtual / velocidadeTopEstimada);

        audioMotor.pitch = Mathf.Lerp(pitchMinimo, pitchMaximo, porcentagemVelocidade);
    }

    void TocarBuzina()
    {
        if (audioBuzina != null && somBuzinaClip != null)
        {
            if (!audioBuzina.isPlaying) 
            {
                audioBuzina.PlayOneShot(somBuzinaClip);
            }

        }
    }


}