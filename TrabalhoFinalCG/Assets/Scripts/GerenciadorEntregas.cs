using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GerenciadorEntregas : MonoBehaviour
{
    [Header("Meta de Vitória")]
    public float metaDeDinheiro = 200f; 
    public float time = 120f;

    [Header("Configurações")]
    public List<GameObject> pontosDeEntrega;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI placar; 

    [Header("Feedbacks Visuais")]
    public GameObject PerdeuTempo;
    public GameObject feedbackDinheiroObj;
    public TextMeshProUGUI feedbackDinheiroTxt;

    public GameObject painelVitoria;
    public GameObject painelDerrota;

    [Header("Referências Externas")]
    public GPSNavegador gps;
    public Minigame game;

    public bool modoUrgencia = false;
    public bool atingiuMeta = false;

    private float dinheiroTotal = 0;
    private bool isTimeOver = false;
    private int indiceAtual = -1; 
    private int entregasFeitas = 0;

    void Start()
    {
        entregasFeitas = 0;
        dinheiroTotal = 0;

        AtualizarUI();


        foreach (GameObject ponto in pontosDeEntrega)
        {
            ponto.SetActive(false);
        }

        if (PerdeuTempo != null) PerdeuTempo.SetActive(false);
        if (feedbackDinheiroObj != null) feedbackDinheiroObj.SetActive(false);


        if (pontosDeEntrega.Count > 0)
        {
            GerarProximoDestino();
        }
    }

    private void Update()
    {
        TimeCount();
    }


    void GerarProximoDestino()
    {
        if (pontosDeEntrega.Count <= 1)
        {
            AtivarPonto(0);
            return;
        }

        int novoIndex = indiceAtual;

        while (novoIndex == indiceAtual)
        {
            novoIndex = Random.Range(0, pontosDeEntrega.Count);
        }

        indiceAtual = novoIndex;
        AtivarPonto(indiceAtual);
    }

    void AtivarPonto(int index)
    {
        if (index < pontosDeEntrega.Count)
        {
            pontosDeEntrega[index].SetActive(true);
            if (gps != null) gps.destino = pontosDeEntrega[index].transform;
            Debug.Log("Novo destino: " + pontosDeEntrega[index].name);
        }
    }


    public void EntregaRealizada()
    {
        if (indiceAtual >= 0 && indiceAtual < pontosDeEntrega.Count)
            pontosDeEntrega[indiceAtual].SetActive(false);

        if (game != null) game.IniciarMinigame();
    }


    public void ConfirmarEntregaReal(float dinheiroGanho)
    {
        dinheiroTotal += dinheiroGanho;

        if (dinheiroTotal >= metaDeDinheiro)
        {
            atingiuMeta = true;
        }
        AtualizarUI();


        StartCoroutine(MostrarFeedbackDinheiro(dinheiroGanho));


        if (atingiuMeta)
        {
            FimDeJogo();
        }
        else
        {
            GerarProximoDestino();
        }
    }

    IEnumerator MostrarFeedbackDinheiro(float valor)
    {
        if (feedbackDinheiroObj != null && feedbackDinheiroTxt != null)
        {
            feedbackDinheiroTxt.text = "+ R$ " + valor.ToString("F0");
            feedbackDinheiroObj.SetActive(true);
            yield return new WaitForSeconds(2f);
            feedbackDinheiroObj.SetActive(false);
        }
    }

    void TimeCount()
    {
        if (!isTimeOver)
        {
            float passoDoTempo = modoUrgencia ? Time.unscaledDeltaTime : Time.deltaTime;
            time -= passoDoTempo;
            AtualizarUI();

            if (time <= 0)
            {
                time = 0;
                painelDerrota.SetActive(true);
                modoUrgencia = false;
                Time.timeScale = 0f;
                isTimeOver = true;
            }
        }
    }

    public void PerderTempo(float segundos)
    {
        time -= segundos;
        StartCoroutine(MostrarFeedbackDeDano());
        AtualizarUI();
    }

    IEnumerator MostrarFeedbackDeDano()
    {
        if (PerdeuTempo != null)
        {
            PerdeuTempo.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            PerdeuTempo.SetActive(false);
        }
    }

    void AtualizarUI()
    {
        if (timer != null) timer.text = time.ToString("F0");

        if (placar != null)
            placar.text = "R$ " + dinheiroTotal.ToString("F0") + ",00";
    }

    void FimDeJogo()
    {

        painelVitoria.SetActive(true);
        Time.timeScale = 0f;
    }
}