using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    public GameObject painelMenuPrincipal; 
    public GameObject painelComoJogar; 

    void Start()
    {
        painelMenuPrincipal.SetActive(true);
        painelComoJogar.SetActive(false);
    }

    public void MostrarPainelComoJogar()
    {
        painelMenuPrincipal.SetActive(false);
        painelComoJogar.SetActive(true);
    }

    public void IniciarSelecaoDeCena()
    {
        SceneManager.LoadScene("Seleção");
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

    public void ODS()
    {
        Debug.Log("abrindo site da ONU...");
        Application.OpenURL("https://brasil.un.org/pt-br/sdgs");
    }
}
