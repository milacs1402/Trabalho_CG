using UnityEngine;

public class MarcaDerrapagem : MonoBehaviour
{

    public TrailRenderer MarcasPneu;
    public Motinho drift;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDrift();
    }

    private void CheckDrift()
    {
        if (drift.estaDriftando == true)
        {
            StartEmmiter();
        }
        else
        {
            StopEmmiter();
        }
    }

    private void StartEmmiter()
    {
            MarcasPneu.emitting = true; 
    }
    private void StopEmmiter()
    {
        MarcasPneu.emitting = false;
    }
}
