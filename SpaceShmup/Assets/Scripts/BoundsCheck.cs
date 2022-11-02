using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// keeps a GameObject on the screen
/// Note: This only works for a othographic Main Camera
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    public enum eType {  center, inset, outset};
    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;

    [Header("Dynamic")]
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
        //restrict the X pos to camWidth
        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
        }
        //restrict the Y pos to caHeight
        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
        }
        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
        }
        transform.position = pos;
    }
}
