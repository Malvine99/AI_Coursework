using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugLogToFile : MonoBehaviour
{
    string filename = "";


    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }


    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/StateTest.txt";
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filename, true);

        tw.WriteLine("" + logString);

        tw.Close();
    }
}
