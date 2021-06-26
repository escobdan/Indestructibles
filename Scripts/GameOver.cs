using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    LevelSelector levelSelector;

    public void Retry()
    {
        //restart level OLD WAY
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        Destroy(WaveSpawner.levelsMusic);
    }

}
