using UnityEngine;

public class PontoEntrega : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GerenciadorEntregas gerente = FindFirstObjectByType<GerenciadorEntregas>();

            if (gerente != null)
            {
                gerente.EntregaRealizada();
            }
        }
    }
}