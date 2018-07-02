using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using csvImporter;

public class CoefficientTexter : MonoBehaviour{
    
    private Text textInstance;
    CsvImporter csvImporter;
    

    // Use this for initialization
    void Start()
    {
       
        textInstance = GetComponent<Text>();
        csvImporter = new CsvImporter();

    }

    // Update is called once per frame
    void Update()
    {
        //csvImporter.CalculateCorrelationCoefficients();
        //textInstance.text = man;

    }

}
