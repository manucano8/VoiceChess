using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;

public class VRMoveVoice : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    public VRBoard B;

    private int i = 0;

    private bool isSelected = false; 
    private Piece currentlySelected = null;

    private string[] keywords = new string[] { 
        "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", 
        "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", 
        "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", 
        "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", 
        "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8",
        "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8",
        "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8",
        "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8"
    };

    //private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start()
    {
        //actions.Add("De A Siete a C ocho", A7C8);
        //actions.Add("De C cinco a C seis", C5C6);
        keywordRecognizer = new KeywordRecognizer(keywords);
        //keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        string letra = speech.text.Substring(0,1);
        int columnaCelda = -1000;
        switch (letra) {
            case "A":
                columnaCelda = 0;
                break;
            case "B":
                columnaCelda = 1;
                break;
            case "C":
                columnaCelda = 2;
                break;
            case "D":
                columnaCelda = 3;
                break;
            case "E":
                columnaCelda = 4;
                break;
            case "F":
                columnaCelda = 5;
                break;
            case "G":
                columnaCelda = 6;
                break;
            case "H":
                columnaCelda = 7;
                break;
        }
        string numero = speech.text.Substring(1);
        int filaCelda = Int32.Parse(numero) - 1;
        Debug.Log("La letra es " + letra);
        Debug.Log("La columna de la celda es " + columnaCelda);
        Debug.Log("El número es " + numero);
        Debug.Log("La fila de la celda es " + filaCelda);
        //turnoJuego(columnaCelda,filaCelda);
        if (currentlySelected != null){
            B.moveChosen(currentlySelected, columnaCelda, filaCelda);
            currentlySelected = null;
        }
        else{
            if (B.choosePiece(columnaCelda, filaCelda) != null){
                currentlySelected = B.choosePiece(columnaCelda, filaCelda);
            }
            else {
                currentlySelected = null;
            }
        } 
        
        //actions[speech.text].Invoke();
    }

    private void turnoJuego(int x, int y) {
        transform.Translate(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

