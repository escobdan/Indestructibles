using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Turret details")]
    public TurretBlueprint button1;
    public TurretBlueprint button2;
    public TurretBlueprint button3;
    public TurretBlueprint button4;
    public TurretBlueprint button5;
    public TurretBlueprint button6;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectButton1()
    {
        Debug.Log("Button 1 Selected, " + button1.prefab.name);
        buildManager.SelectTurretToBuild(button1);
    }

    public void SelectButton2()
    {
        Debug.Log("Button 2 Selected, " + button2.prefab.name);
        buildManager.SelectTurretToBuild(button2);
    }

    public void SelectButton3()
    {
        Debug.Log("Button 3 Selected, " + button3.prefab.name);
        buildManager.SelectTurretToBuild(button3);
    }

    public void SelectButton4()
    {
        Debug.Log("Button 4 Selected, " + button4.prefab.name);
        buildManager.SelectTurretToBuild(button4);
    }

    public void SelectButton5()
    {
        Debug.Log("Button 5 Selected, " + button5.prefab.name);
        buildManager.SelectTurretToBuild(button5);
    }
    public void SelectButton6()
    {
        Debug.Log("Button 6 Selected, " + button6.prefab.name);
        buildManager.SelectTurretToBuild(button6);
    }
}
