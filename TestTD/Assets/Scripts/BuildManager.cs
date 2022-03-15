using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake ()
    {
        //На всякий случай проверка отстуствия на сцене нескольких билд мэнеджеров
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager!");
        }
        instance = this;
        
    }
    // Закидываем префабы турелек
    public GameObject standardTurretPrefab;
    public GameObject SlowTurretPrefab;
    public TurretBP turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool  HasMoney { get { return PlayerStats.Money >= turretToBuild.price; } }

    public void BuildTurretOn (Node node)
    {
       
        if (PlayerStats.Money < turretToBuild.price)
        {
            //Debug.Log("Not enough gold");
            return;
        }
        
        PlayerStats.Money -= turretToBuild.price;
        //Инициализурем выбранную турель в ноде с отклонением по y
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPos(), Quaternion.identity);
        node.turret = turret;
        //Debug.Log("Money left" + PlayerStats.Money);
    }

    public void SelectTurretToBuild (TurretBP turret)
    {
        turretToBuild = turret;
    }
}
