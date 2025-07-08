using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    //Variables btnJugar
    private jugar j;
    private Puntero puntero;

    public Boolean pulsadobtn = false;
    
    public GameObject canvaBase;

    public GameObject tutorialCanva;
    public GameObject CanvaInicio;

    void Start()
    {
        j = GameObject.Find("ControlBangBang").GetComponent<jugar>();
        puntero = GameObject.Find("Puntero").GetComponent<Puntero>();

    }


    public void jugar()
    {
        CanvaInicio.SetActive(false);

        puntero.juegoAcabado = true;
        canvaBase.SetActive(true);
       
        pulsadobtn = true;
        j.ControlTiempo = true;
        j.ControlPlatos = true;
    }

    public void tutorial()
    {
        CanvaInicio.SetActive(false);
        tutorialCanva.SetActive(true);
    }

    public void reanudar()
    {
        pulsadobtn = false;
       //
    }

    public void inicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void salir()
    {
      
        Application.Quit();
        Debug.Log("salio del juego");
    }

}
