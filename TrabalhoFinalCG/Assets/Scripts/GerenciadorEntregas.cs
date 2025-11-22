using System.Collections.Generic; // usar listas
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GerenciadorEntregas : MonoBehaviour
{
    [Header("Configurações")]
    public List<GameObject> pontosDeEntrega;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI placar;
    public GameObject PerdeuTempo;
    public float time;
    public GameObject painelVitoria;
    public GameObject painelDerrota;
    public GPSNavegador gps;


    private float pontos = 0;
    private bool isTimeOver = false;
    private int indiceAtual = 0; 
    private int entregasFeitas = 0;
    private int totalParaVencer;
    private int PlacarGeral;

    void Start()
    {
        PlacarGeral = PlayerPrefs.GetInt("PlacarGeral");
        totalParaVencer = pontosDeEntrega.Count; 
        entregasFeitas = 0;
        AtualizarUI();

        foreach (GameObject ponto in pontosDeEntrega)
        {
            ponto.SetActive(false);
        }

        if (pontosDeEntrega.Count > 0)
        {
            AtivarPonto(0);
        }
    }

    private void Update()
    {
        TimeCount();
    }

    void AtivarPonto(int index)
    {
        // Ativa o visual do ponto novo
        pontosDeEntrega[index].SetActive(true);

        // --- AQUI ESTÁ A CORREÇÃO ---
        // Avisa o GPS: "Ei, o novo destino é esse aqui!"
        if (gps != null)
        {
            // Estamos mudando a variável 'destino' dentro do outro script
            gps.destino = pontosDeEntrega[index].transform;
            Debug.Log("MUDEI O DESTINO PARA: " + pontosDeEntrega[index].name); // <--- OLHE O CONSOLE
        }
    }

    public void EntregaRealizada()
    {
        pontosDeEntrega[indiceAtual].SetActive(false);

        entregasFeitas++;
        pontos++;
        AtualizarUI();

        if (entregasFeitas >= totalParaVencer)
        {
            FimDeJogo();
        }
        else
        {
            indiceAtual++;

            AtivarPonto(indiceAtual);
        }
    }

    void TimeCount()
    {
        if(!isTimeOver)
        {
            time -= Time.deltaTime;
            AtualizarUI();

            if(time <= 0)
            {
                time = 0;
                painelDerrota.SetActive(true);
                Time.timeScale = 0f;
                isTimeOver = true;
            }
        }
    }

    public void PerderTempo(float segundos)
    {
        time -= segundos;

        Debug.Log("BATEU! Perdeu " + segundos + " segundos!");
        StartCoroutine(MostrarFeedbackDeDano());
        AtualizarUI(); 
    }

    IEnumerator MostrarFeedbackDeDano()
    {
        // 1. Mostra o texto "-5"
        if (PerdeuTempo != null)
        {
            PerdeuTempo.SetActive(true);
        }

        // 2. Espera 1 segundo (o jogo continua rodando)
        yield return new WaitForSeconds(2f);

        // 3. Esconde o texto de novo
        if (PerdeuTempo != null)
        {
            PerdeuTempo.SetActive(false);
        }
    }
    void AtualizarUI()
    {
        timer.text = time.ToString("F0");
        placar.text = pontos.ToString("F0") + "/7";
    }

    void FimDeJogo()
    {
        if (painelVitoria != null)
            painelVitoria.SetActive(true);
        Time.timeScale = 0f;

        if (PlayerPrefs.GetInt("PrimeiraVitoria1") == 1) //isso nn ta funcionandooooooooooooooooooo
        {
            PlayerPrefs.SetInt("PlacarGeral", PlacarGeral++);
            PlayerPrefs.SetInt("PrimeiraVitoria1", 0);
        }


    }
}