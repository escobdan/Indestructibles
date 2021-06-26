using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool GameIsOver;

    public GameObject gameOverUI;

    public GameObject completeLevelUI;

    private void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;

        //TESTING END GAME
        //if(Input.GetKeyDown("e"))
        //{
        //    EndGame();
        //}

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);

    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
