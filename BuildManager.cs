using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    private void Awake()
    {

        if(instance != null) {

           // Debug.LogError("more than buildmanager");
            return;
        }
        instance = this;


    }

    public GameObject baseTurretPrefab;

    public GameObject heavyTurretPrefab;

    public GameObject slowTurretPrefab;

    private TurretBlueprint turretToBuild;

    private PlataformaBuildeable nodoseleccionado;

    public NodeUI nodeUI;

  public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.dinero >= turretToBuild.cost; } }
 
    public void SelectNode (PlataformaBuildeable nodo)
    {
        if (nodoseleccionado == nodo)
        {

            DeselectNode();
            return;
        }
        nodoseleccionado = nodo;
        turretToBuild = null;

        nodeUI.SetTarget(nodo);

    }

    

    public void DeselectNode()
    {

        nodoseleccionado = null;
        nodeUI.Hide();


    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {

        turretToBuild = turret;
        DeselectNode();
    }

    private void OnMouseDown(TurretBlueprint turret)
    {

        if (turret == null)
        {

         DeselectNode();

        }

    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }


}
