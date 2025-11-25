using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaPersistente : MonoBehaviour
{
    public static MusicaPersistente instancia;

    public string nomeCenaDoJogo = "Jogo";

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

        Scene cenaAtual = SceneManager.GetActiveScene();


        if (cenaAtual.name == nomeCenaDoJogo)
        {
            Destroy(gameObject);
        }
    }
}