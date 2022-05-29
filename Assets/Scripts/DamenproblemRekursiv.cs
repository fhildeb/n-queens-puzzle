using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//@ Mariska Steinfeldt
//Klasse für den rekursiven Algorithmus
//inklusive der Prüfung auf eindeutige Lösungen
//und der Ausgabe von Ergebnis-Matrizen
public class DamenproblemRekursiv
{
    //Größe der Matrix
    private int n;

    //Konstruktor mit Zuweisung der Größe
    public DamenproblemRekursiv(int n)
    {
        this.n = n;
    }

    //Getter für die Größe des N-Dame-Problems
    public int getN()
    {
        return n;
    }

    //@Lukas Schmitz
    //Erstellen der n x n-Matrix aus eindimensionalen Positionsarray
    private int[][] getMatrixFromPosArray(int[] pos) 
    {
        int[][] ausgabe = new int[n][];

        for (int i = 0; i < pos.Length; i++)
        {
            int[] zeile = new int[n];
            zeile[pos[i]] = 1;
            ausgabe[i] = zeile;
        }

        return ausgabe;
    }

    //Liste der Erstellten Ergebnis-Matrizen
    private List<int[][]> ausgabe;

    //@ Mariska Steinfeldt
    //Methode welche den Algorithmus startet 
    //und eine Liste mit allen Lösungs-Matrizen ausgegeben
    public List<int[][]> getAllPositions()
    {
        ausgabe = new List<int[][]>();

        int[] x = new int[n];

        getPositionRekursiv(x, 0);

        return ausgabe;
    }

    //@ Mariska Steinfeldt
    //Rekursiver Algorithmus
    void getPositionRekursiv(int[] x,int k)
    {
        if (k >= n)
        {
            return;
        }

        for(int i = 0; i < n; i++)
        {
            if (k == 0 && k < n-1)
            {
                x[k] = i;
                getPositionRekursiv(x, k+1);
            }

            else
            {
                bool problemFound = false;
                for (int y = 0; y < k; y++)
                {
                    if (i == x[y] || Mathf.Abs(i - x[y]) == k - y)
                    {
                        problemFound = true;
                        break;
                    }

                }
                
                if (problemFound)
                    continue;

                if (k == n - 1)
                {
                    x[k] = i;

                    ausgabe.Add(getMatrixFromPosArray(x));
                }
                else
                {
                    x[k] = i;
                    getPositionRekursiv(x, k + 1);
                }

            }
        }
    }

    //@Lukas Schmitz
    //Es werden 2 Matrizen auf Feldgleichheit geprüft
    private static bool matrixEquals(int[][] m1, int[][] m2)
    {
        if (m1.Length == m2.Length)
        {
            //Alle Spalten der Matrix durchgehen
            for (int i = 0; i < m1.Length; i++)
            {
                //Auf Ungleichheit der Arrays prüfen
                if (!Enumerable.SequenceEqual(m1[i], m2[i]))
                    return false;
            }
            return true;
        }
        else return false;
    }

    //@Lukas Schmitz
    //Vertikale Spiegelmatrix erstellen
    private int[][] getSpiegelMatrixVertikal(int[][] matrix)
    {
        int[][] ausgabe = new int[matrix.Length][];
        //Gleich große Matrix bereitstellen
        for (int i = 0; i < matrix.Length; i++)
        {
            int[] zeile = new int[matrix.Length];
            ausgabe[i] = zeile;
        }
        for (int spalte = 0; spalte < matrix.Length; spalte++)
        {
            for (int zeile = 0; zeile < matrix.Length; zeile++)
            {
                ausgabe[spalte][zeile] = matrix[matrix.Length - 1 - spalte][zeile];
            }
        }

        return ausgabe;
    }

    //Horizontale Spielgelmatrix erstellen
    private int[][] getSpiegelMatrixHorizontal(int[][] matrix)
    {
        int[][] ausgabe = new int[matrix.Length][];
        //Gleich große Matrix bereitstellen
        for (int i = 0; i < matrix.Length; i++)
        {
            int[] zeile = new int[matrix.Length];
            ausgabe[i] = zeile;
        }
        for (int spalte = 0; spalte < matrix.Length; spalte++)
        {
            for (int zeile = 0; zeile < matrix.Length; zeile++)
            {
                ausgabe[spalte][zeile] = matrix[spalte][matrix.Length - 1 - zeile];
            }
        }

        return ausgabe;
    }

    //Rotierte Matix erhalten
    public int[][] getRotatedMatrix(int[][] matrix, int n)
    {
        int[][] ausgabe = new int[matrix.Length][];
        //Gleich große Matrix bereitstellen
        for (int i = 0; i < matrix.Length; i++)
        {
            int[] zeile = new int[matrix.Length];
            ausgabe[i] = zeile;
        }

        switch (n)
        {
            //Matrix um 90° drehen
            case 1:
                {
                    for (int spalte = 0; spalte < matrix.Length; spalte++)
                    {
                        for (int zeile = 0; zeile < matrix.Length; zeile++)
                        {
                            ausgabe[zeile][spalte] = matrix[matrix.Length - 1 - spalte][zeile];
                        }
                    }
                    break;
                }
            //Matrix um 180° drehen
            case 2:
                {
                    ausgabe = getRotatedMatrix(getRotatedMatrix(matrix, 1), 1);
                    break;
                }
            //Matrix um 270° drehen
            case 3:
                {
                    ausgabe = getRotatedMatrix(getRotatedMatrix(matrix, 2), 1);
                    break;
                }
            default: break;

        }

        return ausgabe;

    }


    //@Lukas Schmitz
    //Uneindeutige Lösungen herausfiltern, 
    //welche nicht durch Spiegelung/Rotation von anderen
    //Lösungen gebildet werden können
    public int getEindeutigeLosungen(List<int[][]> losungen)
    {
        if (losungen.Count == 1)
            return 1;
        for (int i = 0; i < losungen.Count; i++)
        {
            for (int j = i + 1; j < losungen.Count; j++)
            {
                if (matrixEquals(losungen[i], losungen[j]))
                {
                    losungen.RemoveAt(j);
                    j--;
                    continue;
                }

                int[][] spiegelVertikal = getSpiegelMatrixVertikal(losungen[j]);
                if (matrixEquals(losungen[i], spiegelVertikal))
                {
                    losungen.RemoveAt(j);
                    j--;
                    continue;
                }

                int[][] spiegelHorizontal = getSpiegelMatrixHorizontal(losungen[j]);
                if (matrixEquals(losungen[i], spiegelHorizontal))
                {
                    losungen.RemoveAt(j);
                    j--;
                    continue;
                }

                if (matrixEquals(losungen[i], getRotatedMatrix(losungen[j], 1)) || matrixEquals(losungen[i], getRotatedMatrix(spiegelVertikal, 1)) || matrixEquals(losungen[i], getRotatedMatrix(spiegelHorizontal, 1)))
                {
                    losungen.RemoveAt(j);
                    j--;
                    continue;
                }
                if (matrixEquals(losungen[i], getRotatedMatrix(losungen[j], 2)) || matrixEquals(losungen[i], getRotatedMatrix(spiegelVertikal, 2)) || matrixEquals(losungen[i], getRotatedMatrix(spiegelHorizontal, 2)))
                {
                    losungen.RemoveAt(j);
                    j--;
                    continue;
                }
                if (matrixEquals(losungen[i], getRotatedMatrix(losungen[j], 3)) || matrixEquals(losungen[i], getRotatedMatrix(spiegelVertikal, 3)) || matrixEquals(losungen[i], getRotatedMatrix(spiegelHorizontal, 3)))
                {
                    losungen.RemoveAt(j);
                    j--;
                    continue;
                }
            }
        }

        return losungen.Count;
    }

    //Test der Klasse
    void Start()
    {

        //Testweise den Algorithmus ausprobieren
        DamenproblemRekursiv damenProblem = new DamenproblemRekursiv(3);
        Debug.Log(damenProblem.getN() + " x " + damenProblem.getN() + " - Damen-Problem:");
        List<int[][]> positionen = damenProblem.getAllPositions();
        Debug.Log("Alle Positionen: " + positionen.Count);
    }
}