using UnityEngine;
using UnityEngine.AI;

public class CarroIA : MonoBehaviour
{
    [Header("Configurações")]
    public float raioDeBusca = 100f; // Quão longe ele pode ir
    public float tempoParado = 1f;   // Tempo que espera ao chegar no destino

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = tempoParado;

        // Manda ir para o primeiro lugar assim que nasce
        MudarDestino();
    }

    void Update()
    {
        // Se já chegamos no destino (ou muito perto dele)
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Conta um tempinho parado (tipo num semáforo imaginário)
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                MudarDestino();
                timer = tempoParado;
            }
        }
    }

    void MudarDestino()
    {
        // Escolhe um ponto aleatório dentro de uma esfera imaginária
        Vector3 randomDirection = Random.insideUnitSphere * raioDeBusca;

        // Adiciona à posição atual (para buscar em volta de onde ele está)
        randomDirection += transform.position;

        NavMeshHit hit;

        // Verifica se esse ponto aleatório cai em cima do NavMesh (Chão Azul)
        if (NavMesh.SamplePosition(randomDirection, out hit, raioDeBusca, 1))
        {
            // Se for um lugar válido, manda o carro ir pra lá
            agent.SetDestination(hit.position);
        }
    }
}