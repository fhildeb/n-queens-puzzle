using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Felix Hildebrandt
//Klasse zum Anzeigen des Effektes, auf welche Platformen die
//Dame gestellt werden darf
public class SpielflaechenEffekte : MonoBehaviour
{
    public static SpielflaechenEffekte Instance { set; get; }

    //Prefab == GameObjekt-Vorlage
    public GameObject effektPrefab;

    //Liste mit allen Effekten (Feldern)
    private List<GameObject> effekte;

    //@Felix Hildebrandt
    private void Start()
    {
        Instance = this;
        effekte = new List<GameObject>();
    }

    //@Felix Hildebrandt
    //Ekkekt-Platform erstellen, wenn nicht schon verfügbar
    public GameObject getEffektObjekt()
    {
        GameObject go = effekte.Find(g => !g.activeSelf);

        if (go == null)
        {
            go = Instantiate(effektPrefab);
            effekte.Add(go);
        }

        return go;
    }

    //@Felix Hildebrandt
    //Prüfen, ob Spielzug mit Effekt gestattet
    //und aktivieren des Effektes auf aktueller Feldposition
    public void SpielzugMitEffektGestattet(bool[,] moves)
    {
        for(int i = 0; i < SpielflaechenManager.groesse; i++)
        {
            for(int j = 0; j < SpielflaechenManager.groesse; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = getEffektObjekt();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i+0.5f, 0.1f, j+0.5f);
                }
            }
        }
    }

    //@Felix Hildebrandt
    //Effekt ausschalten
    public void HideEffekte()
    {
        foreach (GameObject go in effekte)
            go.SetActive(false);
    }
}