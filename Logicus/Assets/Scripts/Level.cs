using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Level", order = 0)]
public class Level : SerializedScriptableObject
{
    public string title;
    public string task;
    public string status = " [???]";
    public GameObject[] allowedElements;
    
    public (string name, bool[] values)[] condition;
    public (string name, bool[] values)[] answer;

    public GameObject inputNode;
    public GameObject outputNode;
}
