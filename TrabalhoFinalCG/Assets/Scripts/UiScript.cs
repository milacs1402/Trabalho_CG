using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UiScript : MonoBehaviour
{
    public GameObject telaPause;
    private bool isPaused = false;
    private MyInputActions controles; 


    private void Start()
    {

    }

    private void Awake()
    {
            controles = new MyInputActions();

            controles.Player.Pause.performed += ctx => AlternarPause();
    }

    void OnEnable() => controles.Enable();
    void OnDisable() => controles.Disable();
    
    public void AlternarPause()
    {
        if (isPaused)
        {
            Continuar();
        }
        else
        {
            Pausar();
        }

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

    public void Recomecar()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuInicial");
    }
}

