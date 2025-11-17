using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    public GameObject telaPause;
    public GameObject instruc;
    private bool isPaused = false;
    private string chavePessoas = "jaViuIntroPessoas";
    private string chavePlaneta = "jaViuIntroPlaneta";
    private string chaveProsperidade = "jaViuIntroProsperidade";
    private string chavePaz = "jaViuIntroPaz";

    private void Start()
    {
        if (PlayerPrefs.GetInt(chavePessoas, 0) == 0)
        {
            instruc.SetActive(true);
            Time.timeScale = 0f;
        }
        if (PlayerPrefs.GetInt(chavePlaneta, 0) == 0)
        {
            instruc.SetActive(true);
            Time.timeScale = 0f;
        }
        if (PlayerPrefs.GetInt(chaveProsperidade, 0) == 0)
        {
            instruc.SetActive(true);
            Time.timeScale = 0f;
        }
        if (PlayerPrefs.GetInt(chavePaz, 0) == 0)
        {
            instruc.SetActive(true);
            Time.timeScale = 0f;
        }

        Continuar();

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
        PlayerPrefs.SetInt(chavePessoas, 0);
        PlayerPrefs.SetInt(chavePlaneta, 0);
        PlayerPrefs.SetInt(chaveProsperidade, 0);
        PlayerPrefs.SetInt(chavePaz, 0);
        SceneManager.LoadScene("Seleção");
    }
    public void VoltarMenu()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetInt(chavePessoas, 0);
        PlayerPrefs.SetInt(chavePlaneta, 0);
        PlayerPrefs.SetInt(chaveProsperidade, 0);
        PlayerPrefs.SetInt(chavePaz, 0);
        SceneManager.LoadScene("Inicial");
    }

    public void JogarMiniGame()
    {
        instruc.SetActive(false);
        PlayerPrefs.SetInt(chavePessoas, 1);
        PlayerPrefs.SetInt(chavePlaneta, 1);
        PlayerPrefs.SetInt(chaveProsperidade, 1);
        PlayerPrefs.SetInt(chavePaz, 1);
        PlayerPrefs.Save();
    }
}

