using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;
    private bool isPlaying;
    LevelSelector levelSelector;
    //private AudioSource menuMusic;

    // Update is called once per frame
    private void Start()
    {
        isPlaying = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseResume();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                PauseResume();
            }
        }
    }

    public void TogglePanel()
    {
        if (!videoPanel.activeInHierarchy)
        {
            videoPanel.SetActive(true);
            LevelSelector.menuAudio.Pause();
        }
        else
        {
            videoPanel.SetActive(false);
            LevelSelector.menuAudio.Play();
        }
    }

    private void PauseResume()
    {
        if (isPlaying)
        {
            Debug.Log("Pause");
            videoPlayer.Pause();
            isPlaying = !isPlaying;
        }
        else
        {
            Debug.Log("Play");
            videoPlayer.Play();
            isPlaying = !isPlaying;
        }
    }
}
