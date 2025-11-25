using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    public GameObject painelMenuPrincipal;

    void Start()
    {
        painelMenuPrincipal.SetActive(true);
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Sair()
    {
        Debug.Log("saindo do jogo...");
        Application.Quit();
    }

}
