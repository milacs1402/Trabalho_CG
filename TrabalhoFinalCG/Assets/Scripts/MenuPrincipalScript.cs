using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalScript : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("seleção");
    }

    public void Creditos()
    {
        Debug.Log("carregando créditos...");
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
