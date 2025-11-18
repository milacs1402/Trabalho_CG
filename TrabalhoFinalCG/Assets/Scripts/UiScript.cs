using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    public string chaveDesteMinigame;
    public GameObject telaPause;
    public GameObject instruc;
    private bool isPaused = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt(chaveDesteMinigame, 0) == 0)
        {
            MostrarInstrucoes();
        }
        else
        {
            instrucJaVistas();
        }
    }

    void MostrarInstrucoes()
    {
        if (instruc != null)
        {
            instruc.SetActive(true);
            Time.timeScale = 0f; 
        }
    }

    public void FecharInstrucoes()
    {
        PlayerPrefs.SetInt(chaveDesteMinigame, 1);
        PlayerPrefs.Save();

        instrucJaVistas();
    }

    void instrucJaVistas()
    {
        if (instruc != null)
            instruc.SetActive(false);

        Time.timeScale = 1f; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continuar();
            }

            else
            {
                Pausar();
            }
        }

    }
    public void Pausar()
    {
        isPaused = true;
        telaPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continuar()
    {
        isPaused = false;
        telaPause.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Recomeçar()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void MudarMissao()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetInt(chaveDesteMinigame, 0);
        SceneManager.LoadScene("Seleção");
    }
    public void VoltarMenu()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetInt(chaveDesteMinigame, 0);
        SceneManager.LoadScene("Inicial");
    }
}

