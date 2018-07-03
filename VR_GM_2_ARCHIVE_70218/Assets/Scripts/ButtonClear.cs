using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using csvImporter;

public class ButtonClear : CsvImporter {

void OnClick()
    {
        GameObject sphereClone = GameObject.Find("sphereClone");
        Destroy(sphereClone);
    }
}
