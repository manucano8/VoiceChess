using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;



public class MenuPausa : MonoBehaviour
{
    //Botón de pausa
    [SerializeField] private GameObject botonPausa;
    //Menú de pausa
    [SerializeField] private GameObject menuPausa;
    //Escena en la que se encuentra
    public string escenaActual;
    private bool pausarTecla = false;
    TimeController tc;
    //public MenuPrincipal mp;

    private void Start()
    {
        //Inicialización de los atributos
        //mp = FindObjectOfType<MenuPrincipal>();
        tc = FindObjectOfType<TimeController>();
    }

    private void Update()
    {
        //Si se le da a la tecla "escape"
        if(Input.GetKeyDown(KeyCode.Escape))
            //Si esta pausado el juego y se le da a la tecla, se reanudará el juego
            if(pausarTecla)
                reanudar();
            else
                //Sino, mostrará el menú de pausa
                pausa();
    }

    //El juego se pausará mostrando un menú de pausa (el tiempo se pausará también)
    public void pausa() 
    {
        if(VRClass.VROn)
            XRGeneralSettings.Instance.Manager.StopSubsystems();
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        pausarTecla = true;
    }

    //El juego se reanudará (el tiempo también)
    public void reanudar()
    {
        if(VRClass.VROn)
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        pausarTecla = false;
    }

    //La partida que se esté jugando se terminará y se volverá a la pantalla de inicio
    public void resetear()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    //Se cerrará el juego
    public void salir()
    {
        Application.Quit();
    }
}
