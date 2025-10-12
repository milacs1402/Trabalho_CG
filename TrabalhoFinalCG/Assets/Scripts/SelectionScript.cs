using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
   public void Voltar()
    {
        SceneManager.LoadScene("Inicial");
    }
}
