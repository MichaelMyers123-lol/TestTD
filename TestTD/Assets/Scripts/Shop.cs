using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBP StandardTurret;
    public TurretBP SlowingTurret;
    public Text StandardTurretPrice;
    public Text SlowingTurretPrice;

    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
        StandardTurretPrice.text = StandardTurret.price + " G";
        SlowingTurretPrice.text = SlowingTurret.price + " G";
    }
public void SelectStandardTurret()
    {
        Debug.Log("PurchaseStandardTurret");
        buildManager.SelectTurretToBuild(StandardTurret);
    }
    public void SelectSlowingTurret()
    {
        Debug.Log("PurchaseSlowingTurret");
        buildManager.SelectTurretToBuild(SlowingTurret);
    }
}
