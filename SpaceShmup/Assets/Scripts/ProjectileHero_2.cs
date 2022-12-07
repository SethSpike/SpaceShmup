using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoundsCheck_2))]

public class ProjectileHero_2 : MonoBehaviour
{
    private BoundsCheck_2 bndCheck_2;
    // Start is called before the first frame update
    private void Awake()
    {
        bndCheck_2 = GetComponent<BoundsCheck_2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck_2.LocIs(BoundsCheck_2.eScreensLocs.offDown))
        {
            Destroy(gameObject);
        }
    }

}