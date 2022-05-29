using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;

//Klasse welche die Oberfläche und Funktionen
//des Hauptmenüs beinhaltet
public class Hauptmenue : MonoBehaviour
{
    //Benutzereingabe
    public InputField groesenInput;

    //Scenes
    public GameObject loesungMenue, iterativ_rekursivMenue, manuellMenue, startMenue, overlay;

    //Benutzerauswahl
    public Button loesung, iterativ, rekuriv, manuell;

    //Warnmeldung
    public Text error;

    //Ladesymbol
    public GameObject loading;

    //@Lukas Brüggemann
    //Initialisieren
    //focus auf Groesen Input setzen
    public void Start()
    {
        groesenInput.Select();
        groesenInput.ActivateInputField();
    }

    //@Lukas Brüggemann
    //Programm Beenden
    public void Quit()
    {
        Application.Quit();
    }

    //@Lukas Brüggemann
    //Eingabe des Benutzers überprüfen
    public void groesse()
    {
        try
        {
            switch (int.Parse(groesenInput.text))
            {
                case 1:
                    aktiv();
                    break;
                case 2:
                    aktiv();
                    break;
                case 3:
                    aktiv();
                    break;
                case 4:
                    aktiv();
                    break;
                case 5:
                    aktiv();
                    break;
                case 6:
                    aktiv();
                    break;
                case 7:
                    aktiv();
                    break;
                case 8:
                    aktiv();
                    break;
                case 9:
                    aktiv();
                    break;
                case 10:
                    aktiv();
                    break;
                case 11:
                    aktiv();
                    break;
                case 12:
                    aktiv();
                    break;
                default:
                    fehler();
                    break;
            }
        }
        catch (System.Exception ex)
        {
            error.text = "Input is not a number.";
            loesung.gameObject.SetActive(false);
            iterativ.gameObject.SetActive(false);
            rekuriv.gameObject.SetActive(false);
            manuell.gameObject.SetActive(false);
        }
    }
    //@Lukas Brüggemann
    //Eingabe zulässig, Menue anpassen
    public void aktiv()
    {
        try
        {
            SpielflaechenManager.groesse = int.Parse(groesenInput.text);
            error.text = "";
            loesung.gameObject.SetActive(true);
            iterativ.gameObject.SetActive(true);
            rekuriv.gameObject.SetActive(true);
            manuell.gameObject.SetActive(true);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
    }

    //@Lukas Brüggemann
    //Einabe nicht zulässig, Menue anpassen
    public void fehler()
    {
        error.text = "Number has to be between 0 and 13.)";
        loesung.gameObject.SetActive(false);
        iterativ.gameObject.SetActive(false);
        rekuriv.gameObject.SetActive(false);
        manuell.gameObject.SetActive(false);
    }

    //@Lukas Brüggemann
    //Ins Lösungs Menü wechseln
    public void loesungMenu()
    {
        startMenue.SetActive(false);
        loesungMenue.SetActive(true);
        overlay.SetActive(true);

        SpielflaechenManager sm = GameObject.Find("Spielflaeche").GetComponent<SpielflaechenManager>();
        SpielflaechenManager.Instance.init();
        Debug.Log(sm);
        SpielflaechenManager.Instance.Ergebnisberechnen(); 
    }

    //@Lukas Brüggemann
    //Ins Manuelle Menü wechseln
    public void manuellMenu()
    {
        startMenue.SetActive(false);
        manuellMenue.SetActive(true);
        overlay.SetActive(true);

        SpielflaechenManager sm = GameObject.Find("Spielflaeche").GetComponent<SpielflaechenManager>();
        Debug.Log(sm);
        SpielflaechenManager.Instance.init();
    }

    //@Lukas Brüggemann
    //Ins Iterative Menü welchseln
    public void iterativMenu(float time)
    {
        startMenue.SetActive(false);
        iterativ_rekursivMenue.SetActive(true);
        overlay.SetActive(true);

        SpielflaechenManager sm = GameObject.Find("Spielflaeche").GetComponent<SpielflaechenManager>();
        SpielflaechenManager.Instance.init();

        sm.StartCoroutine(SpielflaechenManager.Instance.IterativerDurchlauf());
    }

    //@Lukas Brüggemann
    //Ins rekursive Menü welchseln
    public void rekursivMenu()
    {
        startMenue.SetActive(false);
        iterativ_rekursivMenue.SetActive(true);
        overlay.SetActive(true);

        SpielflaechenManager sm = GameObject.Find("Spielflaeche").GetComponent<SpielflaechenManager>();
        //sm.StartCoroutine("RekursiverDurchlauf", (int)0);
        SpielflaechenManager.Instance.init();
        int[] x = new int[SpielflaechenManager.groesse];
        sm.StartCoroutine(SpielflaechenManager.Instance.RekursiverDurchlauf(x, 0));
    }
}
 