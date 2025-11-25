using UnityEngine;
using UnityEngine.AI;

public class GPSNavegador : MonoBehaviour
{
    [Header("Referências")]
    public Transform moto;
    public Transform destino;
    public LineRenderer linha;

    [Header("Ajustes Visuais")]
    public float alturaDaLinha = 2.0f; 
    public float raioDeBusca = 50f;    

    private NavMeshPath caminho;

    void Start()
    {
        caminho = new NavMeshPath();
       
        if (linha == null) linha = GetComponent<LineRenderer>();
    }

    void Update()
    {
       
        if (moto != null && destino != null && destino.gameObject.activeInHierarchy)
        {
            CalcularRota();
        }
        else
        {
           
            linha.positionCount = 0;
        }
    }

    void CalcularRota()
    {
        NavMeshHit hitMoto, hitDestino;

       
        bool achouMoto = NavMesh.SamplePosition(moto.position, out hitMoto, raioDeBusca, NavMesh.AllAreas);
        bool achouDestino = NavMesh.SamplePosition(destino.position, out hitDestino, raioDeBusca, NavMesh.AllAreas);

        if (achouMoto && achouDestino)
        {
            
            if (NavMesh.CalculatePath(hitMoto.position, hitDestino.position, NavMesh.AllAreas, caminho))
            {
               
                linha.positionCount = caminho.corners.Length;

                for (int i = 0; i < caminho.corners.Length; i++)
                {
                    Vector3 ponto = caminho.corners[i];
                    ponto.y += alturaDaLinha; 
                    linha.SetPosition(i, ponto);
                }
            }
        }
        else
        {
           
            linha.positionCount = 0;
        }
    }
}