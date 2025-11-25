using UnityEngine;

public class CameraGTA_V2 : MonoBehaviour
{
    [Header("Alvo")]
    public Transform moto; 
    public Transform pontoFoco; 

    [Header("Posicionamento")]
    public float distancia = 6.0f;
    public float altura = 3.0f;   
    public float suavidade = 10f; 

    [Header("Colisão Inteligente")]
    public LayerMask oQueEParede; 
    public float distanciaMinima = 1.0f;
    public float margemParede = 0.2f; 

    void LateUpdate()
    {
        if (moto == null) return;

        
        Vector3 foco = (pontoFoco != null) ? pontoFoco.position : moto.position + Vector3.up * 1.5f;
        Vector3 direcaoIdeal = -moto.forward * distancia + Vector3.up * altura;
        Vector3 posicaoDesejada = moto.position + direcaoIdeal;

       
        RaycastHit hit;
        Vector3 direcaoDaVisao = posicaoDesejada - foco;
        float distanciaTotal = direcaoDaVisao.magnitude;

        if (Physics.Raycast(foco, direcaoDaVisao.normalized, out hit, distanciaTotal, oQueEParede))
        {
           
            float novaDistancia = Mathf.Clamp(hit.distance - margemParede, distanciaMinima, distanciaTotal);

            posicaoDesejada = foco + (direcaoDaVisao.normalized * novaDistancia);
        }

        
        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, Time.deltaTime * suavidade);

       
        transform.LookAt(foco);
    }
    void OnDrawGizmos() //meio que debug
    {
        if (moto != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 foco = moto.position + Vector3.up * 1.5f;
            Gizmos.DrawLine(foco, transform.position);
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}