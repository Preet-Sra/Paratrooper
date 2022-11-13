using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliControl : MonoBehaviour
{
    [SerializeField] float Speed;
    bool RightMoving;
    int flightLevel;
    AirTrafficController ACControl;

    //Trooper
    [SerializeField] GameObject TrooperObj;
    bool SpawnedTrooper;
    float SpawnedPos;


    // Start is called before the first frame update
    void Start()
    {
        ACControl = FindObjectOfType<AirTrafficController>();
        Vector3 newPosition = new Vector3();
        newPosition = transform.position;

        if (Random.value < 0.5)
        {
            RightMoving = true;
            newPosition.x = -12f;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            RightMoving = false;
            newPosition.x = 12f;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        flightLevel = ACControl.GetAvailableLevel(RightMoving);
        if (flightLevel == -1) { gameObject.SetActive(false); }//no level,cancel flight
        newPosition.y = 4.2f - flightLevel * 0.85f;
        transform.position = newPosition;

        //SetTrooper
        SpawnedTrooper = true;
        SpawnedPos = Random.Range(-8f, 8f);

    }


  

    // Update is called once per frame
    void Update()
    {
        if (RightMoving)
        {
            transform.position += Vector3.right* Speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * -Speed * Time.deltaTime;
        }

        if (transform.position.x > 12f || transform.position.x < -12)
        {
            ACControl.LeftLevel(flightLevel);
            Destroy(gameObject);        
        }

        if (SpawnedTrooper)
        {
            if (RightMoving && (transform.position.x > SpawnedPos) || !RightMoving && (transform.position.x < SpawnedPos))
            {
                GameObject trooper = ObjectPooler.instance.GetPooledTrooper();
                trooper.transform.position = transform.position;
                trooper.SetActive(true);
                SpawnedTrooper = false;
            }
        }
    }


    public void Blast()
    {
        ACControl.LeftLevel(flightLevel);
        Destroy(gameObject);
    }
}
