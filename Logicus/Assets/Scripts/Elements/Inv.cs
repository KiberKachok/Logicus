using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv : Element, IUpdatable
{
    public Connector a;
    public Connector f;
        
    // Update is called once per frame
    public new void Update()
    {
        f.IsActive = !a.IsActive;
    }

    public new string GetData()
    {
        return $"{f.IsActive}";
    } 
    
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
