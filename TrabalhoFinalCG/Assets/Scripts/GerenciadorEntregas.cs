using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; // usar listas

public class GerenciadorEntregas : MonoBehaviour
{
    [Header("Configurações")]
    public List<GameObject> pontosDeEntrega;
    public TextMeshProUGUI timer;
    public float time;
    public bool isTimeOver = false;
    public GameObject painelVitoria;
    public GameObject painelDerrota;


    private int indiceAtual = 0; 
    private int entregasFeitas = 0;
    private int totalParaVencer;

    void Start()
    {
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
        pontosDeEntrega[index].SetActive(true);
    }

    public void EntregaRealizada()
    {
        pontosDeEntrega[indiceAtual].SetActive(false);

        entregasFeitas++;
        AtualizarUI();

        if (entregasFeitas >= totalParaVencer)
        {
            FimDeJogo();
        }
        else
        {
            indiceAtual = Random.Range(0, pontosDeEntrega.Count);

            AtivarPonto(indiceAtual);
        }
    }

    void TimeCount()
    {
        isTimeOver = false;

        if(!isTimeOver && time > 0)
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

    void AtualizarUI()
    {
        timer.text = time.ToString("F0");
    }

    void FimDeJogo()
    {
        Debug.Log("Todas as entregas feitas!");
        if (painelVitoria != null)
            painelVitoria.SetActive(true);
        Time.timeScale = 0f;
    }
}