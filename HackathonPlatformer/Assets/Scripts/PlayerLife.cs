using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private int health = 2;
    [SerializeField] private float dmgForce = 14f;

    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private AudioSource hurtSFX;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (health > 0)
            {
                Damage(collision);
            }
            else 
            {
                Die();
            }
        }
    }
    private void Die()
    {
        deathSFX.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    private void Damage(Collision2D other) 
    {
        //enemy is to the right
        hurtSFX.Play();
        anim.SetInteger("state", 4);
        //Debug.Log("State:" + anim.GetInteger("state"));
        if (other.gameObject.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(-dmgForce, dmgForce/2);
            
        }
        //if enemy is to the left
        else
        {
            rb.velocity = new Vector2(dmgForce, dmgForce/2);
        }
        health--;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
