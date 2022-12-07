using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_2 : MonoBehaviour
{
    static public Hero_2 S { get; private set; }
    [Header("Inscribe")]
    //Ship movement
    public float speed = 30;
    public float rollMult = -45;
    public GameObject projectilePrefab_2;
    public float projectileSpeed = 40;

    [Header("Dynamic")]
    [Range(0, 4)]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject lastTriggerGo = null;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero_2.Awake() - attempt to assign second hero");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //pull in info from input class
        float hAxis = Input.GetAxis("Horizontal_2");

        //Change our trans.pos based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        transform.position = pos;

        //Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(0, hAxis * rollMult, 0);

        if (Input.GetKeyDown(KeyCode.W))
        {
            TempFire_2();
        }
    }

    void TempFire_2()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab_2);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        if (go == lastTriggerGo) return;
        lastTriggerGo = go;

        ProjectileHero projectile = go.GetComponent<ProjectileHero>();
        if (projectile != null)
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            Debug.LogWarning("Shield trigger hit by non-enemy" + go.name);
        }
    }
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        private set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.HERO_DIED();
            }
        }
    }
}
