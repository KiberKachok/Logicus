using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementCreator : MonoBehaviour
{
    public GameObject[] allowedElements;
    public GameObject spawnButtonPrefab;
    public Transform buttonsRoot;

    public RectTransform elementCreator;
    public RectTransform targetAlong;
    
    public GraphicRaycaster raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem eventSystem;

    private void Update()
    {
        m_PointerEventData = new PointerEventData(eventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(m_PointerEventData, results);
        
        List<GameObject> underMouse = results
            .Where(r => r.gameObject.layer == 7)
            .Select(r => r.gameObject)
            .ToList();
        
        if (Input.anyKeyDown  && underMouse.Count == 0 && elementCreator.gameObject.activeSelf)
        {
            elementCreator.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(targetAlong, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out Vector2 localPoint);
            elementCreator.anchoredPosition = localPoint;

            for (int i = 0; i < buttonsRoot.childCount; i++)
            {
                Destroy(buttonsRoot.GetChild(i).gameObject);
            }
            
            foreach (GameObject element in allowedElements)
            {
                GameObject buttonObject = Instantiate(spawnButtonPrefab, buttonsRoot);
                buttonObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = element.name;
                buttonObject.GetComponent<Button>().onClick.AddListener(() => SpawnElementInPoint(element, localPoint));
            }
            elementCreator.gameObject.SetActive(true);
        }
    }

    public void SpawnElementInPoint(GameObject element, Vector2 pos)
    {
        elementCreator.gameObject.SetActive(false);
        Instantiate(element, targetAlong).GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
