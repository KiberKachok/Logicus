using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nand : Element, IUpdatable
{
    public Connector a;
    public Connector b;
    public Connector f;
    
    // Start is called before the first frame update
    public new void Update()
    {
        f.IsActive = !(a.IsActive && b.IsActive);
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
