using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Felix Hildebrandt
//Abstrakte Klasse von welcher alle Spielfiguren erben.
//Definiert grundlegend benötigte Funktionen
public abstract class Spielfigur : MonoBehaviour {

    //Positionen auslesen
    public int aktuelleXPos { set; get; }
    public int aktuelleYPos { set; get; }

    //@Felix Hildebrandt
    //Positionen setzen
    public void setPosition(int x, int y)
    {
        aktuelleXPos = x;
        aktuelleYPos = y;
    }

    //@Felix Hildebrandt
    //Prüfen in welche Richtung die Figur bewegt werden darf
    public virtual bool[,] SpielzugGestattet()
    {
        return new bool[SpielflaechenManager.groesse,SpielflaechenManager.groesse];
    }
}
