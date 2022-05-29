using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

//Hauptklasse des Damen-Problems
//Beinhaltet alle Funktionen zur Spielflaeche und der
//Dame-Figur
public class SpielflaechenManager : MonoBehaviour
{
    //Spielflaechenmanager als Instanz des Spiel
    //samt Getter und Setter für globale verwendung
    public static SpielflaechenManager Instance { set; get; }

    //Boolische Matrix auf welchen Feldern
    //die Dame platziert werden darf
    private bool[,] spielzugGestattet { set; get; }

    //GameObjects die auf dem Spielfeld platziert
    //werden können
    public Spielfigur[,] Spielfiguren { set; get; }

    //Spielfigur, welche aktuell mit der Maustaste
    //ausgewählt ist
    private Spielfigur ausgewaehlteSpielfigur;
    
    //Indikator ob Manuelles Menü gerade aktiv ist
    public bool manuellAktiv = false;

    //Indikator ob Iterativer oder Rekursiver Durchlauf für Struktogramme
    public bool isIterativ;
    public bool isRekursiv;

    //Position in der Liste von Lösungs-Matrizen
    public int ergebnisNummer = 0;

    //Anzahl der eindeutigen Lösungen
    public int anzahlLoesungen = 0;

    //Indikator ob Dame an dieser Position abgelegt werden darf
    public bool stellungerlaubt;

    //Damen welche über die Benutzereingabe im manuellen Menü
    //festgelegt wird
    public int anzahlManuellerDamen;

    //Konstanten für das Schachbrett-Muster
    private const float FLAECHEN_GROESSE = 1.0f;
    private const float FLAECHEN_ABSTAND = 0.5f;

    //Ausgewählte X- und Y-Komponenten auf dem Feld
    //Initialisierung mit -1 := kein Feld aktiv
    private int auswahlX = -1;
    private int auswahlY = -1;

    //Größe des N-Dame-Problems
    public static int groesse=8;

    //Liste welche alle Damen, aktiveDamen, Flaechen
    //beinhaltet
    public List<GameObject> dame;
    public List<GameObject> aktiveDame;
    private List<GameObject> fleachen;
    private List<int[][]> positionen;
    //Schnelligkeit des Durchlaufes
    public float time;

    //Indikator ob Algorithmus gerade pausiert wurde
    public bool isPaused = false;

    private int[] userWahl;

    //Container mit allen Koroutienen
    public Coroutine[] coroutines;

    //Ladesymbol
    public GameObject loading;

    //@Felix Hildebrandt
    //@Lukas Brüggemann
    //Initialisieren des Spielflaechen-Managers
    private void Start()
    {
        //Instanz festlegen
        Instance = this;

        //Listen zur Organisation anlegen
        fleachen = new List<GameObject>();
        aktiveDame = new List<GameObject>();
        Spielfiguren = new Spielfigur[groesse, groesse];
    }

    //@Felix Hildebrandt
    //Kontinuierliche Aktualisierung des Spielflächen-Managers
    private void Update()
    {
        UpdateAuswahl();
        ZeichneSpielflaeche();

        //ausgewählte Spielfigur updaten
        if (Input.GetMouseButtonDown(0))
        {
            if (auswahlX >= 0 && auswahlY >= 0)
            {
                if (ausgewaehlteSpielfigur == null)
                {
                    WaehleSpielfigurAus(auswahlX, auswahlY);
                }
                else
                {
                    BewegeSpielfigur(auswahlX, auswahlY);
                }
            }
        }
    }

    //@Felix Hildebrandt
    //@Lukas Schmitz
    //@Lukas Brüggemann
    //Initialisierung des SpielflaechenManagers
    public void init()
    {
        //Umgebung Erstellen und Skalieren
        ZeichneSpielfeld();
        OberflaecheSkalieren();
        TischSkalieren();
        KameraDrehpunktSkalieren();
        KameraSkalieren();

        //Listen zur Organisation anlegen
        aktiveDame = new List<GameObject>();
        Spielfiguren = new Spielfigur[groesse, groesse];

        //Manuell gesetzte Damen registrieren
        initUserWahl();
        //Dient zur Verzögerung beim rekursiven Durchlauf
        coroutines = new Coroutine[groesse];
    }

    //------------------------------------------
    //Coroutinen/Algorithmen
    //------------------------------------------

    //@Felix Hildebrandt
    //@Lukas Brüggemann
    //@Lukas Schmitz
    //@Marc Ulbricht
    //Rekursiver Algorithmus mit
    //Verknüpfung der Oberflaeche
    public IEnumerator RekursiverDurchlauf(int[] x, int k)
    {
        isIterativ = false;
        isRekursiv = true;
        moveZeigerRek(3.34f, 0.02f, -4.31f);
        moveZeigerRek(2.416f, 0.02f, -3.86f);
        moveZeigerRek(1.077f, 0.02f, -3.42f);
        moveZeigerRek(0.73f, 0.02f, -2.97f);
        //Bestimmen der Reihen, die manuell gesetzte Damen enthalten -> keine Änderung!
        bool[] stellungen;
        try
        {
            stellungen = getManuelleReihen();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            stellungen = new bool[groesse];
        }
        int[] userwahl = getManuelleStellen();

        moveZeigerRek(-0.597f, 0.02f, -2.58f);
        //Bei zu großen Werten automatisch abbrechen
        if (k >= groesse)
        {
            moveZeigerRek(2.92f, 0.02f, -2.11f);
            yield break;
        }

        moveZeigerRek(3.04f, 0.02f, -1.66f);
        //Alle Zeilen auf Konflikte mit bestehenden Damen prüfen
        for (int i = 0; i < groesse; i++)
        {
            moveZeigerRek(-0.38f, 0.02f, -1.22f);
            //Wenn in dieser Reihe bereits eine benutzerdef. Dame steht, nicht verändern
            if (stellungen[k])
            {
                moveZeigerRek(3.54f, 0.02f, -0.65f);
                i = userwahl[k];
                moveZeigerRek(3.54f, 0.02f, 0.56f);
            }
            moveZeigerRek(-1.66f, 0.02f, -0.65f);
            bool problemFound = false;
            //Anzeige der Dame auf dem Schachbrett
            SpawnDame(0, k, i);

            moveZeigerRek(-0.749f, 0.02f, -0.22f);
            //Alle Zeilen auf Konflikte mit bestehenden, vorherigen Damen prüfen
            for (int y = 0; y < k; y++)
            {
                moveZeigerRek(-3.585f, 0.02f, 0.19f);
                //Prüfen, ob Dame in gleicher Reihe wie andere Dame abgesetzt 
                //wurde oder Damen diagonal zueinander stehen
                if (i == x[y] || Mathf.Abs(i - x[y]) == k - y)
                {

                    //Damenfarbe auf rot stellen
                    aktiveDame[aktiveDame.Count - 1].GetComponent<Renderer>().material.color = Color.red;
                    //Kurz warten
                    yield return new WaitForSeconds(time);
                    
                    //Pause-Funktion einbauen
                    while (isPaused)
                    {
                        yield return null;
                    }
                    
                    //Da Dame fehlerhaft positioniert ist -> Löschung
                    Destroy(aktiveDame[aktiveDame.Count - 1]);
                    aktiveDame.RemoveAt(aktiveDame.Count - 1);

                    moveZeigerRek(-1.821f, 0.02f, 1f);
                    //Merken, dass keine LÖsung gefunden wurde
                    problemFound = true;
                    moveZeigerRek(0.075f, 0.02f, 1.42f);
                    break;
                }

            }

            moveZeigerRek(-2.857f, 0.02f, 1.82f);
            if (problemFound)
            {
                if (!stellungen[k])
                {
                    moveZeigerRek(0.145f, 0.02f, 2.44f);
                    //Bei nicht-fixer Dame nächste Stelle ausprobieren
                    continue;
                }
                else
                {
                    //Bei fixer Dame zur vorherigen Dame zurückkehren
                    break;
                }
            }

            moveZeigerRek(-2.15f, 0.02f, 2.894f);
            //Prüfen, ob die letzte Reihe erreicht wurde
            if (k == groesse - 1)
            {
                moveZeigerRek(-1.296f, 0.02f, 3.93f);
                //Passende Stellung gefunden -> Lösung in Liste ablegen
                anzahlLoesungen++;
                GameObject[] manuelleDamen;

                manuelleDamen = GameObject.FindGameObjectsWithTag("dame");
                foreach (GameObject manuelleDame in manuelleDamen)
                    manuelleDame.GetComponent<Renderer>().material.color = Color.green;
                yield return new WaitForSeconds(5f);

                while (isPaused)
                {
                    yield return null;
                }

                moveZeigerRek(0.171f, 0.02f, 3.46f);
                x[k] = i;

            }
            else
            {
                moveZeigerRek(-2.158f, 0.02f, 3.491f);
                //Aktuelle Position abspeichern
                x[k] = i;
                //Kurz warten
                yield return new WaitForSeconds(time);

                moveZeigerRek(-4.555f, 0.02f, 3.907f);
                //Rekursiv eine weitere Coroutine aufrufen
                Coroutine coroutine = StartCoroutine(RekursiverDurchlauf(x, k + 1));
                coroutines[k + 1] = coroutine;
                //Diese Coroutine pausieren, bis die aufgerufene Coroutine beendet ist
                while (coroutines[k + 1] != null)
                {
                    yield return null;
                }
            }
            //Die in dieser Reihe gesetzte Dame löschen
            Destroy(aktiveDame[aktiveDame.Count - 1]);
            aktiveDame.RemoveAt(aktiveDame.Count - 1);
            //Bei fixer Dame keine Umpositionierung zulassen
            if (stellungen[k])
                break;
        }
        //Kurz warten
        yield return new WaitForSeconds(time);
        //Diese Coroutine für beendet erklären und Vorgänger weiterlaufen lassen
        coroutines[k] = null;
        if (k == 0)
        {
            //Alle vorhandenen Damen löschen und neu erstellen, um sie weiter verschieben zu können
            GameObject[] manuelleDamen = GameObject.FindGameObjectsWithTag("dame");
            foreach (var manuelleDame in manuelleDamen)
                Destroy(manuelleDame);
            SpielflaechenManager.Instance.setManuelleStellen(userwahl);
            for (int i = 0; i < userwahl.Length; i++)
            {
                if (userwahl[i] != -1)
                {
                    SpawnDame(0, i, userwahl[i]);
                }
            }
        }

        moveZeigerRek(0.368f, 0.02f, 4.4f);

    }

    //@Felix Hildebrandt
    //@Lukas Schmitz
    //Algorithmus für das manuelle Positionieren
    public IEnumerator ManuellerDurchlauf()
    {
        anzahlLoesungen = 0;

        //Alle bereits vorhandenen Damen löschen, um Dopplungen zu vermeiden
        damenloeschen();

        initUserWahl();
        //Manueller Modus auf Aktiv schalten
        manuellAktiv = true;
        for (int i = 0; i < anzahlManuellerDamen; i++)
        {
            SpawnDame(0, i, 0);
            aktiveDame[i].GetComponent<Renderer>().material.color = Color.green;
            userWahl[i] = 0;
            yield return new WaitForSeconds(time);
        }
    }

    public void initUserWahl()
    {
        userWahl = new int[groesse];
        for (int i = 0; i < groesse; i++)
        {
            userWahl[i] = -1;
        }
    }

    //@Lukas Schmitz
    //Menuelle Stellung auf der Matrix mit der 
    //Lösungsmatrix vergleichen
    public bool prufeManuelleStellung()
    {
        bool flag = true;

        //Stellungen bestimmen

        //Prüfen, ob sich die voreingestellten Damen bedrohen
        for (int j = 0; j < groesse; j++)
        {
            for (int x = j + 1; x < groesse; x++)
            {
                //Wenn sich eine Dame außerhalb des Felds befindet, diese überspringen
                if (userWahl[j] == -1)
                    break;
                if (userWahl[x] == -1)
                    continue;
                //Prüfen, ob Dame in gleicher Reihe wie andere Dame abgesetzt 
                //wurde oder Damen diagonal zueinander stehen
                if (userWahl[j] == userWahl[x] ||
                    Mathf.Abs(userWahl[j] - userWahl[x]) == x - j)
                {
                    //Wenn Damen bedroht werden, nächste Lösung suchen
                    //und gespawnte Dame löschen
                    flag = false;
                    break;
                }
            }
        }

        return flag;
    }

    //@Felix Hildebrandt
    //@Lukas Schmitz
    //@Lukas Brüggemann
    //Iterativer Algorithmus mit
    //Verknüpfung der Oberflaeche
    public IEnumerator IterativerDurchlauf()
    {
        moveZeigerIt(2.4f, 0.02f, -4.7f);

        //Iterativ für Struktogramme
        isIterativ = true;
        isRekursiv = false;
        //Lösungen zurücksetzen
        anzahlLoesungen = 0;
        //Wenn manuell aufgestellte Damen sich gegenseitig bedrohen, abbrechen
        if (!prufeManuelleStellung())
        {
            anzahlLoesungen = -1;
            ManuellMenue.iterativBlock = false;
            yield break;
        }

        moveZeigerIt(3.2f, 0.02f, -4.25f);

        //Bestimmen der Reihen, die manuell gesetzte Damen enthalten -> keine Änderung!
        bool[] stellungen;
        try
        {
            stellungen = getManuelleReihen();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            stellungen = new bool[groesse];
        }

        //Vom Nutzer festgelegte Dame-Positionen sichern
        int[] userwahl = getManuelleStellen();

        init();

        aktiveDame = new List<GameObject>();
        Spielfiguren = new Spielfigur[groesse, groesse];

        moveZeigerIt(2.2f, 0.02f, -3.75f);
        //Lösungsliste anlegen
        List<int[][]> ausgabe = new List<int[][]>();

        moveZeigerIt(2.2f, 0.02f, -3.33f);
        //Zeiger auf die entsprechende Zeile anlegen und initialisieren
        int k = 0;

        moveZeigerIt(1.105f, 0.02f, -2.888f);
        //Zeilen-Array anlegen
        int[] x = new int[groesse];

        moveZeigerIt(2.886f, 0.02f, -2.487f);
        //Zu Beginn erste Dame vor dem Schachfeld platzieren
        x[k] = -1;

        moveZeigerIt(2.494f, 0.02f, -2.01f);
        //Solange auf eine Schachbrettreihe gezeigt wird
        while (k >= 0)
        {
            moveZeigerIt(1.692f, 0.02f, -1.57f);
            //Flag, ob eine Lösung gefunden wurde
            bool flag = false;

            //Solange keine Lösung gefunden wurde und nicht alle Stellen der aktuellen Reihe besucht wurden
            while (!flag && x[k] < groesse - 1)
            {
                moveZeigerIt(-3.574f, 0.02f, -1.005f);

                //Wenn in aktueller Reihe keine fixe Dame zu finden ist
                if (!stellungen[k])
                {
                    moveZeigerIt(0.214f, 0.02f, -0.55f);
                    //Nächsten Platz besuchen
                    x[k]++;
                    moveZeigerIt(1.54f, 0.02f, -0.08f);
                    //Mögliche Lösung gefunden
                    flag = true;
                    //Dame spawnen
                    SpawnDame(0, k, x[k]);
                }
                else
                {
                    //Fixe Dame positionieren
                    x[k] = userwahl[k];
                    moveZeigerIt(1.54f, 0.02f, -0.08f);
                    flag = true;
                    
                    SpawnDame(0,k,x[k]);
                }

                //Kurz warten
                yield return new WaitForSeconds(time);
                //Alle Zeilen auf Konflikte mit bestehenden Damen prüfen
                for (int i = 0; i < k; i++)
                {
                    moveZeigerIt(2.05f, 0.02f, 0.39f);
                    //Pause-Funktion einbauen
                    while (isPaused)
                    {
                        yield return null;
                    }

                    moveZeigerIt(-2.316f, 0.02f, 0.86f);
                    //Prüfen, ob Dame in gleicher Spalte wie andere Dame abgesetzt 
                    //wurde oder Damen diagonal zueinander stehen
                    if (x[i] == x[k] || Mathf.Abs(x[i] - x[k]) == k - i)
                    {
                        moveZeigerIt(1.046f, 0.02f, 1.64f);
                        //Wenn Damen bedroht werden, nächste Lösung suchen
                        //und gespawnte Dame löschen
                        flag = false;

                        //Dame auf rot stellen
                        aktiveDame[aktiveDame.Count - 1].GetComponent<Renderer>().material.color = Color.red;
                        yield return new WaitForSeconds(time);

                        while (isPaused)
                        {
                            yield return null;
                        }

                        //Fehlerhafte Dame löschen
                        Destroy(aktiveDame[aktiveDame.Count - 1]);
                        aktiveDame.RemoveAt(aktiveDame.Count - 1);
                        break;
                    }
                }
                if (stellungen[k])
                    break;
            }

            moveZeigerIt(-1.317f, 0.02f, 2.08f);
            //Wenn eine mögliche (Teil-)Lösung gefunden wurde
            if (flag)
            {
                moveZeigerIt(1.313f, 0.02f, 2.75f);
                //Prüfen, ob in letzter Reihe angekommen
                if (k == groesse - 1)
                {
                    moveZeigerIt(3.25f, 0.02f, 3.79f);
                    //Passende Stellung gefunden -> Lösung in Liste ablegen
                    ausgabe.Add(getMatrixFromPosArray(x));
                    for (int i = 0; i < groesse; i++)
                    {
                        aktiveDame[i].GetComponent<Renderer>().material.color = Color.green;
                    }
                    anzahlLoesungen++;
                    yield return new WaitForSeconds(5f);

                    while (isPaused)
                    {
                        yield return null;
                    }

                    //Bei einer manuell gesetzten Dame diese nicht löschen
                    if (stellungen[k])
                    {
                        k--;
                        Destroy(aktiveDame[aktiveDame.Count - 1]);
                        aktiveDame.RemoveAt(aktiveDame.Count - 1);
                    }
                    //else
                    //{
                        Destroy(aktiveDame[aktiveDame.Count - 1]);
                        aktiveDame.RemoveAt(aktiveDame.Count - 1);
                    //}

                }
                else
                {
                    moveZeigerIt(0.577f, 0.02f, 3.395f);
                    //In nächste Reihe wechseln und weitere Dame vorbereiten
                    x[++k] = -1;
                    moveZeigerIt(0.46f, 0.02f, 3.829f);
                }
            }
            else
            {
                moveZeigerIt(-2.08f, 0.02f, 2.786f);
                //In vorherige Reihe wechseln
                k--;
                //Alle fixen Damen überspringen
                while (k >= 0 && stellungen[k])
                {
                    k--;
                    Destroy(aktiveDame[aktiveDame.Count - 1]);
                    aktiveDame.RemoveAt(aktiveDame.Count - 1);
                }

                while (isPaused)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(time);
                try
                {
                    //Fehler tritt am Ende bei letzter Dame auf
                    Destroy(aktiveDame[aktiveDame.Count - 1]);
                    aktiveDame.RemoveAt(aktiveDame.Count - 1);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }
            moveZeigerIt(2.288f, 0.02f, 4.33f);

        }

        //Alle vorhandenen Damen löschen und neu erstellen, um sie weiter verschieben zu können
        GameObject[] manuelleDamen = GameObject.FindGameObjectsWithTag("dame");
        foreach (var manuelleDame in manuelleDamen)
            Destroy(manuelleDame);
        SpielflaechenManager.Instance.setManuelleStellen(userwahl);
        for (int i = 0; i < userwahl.Length; i++)
        {
            if (userwahl[i] != -1)
            {
                SpawnDame(0, i, userwahl[i]);
            }
        }

        if (ManuellMenue.iterativBlock)
            ManuellMenue.iterativBlock = false;
    }

    //@Lukas Schmitz
    //Matrix anhand eines eindimensionalen Positionsarrays erstellen
    private int[][] getMatrixFromPosArray(int[] pos)
    {
        int[][] a = new int[groesse][];

        for (int i = 0; i < pos.Length; i++)
        {
            int[] zeile = new int[groesse];
            zeile[pos[i]] = 1;
            a[i] = zeile;
        }

        return a;
    }

    //@Felix Hildebrandt
    //@Lukas Brüggemann
    //Anzeigen der Ergebnis-Matrix
    public IEnumerator ErgebnisAnzeigen()
    {
        for (int i = 0; i < groesse; i++)
        {
            for (int j = 0; j < groesse; j++)
            {
                //ergebnisNummer = aktuelle Position der Matrix
                int[][] value = positionen[ergebnisNummer];
                if (value[i][j] == 1)
                {
                    SpawnDame(0, i, j);
                    yield return new WaitForSeconds(0f);
                }
            }
        }
    }

    //@Lukas Brüggemann
    //Berechnen der Ergebnis-Matrix
    public void Ergebnisberechnen()
    {
        DamenproblemRekursiv damenProblem = new DamenproblemRekursiv(groesse);
        positionen = damenProblem.getAllPositions();
        damenProblem.getEindeutigeLosungen(positionen);
        anzahlLoesungen = positionen.Count;
        StartCoroutine("ErgebnisAnzeigen");

    }

    //------------------------------------------
    //Benutzereingaben handeln
    //------------------------------------------

    //@Felix Hildebrandt
    //Spielfigur vom Spielfeld für manuelle Positionierung auswählen
    private void WaehleSpielfigurAus(int x, int y)
    {
        if (Spielfiguren == null || x >= groesse || y >= groesse || Spielfiguren[x, y] == null)
            return;

        spielzugGestattet = Spielfiguren[x, y].SpielzugGestattet();
        ausgewaehlteSpielfigur = Spielfiguren[x, y];
        SpielflaechenEffekte.Instance.SpielzugMitEffektGestattet(spielzugGestattet);
    }

    //@Felix Hildebrandt
    //@Lukas Schmitz
    //Ausgewählte Spielfigur auf anderes Feld bewegen
    private void BewegeSpielfigur(int x, int y)
    {
        if (spielzugGestattet[x, y])
        {
            Spielfigur s = Spielfiguren[x, y];
            userWahl[ausgewaehlteSpielfigur.aktuelleXPos] = -1;
            if (s != null)
            {
                Destroy(s.gameObject);
            }

            Spielfiguren[ausgewaehlteSpielfigur.aktuelleXPos, ausgewaehlteSpielfigur.aktuelleYPos] = null;
            ausgewaehlteSpielfigur.transform.position = GetFlaechenMitte(x, y);
            ausgewaehlteSpielfigur.setPosition(x, y);
            Spielfiguren[x, y] = ausgewaehlteSpielfigur;
            userWahl[x] = y;
        }

        ausgewaehlteSpielfigur = null;
        SpielflaechenEffekte.Instance.HideEffekte();
    }

    //@Felix Hildebrandt
    //Die Feldparameter der aktuellen Mausposition
    //auf die aktuelle Auswahl setzen (ganze Zahlen)
    private void UpdateAuswahl()
    {
        if (!Camera.main)
            return;

        RaycastHit kollision;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out kollision, 25.0f, LayerMask.GetMask("Oberflaeche")))
        { 
            auswahlX = (int)kollision.point.x;
            auswahlY = (int)kollision.point.z;
        }
        else
        {
            //Standardmäßig := Spielfeld nicht ausgewählt
            auswahlX = -1;
            auswahlY = -1;
        }
    }

    //------------------------------------------
    //Damen handeln
    //------------------------------------------

    //@Felix Hildebrandt
    //Dame auf Position Spawnen
    //index := GameObjekt-Index -> nicht verändern, da nur eine Spielfigur vorhanden (Dame)
    //x := xPos
    //y := yPos
    public void SpawnDame(int index, int x, int y)
    {
        GameObject go = Instantiate(dame[index], GetFlaechenMitte(x, y), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        Spielfiguren[x, y] = go.GetComponent<Spielfigur>();
        Spielfiguren[x, y].setPosition(x, y);
        aktiveDame.Add(go);


    }

    //@Felix Hildebrandt
    //Löscht die Damen in der Lösungsansicht
    //mit der besonderheit, dass die aktiveDame-Liste
    //durchlaufen und deren Elemente gelöscht wird
    public void damenloeschen()
    {
        while (true)
        {
            if (aktiveDame.Count != 0)
            {
                Destroy(aktiveDame[aktiveDame.Count - 1]);
                aktiveDame.RemoveAt(aktiveDame.Count - 1);
            }
            else
            {
                break;
            }

        }
    }

    //@Felix Hildebrandt
    //Spielflaeche Spawnen (Wird bei ZeichneSpielflaeche aufgerufen)
    //Spawnt die Schwarz-Weiß-Plates
    public void SpawnSpielflaeche(int index, Vector3 position)
    {
        GameObject go = Instantiate(dame[index], position, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);

        fleachen.Add(go);
    }

    //------------------------------------------
    //Umgebung erstellen und skalieren
    //------------------------------------------

    //@Felix Hildebrandt
    //Skaliert Oberflaeche für die Maus-Auswahl auf
    //aktuelle Spielfeld-Groesse
    private void OberflaecheSkalieren()
    {
        GameObject go = GameObject.Find("Oberflaeche");
        go.transform.position = new Vector3(0.5f * groesse, 0f, 0.5f * groesse);
        go.transform.localScale = new Vector3(0.1f * groesse, 1f, 0.1f * groesse);
    }

    //@Felix Hildebrandt
    //Skalieren vom Tisch auf Spielfeldgröße
    private void TischSkalieren()
    {
        GameObject go = GameObject.Find("Tisch");
        go.transform.position = new Vector3(0.5f * groesse, -0.51f, 0.5f * groesse);
        go.transform.localScale = new Vector3(1.3f * groesse, 1f, 1.3f * groesse);
    }

    //@Felix Hildebrandt
    //Skalieren vom KameraDrehpunkt auf Spielfeldgröße
    public void KameraDrehpunktSkalieren()
    {
        GameObject go = GameObject.Find("KameraDrehpunkt");
        go.transform.position = new Vector3(0.5f * groesse, -0.51f, 0.5f * groesse);
    }

    //@Felix Hildebrandt
    //Skalieren vom Kamera auf Spielfeldgröße
    private void KameraSkalieren()
    {
        GameObject go = GameObject.Find("Kamera");
        go.transform.position = new Vector3(groesse - 0.5f * groesse, 17.32f, -6f);
    }

    //@Felix Hildebrandt
    //Berechnet anhand der Position des GameObject die Mitte
    //des Feldes aus (damit Spawn mittig ist)
    private Vector3 GetFlaechenMitte(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (FLAECHEN_GROESSE * x) + FLAECHEN_ABSTAND;
        origin.z += (FLAECHEN_GROESSE * y) + FLAECHEN_ABSTAND;
        origin.y = 0.0f;
        return origin;
    }

    //@Felix Hildebrandt
    //Zeichnet die Schwarz-Weiß-Plates
    private void ZeichneSpielfeld()
    {
        for (int i = 0; i < groesse; i = i + 2)
        {
            for (int j = 0; j < groesse; j = j + 2)
            {

                SpawnSpielflaeche(1, GetFlaechenMitte(j, i));
                if (j != groesse - 1)
                    SpawnSpielflaeche(2, GetFlaechenMitte(j + 1, i));
            }
        }

        for (int i = 1; i < groesse; i = i + 2)
        {
            for (int j = 0; j < groesse; j = j + 2)
            {
                SpawnSpielflaeche(2, GetFlaechenMitte(j, i));
                if (j != groesse - 1)
                    SpawnSpielflaeche(1, GetFlaechenMitte(j + 1, i));
            }
        }
    }

    //@Felix Hildebrandt
    //Zeichnet das Mesh unter den Plates
    private void ZeichneSpielflaeche()
    {

        Vector3 breitenLinien = Vector3.right * groesse;
        Vector3 tiefenLinien = Vector3.forward * groesse;


        for (int i = 0; i <= groesse; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + breitenLinien);

            for (int j = 0; j <= groesse; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + tiefenLinien);
            }
        }

        //Auswahl zeichnen
        if (auswahlX >= 0 && auswahlY >= 0 && auswahlX < groesse && auswahlY < groesse)
        {
            Debug.DrawLine(
                Vector3.forward * auswahlY + Vector3.right * auswahlX,
                (Vector3.forward * (auswahlY + 1)) + (Vector3.right * (auswahlX + 1)));

            Debug.DrawLine(
                Vector3.forward * (auswahlY + 1) + Vector3.right * auswahlX,
                Vector3.forward * auswahlY + Vector3.right * (auswahlX + 1));
        }

    }

    //@Lukas Brüggemann
    //@Felix Hildebrandt
    //Setzt Kamera, Damen sowie die Gesamte Spielfläche
    //auf den Initialisierungszustand zurück
    public void clear()
    {
        KameraZuruecksetzen();
        int count = aktiveDame.Count;
        //for (int i = 0; i == aktiveDame.Count - 1; i++)
        while (true)
        {
            if (aktiveDame.Count != 0)
            {
                Destroy(aktiveDame[aktiveDame.Count - 1]);
                aktiveDame.RemoveAt(aktiveDame.Count - 1);
            }
            else
            {
                break;
            }

        }

        while (true)
        {
            if (fleachen.Count != 0)
            {
                Destroy(fleachen[fleachen.Count - 1]);
                fleachen.RemoveAt(fleachen.Count - 1);
            }
            else
            {
                break;
            }

        }

        manuellAktiv = false;
        anzahlLoesungen = 0;
        isPaused = false;
    }

    //@Felix Hildebrandt
    //Zurücksetzen der Kamerarotation auf
    //Initialisierungswert
    public void KameraZuruecksetzen()
    {
        GameObject go = GameObject.Find("KameraDrehpunkt");
        go.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //------------------------------------------
    //Getter/Setter
    //------------------------------------------

    //@Lukas Schmitz
    //Bestimmen von fixen Reihen (durch benutzerdefinierte Damen)
    public bool[] getManuelleReihen()
    {
        bool[] ausgabe = new bool[groesse];
        //Liste durchsuchen, um ein Unveränderlichkeit-Flag zu setzen
        for (int i = 0; i < groesse; i++)
        {
            //Überall ein Flag setzen, wo eine manuelle Dame ist
            if (userWahl[i] != -1)
            {
                ausgabe[i] = true;
            }
        }

        return ausgabe;
    }

    //@Lukas Schmitz
    //Getter für die manuelle Dame Stellung
    public int[] getManuelleStellen()
    {
        return SpielflaechenManager.Instance.userWahl;
    }

    //@Lukas Schmitz
    //Setter für die manuelle Dame Stellung
    public void setManuelleStellen(int[] userwahl)
    {
        this.userWahl = userwahl;
    }
    //Getter für Entscheidung Iterative/Rekursiv Struktogramm
    public Boolean getIsIterativ()
    {
        return isIterativ;     
    }
    
    //Zeiger Bewegen Struktogramme
    public void moveZeigerRek(float x, float y, float z)
    {
        GameObject go = GameObject.Find("ZeigerRekursiv");
        go.transform.localPosition = new Vector3(x,y,z);
    }
    public void moveZeigerIt(float x, float y, float z)
    {
        GameObject go = GameObject.Find("ZeigerIterativ");
        go.transform.localPosition = new Vector3(x, y, z);
    }
}