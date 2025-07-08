using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //VARIABLES
    public float timeRemaining = 60;
    public GameObject AnimatorAux;


    private jugar j;
    public TMP_Text campoTiempo;
    

    //---------------------------------METODO START--------------------------------------------------
    void Start()
    {
        //Cogemos el script de jugar
        j = GameObject.Find("ControlBangBang").GetComponent<jugar>();

    }
    //---------------------------------METODO START FIN--------------------------------------------------



    //---------------------------------METODO UPDATE--------------------------------------------------
    void Update()//Creamos el timer
    {
        if (j.ControlTiempo)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
               // Debug.Log(timeRemaining);
            }
            else
            {
                Debug.Log("TIEMPO AGOTADO");

                j.ControlTiempo = false;
            }

            mostrarTiempo(timeRemaining);
            if (timeRemaining <= 10)
            {
                AnimatorAux.GetComponent<Animator>().SetBool("10", true);
                
            }
        }       
    }
    //---------------------------------METODO UPDATE FIN--------------------------------------------------

     void mostrarTiempo(float tiempoAux)
    {
        if (tiempoAux < 0)
        {
            tiempoAux = 0;
        }
        else if (tiempoAux > 0)
        {
            tiempoAux += 1;
        }

        float minutos = Mathf.FloorToInt(tiempoAux / 60);
        float segundos = Mathf.FloorToInt(tiempoAux % 60);

        campoTiempo.text = string.Format("{00:00}:{01:00}", minutos, segundos);
    }

}
