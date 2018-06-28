using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CsvImporter : MonoBehaviour
{

    List<Player> Players = new List<Player>();
    public GameObject spherePreFab;
    GameObject sphereClone;
    int j = 1;
    Dictionary<int, Player> PlayerDictionary = new Dictionary<int, Player>();
    Dictionary<int, string> collegeIndex = new Dictionary<int, string>();
    List<string> collegeNames = new List<string>();
    Dictionary<int, int> collegeHistogramCollection = new Dictionary<int, int>();
    int plotScale = 50;

    // Use this for initialization
    void Start()
    {

        //this sets up your playspace
        CreatePlayerList();
        AssignPlayerID();
        CreatePlayerDictionary();
        List<Player> WinnowedPlayers = new List<Player>();
        WinnowedPlayers = WinnowMaker();
        MakingDatScatterPlot(WinnowedPlayers);

    }

    void Testing()
    {
        //PrintPlayerDictionary();

        //List<Player> alabamaFrequency = Players.FindAll(i => i.college == "Alabama");
        //print(alabamaFrequency.Count);

        //ListSchools();

        //CreateCollegeList();
        //List<int> list = new List<int>(collegeIndex.Keys); //a list of Keys of the college Index which has integer keys and college name string values

        //double correlationCoeff = CalculateCorrelationCoefficients();
        //print(correlationCoeff);

        //collegeHistogramCollection = CreateCollegeHistogram(); //cross-reference this dictionary with the collegeIndex to match college ID key with college name count in Player Dictionary to 
        //create the values for your college Histogram graph
    } //this method holds test functions to check functionality of other methods using the PRINT function in the Unity Editor

    public Dictionary<int,int> CreateCollegeHistogram() //this method will return a dictionary that you have to crossreference with the collegeIndex to match college name with its count in this returned dictionary
    {
        //take the list that has every college in it
        //make this list a dictionary where the key value is string collegeName, and the value is the number of times the college name appears in playerDictionary
        //run that list against the player dictionary
        //every time there is a ping and increase the count of the college
        Dictionary<int, int> collegeHistogramDictionary = new Dictionary<int, int>();
            
        foreach(KeyValuePair<int, string> pair in collegeIndex)
        {
            int count = 0;
            string collegeName = pair.Value;

            for (int i = 0; i < PlayerDictionary.Count; i++)
            {
                if (pair.Value == PlayerDictionary[i].college)
                {
                    count++;
                }

                collegeHistogramDictionary.Add(pair.Key, count); //this stores the key of the college and its count in the playerdictionary in its own dictionary
            }
        }

        return collegeHistogramDictionary; //this method returns a dictionary that has key value college ID number and value value of college's count in the data set
    }

    public List<Player> WinnowMaker() //this function lets you choose which two variables to compare for your regression by elminating the zeroes
    {
        List<Player> WinnowedPlayers = new List<Player>();

        var noZeroes = from Player in Players //this Winnomaker reduces the Players List to only players who have values for 40 yard time and Pick Total
                       where Player.pickTotal != 0 && Player.fortyYd != 0
                       select Player;

        foreach (var player in noZeroes)
        {
            WinnowedPlayers.Add(player);//this will publish the list of Players without "0" in both Forty and PickTotal column
        }

        print(WinnowedPlayers.Count); // this is working
        print(WinnowedPlayers[0].name + "," + WinnowedPlayers[0].fortyYd + "," + WinnowedPlayers[0].pickTotal); // this is working: Jared Abbrederis,4.5,176

        return WinnowedPlayers;
    }

    void MakingDatScatterPlot(List<Player>WinnowedPlayers) // this function will create a scatterplot from the linq querred  X and Y values in WinnowedPlayers
    {
        float xVal;
        float yVal;
        List<float> xValues = new List<float>();
        List<float> yValues = new List<float>();
        
        //first, get the x and y fields -- these fields are going to be X Axis: forty time, and Y Axis: pick total from Players in WinnowedPlayers list
        //isolate the X and Y values in their respective lists
        foreach (Player player in WinnowedPlayers)
        {
            //for each player, chop out their 40 time (Players[i].forty) and their pick total (Players[i].pickTotal
            xVal = player.fortyYd;
            yVal = player.pickTotal;
            //push these values into their respective lists, wherein their indices will correspond to the same player
            xValues.Add(xVal);
            yValues.Add(yVal);
        }


        
        //locate the X-MAX value and the X-MIN value
        float xMin = xValues.Min();
        float xMax = xValues.Max();
        //locate the Y-MAX value and the Y-MIN value
        float yMin = yValues.Min();
        float yMax = yValues.Max();
        //normalize each value so that your scale is on a 0 - 1 range
        //normalize X Axis by dividing (x-xMin) / (xMax - xMin) 
        List<float> xNormalized = new List<float>();
        foreach (float x in xValues)
        {
            //this creates a list of normalized X Values
            xNormalized.Add((x - xMin) / (xMax - xMin));
        }
        //normalize Y Axis by dividing (y-yMin) / (yMax - yMin)
        List<float> yNormalized = new List<float>();
        foreach (float y in yValues)
        {
            //this creates a list of normalized Y Values
            yNormalized.Add((y - yMin) / (yMax - yMin));
        }
        //create a clone of your preFab and instantiate it
        List<GameObject> scatterOrbClones = new List<GameObject>();

        for (int i = 0; i < xNormalized.Count; i++)
        {
            GameObject sphereClone = new GameObject();
            sphereClone.transform.position = new Vector3(xNormalized[i], yNormalized[i]);
            scatterOrbClones.Add(sphereClone);
        }

        //instantiate a prefab clone with the vector 3 pos
        foreach (GameObject orb in scatterOrbClones)
        {
            Instantiate(spherePreFab, orb.transform.position * plotScale, Quaternion.identity);
        }
    }

    public double CorrelationCoefficientCalculator(List<Player> WinnowedPlayers) // this function calculates the correlation between a player's x and y value
    {
        int sampleCount = WinnowedPlayers.Count;
        //okay, so first, i'm going to make an array of all the x values, which is the 40 yard dash time;

        float[] dashTime = new float[sampleCount]; // dashTime is the array of all x values
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            dashTime[i] = WinnowedPlayers[i].fortyYd;
            //print(dashTime[i]); -- THIS WORKS, proving array dashTime holds all the dash times that are not zero
        }
        float xValueSum = dashTime.Sum(); // this number holds the value of Sigma X

        float[] yValues = new float[sampleCount];
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            yValues[i] = WinnowedPlayers[i].pickTotal;
        }
        float yValuesSum = yValues.Sum(); //this number holds the value of Sigma Y

        float[] xyProduct = new float[sampleCount]; //this is array of all the xyProducts that are tallied by the next loop
        for (int i = 0; i < WinnowedPlayers.Count; i++) //this loop calculates the products of all x and their corresponding y
        {
            xyProduct[i] = WinnowedPlayers[i].pickTotal * dashTime[i];
        }
        float xyProductSum = xyProduct.Sum(); //this number holds the value of Sigma XY

        float[] xSquared = new float[sampleCount];
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            xSquared[i] = WinnowedPlayers[i].pickTotal;
        }
        float xSquaredSum = xSquared.Sum(); //this number holds the value of Sigma X Squared

        float[] ySquared = new float[sampleCount]; // this array holds the values of every Y squared
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            ySquared[i] = WinnowedPlayers[i].pickTotal * WinnowedPlayers[i].pickTotal;
        }
        float ySquaredSum = ySquared.Sum(); //this number is the Sigma of Y Squared

        float corrCoeffNum = ((xyProductSum * sampleCount) - (xValueSum * yValuesSum)); //this calculates the numerator of the correlation coefficient equation
        float corrCoeffDenSquared = ((xSquaredSum * sampleCount - (xValueSum * xValueSum)) * ((ySquaredSum * sampleCount) - (yValuesSum * yValuesSum))); //this calculates the denomominator of the correlation coefficient equation
        var corrCoeffDen = Math.Sqrt((double)corrCoeffDenSquared); //this takes the square root of the correlation coefficient's denominator

        double corrCoeff = corrCoeffNum / corrCoeffDen; //this is the final value of the correlation coefficient

        return corrCoeff;
    }

    public double CalculateCorrelationCoefficients()
    {
        //the method above has isolated the linq query that appears below.  
        //this method EXCLUSIVELY compares 40 yard time as the independent X variable and Draft Pick Total as the dependent Y variable

        List<Player> WinnowedPlayers = new List<Player>();
        //what variables do you want to compare?
        //show me the correlation coefficients for 40 yrd time and overall pick position (as long as it's not zero)
        //first, let's calculate the sample size:

        var noZeroes = from Player in Players
                       where Player.pickTotal != 0 && Player.fortyYd != 0
                       select Player;

        foreach (var player in noZeroes)
        {
            WinnowedPlayers.Add(player);//this will publish the list of Players without "0" in both Forty and PickTotal column
            print(player.name + " " + player.fortyYd + ", " + player.pickTotal);
        }

        //print(WinnowedPlayers.Count); // this is working
        //print(WinnowedPlayers[0].name + "," + WinnowedPlayers[0].fortyYd + "," + WinnowedPlayers[0].pickTotal); // this is working: Jared Abbrederis,4.5,176

        int sampleCount = WinnowedPlayers.Count;
        //okay, so first, i'm going to make an array of all the x values, which is the 40 yard dash time;

        float[] dashTime = new float[sampleCount]; // dashTime is the array of all x values
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            dashTime[i] = WinnowedPlayers[i].fortyYd;
            //print(dashTime[i]); -- THIS WORKS, proving array dashTime holds all the dash times that are not zero
        }
        float xValueSum = dashTime.Sum(); // this number holds the value of Sigma X

        float[] yValues = new float[sampleCount];
        for (int i = 0; i < WinnowedPlayers.Count; i ++)
        {
            yValues[i] = WinnowedPlayers[i].pickTotal;
        }
        float yValuesSum = yValues.Sum(); //this number holds the value of Sigma Y

        float[] xyProduct = new float[sampleCount]; //this is array of all the xyProducts that are tallied by the next loop
        for(int i = 0; i < WinnowedPlayers.Count; i++) //this loop calculates the products of all x and their corresponding y
        {
            xyProduct[i] = WinnowedPlayers[i].pickTotal * dashTime[i];
        }
        float xyProductSum = xyProduct.Sum(); //this number holds the value of Sigma XY

        float[] xSquared = new float[sampleCount];
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            xSquared[i] = WinnowedPlayers[i].pickTotal;
        }
        float xSquaredSum = xSquared.Sum(); //this number holds the value of Sigma X Squared

        float[] ySquared = new float[sampleCount]; // this array holds the values of every Y squared
        for (int i = 0; i < WinnowedPlayers.Count; i++)
        {
            ySquared[i] = WinnowedPlayers[i].pickTotal * WinnowedPlayers[i].pickTotal;
        }
        float ySquaredSum = ySquared.Sum(); //this number is the Sigma of Y Squared

        float corrCoeffNum = ((xyProductSum * sampleCount) - (xValueSum * yValuesSum)); //this calculates the numerator of the correlation coefficient equation
        float corrCoeffDenSquared = ((xSquaredSum * sampleCount - (xValueSum * xValueSum)) * ((ySquaredSum * sampleCount) - (yValuesSum * yValuesSum))); //this calculates the denomominator of the correlation coefficient equation
        var corrCoeffDen = Math.Sqrt((double)corrCoeffDenSquared); //this takes the square root of the correlation coefficient's denominator

        double corrCoeff = corrCoeffNum / corrCoeffDen; //this is the final value of the correlation coefficient

        return corrCoeff;
    }

    void CreateCollegeList()
    {
        int count = 0;
        foreach (KeyValuePair<int,Player> player in PlayerDictionary)
        {
            if(!collegeNames.Contains(player.Value.college))
            {
                collegeNames.Add(player.Value.college);
            }
            //else
            //{
            //    print(count + " " + player.Value.college);
            //    count++;
            //}
        }
        for (int i = 0; i < collegeNames.Count; i++)
        {
            collegeIndex.Add(count, collegeNames[i]);
            count++;
        }
        
        //foreach (KeyValuePair<int, string> college in collegeIndex) -- USE THIS to test if the college names list is populating
        //{
        //    print(college.Key + "," + college.Value);
        //}

    }

    void ListSchools()
    {
        int s = 0;
        var playerSchools = from player in Players
                            where player.college == "Alabama"
                            select player;

        foreach (var school in playerSchools)
        {
            s++;
        }

        print(s);
    }

    void CreatePlayerList()
    {
        TextAsset combineData = Resources.Load<TextAsset>("combineData");
        string[] data = combineData.text.Split(new char[] { '#' });
        Debug.Log("You have " + data.Length + "rows.");

        string[] columnNames = data[0].Split(new char[] { ',' });
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Player p = new Player();
            int.TryParse(row[0], out p.year);
            p.name = row[1];
            p.firstName = row[2];
            p.lastName = row[3];
            p.position = row[4];
            int.TryParse(row[5], out p.heightFeet);
            int.TryParse(row[6], out p.heightInches);
            float.TryParse(row[7], out p.heightInchesTotal);
            int.TryParse(row[8], out p.weight);
            float.TryParse(row[9], out p.fortyYd);
            float.TryParse(row[10], out p.twentySs);
            float.TryParse(row[11], out p.threeCone);
            float.TryParse(row[12], out p.vertical);
            int.TryParse(row[13], out p.broad);
            int.TryParse(row[14], out p.bench);
            int.TryParse(row[15], out p.round);
            p.college = row[16];
            int.TryParse(row[17], out p.pick);
            int.TryParse(row[18], out p.pickRound);
            int.TryParse(row[19], out p.pickTotal);
            int.TryParse(row[20], out p.wonderLick);
            float.TryParse(row[21], out p.nflGrade);
            Players.Add(p);
        }
    }

    void AssignPlayerID()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].ID = j;
            j++;
            //print(Players[i].ID + "," + Players[i].name);
        }
    }

    void CreatePlayerDictionary()
    {
        foreach (Player player in Players)
        {
            PlayerDictionary.Add(player.ID, player);
        }
    } 

    void PrintPlayerDictionary()
    {
        foreach (KeyValuePair<int, Player> player in PlayerDictionary)
        {
            print(player.Key + "," + player.Value.name);
        }
    }
}



