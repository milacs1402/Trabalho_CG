using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DetectorDeColisao : MonoBehaviour
{
    [Header("Regras de Jogo")]    
    public float punicaoEmSegundos = 5f;
    public float tempoDeImunidade = 2f; // Fica 2 segundos sem perder tempo de novo

    [Header("Sensação de Impacto")]

    public AudioClip somBatida;
    //public ParticleSystem faiscasColisao; 
    
    [Header("Visuais da Moto")]

    public Renderer[] modelosVisuais;
    
    public float forcaDoRecuo = 5000f; 
    private bool estaImune = false;
    private float tempoImuneAtual = 0f;

    private AudioSource audioSource;

    private Rigidbody rb;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        
        if (modelosVisuais == null || modelosVisuais.Length == 0)
        {
            modelosVisuais = GetComponentsInChildren<Renderer>();
        }
    }

    void Update()
    {
        // Conta o tempo de imunidade
        if (estaImune)
        {
            tempoImuneAtual -= Time.deltaTime;
            EfeitoPiscar();
            
            if (tempoImuneAtual <= 0)
            {
                PararImunidade();
            }
        }
    }

    // Detecta colisão física 
    void OnCollisionEnter(Collision other)
    {

        if (!estaImune && other.gameObject.CompareTag("obstaculo"))
        {
            AplicarDano(other);
        }
    }

    void AplicarDano(Collision other)
    {
        GerenciadorEntregas timer = FindFirstObjectByType<GerenciadorEntregas>();
        if (timer != null)
        {
            timer.PerderTempo(punicaoEmSegundos);
        }

        if (somBatida != null)
        {
            audioSource.PlayOneShot(somBatida);
        }

//        if (faiscasColisao != null)
//        {
//            faiscasColisao.transform.position = other.contacts[0].point;
//            faiscasColisao.Play();
//        }

        Vector3 direcaoImpacto = (transform.position - other.contacts[0].point).normalized;
        rb.AddForce((direcaoImpacto + Vector3.up) * forcaDoRecuo, ForceMode.Impulse);

        estaImune = true;
        tempoImuneAtual = tempoDeImunidade;
    }

    void EfeitoPiscar()
    {

        bool deveAparecer = Mathf.Repeat(Time.time * 10f, 1f) > 0.5f;

        foreach (Renderer modelo in modelosVisuais)
        {
            if(modelo != null) modelo.enabled = deveAparecer;
        }
    }

    void PararImunidade()
    {
        estaImune = false;
        
        foreach (Renderer modelo in modelosVisuais)
        {
            if(modelo != null) modelo.enabled = true;
        }
    }

}