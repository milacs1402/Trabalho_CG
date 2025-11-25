using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimacaoCelular : MonoBehaviour
{
    [Header("Configurações")]
    public RectTransform celularRect;
    public GameObject fundinho;
    public GameObject termos;
    public GameObject CanvaValores;

    [Header("Destino")]
    public Vector2 posicaoFinalMin;
    public Vector2 posicaoFinalMax;
    public float escalaFinalMin;  
    public float escalaFinalMax;  
    public float duracao;      

    public void IniciarJogo()
    {
        termos.SetActive(false);
        CanvaValores.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(MinimizarCelular());
    }

    public IEnumerator MinimizarCelular()
    {
        
        // Guarda os valores iniciais
        Vector2 posicaoInicial = celularRect.anchoredPosition;
        Vector3 escalaInicial = celularRect.localScale;
        escalaFinalMin = 0.6f;
        duracao = 1.5f;
        Vector3 escalaAlvo = new Vector3(escalaFinalMin, escalaFinalMin, 1f);
        float tempoPassado = 0f;

        Time.timeScale = 1f;
        fundinho.SetActive(false);


        // 2. O Loop da Animação
        while (tempoPassado < duracao)
        {
            tempoPassado += Time.unscaledDeltaTime;
            float porcentagem = tempoPassado / duracao;

            fundinho.SetActive(false);

            // Curva de suavização (opcional, deixa o movimento mais bonito)
            porcentagem = Mathf.SmoothStep(0, 1, porcentagem);

            // Move suavemente (Lerp = Linear Interpolation)
            celularRect.anchoredPosition = Vector2.Lerp(posicaoInicial, posicaoFinalMin, porcentagem);

            // Encolhe suavemente
            celularRect.localScale = Vector3.Lerp(escalaInicial, escalaAlvo, porcentagem);

            yield return null; // Espera o próximo frame
        }

        // 3. Garante que chegou exatamente no final
        celularRect.anchoredPosition = posicaoFinalMin;
        celularRect.localScale = escalaAlvo;


    }

    public IEnumerator MaximizarCelular()
    {
        Vector2 posicaoInicial = celularRect.anchoredPosition;
        Vector3 escalaInicial = celularRect.localScale;
        escalaFinalMax = 1f;
        duracao = 1.5f;
        Vector3 escalaAlvo = new Vector3(escalaFinalMax, escalaFinalMax, 1f);
        float tempoPassado = 0f;

        fundinho.SetActive(true);

        // 2. O Loop da Animação
        while (tempoPassado < duracao)
        {
            tempoPassado += Time.unscaledDeltaTime;
            float porcentagem = tempoPassado / duracao;

            // Curva de suavização (opcional, deixa o movimento mais bonito)
            porcentagem = Mathf.SmoothStep(0, 1, porcentagem);

            // Move suavemente (Lerp = Linear Interpolation)
            celularRect.anchoredPosition = Vector2.Lerp(posicaoInicial, posicaoFinalMax, porcentagem);

            // Encolhe suavemente
            celularRect.localScale = Vector3.Lerp(escalaInicial, escalaAlvo, porcentagem);

            yield return null; // Espera o próximo frame
        }

        // 3. Garante que chegou exatamente no final
        celularRect.anchoredPosition = posicaoFinalMax;
        celularRect.localScale = escalaAlvo;

    }
}