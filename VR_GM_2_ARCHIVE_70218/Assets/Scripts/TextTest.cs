using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using csvImporter;

public class TextTest : MonoBehaviour {

    private Text textInstance;
    public string message;
    string[] startColumnHeaders = new string[3];
    string[] columnNames;

	// Use this for initialization
	void Start () {
        textInstance = GetComponent<Text>();


    }

    void Update()
    {
        string[] tempArray = GameObject.Find("GameLogic").GetComponent<CsvImporter>().columnNames;
        print(tempArray[0]);
        string[] columnNames = new string[tempArray.Length];
        columnNames = tempArray;
        ColumnReader(columnNames);
        message = HeaderSpitter(startColumnHeaders);
        textInstance.text = message;

    }

    public string [] ColumnReader(string[] columnNames)
    {
        string columnA = columnNames[9];
        string columnB = columnNames[11];
        string columnC = columnNames[14];
        string[] startColumnHeaders = new string[] { columnA, columnB, columnC };
        return startColumnHeaders;
        

    }

    string HeaderSpitter(string[] startColumnHeaders)
    {
        

        string columnHeaderA = startColumnHeaders[0];
        string columnHeaderB = startColumnHeaders[1];
        string columnHeaderC = startColumnHeaders[2];
        string iMessage = columnHeaderA;
        return iMessage;
        
    }



}
