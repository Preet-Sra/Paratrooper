using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TrooperController : MonoBehaviour
{
    [SerializeField] float gravity;
    float fallSpeed;
    [SerializeField] float groundpos;
    [SerializeField] bool onGround;
    bool usedParachute;
    GameObject parachute;
    bool parachuteDestroyed;
    bool SpawnRightSide;
    Animator anim;
    bool setpos = false;
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        onGround = false;
        anim = GetComponent<Animator>();
        parachute = transform.GetChild(0).gameObject;
        if (transform.position.x >= GameObject.FindGameObjectWithTag("Canon").transform.position.x)
        {
            SpawnRightSide = true;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            SpawnRightSide = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        gc = GameController.instance;
    }


    private void OnEnable()
    {
        parachute = transform.GetChild(0).gameObject;
        onGround = false;
        parachute.SetActive(false);
        fallSpeed = 0;
        usedParachute = false;
        parachuteDestroyed = false;
        if (transform.position.x >= GameObject.FindGameObjectWithTag("Canon").transform.position.x)
        {
            SpawnRightSide = true;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            SpawnRightSide = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGround)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= fallSpeed * Time.deltaTime;
            if(!usedParachute)
            fallSpeed += gravity * Time.deltaTime;
            if (newPosition.y < groundpos)
            {
                newPosition.y = groundpos;
                onGround = true;
                if (parachuteDestroyed)
                {
                    gameObject.SetActive(false);
                    GameObject blood = GameObject.FindGameObjectWithTag("Blood");
                    blood.transform.position = transform.position;
                    blood.GetComponent<ParticleSystem>().Play();
                    gc.IncreaseScore(1);
                    SoundManager.instance.PlayKillSound();
                }
                else
                {
                    GetComponent<PolygonCollider2D>().enabled = false;
                    gc.IncreaseTrooper(SpawnRightSide);
                    
                }

            }
            transform.position = newPosition;

            if(!usedParachute&& fallSpeed>3.5f &&!parachuteDestroyed)
            {
                SpawParachute();
                fallSpeed = 1f;
                usedParachute = true;
            }
        }
        else
        {
            parachute.SetActive(false);
            if (SpawnRightSide)
            {
                int spawnNumber =gc.TropersInRightSide;
                if (spawnNumber < 4 && !setpos)
                {
                    setpos = true;
                    GameObject cannon = GameObject.FindGameObjectWithTag("Canon");
                    anim.SetBool("Walking", true);
                    switch (spawnNumber)
                    {
                        case 1:
                            transform.DOMove(new Vector3(cannon.transform.position.x + 1f, transform.position.y, transform.position.z),3).OnComplete(() => anim.SetBool("Walking", false));
                            break;

                       case 2:
                            transform.DOMove(new Vector3(cannon.transform.position.x + 1.38f, transform.position.y, transform.position.z), 3).OnComplete(() => anim.SetBool("Walking", false));
                            break;
                        case 3:
                            transform.DOMove(new Vector3(cannon.transform.position.x + 1f, transform.position.y, transform.position.z), 3).OnComplete(()=> 
                            {
                                transform.DOMove(new Vector3(cannon.transform.position.x + 1f, transform.position.y + 1, transform.position.z), 0.3f).OnComplete(() => 
                                {
                                    anim.SetBool("Walking", false);
                                    anim.SetTrigger("Attack");
                                    GameController.instance.GameEnd();
                                });
                            });
                            break;


                    }
                }
                else
                {
                    this.enabled = false;
                }
            }
            else
            {

                int spawnNumber = gc.TrooperInLeftSide;
                if (spawnNumber < 4 && !setpos)
                {
                    setpos = true;
                    GameObject cannon = GameObject.FindGameObjectWithTag("Canon");
                    anim.SetBool("Walking", true);
                    switch (spawnNumber)
                    {
                        
                        case 1:
                            transform.DOMove(new Vector3(cannon.transform.position.x - 1f, transform.position.y, transform.position.z), 3).OnComplete(()=> anim.SetBool("Walking",false));
                            break;

                        case 2:
                            transform.DOMove(new Vector3(cannon.transform.position.x - 1.38f, transform.position.y, transform.position.z), 3).OnComplete(() => anim.SetBool("Walking", false));
                            break;
                        case 3:
                            transform.DOMove(new Vector3(cannon.transform.position.x - 1f, transform.position.y, transform.position.z), 3).OnComplete(() =>
                            {
                                transform.DOMove(new Vector3(cannon.transform.position.x - 1f, transform.position.y + 1, transform.position.z), 0.3f).OnComplete(() =>
                                {
                                    anim.SetBool("Walking", false);
                                    anim.SetTrigger("Attack");
                                    GameController.instance.GameEnd();
                                });

                            });
                            break;


                    }
                }
                else
                {
                    this.enabled = false;
                }
            }

        }
    }


    void SpawParachute()
    {
        parachute.SetActive(true);
    }

    public void KillTrooper()
    {
        gameObject.SetActive(false);
        parachute.SetActive(false);
        GameObject blood = GameObject.FindGameObjectWithTag("Blood");
        blood.transform.position = transform.position;
        blood.GetComponent<ParticleSystem>().Play();
        GameController.instance.IncreaseScore(1);
    }

    public void RemoveParachute()
    {
        parachute.SetActive(false);
        usedParachute = false;
        parachuteDestroyed = true;
        fallSpeed = 3f;
    }
}
