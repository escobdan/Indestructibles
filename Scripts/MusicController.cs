using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource music;
    public static bool isMuted;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void MuteToggle()
    {
        if(!music.mute)
        {
            music.mute = true;
            isMuted = true;
            //isPlaying = false;
        }
        else
        {
            music.mute = false;
            isMuted = false;
            //isPlaying = true;
        }
    }
}
