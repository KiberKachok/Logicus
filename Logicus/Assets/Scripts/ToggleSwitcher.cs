using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitcher : MonoBehaviour
{
    private Toggle toggle;

    public GameObject activeObject;
    public GameObject disabledObject;
    public Connector a;
    
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        
        activeObject.SetActive(toggle.isOn);
        disabledObject.SetActive(!toggle.isOn);
        a.IsActive = toggle.isOn;
        
        toggle.onValueChanged.AddListener(isOn =>
        {
            activeObject.SetActive(isOn);
            disabledObject.SetActive(!isOn);
            a.IsActive = toggle.isOn;
        });
    }

    // Update is called once per frame
    void Update()
    {
        toggle.isOn = a.IsActive;
    }
}
