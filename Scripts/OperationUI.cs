using UnityEngine;
using UnityEngine.UI;

public class OperationUI : MonoBehaviour
{
    public GameObject gameMaster;
    WaveSpawner waveSpawner;
    public Text operationText;
    public float answerVar;
    public Node node;

    private void Start()
    {
        waveSpawner = gameMaster.GetComponent<WaveSpawner>();
        answerVar = waveSpawner.waves[0].enemy.GetComponent<EnemyMovement>().enemyVar;

        switch (waveSpawner.waves[0].operation)
        {
            case Wave.op.None:
            operationText.text = "S/O\n? + ? + ?";
            break;
            case Wave.op.Suma:
            operationText.text = "Suma\n? + ? = " + answerVar;
            break;
            case Wave.op.Resta:
            operationText.text = "Resta\n? - ? = " + answerVar;
            break;
            case Wave.op.Multiplicaci�n:
            operationText.text = "Multiplicaci�n\n? x ? = " + answerVar;
            break;
            case Wave.op.Divisi�n:
            operationText.text = "Divisi�n\n? � ? = " + answerVar;
            break;
            default:
            operationText.text = "S/O\n? + ? + ?";
            break;
        }
    }
}
