using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// keeps a GameObject on the screen
/// Note: This only works for a othographic Main Camera
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreensLocs
    {
        onScreen = 0, //0000 in binary
        offRight = 1, //0001
        offLeft = 2, //0010
        offUp = 4, //0100
        offDown = 8 //1000
    }
    public enum eType {  center, inset, outset};
    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreensLocs screenlocs = eScreensLocs.onScreen;
    //public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Called after Update() has been called on all GameObjects
    void LateUpdate()
    {
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenlocs = eScreensLocs.onScreen;
        //isOnScreen = true;
        //restrict the X pos to camWidth
        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            screenlocs = eScreensLocs.offRight;
            //isOnScreen = false;
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            screenlocs = eScreensLocs.offLeft;
            //isOnScreen = false;
        }
        //restrict the Y pos to caHeight
        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            screenlocs = eScreensLocs.offUp;
            //isOnScreen = false;
        }
        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            screenlocs = eScreensLocs.offDown;
            //isOnScreen = false;
        }
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            screenlocs = eScreensLocs.onScreen;
            //isOnScreen = true;
        }
    }
    public bool isOnScreen
    {
        get { return (screenlocs == eScreensLocs.onScreen); }
    }
    public bool LocIs (eScreensLocs checkLoc)
    {
        if (checkLoc == eScreensLocs.onScreen) return isOnScreen;
        return ((screenlocs & checkLoc) == checkLoc);
    }
}
