using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

[Serializable]
public class Connector : SerializedMonoBehaviour
{
    private bool isActive;
    [SerializeField]
    public bool IsActive
    {
        set
        {
            isActive = value;
            if(image != null)
                image.color = isActive ? activeColor : passiveColor;
        }
        get
        {
            return isActive;
        }
    }
    
    public Color activeColor;
    public Color passiveColor;
    
    public ConnectorType connectorType;
    public ProceduralImage image;

    public Connection mainConnection;
    
    private void Start()
    {
        image = GetComponent<ProceduralImage>();
    }

    private void Update()
    {

    }
}

public enum ConnectorType
{
    input,
    output
}
