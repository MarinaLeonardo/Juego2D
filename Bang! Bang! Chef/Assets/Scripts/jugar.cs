using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class jugar : MonoBehaviour
{
    //---------------------------------VARIABLES--------------------------------------------------
    //Gameobject unidos a cada tipo de plato:
    public GameObject platoVerde;
    public GameObject platoAmarillo;
    public GameObject platoRojo;

    //GameObject que guarda las pantallas de conseguido o no conseguido.
    public GameObject conseguido;
    public GameObject noConseguido;
    

    //Booleans para el control de los platos en pantalla y el tiempo
    public Boolean ControlTiempo = false;
    public Boolean ControlPlatos = false;

    //Sonidos durante el juego
    public AudioClip ganar;
    public AudioClip perder;


    //Variables que guardan el numero minimo de platos que se deben romper
    private int platoVerdeNumero=20;
    private int platoAmarilloNumero = 15;
    private int platoRojoNumero = 5;

    //Varibales que guardan el numero de platos rotos por el usuario
    public int platoVerdeRotosPorUser = 0;
    public int platoAmarilloRotosPorUser = 0;
    public int platoRojoRotosPorUser = 0;

    //Contador de platos en pantalla
    private int contPlatos = 0;
    //int que guarda el numero aleatorio para las probabilidades de cada plato
    private int probabilidad;
    //Numero de platos max en pantalla segun el tiempo
    private int contPlatosSegunTiempo=2;

    //temporizador y btn jugar (SCRIPTS)
    private Timer t;
    private buttons btnJ;
    private Puntero puntero;



    //Campos de texto contadores
    public TMP_Text verdeCont;
    public TMP_Text amarilloCont;
    public TMP_Text rojoCont;

    public TMP_Text verdeCont1;
    public TMP_Text amarilloCont1;
    public TMP_Text rojoCont1;

    public TMP_Text verdeCont2;
    public TMP_Text amarilloCont2;
    public TMP_Text rojoCont2;

    public GameObject CanvaConseguido;
    public GameObject CanvaNoConseguido;



    public GameObject pausa;

    private bool primeraPasada = false;
    //--------------------------------- FIN VARIABLES--------------------------------------------------



    //---------------------------------METODO START--------------------------------------------------
    void Start()
    {
        //Cargamos el obejto timer y el objeto btn
        t = GameObject.Find("ControlBangBang").GetComponent<Timer>();
        btnJ = GameObject.Find("ControlBangBang").GetComponent<buttons>();
        puntero = GameObject.Find("Puntero").GetComponent<Puntero>();

    }
    //---------------------------------METODO START FIN--------------------------------------------------





    //---------------------------------METODO UPDATE--------------------------------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
            //CONTROL DE LOS PLATOS EN PANTALLA SEGUN EL TIEMPO QUE QUEDE
            //Entre 40 y 20 se generan 3
            if (t.timeRemaining <= 40 && t.timeRemaining >= 20)
        {
            contPlatosSegunTiempo = 3;
        }else if (t.timeRemaining <= 20)//menos de 20 se generan 4
        {
            contPlatosSegunTiempo = 4;
        }else if (t.timeRemaining == 0)//Mas de 40 se generan 2
        {
            contPlatosSegunTiempo = 0;
        }


       //Comprobamos el numero de platos que ahi en pantalla sea menor a los que debe y que se haya pulsado el btnjugar 
        if (contadorPlatosEnPantalla(0) < contPlatosSegunTiempo && ControlPlatos)
        {
            //Numero random para la probabilidad de cada plato
            probabilidad = UnityEngine.Random.Range(0, 100);

            //Debug.Log("probabilidad" + probabilidad);



            //Vector que determina la posicion en la que se genera
            Vector3 v3 = new Vector3(UnityEngine.Random.Range(-7, 8), -7.0f, 35f);
            //Vector que dicta la direccion en la que sale.
            Vector2 v2 = new Vector2(UnityEngine.Random.Range(-3, 3), 2);

            //50% prob de un plato verde
            if (probabilidad >= 1 && probabilidad <= 50)
            {
               
                if (minPlatosGenerados() || platoVerdeNumero > 0)//Comprueba para que se generen los platos verdes minimos
                {
                    //Debug.Log("PLATO verde" + platoVerdeNumero);
                    //Instancia el objeto plato verde y le da una direccion aleatoria
                    GameObject plVerde = Instantiate(platoVerde, v3, Quaternion.identity);
                    plVerde.transform.parent = GameObject.Find("PlatosGeneral").transform;
                    plVerde.GetComponent<Rigidbody2D>().velocity = v2 * 1.5f;
                    platoVerdeNumero--;//Variable para comprobar cuantos platos faltan por generar
                    contadorPlatosEnPantalla(1); //Suma a la variable que controla cuantos platos hay en pantalla
                }
                
            }
            //35% de probabilidad
            else if (probabilidad > 50 && probabilidad <= 85)
            {
               
                if (minPlatosGenerados()  || platoAmarilloNumero > 0)//Comprueba para que se generen los platos amarillos minimos
                {
                    //Debug.Log("PLATO AMARILLO" + platoAmarilloNumero);
                    //Instancia el objeto plato amarillo y le da una direccion aleatoria
                    GameObject plAmrillo = Instantiate(platoAmarillo, v3, Quaternion.identity);
                    plAmrillo.transform.parent = GameObject.Find("PlatosGeneral").transform;
                    plAmrillo.GetComponent<Rigidbody2D>().velocity = v2 * 2f;
                    platoAmarilloNumero--;//Variable para comprobar cuantos platos faltan por generar
                    contadorPlatosEnPantalla(1);//Suma a la variable que controla cuantos platos hay en pantalla

                }
                   
            }
            //15% de probabilidad
            else if (probabilidad > 85 && probabilidad <= 100)
            {
                if (minPlatosGenerados()  || platoRojoNumero > 0)//Comprueba para que se generen los platos rojos minimos
                {
                    //Debug.Log("PLATO ROJO" + platoRojoNumero);
                    //Instancia el objeto plato amarillo y le da una direccion aleatoria
                    GameObject plRojo = Instantiate(platoRojo, v3, Quaternion.identity);
                    plRojo.transform.parent = GameObject.Find("PlatosGeneral").transform;
                    plRojo.GetComponent<Rigidbody2D>().velocity = v2 * 3f;
                    platoRojoNumero--;//Variable para comprobar cuantos platos faltan por generar
                    contadorPlatosEnPantalla(1);//Suma a la variable que controla cuantos platos hay en pantalla
                }
                    
            }
        } 
        else if (!ControlTiempo && btnJ.pulsadobtn  && primeraPasada == false)//En caso de que el btn jugar haya sido pulsado y el tiempo se haya acabado
        {

            if (platoVerdeRotosPorUser >= 20 && platoAmarilloRotosPorUser >= 15 && platoRojoRotosPorUser >= 5)//Comprueba los platos rotos por el usuario y da el mensaje de victoria
            {
                conseguido.SetActive(true);

                puntero.juegoAcabado = false;

                verdeCont1.text = string.Format("{00}\n--\n20", platoVerdeRotosPorUser);
                amarilloCont1.text = string.Format("{00}\n--\n15", platoAmarilloRotosPorUser);
                rojoCont1.text = string.Format("{00}\n--\n5", platoRojoRotosPorUser);

                CanvaConseguido.SetActive(true);
              
                GameObject.Find("PlatosGeneral").SetActive(false);
                ControlPlatos = false;
               
                Camera.main.GetComponent<AudioSource>().PlayOneShot(ganar);
                

            }
            else//da el mensaje de fallo
            {
                noConseguido.SetActive(true);
                puntero.juegoAcabado = false;

                verdeCont2.text = string.Format("{00}\n--\n20", platoVerdeRotosPorUser);
                amarilloCont2.text = string.Format("{00}\n--\n15", platoAmarilloRotosPorUser);
                rojoCont2.text = string.Format("{00}\n--\n5", platoRojoRotosPorUser);

                CanvaNoConseguido.SetActive(true);
               
                Debug.Log("MISION FALLIDA");
                ControlPlatos = false;
               
                Camera.main.GetComponent<AudioSource>().PlayOneShot(perder);
                
            }
            primeraPasada = true;
        }


        //CONTADOR DE PLATOS EN PANTALLA
        verdeCont.text = string.Format("{00}\n--\n20", platoVerdeRotosPorUser);
        amarilloCont.text = string.Format("{00}\n--\n15", platoAmarilloRotosPorUser);
        rojoCont.text = string.Format("{00}\n--\n5", platoRojoRotosPorUser);

    }
    //---------------------------------METODO UPDATE FIN--------------------------------------------------




    //---------------------------------METODO minPlatosGenerados --------------------------------------------------
    /*
     * Hace una comprobaci�n de que los platos minimos han llegado a ser generados
     * */
    private Boolean minPlatosGenerados()
    {
        if (platoVerdeNumero <= 0 && platoAmarilloNumero <= 0 && platoRojoNumero <= 0)
        {

            Debug.Log("PLATOS MINIMOS GENERADOS");
            return true;
            
        }
        else
        {
            return false;
        }

    }
    //---------------------------------METODO minPlatosGenerados FIN--------------------------------------------------




    //---------------------------------METODO contadorPlatosEnPantalla --------------------------------------------------
    /*
    * Hace una comprobaci�n de cuantos platos hay en pantalla
    * */
    public int contadorPlatosEnPantalla(int opera)
    {        
        if(opera == 1)
        {
            contPlatos++;
        }else if(opera == 2)
        {
            contPlatos--;
        }
        return contPlatos;
    }
    //---------------------------------METODO contadorPlatosEnPantalla FIN--------------------------------------------------

}
