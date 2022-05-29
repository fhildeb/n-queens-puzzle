using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasse welche die Oberfläche und Funktionen
//des Hauptmenüs beinhaltet
public class Iterativ_RekursivMenue : MonoBehaviour
{
    //Scenes
    public GameObject startMenue, iterativ_rekursivMenue, overlay, StruktoIterativ;

    //Geschwindigkeitsregulation
    public Slider mainSlider;

    //Textanzeigen
    public Text countertext, pausetext;

    //Indikator ob Struktogramm aufgerufen wurde
    public bool flagstrukto = false;

    //Indikator ob Pause schon gedrückt wurde
    private bool flag=false;

    //@Lukas Brüggemann
    //@Felix Hildebrandt
    //Zum Hauptmenü wechseln
    public void zurueck()
    {        
        //Menü wechsel
        startMenue.SetActive(true);
        iterativ_rekursivMenue.SetActive(false);
        overlay.SetActive(false);

        //Alle werte zurück setzen, Alle Damen löschen
        SpielflaechenManager.Instance.clear();
        SpielflaechenManager.Instance.StopAllCoroutines();
        SpielflaechenEffekte.Instance.HideEffekte();


        if (flagstrukto)
        {

            kameraminus();
            GameObject go = GameObject.Find("StruktoIterativ");
            go.transform.localScale = new Vector3(0, 0, 0);
            GameObject zeiger = GameObject.Find("ZeigerIterativ");
            zeiger.transform.localScale = new Vector3(0, 0, 0);

            GameObject go2 = GameObject.Find("StruktoRekursiv");
            go2.transform.localScale = new Vector3(0, 0, 0);
            GameObject zeiger2 = GameObject.Find("ZeigerRekursiv");
            zeiger2.transform.localScale = new Vector3(0, 0, 0);

            SpielflaechenManager.Instance.isRekursiv = false;
            SpielflaechenManager.Instance.isIterativ = false;

            flagstrukto = false;

        }

    }

    //@Lukas Brüggemann
    //Geschwindigkeit ändern
    public void geschwindigkeit()
    {
        SpielflaechenManager.Instance.time = mainSlider.value;
    }

    //@Felix Hildebrandt
    //raus zoomen
    public void kameraplus()
    {
        GameObject kamera = GameObject.Find("Kamera");
        if (SpielflaechenManager.groesse == 1)
        {
            kamera.transform.position += new Vector3(2.95f, 0f, 0f);
            Camera.main.fieldOfView += 8.0f;
        }
        if (SpielflaechenManager.groesse == 2)
        {
            kamera.transform.position += new Vector3(2.45f, 0f, 0f);
            Camera.main.fieldOfView += 8.0f;
        }
        if (SpielflaechenManager.groesse == 3)
        {
            kamera.transform.position += new Vector3(2.17f, 0f, 0f);
            Camera.main.fieldOfView += 11.0f;
        }
        if (SpielflaechenManager.groesse == 4)
        {
            kamera.transform.position += new Vector3(1.7f, 0f, 0f);
            Camera.main.fieldOfView += 11.0f;
        }
        if (SpielflaechenManager.groesse == 5)
        {
            kamera.transform.position += new Vector3(1.17f, 0f, 0f);
            Camera.main.fieldOfView += 11.0f;
        }
        if (SpielflaechenManager.groesse == 6)
        {
            kamera.transform.position += new Vector3(0.7f, 0f, 0f);
            Camera.main.fieldOfView += 11.0f;
        }
        if (SpielflaechenManager.groesse == 7)
        {
            kamera.transform.position += new Vector3(0.2f, 0f, 0f);
            Camera.main.fieldOfView += 11.0f;
        }
        if (SpielflaechenManager.groesse == 8)
        {
            kamera.transform.position += new Vector3(-0.1f,0f, 0f);
            Camera.main.fieldOfView += 13.0f;
        }
        if (SpielflaechenManager.groesse == 9)
        {
            kamera.transform.position += new Vector3(-0.6f, 0f, 0f);
            Camera.main.fieldOfView += 13.0f;
        }
        if (SpielflaechenManager.groesse == 10)
        {
            kamera.transform.position += new Vector3(-0.9f, 0f, 0f);
            Camera.main.fieldOfView += 15.0f;
        }
        if (SpielflaechenManager.groesse == 11)
        {
            kamera.transform.position += new Vector3(-1.4f, 0f, 0f);
            Camera.main.fieldOfView += 15.0f;
        }
        if (SpielflaechenManager.groesse == 12)
        {
            kamera.transform.position += new Vector3(-1.9f, 0f, 0f);
            Camera.main.fieldOfView += 15.0f;
        }
    }

    //@Felix Hildebrandt
    //rein zoomen
    public void kameraminus()
    {
        GameObject kamera = GameObject.Find("Kamera");
        if (SpielflaechenManager.groesse == 1)
        {
            kamera.transform.position -= new Vector3(2.95f, 0f, 0f);
            Camera.main.fieldOfView -= 8.0f;
        }
        if (SpielflaechenManager.groesse == 2)
        {
            kamera.transform.position -= new Vector3(2.45f, 0f, 0f);
            Camera.main.fieldOfView -= 8.0f;
        }
        if (SpielflaechenManager.groesse == 3)
        {
            kamera.transform.position -= new Vector3(2.17f, 0f, 0f);
            Camera.main.fieldOfView -= 11.0f;
        }
        if (SpielflaechenManager.groesse == 4)
        {
            kamera.transform.position -= new Vector3(1.7f, 0f, 0f);
            Camera.main.fieldOfView -= 11.0f;
        }
        if (SpielflaechenManager.groesse == 5)
        {
            kamera.transform.position -= new Vector3(1.17f, 0f, 0f);
            Camera.main.fieldOfView -= 11.0f;
        }
        if (SpielflaechenManager.groesse == 6)
        {
            kamera.transform.position -= new Vector3(0.7f, 0f, 0f);
            Camera.main.fieldOfView -= 11.0f;
        }
        if (SpielflaechenManager.groesse == 7)
        {
            kamera.transform.position -= new Vector3(0.2f, 0f, 0f);
            Camera.main.fieldOfView -= 11.0f;
        }
        if (SpielflaechenManager.groesse == 8)
        {
            kamera.transform.position -= new Vector3(-0.1f, 0f, 0f);
            Camera.main.fieldOfView -= 13.0f;
        }
        if (SpielflaechenManager.groesse == 9)
        {
            kamera.transform.position -= new Vector3(-0.6f, 0f, 0f);
            Camera.main.fieldOfView -= 13.0f;
        }
        if (SpielflaechenManager.groesse == 10)
        {
            kamera.transform.position -= new Vector3(-0.9f, 0f, 0f);
            Camera.main.fieldOfView -= 13.0f;
        }
        if (SpielflaechenManager.groesse == 11)
        {
            kamera.transform.position -= new Vector3(-1.4f, 0f, 0f);
            Camera.main.fieldOfView -= 13.0f;
        }
        if (SpielflaechenManager.groesse == 12)
        {
            kamera.transform.position -= new Vector3(-1.9f, 0f, 0f);
            Camera.main.fieldOfView -= 13.0f;
        }
    }

    //Struktogramm anzeigen
    public void Struktogramm()
    {
        //Prüfen ob iterativ oder rekursiv
        if (SpielflaechenManager.Instance.isIterativ == true)
        {
            //Prüfen ob Button gedrückt
            if (flagstrukto == true)
            {
                kameraminus();
                flagstrukto = false;
                GameObject go = GameObject.Find("StruktoIterativ");
                go.transform.localScale = new Vector3(0, 0, 0);
                GameObject zeiger = GameObject.Find("ZeigerIterativ");
                zeiger.transform.localScale = new Vector3(0, 0, 0);

            }
            else
            {
                kameraplus();
                flagstrukto = true;
                GameObject go = GameObject.Find("StruktoIterativ");
                go.transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
                GameObject zeiger = GameObject.Find("ZeigerIterativ");
                zeiger.transform.localScale = new Vector3(0.01f, 0.01666667f, 0.01666667f);
            }
        }
        else
        {

        }

        if(SpielflaechenManager.Instance.isRekursiv == true)
        {
            //Prüfen ob Button gedrückt
            if (flagstrukto == true)
            {
                kameraminus();
                flagstrukto = false;
                GameObject go = GameObject.Find("StruktoRekursiv");
                go.transform.localScale = new Vector3(0, 0, 0);
                GameObject zeiger = GameObject.Find("ZeigerRekursiv");
                zeiger.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                kameraplus();
                flagstrukto = true;
                GameObject go = GameObject.Find("StruktoRekursiv");
                go.transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
                GameObject zeiger = GameObject.Find("ZeigerRekursiv");
                zeiger.transform.localScale = new Vector3(0.01f, 0.01666667f, 0.01666667f);
            }

        }    
    }

    //@Lukas Brüggemann
    //Algorithmus pausieren
    public void pause()
    {
        if (flag==true)
        {
            flag = false;
            SpielflaechenManager.Instance.isPaused = false;
            pausetext.text="pause";
        }
        else
        {
            flag = true;
            SpielflaechenManager.Instance.isPaused = true;
            pausetext.text = "continue";
        }

    }

    //@Lukas Brüggemann
    //Kontinuierliche Aktualisierung des Menüs
    void Update()
    {
        //Aktualisierung der Anzahl der Lösungen
        //während des Algorithmusdurchlaufes
        countertext.text = "Solutions found: " + SpielflaechenManager.Instance.anzahlLoesungen.ToString();
    }
    //@Lukas Brüggemann
    //Initialisieren, geschwindigkeit auf initialwert setzten
    private void Start()
    {
        geschwindigkeit();
    }
}
