using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;

    }

    //old way to spawn objects
    //public GameObject standardTurretPrefab;
    //public GameObject missileLauncherPrefab;

    public GameObject buildEffect;
    public GameObject sellEffect;

    //click on turret to place at start of the game
    //private void Start()
    //{
    //    turretToBuild = standartTurretPrefab;
    //}

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    //old way to place turret without sotre
    //public GameObject GetTurretToBuild()
    //{
    //    return turretToBuild;
    //}

    //check if there is turret placed already
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
