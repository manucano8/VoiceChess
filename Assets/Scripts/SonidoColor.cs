using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonidoColor : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public AudioSource fuente;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        fuente.clip = clip;
    }

    // Update is called once per frame
    public void normal()
    {
        //Suena el sonido de click
        fuente.Play();
        //Cambia los colores a la hora de seleccionar algún botón
        button1.GetComponent<Image>().color = Color.white;
        button2.GetComponent<Image>().color = Color.red;
        button3.GetComponent<Image>().color = Color.red;
    }

    public void semirrapido()
    {
        //Suena el sonido de click
        fuente.Play();
        //Cambia los colores a la hora de seleccionar algún botón
        button1.GetComponent<Image>().color = Color.red;
        button2.GetComponent<Image>().color = Color.white;
        button3.GetComponent<Image>().color = Color.red;
    }

    public void relampago()
    {
        //Suena el sonido de click
        fuente.Play();
        //Cambia los colores a la hora de seleccionar algún botón
        button1.GetComponent<Image>().color = Color.red;
        button2.GetComponent<Image>().color = Color.red;
        button3.GetComponent<Image>().color = Color.white;
    }
}
