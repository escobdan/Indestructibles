using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{

    public Text livesText;

    // Update is called once per frame
    void Update()
    {
            livesText.text = " VIDAS\n" + PlayerStats.Lives;
        //OPTIMIZATION OPTION, DO IN CORROUTINE OR LINK TO PLAYER STATS
        
    }
}
