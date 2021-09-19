using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public bool attacking;
    public Animator animator;
   
    public GameObject hitbox;
    
    public GameObject hitboxSpawn;
    
    public PlayerMovementScript movement;
    public PlayerMovementScript IsGrounded;
    public GameObject spawnedBox;
    public AudioSource whack;

    public int playerDamage;
    public float timer = 0;
    public float attackSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerDamage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        attack();

    }

    void attack()
    {

        //         Debug.Log(timer);



        if (timer >= attackSpeed)
        {

            if (Input.GetKeyDown(KeyCode.C) && IsGrounded)
            {
                //Attacking here when x is pressed
                attacking = true;
                animator.SetBool("attacking", true);
                
               
                
                    Invoke("spawnHitBox", 1f);//this will happen after 2 seconds


                timer = attackSpeed;
                whack.Play();

            }

        }

        if (attacking == true)
        {
            movement.enabled = false;

        }
        else
        {
            movement.enabled = true;
            animator.SetBool("attacking", false);
         
        }

    }

    void spawnHitBox()
    {
      
            Invoke("attackReset", .5f);
            spawnedBox = Instantiate(hitbox, hitboxSpawn.transform.position, Quaternion.identity);
            Destroy(spawnedBox, .5f);
        
    }

    void attackReset()
    {
        timer = 0;
        attacking = false;
    }


}
