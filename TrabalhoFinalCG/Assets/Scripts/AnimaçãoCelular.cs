using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Necessário para mexer na UI

public class AnimacaoCelular : MonoBehaviour
{
    [Header("Configurações")]
    public RectTransform celularRect; // Arraste o objeto do celular aqui
    public GameObject fundinho;

    [Header("Destino")]
    public Vector2 posicaoFinal;      // Vamos descobrir esses números jajá
    public float escalaFinal = 0.3f;  // 30% do tamanho original
    public float duracao = 1.5f;      // Quanto tempo demora a animação

    public void IniciarJogo()
    {
        fundinho.SetActive(true);
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

        // 4. AQUI VOCÊ INICIA O JOGO REALMENTE!
        // Ex: Ativar o script da moto, ligar o timer, etc.
        Debug.Log("Animação terminou! Jogo começando...");

        // Se precisar chamar outro script:
        // FindFirstObjectByType<GerenciadorEntregas>().ComecarTimer();
    }
}