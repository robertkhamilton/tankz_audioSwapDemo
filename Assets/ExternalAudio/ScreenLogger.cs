using System.Collections.Generic;
using UnityEngine;

public class ScreenLogger : MonoBehaviour
{
    private Queue<string> myLogQueue = new Queue<string>();
    private string myLogString = "";
    private const int maxLogs = 15; // Max number of lines on screen

    void OnEnable() { Application.logMessageReceived += HandleLog; }
    void OnDisable() { Application.logMessageReceived -= HandleLog; }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Format the message with its log type prefix
        string newString = $"[{type}] {logString}";
        myLogQueue.Enqueue(newString);

        // Include stack trace only for serious errors
        if (type == LogType.Exception || type == LogType.Error)
        {
            myLogQueue.Enqueue($"  StackTrace: {stackTrace}");
        }

        // Clip the queue length to prevent scrolling off the screen
        while (myLogQueue.Count > maxLogs)
        {
            myLogQueue.Dequeue();
        }

        // Rebuild the final display block
        myLogString = string.Empty;
        foreach (string log in myLogQueue)
        {
            myLogString += log + "\n";
        }
    }

    void OnGUI()
    {
        // Draws a basic text block on the top-left area of the screen
        GUILayout.BeginArea(new Rect(400, 1000, 2000, Screen.height - 20));
        GUILayout.Label(myLogString, new GUIStyle { fontSize = 16, normal = new GUIStyleState { textColor = Color.white } });
        GUILayout.EndArea();
    }
}