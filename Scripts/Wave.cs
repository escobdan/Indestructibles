using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int count;
    public float rate;
    public enum op
    {
        None,
        Suma,
        Resta,
        Multiplicación,
        División
    };
    public op operation;
    public float turretVariable;
}
