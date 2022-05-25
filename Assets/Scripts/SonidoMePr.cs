using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoMePr : MonoBehaviour
{

    //Silenciar
    public void Muted()
    {
        AudioListener.volume = 0;
    }

    //Revelar
    public void UnMuted()
    {
        AudioListener.volume = 1;
    }
}
