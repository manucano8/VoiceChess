using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimeController : MonoBehaviour
{
    private static int min1 = 60, seg1 = 0;
    private static int min2 = 60, seg2 = 0;
    [SerializeField] Text tiempo1;
    [SerializeField] Text tiempo2;
    private float restante1;
    private float restante2;
    private bool enMarcha1;
    private bool enMarcha2;
    SonidoColor sc;
    public Board B;
    // Start is called before the first frame update

    void Start()
    {
        sc = FindObjectOfType<SonidoColor>();
        enMarcha1 = true;
        enMarcha2 = true;
    }

    //Tiempo restante
    private void Awake()
    {
        restante1 = (min1 * 60) + seg1;
        restante2 = (min2 * 60) + seg2;
    }

    //Si se elige la opción de juego normal
    public void normal()
    {
        sc.normal();
        min1 = 60;
        min2 = 60;
    }

    //Si se elige la opción de juego semirrápido
    public void semirrapido()
    {
        sc.semirrapido();
        min1 = 30;
        min2 = 30;
    }

    //Si se elige la opción de juego relámpago
    public void relampago()
    {
        sc.relampago();
        min1 = 5;
        min2 = 5;
    }

    public void timeChrono() {
        //Tiempos
        int tempMin2 = Mathf.FloorToInt(restante2 / 60);
        int tempSeg2 = Mathf.FloorToInt(restante2 % 60);
        int tempMin1 = Mathf.FloorToInt(restante1 / 60);
        int tempSeg1 = Mathf.FloorToInt(restante1 % 60);

        if (B.whiteTurn)
        {
            restante1 -= Time.deltaTime;
            if(restante1 < 1)
            {
                //Si llega a 0, vuelve a la pantalla de derrota las blancas
                SceneManager.LoadScene("MenuPerdedorBlancas");
            }
            //Muestra el texto actualizado
            tiempo1.text = string.Format("{00:00}:{01:00}", tempMin1, tempSeg1);
            //Inicializar el valor del tiempo
            if(enMarcha1)
            {
                //Muestra el texto actualizado
                tiempo2.text = string.Format("{00:00}:{01:00}", tempMin2, tempSeg2);
                enMarcha1 = false;
            }
        }
        else
        {
            restante2 -= Time.deltaTime;
            //Si llega a 0, vuelve a la pantalla de derrota de las negras
            if(restante2 < 1)
            {
                SceneManager.LoadScene("MenuPerdedorNegras");
            }
                //Muestra el texto actualizado
            tiempo2.text = string.Format("{00:00}:{01:00}", tempMin2, tempSeg2);
            //Inicializar el valor del tiempo
            if(enMarcha2)
            {
                //Muestra el texto actualizado
                tiempo1.text = string.Format("{00:00}:{01:00}", tempMin1, tempSeg1);
                enMarcha2 = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeChrono();
    }
}