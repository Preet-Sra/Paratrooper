using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]float offset;
    [SerializeField] float maxAngle, minAngle;
    [SerializeField] float BulletCooldownTimer;
    float delay;
    [SerializeField] Transform shotpos;


    private void Start()
    {
        delay = BulletCooldownTimer;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg - offset;
        transform.rotation =  Quaternion.AngleAxis(Mathf.Clamp(angle,minAngle,maxAngle) , Vector3.forward);


        if (delay >= BulletCooldownTimer)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
                delay = 0f;
            }
        }
        else
        {
            delay += Time.deltaTime;
        }

      
    }

    void Fire()
    {
        GameObject obj = ObjectPooler.instance.GetPooledBullet();
        if (obj == null) return;
        obj.transform.position = shotpos.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        SoundManager.instance.PlayShotSound();
    }
}
