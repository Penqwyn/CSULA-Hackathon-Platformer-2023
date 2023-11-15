using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource BGSFX;
    public void StartGame()
    {
        GetComponent<AudioSource>().Play();
        BGSFX.Stop();
        Invoke("Starting", 1f);
    }
    private void Starting()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
