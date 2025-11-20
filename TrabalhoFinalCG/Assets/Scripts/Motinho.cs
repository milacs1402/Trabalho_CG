using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class ControleMotinho : MonoBehaviour
{
    [Header("Configurações da Moto")]
    public float velocidade = 10000f;
    public float velocidadeVirar = 500f;
    
    [Header("Configurações de Drift")]
    public float multiplicadorGiroDrift = 1.5f; // Gira mais rápido no drift
    [Range(0, 1)] 
    public float aderenciaNormal = 0.95f; // 1 = Gruda no chão, 0 = Gelo
    [Range(0, 1)] 
    public float aderenciaDrift = 0.2f;   // Quanto menor, mais escorrega

    private Rigidbody rb;
    private MyInputActions controles; 
    private Vector2 entradaMovimento;
    private bool estaDriftando; // Variável para saber se o botão está apertado

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controles = new MyInputActions();

        // Configuração do Movimento (WASD / Stick Esquerdo)
        controles.Player.Movimento.performed += ctx => entradaMovimento = ctx.ReadValue<Vector2>();
        controles.Player.Movimento.canceled += ctx => entradaMovimento = Vector2.zero;

        // --- Configuração do Drift (Espaço / Botão Sul / Shift) ---
        // Você precisará adicionar uma ação chamada "Drift" no seu Input Actions
        controles.Player.Drift.performed += ctx => estaDriftando = true;
        controles.Player.Drift.canceled += ctx => estaDriftando = false;
    }

    void OnEnable() => controles.Enable();
    void OnDisable() => controles.Disable();

    void FixedUpdate()
    {
        MoverMoto();
        ControlarAderencia(); // A mágica do drift acontece aqui
    }

    void MoverMoto()
    {
        float acelerar = entradaMovimento.y;
        float virar = entradaMovimento.x;

        // Se estiver driftando, aumentamos a velocidade de giro para facilitar a curva fechada
        float giroAtual = estaDriftando ? velocidadeVirar * multiplicadorGiroDrift : velocidadeVirar;

        // Aplica força para frente
        rb.AddForce(transform.forward * acelerar * velocidade * Time.fixedDeltaTime);
        
        // Aplica rotação
        rb.AddTorque(transform.up * virar * giroAtual * Time.fixedDeltaTime);
    }

    void ControlarAderencia()
    {
        // 1. Transforma a velocidade do mundo para local (X = lado, Z = frente)
        Vector3 velocidadeLocal = transform.InverseTransformDirection(rb.linearVelocity);

        // 2. Decide o quanto vamos "matar" a velocidade lateral
        // Se driftando, escorrega mais (aderenciaDrift). Se normal, segura mais (aderenciaNormal).
        float aderenciaAtual = estaDriftando ? aderenciaDrift : aderenciaNormal;

        // 3. Aplica a aderência no eixo X (Lateral)
        // Mathf.Lerp faz a transição suave da velocidade lateral para 0 baseada na aderência
        velocidadeLocal.x = Mathf.Lerp(velocidadeLocal.x, 0f, aderenciaAtual);

        // 4. Devolve a velocidade calculada para o Rigidbody (Mundo)
        rb.linearVelocity = transform.TransformDirection(velocidadeLocal);
    }
}