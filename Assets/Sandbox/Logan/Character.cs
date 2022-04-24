using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// TO DO add this in new file called "Character"
public abstract class Character
{
    public string Name { get; set; }
    protected string Description { get; set; }

    [SerializeField]
    private GameObject residentBuilding;
    // default constructor 
    public Character()
    {
        Name = "DefaultName";
    }
    // constructor if name is provided
    public Character(string name)
    {
        Name = name;
    }
    public abstract float perkMultipliers(ArrayList traces);

    // Method that overrides the base class (System.Object) implementation if character is printed
    public override string ToString()
    {
        return Name;
    }
}

public class MainUser : Character
{
    public static int totalCoins;
    public static int accessScore;
    public string username; 
    // Instance constructor
    public MainUser(string name)
    {
        this.Name = name;
        totalCoins = 0; 
    }
    public void loadCharacterData(string username)
    {
        // get data from database?
        int savedCoins = 10;
        totalCoins = savedCoins; 
    }
    public void addRewards(int coins, int accessScoreIncrease)
    {
        totalCoins += coins;
        accessScore += accessScoreIncrease; 
    }

    public override float perkMultipliers(ArrayList traces)
    {
        float score = 0;
        // First, loop through all lines user made
        for (int i = 0; i < traces.Count; i++)
        {
            ArrayList trace = (ArrayList)traces[i];
            // Second, loop through all coordinates of line
            for (int j = 0; j < trace.Count; j++)
            {
                Vector3 currPos = (Vector3)trace[j];
                score += 1;
            }
        }

        return score;
    }
}

public class WheelChairUser : Character
{

    // Instance constructor 
    public WheelChairUser(string name, string desc)
    {
        // The following properties are inherited from Character.
        this.Name = name;
        this.Description = desc;
    }


    public override float perkMultipliers(ArrayList traces)
    {
        float score = 0;
        // First, loop through all lines user made
        for (int i = 0; i < traces.Count; i++)
        {
            ArrayList trace = (ArrayList) traces[i]; 
            Vector3 startPos = (Vector3)trace[0];
            Vector3 endPos = (Vector3)trace[trace.Count-1];
            if (Math.Abs(startPos.x - endPos.x) < 1 && Math.Abs(startPos.y - endPos.y) < 1)
            { // start and end position are similar
                score += 50;
            }
            // Second, loop through all coordinates of line
            for (int j = 0; j < trace.Count; j++)
            {
                Vector3 currPos = (Vector3)trace[j];
                score += 1; 
                //score += currPos.y + currPos.x;
            }
        }

        return score;
    }
}

public class VisuallyImpaired : Character
{

    // Instance constructor that has four parameters.
    public VisuallyImpaired(string name, string desc)
    {
        // The following properties are inherited from Character.
        this.Name = name;
        this.Description = desc;
    }

    /**
     * @abstract calculates score based off character perks
     */
    private float glassesBonus(ArrayList traces, Vector3 monoclePos, Vector3 endPos)
    {
        float rightSide = Math.Max(monoclePos.x, endPos.x);
        float leftSide = Math.Min(monoclePos.x, endPos.x);

        for (int i = 0; i < traces.Count; i++)
        {
            ArrayList trace = (ArrayList)traces[i];
            for (int j = 0; j < trace.Count; j++)
            {
                Vector3 currPos = (Vector3)trace[j];
                if (currPos.x < rightSide && currPos.x > leftSide) return 100; // "glasses" bonus
            }
        }
        return 10; // "monocle" bonus
    }
    /**
     * @abstract calculates score based off character perks
     */
    public override float perkMultipliers(ArrayList traces)
    {
        float score = 0;
        bool caneCond1 = false, caneCond2 = false;
        
        // First, loop through all lines user made
        for (int i = 0; i < traces.Count; i++)
        {
            ArrayList trace = (ArrayList)traces[i];
            Vector3 startPos = (Vector3)trace[0];
            Vector3 endPos = (Vector3)trace[trace.Count - 1];
            Vector3 monoclePos = startPos; 
            if (Math.Abs(startPos.x - endPos.x) < 0.75 && Math.Abs(startPos.y - endPos.y) < 0.75)
            { // start and end position are similar
                if (monoclePos != startPos)
                {
                    score += glassesBonus(traces, monoclePos,endPos); // checks if theres a trace between the 2 'ovals'
                }
                else monoclePos = endPos;                 
            }
            // Second, loop through all coordinates of line
            for (int j = 0; j < trace.Count; j++)
            {
                Vector3 currPos = (Vector3)trace[j];
                if(startPos.y < currPos.y)
                { // start or end of cane trace
                    caneCond1 = true; 
                }
                if(startPos.y > currPos.y && caneCond1 == true)
                { // user switches direction to go down (could be making handle or the pole of cane, either way is fine)
                    if(Math.Abs(currPos.x - startPos.x) > 10) caneCond2 = true; // loose condition allows for non-cane looking traces to fit, but thats fine
                }
                score += 1;
            }
            score += caneCond2 ? 100 : 10; 
        }

        return score;
    }
}


public class HardOfHearing : Character
{

    // Instance constructor that has four parameters.
    public HardOfHearing(string name, string desc)
    {
        // The following properties are inherited from Character.
        this.Name = name;
        this.Description = desc;
    }

    private float expressiveBonus(Vector3 startPos, Vector3 currPos)
    {
        Debug.Log("expression check " + Math.Abs(startPos.x - currPos.x));
        float longLine = 5;
        if (Math.Abs(startPos.x - currPos.x) > longLine)
        { // 'expressive' bonus
            Debug.Log("expressions bonus!");
            if (Math.Abs(startPos.y - currPos.y) > longLine)
            { // 'expressive' bonus x2
                return 10;
                Debug.Log("expressions x2 bonus!");
            }
            return 5;
        }
        return 1;
    }
    public override float perkMultipliers(ArrayList traces)
    {
        float score = 0;
        
        if (traces.Count == 5) score += 50; // '5-finger' bonus
        // First, loop through all lines user made
        for (int i = 0; i < traces.Count; i++)
        {
            ArrayList trace = (ArrayList)traces[i];
            Vector3 startPos = (Vector3)trace[0];
            Vector3 endPos = (Vector3)trace[trace.Count - 1];

            // Second, loop through all coordinates of line
            for (int j = 0; j < trace.Count; j++)
            {
                Vector3 currPos = (Vector3)trace[j];
                score += expressiveBonus(startPos,currPos);
            }
        }

        return score;
    }
}