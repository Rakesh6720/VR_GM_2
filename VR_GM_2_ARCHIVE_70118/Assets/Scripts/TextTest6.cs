using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using csvImporter;

public class TextTest6 : MonoBehaviour {

    List<float> BenchNums = new List<float>();
    private Text textInstance;

    // Use this for initialization
    void Start()
    {

        textInstance = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        List<Player> PlayerList2 = new List<Player>();
        PlayerList2 = GameObject.Find("GameLogic").GetComponent<CsvImporter>().Players;

        foreach (Player player in PlayerList2)
        {
            if (player.bench != 0 && player.year == 2015)
            {
                BenchNums.Add(player.bench);
            }
        }

        BenchNums.Remove(BenchNums.Max());
        BenchNums.Remove(BenchNums.Min());
        float BenchNumsSum = BenchNums.Sum();
        //print(fortyTimeSum);
        float ThreeConeTimeAverage = BenchNumsSum / BenchNums.Count;

        textInstance.text = ("Average: \n" + ThreeConeTimeAverage.ToString("0.00"));

    }
}
