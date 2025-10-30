using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
   public void Voltar()
    {
        SceneManager.LoadScene("Inicial");
    }

    public void Pessoas()
    {
        SceneManager.LoadScene("Pessoas");
    }

    public void Planeta()
    {
        SceneManager.LoadScene("Planeta");
    }

    public void Prosperidade()
    {
        SceneManager.LoadScene("Prosperidade");
    }

    public void Paz()
    {
        SceneManager.LoadScene("Paz e Parceria");
    }


}
