using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;
    

    private void Start()
    {
        //necessary if you decide to reload the scene, the static int value will carry over unless we restart like this
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }

}
