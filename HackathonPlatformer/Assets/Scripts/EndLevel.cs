using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private AudioSource finishSFX;
    private Animator anim;
    private BoxCollider2D bc;


    // Start is called before the first frame update
    private void Start()
    {
        finishSFX = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && bc.enabled)
        {
            anim.SetTrigger("theEnd");
            finishSFX.Play();
            bc.enabled = false;
            Invoke("CompleteLevel", 3f);
        }
    }
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
