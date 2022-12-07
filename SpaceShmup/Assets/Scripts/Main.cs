using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S; //singleton
    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemtInsetDefault = 1.5f;
    public float gameRestartDelay = 2;
    private BoundsCheck bndCheck;
    private BoundsCheck_2 bndCheck_2;

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        bndCheck_2 = GetComponent<BoundsCheck_2>();
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }
 
    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        float enemyInset = enemtInsetDefault;
        
        if (go.GetComponent<BoundsCheck>()!= null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float yMin = -bndCheck.camWidth + enemyInset;
        float yMax = bndCheck.camWidth - enemyInset;
        pos.x = bndCheck.camHeight + enemyInset;
        pos.y = Random.Range(yMin, yMax);

        go.transform.position = pos;
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }
    
    void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }
    void Restart()
    {
        SceneManager.LoadScene("__Scene_1");
    }
    
    static public void HERO_DIED()
    {
        S.DelayedRestart();
    }

}
