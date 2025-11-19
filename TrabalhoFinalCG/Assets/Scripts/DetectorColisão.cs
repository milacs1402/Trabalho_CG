using UnityEngine;

public class DetectorDeColisao : MonoBehaviour
{
    [Header("Configurações")]
    public float punicaoEmSegundos = 5f;
    public float tempoDeImunidade = 2f; // Fica 2 segundos sem perder tempo de novo

    private bool estaImune = false;
    private float tempoImuneAtual = 0f;

    void Update()
    {
        // Conta o tempo de imunidade
        if (estaImune)
        {
            tempoImuneAtual -= Time.deltaTime;
            if (tempoImuneAtual <= 0)
            {
                estaImune = false;
            }
        }
    }

    // Detecta colisão física 
    void OnCollisionEnter(Collision other)
    {
        // 1. Verifica se não esta imune e se bateu
        if (!estaImune && other.gameObject.CompareTag("obstaculo"))
        {
            // 2. Tenta achar o script do Timer na cena
            GerenciadorEntregas timer = FindFirstObjectByType<GerenciadorEntregas>();

            if (timer != null)
            {
                // 3. Tira o tempo
                timer.PerderTempo(punicaoEmSegundos);

                // 4. Ativa a imunidade para não perder de novo imediatamente
                estaImune = true;
                tempoImuneAtual = tempoDeImunidade;

                // colocar som de batida
                // AudioSource.PlayClipAtPoint(somBatida, transform.position);
            }
        }
    }
}