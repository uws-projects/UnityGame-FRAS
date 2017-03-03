using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public List<AudioSource> Songs = new List<AudioSource>();
    private AudioSource current;
    private int playing = 0;

    public static GameObject bgm;

    private void Awake()
    {
        if (bgm)
        {
            Destroy(gameObject);
            return;
        }

        bgm = gameObject;
        swap();
    }

    public void swap()
    {
        if (Songs[playing].isPlaying)
        {
            Songs[playing].Stop();
        }
        playing++;
        if (playing == Songs.Count)
        {
            playing = 0;
        }

        current = Songs[playing];
        current.volume = 1;
        current.Play();
    }

    public void Update()
    {
        if (!current.isPlaying)
        {
            swap();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            swap();
        }
        if (Input.GetKey(KeyCode.F5) && current.volume > 0)
        {
            current.volume -= 0.004f;
        }
        if (Input.GetKey(KeyCode.F6) && current.volume < 1)
        {
            current.volume += 0.004f;
        }
    }
}
