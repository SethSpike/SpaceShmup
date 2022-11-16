using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoundsCheck))]

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        /*if (!bndCheck.isOnScreen)
        {
            if (pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(gameObject);
            }
        }*/
        if (bndCheck.LocIs(BoundsCheck.eScreensLocs.offDown))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.GetComponent<ProjectileHero>() != null)
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enemy hit by not Projectile" + otherGO.name);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
