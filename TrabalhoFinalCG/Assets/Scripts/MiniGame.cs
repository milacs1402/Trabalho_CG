using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class Minigame : MonoBehaviour
{

    public GameObject painelMinigame;     // Onde está a barra de espaço
    public Slider barraDeProgresso;
    public VideoPlayer videoPlayer;       // O vídeo da escada
    public RawImage telaDoVideo;          // A imagem onde o vídeo passa
    public AnimacaoCelular animacaoCelular; 
    public float dificuldade = 0.15f;     // Quanto enche por clique
    public AudioSource musicaCidade;     
    public AudioSource audioMinigame;    
    public AudioClip somChegada;         
    public AudioClip dinheiro;         
    public AudioClip musicaTensao;
    public AudioSource somMotor;
    public float atrasoParaComecar = 0.5f;
    public GerenciadorEntregas tempo;
    public float valorMaximo = 50f;      
    public float valorMinimo = 10f;    
    public float tempoParaPiorNota = 5f;
    public GerenciadorEntregas gerenciador;

    private float tempoGasto = 0f;
    private bool minigameAtivo = false;
    private AnimacaoCelular celular;


    void Start()
    {
        painelMinigame.SetActive(false);
        if (telaDoVideo != null) telaDoVideo.gameObject.SetActive(false);
        gerenciador = FindFirstObjectByType<GerenciadorEntregas>();
        celular = FindFirstObjectByType<AnimacaoCelular>();
    }

    void Update()
    {
        if (minigameAtivo)
        {
            tempoGasto += Time.unscaledDeltaTime;

            // Mecânica de apertar botão
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                barraDeProgresso.value += dificuldade;
                
                if (barraDeProgresso.value >= 1f)
                {
                    FinalizarEntrega();
                }
            }
            
            barraDeProgresso.value -= Time.unscaledDeltaTime * 0.5f;
        }
    }

    public float CalcularRecompensa()
    {
        float fatorDemora = Mathf.Clamp01(tempoGasto / tempoParaPiorNota);

        // Faz uma mistura (Lerp) entre 50 e 10 baseado na demora
        // Se fator for 0, retorna Maximo. Se for 1, retorna Minimo.
        return Mathf.Lerp(valorMaximo, valorMinimo, fatorDemora);
    }


    public void IniciarMinigame()
    {
        painelMinigame.SetActive(true);
        barraDeProgresso.value = 0;
        Time.timeScale = 0f;
        tempoGasto = 0f;


        if (animacaoCelular != null) 
            StartCoroutine(celular.MaximizarCelular());


        if (videoPlayer != null && telaDoVideo != null)
        {
            telaDoVideo.gameObject.SetActive(true); 
            videoPlayer.Play();
        }

        if (tempo != null) 
            tempo.modoUrgencia = true;

        StartCoroutine(SequenciaDeInicio());

        if (barraDeProgresso.value >= 1f)
        {
            FinalizarEntrega();
        }

    }

    public void FinalizarEntrega()
    {
        minigameAtivo = false;

        float lucro = CalcularRecompensa();

        if (gerenciador != null)
        {
            gerenciador.modoUrgencia = false; 
            gerenciador.ConfirmarEntregaReal(lucro); 
        }

        if(gerenciador.atingiuMeta == false)
        {
            Time.timeScale = 0f; //NAO SEI PQ N FUNFA
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (tempo != null)
            tempo.modoUrgencia = false;

        audioMinigame.Stop();

        AudioSource.PlayClipAtPoint(dinheiro, Camera.main.transform.position, 1.0f);

        musicaCidade.UnPause();
        somMotor.UnPause();

        painelMinigame.SetActive(false);
        telaDoVideo.gameObject.SetActive(false);
        videoPlayer.Stop();
        StartCoroutine(celular.MinimizarCelular());
    }

    IEnumerator SequenciaDeInicio()
    {

        minigameAtivo = false;



        if (musicaCidade != null) musicaCidade.Pause();
        if (somMotor != null) somMotor.Pause();


        if (somChegada != null)
            AudioSource.PlayClipAtPoint(somChegada, Camera.main.transform.position, 1.0f);


        yield return new WaitForSecondsRealtime(atrasoParaComecar);

        if (audioMinigame != null)
        {
            audioMinigame.clip = musicaTensao;
            audioMinigame.loop = true;
            audioMinigame.ignoreListenerPause = true;
            audioMinigame.spatialBlend = 0f;
            audioMinigame.Play();
        }

        minigameAtivo = true;
    }
}