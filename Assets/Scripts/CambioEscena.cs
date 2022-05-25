using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void salir()
    {
        Application.Quit();
    }

}
