using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasse welche die Oberfläche und Funktionen
//des Hauptmenüs beinhaltet
public class ManuellMenue : MonoBehaviour
{
    //Scenes
    public GameObject startMenue, manuellMenue, overlay;

    //Geschwindigkeitsregulation
    public Slider mainSlider;

    //Benutzerinteraktion
    public InputField damenInput;

    //Warnmeldungen
    public Text Anzeige;

    //Indikator ob Spawn-Zahl auf dem
    //Spielbrett möglich ist
    private bool anzahlerlaubt = true;

    public static bool iterativBlock = false;

    //@Lukas Brüggemann
    //@Felix Hildebrandt
    //Zum Hauptmenü wechseln
    public void zurueck()
    {
        try
        {
            //Menü wechsel
            startMenue.SetActive(true);
            manuellMenue.SetActive(false);
            overlay.SetActive(false);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

        //Alle Damen löschen
        GameObject[] manuelleDamen;

        manuelleDamen = GameObject.FindGameObjectsWithTag("dame");
        foreach (GameObject manuelleDame in manuelleDamen)
            Destroy(manuelleDame);

        //Alle werte zurück setzen, Alle Damen löschen
        SpielflaechenEffekte.Instance.HideEffekte();
        SpielflaechenManager.Instance.clear();
        SpielflaechenManager.Instance.StopAllCoroutines();

        iterativBlock = false;
    }

    //@Lukas Brüggemann
    //Geschwindigkeit ändern
    public void geschwindigkeit()
    {
        //SpielflaechenManager.Instance.multiplikator = mainSlider.value;
        SpielflaechenManager.Instance.time = mainSlider.value;
        //SpielflaechenManager.Instance.time = 2f;
    }

    //@Lukas Brüggemann
    //Anzahl der zu Spawnen Damen aus Eingabefeld
    //prüfen und im Manager setzen
    public void anzahl()
    {
        if (int.Parse(damenInput.text) <= SpielflaechenManager.groesse)
        {
            anzahlerlaubt = true;

            SpielflaechenManager.Instance.anzahlManuellerDamen = int.Parse(damenInput.text);
        }
        else
        {
            anzahlerlaubt = false;
            SpielflaechenManager.Instance.anzahlManuellerDamen = SpielflaechenManager.groesse;

        }

    }

    //@Lukas Schmitz
    //Damen über ManuellerDurchlauf platzieren
    public void spawndamen()
    {
        if (!iterativBlock)
        {
            //Maximal N Damen auf dem NxN-Feld spawnen lassen
            if (SpielflaechenManager.Instance.anzahlManuellerDamen > SpielflaechenManager.groesse)
                SpielflaechenManager.Instance.anzahlManuellerDamen = SpielflaechenManager.groesse;
            SpielflaechenManager.Instance.StartCoroutine("ManuellerDurchlauf");
            anzahlerlaubt = true;
        }

    }

    //@Lukas Schmitz
    //@Lukas Brüggemann
    //Iterativen Durchlauf über die vorher
    //getetigte Eingabe starten
    public void Iterativ()
    {
        if (!iterativBlock)
        {
            iterativBlock = true;
            SpielflaechenManager.Instance.StartCoroutine("IterativerDurchlauf");
        }
    }

    //@Lukas Brüggemann
    //Kontinuierliche Aktualisierung des Manuell-Menüs
    void Update()
    {
        if (SpielflaechenManager.Instance.anzahlLoesungen == -1 && anzahlerlaubt == true)
        {
            Anzeige.text = "Preset queens cause conflicts!";
        }
        else
        {
            if (anzahlerlaubt == false)
            {
                Anzeige.text = "Number needs to be lower than: " + SpielflaechenManager.groesse;
            }
            else
            {
                Anzeige.text = "Solutions found: " + SpielflaechenManager.Instance.anzahlLoesungen.ToString();
            }
        }
    }
}