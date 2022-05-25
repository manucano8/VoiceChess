using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Crono : MonoBehaviour
{
    private List<InputDevice> inputDevices = new List<InputDevice>();
    private InputDevice device;

    public VRBoard vrboard;

    private bool ontrigger;

    public GameObject camaraVR;

    //Toma el dispositivo de entrada. No funciona con el Mock HMD
    void GetDevice() {
        InputDevices.GetDevices(inputDevices);
        device = inputDevices.FirstOrDefault();
    }
    void Start()
    {
        ontrigger = false;
        //GetDevice();
    }

    //Si una de las manos está tocando el cronómetro y está pulsada el click izquierdo, cambia el turno
    private void Update() {
        
        /*if (!device.isValid) {
            GetDevice();
        }*/

        //ESTO DETECTARÍA SI SE ESTÁ PULSANDO EL TRIGGER DE UN CONTROLADOR DE VR, PERO NO FUNCIONA CON EL DEVICE SIMULATOR
        /*bool triggerValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            vrboard.whiteTurn = !vrboard.whiteTurn;
            camaraVR.transform.position = new Vector3(camaraVR.transform.position.x,camaraVR.transform.position.y,camaraVR.transform.position.z * -1);
            camaraVR.transform.Rotate(0f,180f,0f);
        }*/
        if (ontrigger && Input.GetMouseButtonDown(0)) {
            vrboard.whiteTurn = !vrboard.whiteTurn;
            camaraVR.transform.position = new Vector3(camaraVR.transform.position.x,camaraVR.transform.position.y,camaraVR.transform.position.z * -1);
            camaraVR.transform.Rotate(0f,180f,0f);
        }

    }

    private void OnTriggerEnter(Collider other) {
        ontrigger = true;
    }

    private void OnTriggerExit(Collider other) {
        ontrigger = false;
    }
}
