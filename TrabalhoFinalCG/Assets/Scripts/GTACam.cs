using UnityEngine;

public class CameraGTA_V2 : MonoBehaviour
{
    [Header("Alvo")]
    public Transform moto; // Arraste a Moto
    public Transform pontoFoco; // (Opcional) Arraste a "Cabeça" do boneco ou deixe vazio

    [Header("Posicionamento")]
    public float distancia = 6.0f; // Distância padrão atrás da moto
    public float altura = 3.0f;    // Altura padrão
    public float suavidade = 10f;  // Rapidez da câmera

    [Header("Colisão Inteligente")]
    public LayerMask oQueEParede; // AQUI É O SEGREDO
    public float distanciaMinima = 1.0f; // Para não entrar dentro da cabeça do player
    public float margemParede = 0.2f;    // Para não "lamber" a parede

    void LateUpdate()
    {
        if (moto == null) return;

        // 1. Define onde queremos olhar (foco)
        // Se não tiver pontoFoco, olha para a moto + um pouquinho pra cima
        Vector3 foco = (pontoFoco != null) ? pontoFoco.position : moto.position + Vector3.up * 1.5f;

        // 2. Calcula a posição ideal (Sem Paredes)
        // Pega a rotação da moto, move para trás (distancia) e para cima (altura)
        Vector3 direcaoIdeal = -moto.forward * distancia + Vector3.up * altura;
        Vector3 posicaoDesejada = moto.position + direcaoIdeal;

        // 3. Verifica se tem parede no caminho (Da moto ATÉ a câmera)
        RaycastHit hit;
        Vector3 direcaoDaVisao = posicaoDesejada - foco;
        float distanciaTotal = direcaoDaVisao.magnitude;

        // Lança o raio. IMPORTANTE: Use a LayerMask aqui!
        if (Physics.Raycast(foco, direcaoDaVisao.normalized, out hit, distanciaTotal, oQueEParede))
        {
            // BATEU! Tem um prédio no caminho.
            // Posiciona a câmera onde bateu, menos um pouquinho (margem)
            float novaDistancia = Mathf.Clamp(hit.distance - margemParede, distanciaMinima, distanciaTotal);

            // Recalcula a posição baseada nessa nova distância
            posicaoDesejada = foco + (direcaoDaVisao.normalized * novaDistancia);
        }

        // 4. Move a câmera suavemente
        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, Time.deltaTime * suavidade);

        // 5. Olha para o foco
        transform.LookAt(foco);
    }

    // Desenha linhas no editor para ajudar a ver o erro
    void OnDrawGizmos()
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