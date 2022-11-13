using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] GameObject Blastvfx;
   

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up *Speed*Time.deltaTime;

        Vector3 temp = transform.position;
        if (temp.x >= 10f || temp.x <= -10f || temp.y >= 10f || temp.y <= -10f)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Helicopter"))
        {
            HeliControl heli = collision.GetComponent<HeliControl>();
            heli.Blast();
            gameObject.SetActive(false);
            Blastvfx = GameObject.FindGameObjectWithTag("Blast");
            Blastvfx.transform.position = collision.transform.position;
            Blastvfx.GetComponent<ParticleSystem>().Play();
            SoundManager.instance.PlaySmallBlastSound();
        }

        if (collision.CompareTag("Trooper"))
        {
            TrooperController trooper = collision.GetComponent<TrooperController>();
            trooper.KillTrooper();
            gameObject.SetActive(false);
            SoundManager.instance.PlayKillSound();
        }
        if (collision.CompareTag("Parachute"))
        {
            TrooperController trooper = collision.transform.parent.GetComponent<TrooperController>();
            trooper.RemoveParachute();
            gameObject.SetActive(false);
            SoundManager.instance.PlayFallingSound();
        }
    }
}
