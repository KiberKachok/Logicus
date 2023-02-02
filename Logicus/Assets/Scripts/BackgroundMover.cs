using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public RectTransform background;
    private Vector3 currentPosition;
    private Vector3 deltaPosition;
    private Vector3 lastPosition;
    public Texture2D cursorDragTexture;

    public float currentZoom;
    public Vector2 zoomClamp;
    public float zoomSensitivity;

    public static Vector2 СurrentOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = Input.mousePosition;
        deltaPosition = currentPosition-lastPosition;
        lastPosition = currentPosition;
        if (Input.GetMouseButton(2))
        {
            background.pivot -= new Vector2(deltaPosition.x, deltaPosition.y) * 0.5f / 10000 / currentZoom;
            background.anchoredPosition = new Vector2(0f, 0f);
            Cursor.SetCursor(cursorDragTexture, new Vector2(12f, 12f), CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(null, Vector3.zero, CursorMode.ForceSoftware);
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, 
            new Vector2(Screen.width, Screen.height) / 2, 
            Camera.main, 
            out Vector2 localPoint);

        currentZoom = background.localScale.x + zoomSensitivity * Input.mouseScrollDelta.y * Mathf.Sqrt(currentZoom);
        currentZoom = Mathf.Clamp(currentZoom, zoomClamp.x, zoomClamp.y);
        background.localScale = new Vector3(currentZoom, currentZoom, 1);

        СurrentOffset = 10000 * (new Vector2(0.5f, 0.5f) - background.pivot);
    }
}
