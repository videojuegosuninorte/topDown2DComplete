using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    private int START_P = 0, END_P = 3, START_T = 15, END_T = 18;
    void Start()
    {
        Application.runInBackground = true;
        writeHeader();
    }

    private void writeHeader()
    {
        StreamWriter writer = new StreamWriter("gameResults.txt", true);
        writer.Write("R" + ",");
        int t = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_T; j < END_T; j++)
            {
                writer.Write("T" + t + ",");
                t++;
            }
        }

        t = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_P; j < END_P; j++)
            {
                writer.Write("P" + t + ",");
                t++;
            }
        }

        writer.Write("T,P");

        writer.WriteLine("");
        writer.Close();
    }

}
