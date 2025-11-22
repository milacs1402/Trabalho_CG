using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimacaoCelular : MonoBehaviour
{
    [Header("Configurações")]
    public RectTransform celularRect;
    public GameObject fundinho;
    public GameObject termos;

    [Header("Destino")]
    public Vector2 posicaoFinal;     
    public float escalaFinal = 0.3f;  
    public float duracao = 1.5f;      

    public void IniciarJogo()
    {
        fundinho.SetActive(false);
        termos.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(AnimarParaOCanto());
    }

    IEnumerator AnimarParaOCanto()
    {
        
        // Guarda os valores iniciais
        Vector2 posicaoInicial = celularRect.anchoredPosition;
        Vector3 escalaInicial = celularRect.localScale;
        Vector3 escalaAlvo = new Vector3(escalaFinal, escalaFinal, 1f);

        float tempoPassado = 0f;

        // 2. O Loop da Animação
        while (tempoPassado < duracao)
        {
            tempoPassado += Time.deltaTime;
            float porcentagem = tempoPassado / duracao;

            fundinho.SetActive(false);

            // Curva de suavização (opcional, deixa o movimento mais bonito)
            porcentagem = Mathf.SmoothStep(0, 1, porcentagem);

            // Move suavemente (Lerp = Linear Interpolation)
            celularRect.anchoredPosition = Vector2.Lerp(posicaoInicial, posicaoFinal, porcentagem);

            // Encolhe suavemente
            celularRect.localScale = Vector3.Lerp(escalaInicial, escalaAlvo, porcentagem);

            yield return null; // Espera o próximo frame
        }

        // 3. Garante que chegou exatamente no final
        celularRect.anchoredPosition = posicaoFinal;
        celularRect.localScale = escalaAlvo;

    }
}