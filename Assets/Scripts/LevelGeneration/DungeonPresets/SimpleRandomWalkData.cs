using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will allow us to create preset dungeon sizes (iterations and walk length)
//Within the Scripts/Data folder we can create a new PCG>SimpleRandomWalkData, where we can store the desired
//values for our presets and name the item accordingly

//Properties of the menu item
[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]

//Inheriting from ScriptableObject instead of Monobehavoiur will allow us to create it through the create menu
public class SimpleRandomWalkData : ScriptableObject
{
    //The number of times we want to run the algorithm, and the length of each iteration
    public int iterations = 10, walkLength = 10;
    //Should each iteration start at a random position, i.e. not the origin each time
    public bool startRandomly = true;
}

