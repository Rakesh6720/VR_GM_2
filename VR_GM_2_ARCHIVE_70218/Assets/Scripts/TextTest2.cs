using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using csvImporter;

public class TextTest2 : MonoBehaviour {
    List<float> fortyTimes = new List<float>();
    private Text textInstance;
    CsvImporter csvImporter;

    // Use this for initialization
    void Start() {

        textInstance = GetComponent<Text>();
        csvImporter = new CsvImporter();
    }
	
	// Update is called once per frame
	void Update () {
        List<Player> PlayerList2 = new List<Player>();
        PlayerList2 = GameObject.Find("GameLogic").GetComponent<CsvImporter>().Players;

        foreach (Player player in PlayerList2)
        {
            if (player.fortyYd != 0 && player.year == 2015)
            {
                fortyTimes.Add(player.fortyYd);
            }
        }

        float fortyTimeSum = fortyTimes.Sum();
        //print(fortyTimeSum);
        float fortyTimeAverage = fortyTimeSum / fortyTimes.Count;
        
        textInstance.text = ("Average: \n" + fortyTimeAverage.ToString("0.00"));

    }
}
