using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBidness : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ColumnReader(string[] columnNames)
    {
        string columnA = columnNames[9];
        string columnB = columnNames[11];
        string columnC = columnNames[14];
        string[] startColumnHeaders = new string[] { columnA, columnB, columnC };
        HeaderSpitter(startColumnHeaders);

    }

    void HeaderSpitter(string[] startColumnHeaders)
    {
        Text[] textInstances = GetComponentsInChildren<Text>();

        string columnHeaderA = startColumnHeaders[0];
        string columnHeaderB = startColumnHeaders[1];
        string columnHeaderC = startColumnHeaders[2];

        textInstances[0].text = columnHeaderA;
    }
}
