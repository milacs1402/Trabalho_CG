using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    public TextMeshProUGUI placar;
    private int PlacarGeral;

    public void Start()
    {
        PlacarGeral = PlayerPrefs.GetInt("PlacarGeral");
        placar.text = PlacarGeral.ToString() + "/4";
    }
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
