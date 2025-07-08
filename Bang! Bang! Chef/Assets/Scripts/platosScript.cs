using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatosScript : MonoBehaviour
{

    //VARIABLES
    private jugar j;
    private Puntero puntero;
    private Animator Animator;
    public bool auxClick = false;

    

    public AudioClip Sound;
    


    //---------------------------------METODO START--------------------------------------------------
    void Start()
    {
        Animator = GetComponent<Animator>();
        j = GameObject.Find("ControlBangBang").GetComponent<jugar>();
        puntero = GameObject.Find("Puntero").GetComponent<Puntero>();

    }
    //---------------------------------METODO START FIN--------------------------------------------------


    private void OnMouseOver()
    {
        puntero.encima = true;
    }

    private void OnMouseExit()
    {
        puntero.encima = false;
    }


    //---------------------------------METODO OnMouseDown FIN--------------------------------------------------
    //Se activa cuando hay un click en el collider
    void OnMouseDown()//Hace que se inicie la animación
    {
        Animator.SetBool("golpe", true);  
    }
    //---------------------------------METODO OnMouseDown FIN--------------------------------------------------



    //---------------------------------METODO OnDeathAnimationFinished FIN--------------------------------------------------
    public void OnDeathAnimationFinished(int platos)//Elimina los platos cuando acaban su animacion y cuenta cuantos de cada se destruyen
    {
        Destroy(gameObject);

        switch (platos)
        {
            case 1://PLATOS VERDES
                j.platoVerdeRotosPorUser++;
                break;
            case 2://PLATOS AMARILLOS
                j.platoAmarilloRotosPorUser++;
                break;
            case 3://PLATOS ROJOS
                j.platoRojoRotosPorUser++;
                break;
        }      
        j.contadorPlatosEnPantalla(2);//Resta un plato en pantalla
        
    }
    //---------------------------------METODO OnDeathAnimationFinished FIN--------------------------------------------------




    //---------------------------------METODO hit FIN--------------------------------------------------
    public void hit()//PONE EL SONIDO DEL PLATO ROMPIENDOSE
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }
    //---------------------------------METODO SThitART FIN--------------------------------------------------


}
