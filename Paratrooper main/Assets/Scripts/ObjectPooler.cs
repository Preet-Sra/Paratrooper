using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    [SerializeField] GameObject BulletObj;
    [SerializeField] int BulletPooledAmt;
    [SerializeField] GameObject PooledBulletHolder;
    List<GameObject> PooledBullet = new List<GameObject>();
    [SerializeField] bool ExpandableBullets;


    [SerializeField] GameObject TrooperObj;
    [SerializeField] int TrooperPooledAmt;
    [SerializeField] GameObject PooledTrooperHolder;
    List<GameObject> PooledTrooper = new List<GameObject>();
    [SerializeField] bool ExpandableTrooper;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < BulletPooledAmt; i++)
        {
            GameObject obj = Instantiate(BulletObj);
            obj.SetActive(false);
            obj.transform.parent = PooledBulletHolder.transform;
            PooledBullet.Add(obj);
        }

        for (int i = 0; i < TrooperPooledAmt; i++)
        {
            GameObject obj = Instantiate(TrooperObj);
            obj.SetActive(false);
            obj.transform.parent = PooledTrooperHolder.transform;
            PooledTrooper.Add(obj);
        }
    }

    public GameObject GetPooledBullet()
    {
        for(int i = 0; i < PooledBullet.Count; i++)
        {
            if (!PooledBullet[i].activeInHierarchy)
            {
                return PooledBullet[i];
            }
        }
        if (ExpandableBullets)
        {
            GameObject obj = Instantiate(BulletObj);
            obj.transform.parent = PooledBulletHolder.transform;
            PooledBullet.Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject GetPooledTrooper()
    {
        for (int i = 0; i < PooledTrooper.Count; i++)
        {
            if (!PooledTrooper[i].activeInHierarchy)
            {
                return PooledTrooper[i];
            }
        }
        if (ExpandableTrooper)
        {
            GameObject obj = Instantiate(TrooperObj);
            obj.transform.parent = PooledTrooperHolder.transform;
            PooledTrooper.Add(obj);
            return obj;
        }

        return null;
    }

}
