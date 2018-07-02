using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClear : CsvImporter {

void OnClick()
    {
        GameObject sphereClone = GameObject.Find("sphereClone");
        Destroy(sphereClone);
    }
}
