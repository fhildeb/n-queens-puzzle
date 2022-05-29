using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Klasse für das Dame-Spielfigur-Verhalten
//Es wird geprüft, wohin dich die Dame bei der
//manuellen Positionierung bewegen
//@Felix Hildebrandt
public class Dame : Spielfigur
{
    //Prüfen auf der Matrix, auf welchen Feldern der
    //Spielzug gestattet ist
    //@Felix Hildebrandt
    public override bool[,] SpielzugGestattet()
    {
        bool[,] r = new bool[SpielflaechenManager.groesse, SpielflaechenManager.groesse];

        //Falls manuelles Platzieren ausgewählt, darf Dame platziert werden
        if (SpielflaechenManager.Instance.manuellAktiv)
        {
            Spielfigur s;
            int i, j;

            // Prüfen von aktueller Damen-Position aus nach Rechts
            i = aktuelleXPos;
            while (true)
            {
                i++;
                if (i >= SpielflaechenManager.groesse)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[i, aktuelleYPos];
                if (s == null)
                    r[i, aktuelleYPos] = true;
                else
                {
                    r[i, aktuelleYPos] = true;
                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus nach Links
            i = aktuelleXPos;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[i, aktuelleYPos];
                if (s == null)
                    r[i, aktuelleYPos] = true;
                else
                {
                    r[i, aktuelleYPos] = true;
                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus nach Oben
            i = aktuelleYPos;
            while (true)
            {
                i++;
                if (i >= SpielflaechenManager.groesse)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[aktuelleXPos, i];
                if (s == null)
                    r[aktuelleXPos, i] = true;
                else
                {
                    r[aktuelleXPos, i] = true;
                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus nach Unten
            i = aktuelleYPos;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[aktuelleXPos, i];
                if (s == null)
                    r[aktuelleXPos, i] = true;
                else
                {
                    r[aktuelleXPos, i] = true;
                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus in Diagonalen Links Oben
            i = aktuelleXPos;
            j = aktuelleYPos;
            while (true)
            {
                i--;
                j++;
                if (i < 0 || j >= SpielflaechenManager.groesse)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[i, j];
                if (s == null)
                    r[i, j] = true;
                else
                {
                    r[i, j] = true;

                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus in Diagonalen Rechts Oben
            i = aktuelleXPos;
            j = aktuelleYPos;
            while (true)
            {
                i++;
                j++;
                if (i >= SpielflaechenManager.groesse || j >= SpielflaechenManager.groesse)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[i, j];
                if (s == null)
                    r[i, j] = true;
                else
                {
                    r[i, j] = true;
                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus in Diagonalen Links Unten
            i = aktuelleXPos;
            j = aktuelleYPos;
            while (true)
            {
                i--;
                j--;
                if (i < 0 || j < 0)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[i, j];
                if (s == null)
                    r[i, j] = true;
                else
                {
                    r[i, j] = true;
                    break;
                }
            }

            // Prüfen von aktueller Damen-Position aus in Diagonalen Rechts Unten
            i = aktuelleXPos;
            j = aktuelleYPos;
            while (true)
            {
                i++;
                j--;
                if (i >= SpielflaechenManager.groesse || j < 0)
                    break;

                s = SpielflaechenManager.Instance.Spielfiguren[i, j];
                if (s == null)
                    r[i, j] = true;
                else
                {
                    r[i, j] = true;
                    break;
                }
            }
        }

        //Sonst Rückgabe false-Matrix
        return r;
    }
}

