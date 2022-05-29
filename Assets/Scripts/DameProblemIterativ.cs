using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//@Lukas Schmitz
//Klasse für den iterativen Algorithmus
//inklusive der Prüfung auf eindeutige Lösungen
//und der Ausgabe von Ergebnis-Matrizen
public class DamenProblemIterativ
{ 
    //Größe der Matrix
    private int n;

    //Konstruktor mit Zuweisung der Größe
    public DamenProblemIterativ(int n)
    {
        this.n = n;
    }

    //Getter für die Größe des N-Dame-Problems
    public int getN()
    {
        return n;
    }

    //Matrix anhand eines eindimensionalen Positionsarrays erstellen
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

    //Iterativer Algorithmus zum Finden aller N-Damen-Stellungen
    public List<int[][]> getAllPositions()
    {
        List<int[][]> ausgabe = new List<int[][]>();

        //Zeiger auf die entsprechende Zeile
        int k = 0;
        int[] x = new int[n];
        x[k] = -1;
        while (k >= 0)
        {
            //Flag, ob eine Lösung gefunden wurde
            bool flag = false;
            while (!flag && x[k] < n - 1)
            {
                x[k]++;
                flag = true;
                //Alle Zeilen auf Konflikte mit bestehenden Damen prüfen
                for (int i = 0; i < k; i++)
                {
                    //Prüfen, ob Dame in gleicher Reihe wie andere Dame abgesetzt wurde oder Damen diagonal zueinander stehen
                    if (x[i] == x[k] || Mathf.Abs(x[i] - x[k]) == k - i)
                    {
                        //Wenn Damen bedroht werden, nächste Lösung suchen
                        flag = false;
                    }
                }
            }

            if (flag)
            {
                if (k == n - 1)
                {
                    //Passende Stellung gefunden -> Lösung in Liste ablegen
                    ausgabe.Add(getMatrixFromPosArray(x));
                }
                else
                {
                    //Hier nächste Dame spawnen lassen
                    x[++k] = -1;
                }
            }
            else
            {
                //Hier Dame löschen und zur vorherigen zurückkehren
                k--;
            }

        }

        //Liste aller Lösungen ausgeben
        return ausgabe;
    }

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

    //Rotierte Matix erhalten (n*90°)
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

    //Matrix für die Ausgabe auf der Konsole 
    //in einen String umwandeln
    public void toString(int[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            String ausgabe = "\t";
            for (int j = 0; j < matrix[i].Length; j++)
            {
                ausgabe += matrix[i][j] + "\t";
            }
            Debug.Log(ausgabe);
        }
    }

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

    //Uneindeutige Lösungen herausfiltern, 
    //welche nicht durch Spiegelung/Rotation von anderen 
    //Lösungen gebildet werden können
    public int getEindeutigeLosungen(List<int[][]> losungen)
    {
        if (losungen.Count == 1)
            return 0;
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
        return losungen.Count();
    }

    //Test der Klasse
    void Start()
    {
        
        //Testweise den Algorithmus ausprobieren
        DamenProblemIterativ damenProblem = new DamenProblemIterativ(5);
        Debug.Log(damenProblem.getN() + " x " + damenProblem.getN() + " - Damen-Problem:");
        List<int[][]> positionen = damenProblem.getAllPositions();
        Debug.Log("Alle Positionen: " + positionen.Count);
    }
}
