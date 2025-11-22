using UnityEngine;
using UnityEngine.AI;

public class GPSNavegador : MonoBehaviour
{
    [Header("Referências")]
    public Transform moto;
    public Transform destino; // O GerenciadorEntregas muda isso
    public LineRenderer linha;

    [Header("Ajustes Visuais")]
    public float alturaDaLinha = 2.0f; // Altura para não entrar no chão/prédios
    public float raioDeBusca = 50f;    // Margem para encontrar o NavMesh

    private NavMeshPath caminho;

    void Start()
    {
        caminho = new NavMeshPath();
        // Pega o LineRenderer automático se você esqueceu de arrastar
        if (linha == null) linha = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Só calcula se tudo estiver configurado e o destino estiver ativo
        if (moto != null && destino != null && destino.gameObject.activeInHierarchy)
        {
            CalcularRota();
        }
        else
        {
            // Se o ponto sumiu ou acabou o jogo, apaga a linha
            linha.positionCount = 0;
        }
    }

    void CalcularRota()
    {
        NavMeshHit hitMoto, hitDestino;

        // 1. Tenta encontrar pontos válidos no NavMesh (Imã)
        bool achouMoto = NavMesh.SamplePosition(moto.position, out hitMoto, raioDeBusca, NavMesh.AllAreas);
        bool achouDestino = NavMesh.SamplePosition(destino.position, out hitDestino, raioDeBusca, NavMesh.AllAreas);

        if (achouMoto && achouDestino)
        {
            // 2. Calcula o caminho
            if (NavMesh.CalculatePath(hitMoto.position, hitDestino.position, NavMesh.AllAreas, caminho))
            {
                // 3. Atualiza a linha visual
                linha.positionCount = caminho.corners.Length;

                for (int i = 0; i < caminho.corners.Length; i++)
                {
                    Vector3 ponto = caminho.corners[i];
                    ponto.y += alturaDaLinha; // Aplica o offset de altura
                    linha.SetPosition(i, ponto);
                }
            }
        }
        else
        {
            // Se não achou caminho, limpa a linha para não travar no frame anterior
            linha.positionCount = 0;
        }
    }
}