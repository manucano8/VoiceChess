using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoGanadorPerdedor : MonoBehaviour
{
    public bool esNegra;
    public Renderer textoNegra;
    public Renderer textoBlanca;
    public bool esGanador;  //false es las blancas y true es las negras
    public AudioSource ganador;
    public AudioSource perdedor;

    void Start()
    {
        //Si es ganador
        if(esGanador)
            //Si es los negros que muestre que ha ganado los negros
            if(esNegra)
            {
                textoNegra.enabled = true;
                textoBlanca.enabled = false;
            }
            //Si es los blancos que muestre que ha ganado las blancas
            else
            {
                textoNegra.enabled = false;
                textoBlanca.enabled = true;
            }
        //Si pierde
        else
            //Si es los blancos que muestre que ha ganado las blancas
            if(esNegra)
            {
                textoNegra.enabled = false;
                textoBlanca.enabled = true;
            }
            //Si es los negros que muestre que ha ganado los negros
            else
            {
                textoNegra.enabled = true;
                textoBlanca.enabled = false;
            }
        //Si gana, que suene el sonido de victoria
        if(esGanador)
        {
            perdedor.volume = 0;
            ganador.volume = 1;
        }
        //Si pierdes, que suene el sonido de derrota
        else
        {
            ganador.volume = 0;
            perdedor.volume = 1;
        }
    }
}
