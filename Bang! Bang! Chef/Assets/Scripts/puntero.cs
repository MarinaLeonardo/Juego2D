using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puntero : MonoBehaviour
{
    public Texture2D crosshair;
    public AudioClip Sound;
    public GameObject sprite;
    public bool encima;
    public bool juegoAcabado;

    void Start()
    {
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);    

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
            
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            if (!encima && juegoAcabado)
            {
                Destroy(Instantiate(sprite, new Vector3(worldPosition.x, worldPosition.y, 35), Quaternion.Euler(new Vector3(0,0,Random.Range(0, 360)))), 0.5f);
            }

        }
    }

}
