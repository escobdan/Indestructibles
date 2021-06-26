using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 15f;
    private float countdown = 30f;

    public Text waveCountdownText;

    public GameManager gameManager;

    public static GameObject levelsMusic;
    private AudioSource levelsAudioSource;

    //Operation text stuff
    public Text OperationUI;
    public static float answerVar;
    private float a;
    private float b;
    private bool hasUpdated;

    //wave number
    private int waveIndex = 0;

    private void Start()
    {
        EnemiesAlive = 0;
        Turret.correctAnswer = false;
        Turret.turretPlaced = false;
        levelsMusic = GameObject.Find("LevelsAudio");
        levelsAudioSource = levelsMusic.GetComponent<AudioSource>();
    }
    private void Update()
    {
        //this makes it false every frame when in action fase (spawning enemies)
        //added after &&

        if(hasUpdated && EnemiesAlive == 0)
        {
            Turret.correctAnswer = false;
            changeOperationText();
            hasUpdated = false;
        }

        if (Turret.turretPlaced && EnemiesAlive == 0)
        {
            changeOperationText();
            Turret.turretPlaced = false;
        }
        
        //problem here is that it updates before the next wave comes
        //changeOperationText();

        if (EnemiesAlive > 0)
        {
            //added this
            //hasUpdated = true;
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
            return;
        }

        if (countdown <= 0f)
        {
            //start spawning enemies
            hasUpdated = true;
            changeOperationText();
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        changeOperationText();

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("TIEMPO {0:00.00}", countdown);

        // old way to count down
        //waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    public void changeOperationText()
    {
        if(waves.Length != waveIndex)
        {
            answerVar = waves[waveIndex].enemy.GetComponent<EnemyMovement>().enemyVar;
            a = waves[waveIndex].turretVariable;

            switch (waves[waveIndex].operation)
            {
                case Wave.op.None:
                OperationUI.text = waves[waveIndex].operation + "???\n ? + ? = ?";
                break;
                case Wave.op.Suma:
                b = answerVar - waves[waveIndex].turretVariable;
                if (Turret.correctAnswer)
                    OperationUI.text = waves[waveIndex].operation + "\n" + a + " + " + b + " = " + answerVar;
                else
                    OperationUI.text = waves[waveIndex].operation + "\n" + a + " + ? = " + answerVar;
                break;
                case Wave.op.Resta:
                b = waves[waveIndex].turretVariable - answerVar;
                if (Turret.correctAnswer)
                    OperationUI.text = waves[waveIndex].operation + "\n" + a + " - " + b + " = " + answerVar;
                else
                    OperationUI.text = waves[waveIndex].operation + "\n " + a + "  - ? = " + answerVar;
                break;
                case Wave.op.Multiplicación:
                b = answerVar / waves[waveIndex].turretVariable;
                if (Turret.correctAnswer)
                    OperationUI.text = waves[waveIndex].operation + "\n" + a + " x " + b + " = " + answerVar;
                else
                    OperationUI.text = waves[waveIndex].operation + "\n " + a + "  x ? = " + answerVar;
                break;
                case Wave.op.División:
                b = waves[waveIndex].turretVariable / answerVar;
                if (Turret.correctAnswer)
                    OperationUI.text = waves[waveIndex].operation + "\n" + a + " ÷ " + b + " = " + answerVar;
                else
                    OperationUI.text = waves[waveIndex].operation + "\n " + a + "  ÷ ? = " + answerVar;
                break;
                default:
                break;
            }
        }
        
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        //to make array of waves with enemy type and #of enemies, timer
        //numOfEnemies = waves[waveNumber].count;

        Debug.Log("waves spawned");
        if (Turret.turretPlaced)
        {
            changeOperationText();
            Turret.turretPlaced = false;
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public void ToggleMute()
    {
        if (levelsAudioSource.mute)
            levelsAudioSource.mute = false;
        else
            levelsAudioSource.mute = true;
    }
}
