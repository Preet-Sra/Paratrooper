using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTrafficController : MonoBehaviour
{
    [SerializeField] GameObject HelicopterObj;
    [SerializeField] float minDelay, maxDelay;
    float launchTimer;

    [SerializeField] bool[] levelDirection;
    [SerializeField] int[] levelNum;
    [SerializeField] int maxOccupancy;
    [SerializeField] GameObject HeliHolder;

    // Start is called before the first frame update
    void Start()
    {
        levelDirection = new bool[4];
        levelNum = new int[4];
        levelNum[0] = 0;
        levelNum[1] = 0;
        levelNum[2] = 0;
        levelNum[3] = 0;

    }

    // Update is called once per frame
    void Update()
    {
        launchTimer -= Time.deltaTime;
        if (launchTimer < 0)
        {
            GameObject obj= Instantiate(HelicopterObj);
            obj.transform.parent = HeliHolder.transform;
            launchTimer = Random.Range(minDelay, maxDelay);
        }
    }


    public int GetAvailableLevel(bool movingRight)
    {
        int i = 0;
        while (i < 4)
        {
            if (levelNum[i] == 0)
            {
                levelDirection[i] = movingRight;
                levelNum[i]++;
                return i;
            }
            else
            {
                //level is same,not crowded
                if ((levelDirection[i] == movingRight) && (levelNum[i] < maxOccupancy))
                {
                    levelNum[i]++;
                    return i;
                }
            }
            i += 1;
        }
        return -1;//no levels
    }

    public void LeftLevel(int level)
    {
        levelNum[level]--;
    }
}
