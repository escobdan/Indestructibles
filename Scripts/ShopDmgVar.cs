using UnityEngine;
using UnityEngine.UI;

public class ShopDmgVar : MonoBehaviour    
{

    public StoreButton[] storeButtons;

    void Start()
    {
        for (int i = 0; i < storeButtons.Length; i++)
        {
            float _dmgVar = storeButtons[i].turret.turretVar;
            storeButtons[i].dmgVar = _dmgVar;
            storeButtons[i].dmgVarText.text = _dmgVar.ToString();
            //to change price automatically add here
        }
        //turretComp = turret.GetComponent<Turret>();
    }
}
