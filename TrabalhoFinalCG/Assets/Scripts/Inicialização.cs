using UnityEngine;

public class Inicializador
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void AoAbrirOJogo()
    {
        Debug.Log("O Jogo acabou de abrir! Rodando configuração inicial...");

        PlayerPrefs.SetInt("PlacarGeral", 0);

        PlayerPrefs.SetInt("PrimeiraVitoria1", 1);
        PlayerPrefs.SetInt("PrimeiraVitoria2", 1);
        PlayerPrefs.SetInt("PrimeiraVitoria3", 1);
        PlayerPrefs.SetInt("PrimeiraVitoria4", 1);
    }
}