using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color NotEnoughMoneyColor;
    public Vector3 posOffSet;
    [HideInInspector]
    public GameObject turret;

    private Color defaultColor;
    private Renderer Rend;
    BuildManager buildManager;

    void Start()
    {
        Rend = GetComponent<Renderer>();
        defaultColor = Rend.material.color;
        buildManager = BuildManager.instance;
        if (turret != null)
        {
            buildManager.BuildTurretOn(this);
        }
    }
    public Vector3 GetBuildPos()
    {
        return transform.position + posOffSet;
    }

 void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!buildManager.CanBuild) return;
        if (buildManager.HasMoney)
        {
            Rend.material.color = hoverColor;
        }
        else
        {
            Rend.material.color = NotEnoughMoneyColor;
        }
    
            
       
        
    }
    void OnMouseExit()
    {
        Rend.material.color = defaultColor;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if(!buildManager.CanBuild)
        {
            return;
        }
        if( turret != null)
        {
            //Debug.Log("Node is occupied");
            return;
        }

        //Building a turret
        buildManager.BuildTurretOn(this);
    }
}
