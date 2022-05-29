using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasse welche die Oberfläche und Funktionen
//des Hauptmenüs beinhaltet
public class LoesungMenue : MonoBehaviour
{
    //Scenes
    public GameObject startMenue, loesungMenue, overlay;

    //Warntext
    public Text text;

    //@Lukas Brüggemann
    //Zum Hauptmenü wechseln
    public void zurueck()
    {
        //Menü wechsel
        startMenue.SetActive(true);
        loesungMenue.SetActive(false);
        overlay.SetActive(false);

        //Alle werte zurück setzen, Alle Damen löschen
        SpielflaechenManager.Instance.clear();
        SpielflaechenManager.Instance.StopAllCoroutines();
        SpielflaechenEffekte.Instance.HideEffekte();

        SpielflaechenManager.Instance.ergebnisNummer = 0;
        SpielflaechenManager.Instance.anzahlLoesungen = 0;
    }

    //@Lukas Brüggemann
    //Weitere Lösungs-Matrizen anzeigen
    public void weiter()
    {
        
        //SpielflaechenManager.Instance.clear();
        SpielflaechenManager.Instance.damenloeschen();
        if (SpielflaechenManager.Instance.ergebnisNummer+1 < SpielflaechenManager.Instance.anzahlLoesungen)
        {
            SpielflaechenManager.Instance.ergebnisNummer++;
            //SpielflaechenManager.Instance.damenloeschen();
        }
        else
        {
            SpielflaechenManager.Instance.ergebnisNummer = 0;
        }
        SpielflaechenManager.Instance.StartCoroutine("ErgebnisAnzeigen");

    }

    //@Lukas Brüggemann
    //Kontinuierliche Aktualisierung des Lösungs-Menüs
    void Update()
    {
        //Aktualisierung der Anzahl der Lösungen
        //während des Algorithmusdurchlaufes
        if(SpielflaechenManager.Instance.anzahlLoesungen==0)
        {
            text.text = "No solution found";
        }
        else
        {
            text.text = "Solutions found: " + SpielflaechenManager.Instance.anzahlLoesungen.ToString() + ". Current solution: " + (SpielflaechenManager.Instance.ergebnisNummer + 1).ToString();
        }

    }
}
