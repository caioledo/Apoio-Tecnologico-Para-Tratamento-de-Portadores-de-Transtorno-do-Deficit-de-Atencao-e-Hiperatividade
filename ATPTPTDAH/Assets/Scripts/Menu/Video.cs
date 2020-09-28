using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer video;
    private float timer;
    private bool segundoJogo;
    private string url;

    private void Start()
    {
        segundoJogo = GameController.segundoJogo;

        if (!(Menu.tempoVideo > 0) || segundoJogo)
        {
            SceneManager.LoadScene("Questionario");
        }

        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        
        timer = (Menu.tempoVideo) * 5;
        url = Menu.videoURL;
        video.url = url;

        video.Play();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene("Questionario");
        }
    }
}
