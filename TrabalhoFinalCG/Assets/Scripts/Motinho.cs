using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControleMotinho : MonoBehaviour
{
    public float velocidade = 10000f;
    public float velocidadeVirar = 500f;
    private Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate() é usado para cálculos de física
    void FixedUpdate()
    {
        // 1. Acelerar e Frear (W e S)
        float inputAcelerar = Input.GetAxis("Vertical"); // Pega W (valor 1) ou S (valor -1)

        // Adiciona força para frente (ou para trás)
        // Usamos Time.deltaTime para a força ser consistente
        rb.AddForce(transform.forward * inputAcelerar * velocidade * Time.fixedDeltaTime);

        // 2. Virar (A e D)
        float inputVirar = Input.GetAxis("Horizontal"); // Pega A (valor -1) ou D (valor 1)

        // Adiciona "torque" (força de rotação) para virar
        rb.AddTorque(transform.up * inputVirar * velocidadeVirar * Time.fixedDeltaTime);
    }
}