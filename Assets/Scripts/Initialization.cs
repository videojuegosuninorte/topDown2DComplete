using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Initialization : MonoBehaviour
{
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
            for (int j = 15; j < 20; j++)
            {
                writer.Write("T" + t + ",");
                t++;
            }
        }

        t = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                writer.Write("P" + t + ",");
                t++;
            }
        }

        writer.WriteLine("");
        writer.Close();
        StreamReader reader = new StreamReader("gameResults.txt");
        reader.Close();
    }

}
