using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VRClass {
    public static bool VROn;
}

public class MenuPrincipal : MonoBehaviour
{
    //Menú de opciones
    public GameObject CanvasOpciones;
    //Menú principal
    public GameObject CanvasMenuPrincipal;
    public AudioSource fuente;
    public AudioClip clip;
    public Toggle VR;

    // Start is called before the first frame update
    void Start()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        //Inicialización de la música
        fuente.clip = clip;
        //Se desactiva la página de opciones
        CanvasOpciones.GetComponent<Canvas>().enabled = false;
        //Se activa la página de inicio
        CanvasMenuPrincipal.GetComponent<Canvas>().enabled = true;
        //VR desactivado desde un principio
        VR.isOn = false;
    }

    //Aparecerá el menú de opciones y se pondrá invisible el menú principal
    public void menuOpciones()
    {
        //Se reproduce el sonido de click
        fuente.Play();
        //Se activa la página de opciones
        CanvasOpciones.GetComponent<Canvas>().enabled = true;
        //Se desactiva la página de inicio
        CanvasMenuPrincipal.GetComponent<Canvas>().enabled = false;
    }

    public void menuPrincipal()
    {
        //Se reproduce el sonido de click
        fuente.Play();
        //Se desactiva la página de opciones
        CanvasOpciones.GetComponent<Canvas>().enabled = false;
        //Se activa la página de inicio
        CanvasMenuPrincipal.GetComponent<Canvas>().enabled = true;
    }

    public void jugar()
    {
        //Si el VR está desactivado, que cargue la escena "LogicaAjedrez"
        if(VR.isOn == false)
        {
            VRClass.VROn = false;
            fuente.Play();
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            SceneManager.LoadScene("LogicaAjedrez");
        }
        //Si el VR está activado, que cargue la escena "VRAjedrez"
        else
        {
            VRClass.VROn = true;
            fuente.Play();
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            SceneManager.LoadScene("VRAjedrez");
        }
    }
    
    //Cierra el juego
    public void salir()
    {
        fuente.Play();
        //Quita el volumen del juego
        AudioListener.volume = 0;
        Application.Quit();
    }

}
