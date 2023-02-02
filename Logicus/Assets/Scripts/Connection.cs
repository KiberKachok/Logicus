using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Connection : MonoBehaviour, IUpdatable 
{
    public Connector input;
    public Connector output;
    
    public Color activeColor;
    public Color passiveColor;
    public Transform background;
    private UILineRenderer uiLineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        uiLineRenderer = GetComponent<UILineRenderer>();
        background = GameObject.Find("Elements").transform;
    }
    
    // Update is called once per frame
    public void Update()
    {
        output.IsActive = input.IsActive;
    }

    private void LateUpdate()
    {
        if (input != null && output != null)
        {
            uiLineRenderer.color = input.IsActive ? activeColor : passiveColor;

            Vector2[] linePoints = new Vector2[4];
        
            linePoints[0] = RectTransformUtility.CalculateRelativeRectTransformBounds(
                background,
                input.transform).center + new Vector3(3f,0,0);
        
            linePoints[3] = RectTransformUtility.CalculateRelativeRectTransformBounds(
                background,
                output.transform).center - new Vector3(3f,0,0);

            linePoints[1] = linePoints[0] + new Vector2(40f, 0);
            linePoints[2] = linePoints[3] + new Vector2(-40f, 0);

            uiLineRenderer.m_points = linePoints;
        
            uiLineRenderer.enabled = false;
            uiLineRenderer.enabled = true;   
        }
        else
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        input.mainConnection = null;
        output.mainConnection = null;
        DestroyImmediate(gameObject);
    }
}
