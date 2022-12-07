using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_2 : MonoBehaviour
{
    [Header("inscribed")]
    public float rotationsPerSecond = 0.1f;

    [Header("Dynamic")]
    public int levelShown = 0; // this is set between lines

    Material mat;


    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        int currlevel = Mathf.FloorToInt(Hero_2.S.shieldLevel);
        if (levelShown != currlevel)
        {
            levelShown = currlevel;
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }

        //rotate the shield a bit every frame in a time based way
        float rZ = -(rotationsPerSecond * Time.time * -360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }

}
