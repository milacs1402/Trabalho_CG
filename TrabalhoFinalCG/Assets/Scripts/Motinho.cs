using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class ControleMotinho : MonoBehaviour
{
    public float velocidade = 10000f;
    public float velocidadeVirar = 500f;

    private Rigidbody rb;
    private MyInputActions controles; 
    private Vector2 entradaMovimento;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controles = new MyInputActions();

        controles.Player.Movimento.performed += ctx => entradaMovimento = ctx.ReadValue<Vector2>();

        controles.Player.Movimento.canceled += ctx => entradaMovimento = Vector2.zero;
    }

    void OnEnable()
    {
        controles.Enable();
    }

    void OnDisable()
    {
        controles.Disable();
    }

    void FixedUpdate()
    {
        float acelerar = entradaMovimento.y;
        float virar = entradaMovimento.x;

        rb.AddForce(transform.forward * acelerar * velocidade * Time.fixedDeltaTime);
        rb.AddTorque(transform.up * virar * velocidadeVirar * Time.fixedDeltaTime);
    }
}