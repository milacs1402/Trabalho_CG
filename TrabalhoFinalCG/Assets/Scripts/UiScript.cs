using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    public GameObject telaPause;
    private bool isPaused = false;

    private void Start()
    {

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

    public void LiTermos()
    {
        Time.timeScale = 1.0f;
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

    public void VoltarMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Inicial");
    }
}

