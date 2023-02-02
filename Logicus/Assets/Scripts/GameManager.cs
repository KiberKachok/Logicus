using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject connectionPrefab;
    public GameObject pseudoConnectorPrefab;

    public GameObject currentPseudoConnection;
    public GameObject currentPseudoConnector;
    
    public GraphicRaycaster raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem eventSystem;
    public RectTransform background;

    void Update()
    {
        m_PointerEventData = new PointerEventData(eventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(m_PointerEventData, results);
        
        List<Connector> connectorsUnderMouse = results
            .Where(r => r.gameObject.layer == 6)
            .Select(r => r.gameObject.GetComponent<Connector>())
            .ToList();

        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Connector fromConnector = connectorsUnderMouse
                .FirstOrDefault(c => c.connectorType == ConnectorType.output);
            
            if (fromConnector != null)
            {
                currentPseudoConnector = Instantiate(pseudoConnectorPrefab, transform);
                currentPseudoConnection = Instantiate(connectionPrefab, transform);
                currentPseudoConnection.transform.SetAsFirstSibling();
                Connection connection = currentPseudoConnection.GetComponent<Connection>();
                        
                connection.input = fromConnector;
                connection.output = currentPseudoConnector.GetComponent<Connector>();
            }
        }

        if (currentPseudoConnector != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(background, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out Vector2 localPoint);
            currentPseudoConnector.GetComponent<RectTransform>().anchoredPosition = localPoint - BackgroundMover.СurrentOffset;
        }
        
        //Check if the left Mouse button is clicked
        if (Input.GetKeyUp(KeyCode.Mouse0) && currentPseudoConnector != null)
        {
            Connector toConnector = connectorsUnderMouse
                .FirstOrDefault(c => c.connectorType == ConnectorType.input);
            
            if (toConnector != null && !toConnector.name.Contains("PseudoConnector"))
            {
                if (toConnector.mainConnection != null)
                {
                    toConnector.mainConnection.SelfDestroy();
                }
                toConnector.mainConnection = currentPseudoConnection.GetComponent<Connection>();
                currentPseudoConnection.GetComponent<Connection>().output = toConnector;
                currentPseudoConnection = null;
            }
            else
            {
                Destroy(currentPseudoConnection);
            }
            
            Destroy(currentPseudoConnector);
        }
    }
}
