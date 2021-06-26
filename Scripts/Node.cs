using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Node : MonoBehaviour
{
    public enum op
    { 
        None,
        Add,
        Subtract,
        Multiply,
        Divide
    };

    [Header("Math")]
    public float amountVar;
    public op operation;
    public TextMeshPro varText;
    private Color green = new Color(58f, 190f, 56f);
    OperationUI opUI;

    [Header("General")]
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    public Color32 cGreen = new Color32(17, 133, 48, 200);
    public Color32 cBlue = new Color32(29, 114, 153, 200);
    public Color32 cRed = new Color32(181, 29, 29, 200);
    public Color32 cPurple = new Color32(161, 35, 121, 200);

    Shop shop;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        switch(operation)
        {
            case op.None:
            rend.material.SetColor("_Color", rend.material.color);
            //varText.text = null;
            varText.enabled = false;
            break;
            case op.Add:
            rend.material.SetColor("_Color", cGreen);
            varText.enabled = true;
            varText.text = "+" + amountVar;
            //varText.color = Color.white;
            break;
            case op.Subtract:
            rend.material.SetColor("_Color", cRed);
            varText.enabled = true;
            varText.text = "-" + amountVar;
            break;
            case op.Multiply:
            rend.material.SetColor("_Color", cBlue);
            varText.enabled = true;
            varText.text = "x" + amountVar;
            break;
            case op.Divide:
            rend.material.SetColor("_Color", cPurple);
            varText.enabled = true;
            varText.text = "÷" + amountVar;
            break;
            default:
            rend.material.SetColor("_Color", rend.material.color);
            //varText.text = null;
            varText.enabled = false;
            break;
        }
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());

    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        
        PlayerStats.Money -= blueprint.cost;

        //opUI.UpdateOperationText();

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        Turret turretScript = turret.GetComponent<Turret>();

        turretScript.DoOperation(amountVar, operation);

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        varText.enabled = false;

        Debug.Log("Turret built!");

        //old way to Build a turret
        //GameObject turretToBuild = buildManager.GetTurretToBuild();
        //turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    //change or delete this for later, change it so you have an array of turrets with same prefab but change components or properties
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Get rid of old turret
        Destroy(turret);

        //Build a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        //math integration
        Turret turretScript = _turret.GetComponent<Turret>();
        turretScript.DoOperation(amountVar, operation);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        //Spawn an effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);

        varText.enabled = true;

        turretBlueprint = null;
        isUpgraded = false;
    }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
