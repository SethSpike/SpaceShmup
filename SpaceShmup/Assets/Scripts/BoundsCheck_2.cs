using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// keeps a GameObject on the screen
/// Note: This only works for a othographic Main Camera
/// </summary>

public class BoundsCheck_2 : MonoBehaviour
{
    [System.Flags]
    public enum eScreensLocs
    {
        onScreen = 0, 
        offRight = 1, 
        offLeft = 2, 
        offUp = 4, 
        offDown = 8 
    }
    public enum eType { center, inset, outset };
    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreensLocs screenlocs = eScreensLocs.onScreen;
    public float camWidth;
    public float camHeight;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Called after Update() has been called on all GameObjects
    void LateUpdate()
    {
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenlocs = eScreensLocs.onScreen;
        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            screenlocs = eScreensLocs.offRight;
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            screenlocs = eScreensLocs.offLeft;
        }
        //restrict the Y pos to caHeight
        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            screenlocs = eScreensLocs.offUp;
        }
        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            screenlocs = eScreensLocs.offDown;
        }
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            screenlocs = eScreensLocs.onScreen;
        }
    }
    public bool isOnScreen
    {
        get { return (screenlocs == eScreensLocs.onScreen); }
    }
    public bool LocIs(eScreensLocs checkLoc)
    {
        if (checkLoc == eScreensLocs.onScreen) return isOnScreen;
        return ((screenlocs & checkLoc) == checkLoc);
    }
}
