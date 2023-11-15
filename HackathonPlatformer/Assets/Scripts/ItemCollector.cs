using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    private Animator anim;
    private BoxCollider2D bc;

    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource itemGrabbedSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bc = collision.gameObject.GetComponent<BoxCollider2D>();
        anim = collision.gameObject.GetComponent<Animator>();
        if (collision.gameObject.CompareTag("Cherry") && bc.enabled)
        {
            itemGrabbedSFX.Play();
            anim.SetTrigger("isGrabbed");
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
            bc.enabled = false;
        }
        else if(collision.gameObject.CompareTag("Cherry") && !bc.enabled)
        {
            Destroy(collision.gameObject, .5f);
        }
    }
}
