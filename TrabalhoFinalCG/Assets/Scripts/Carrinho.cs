using UnityEngine;
using UnityEngine.AI;

public class CarroIA : MonoBehaviour
{
    [Header("Configurações")]
    public float raioDeBusca = 100f; 
    public float tempoParado = 1f;   

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = tempoParado;


        MudarDestino();
    }

    void Update()
    {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {

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

        Vector3 randomDirection = Random.insideUnitSphere * raioDeBusca;


        randomDirection += transform.position;

        NavMeshHit hit;


        if (NavMesh.SamplePosition(randomDirection, out hit, raioDeBusca, 1))
        {

            agent.SetDestination(hit.position);
        }
    }
}