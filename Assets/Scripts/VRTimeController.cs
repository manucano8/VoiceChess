using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VRTimeController : MonoBehaviour
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
    public VRBoard B;
    // Start is called before the first frame update

    void Start()
    {
        sc = FindObjectOfType<SonidoColor>();
        enMarcha1 = true;
        enMarcha2 = true;
    }
    private void Awake()
    {
        restante1 = (min1 * 60) + seg1;
        restante2 = (min2 * 60) + seg2;
    }

    public void normal()
    {
        sc.normal();
        min1 = 60;
        min2 = 60;
    }

    public void semirrapido()
    {
        sc.semirrapido();
        min1 = 30;
        min2 = 30;
    }

    public void relampago()
    {
        sc.relampago();
        min1 = 5;
        min2 = 5;
    }

    public void timeChrono() {
        int tempMin2 = Mathf.FloorToInt(restante2 / 60);
        int tempSeg2 = Mathf.FloorToInt(restante2 % 60);
        int tempMin1 = Mathf.FloorToInt(restante1 / 60);
        int tempSeg1 = Mathf.FloorToInt(restante1 % 60);
        if (B.whiteTurn)
        {
            restante1 -= Time.deltaTime;
            if(restante1 < 1)
            {
                SceneManager.LoadScene("MenuPerdedorBlancas");
            }
            tiempo1.text = string.Format("{00:00}:{01:00}", tempMin1, tempSeg1);
            if(enMarcha1)
            {
                tiempo2.text = string.Format("{00:00}:{01:00}", tempMin2, tempSeg2);
                enMarcha1 = false;
            }
        }
        else
        {
            restante2 -= Time.deltaTime;
            if(restante2 < 1)
            {
                SceneManager.LoadScene("MenuPerdedorNegras");
            }
            tiempo2.text = string.Format("{00:00}:{01:00}", tempMin2, tempSeg2);
            if(enMarcha2)
            {
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