using UnityEngine;

public class CameraSeguidora : MonoBehaviour
{

    public Transform alvo;

    // O quão "atrasada" e "para cima" a câmera fica
    public Vector3 offset = new Vector3(0f, 1.23f, -3.13f);

    // O quão suave é a perseguição (valores menores = mais suave)
    public float suavidade = 10f;

    // LateUpdate() roda DEPOIS de todos os cálculos de física (FixedUpdate)
    void LateUpdate()
    {
        if (alvo == null) return; // Se não houver alvo, não faz nada

        // 1. Posição Desejada: Posição do alvo + rotação do alvo * offset
        // Isso faz a câmera ficar sempre "atrás" da motinho, não importa para onde ela vire.
        Vector3 posicaoDesejada = alvo.position + (alvo.rotation * offset);

        // 2. Interpola suavemente da posição atual para a desejada
        Vector3 posicaoSuavizada = Vector3.Lerp(transform.position, posicaoDesejada, suavidade * Time.deltaTime);

        // 3. Aplica a nova posição
        transform.position = posicaoSuavizada;

        // 4. Faz a câmera sempre olhar para a motinho
        transform.LookAt(alvo.position);
    }
}