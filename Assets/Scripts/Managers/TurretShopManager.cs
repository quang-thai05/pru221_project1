using Assets.Scripts.SaveLoad;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
    [SerializeField] private GameObject turretCardPrefab;
    [SerializeField] private Transform turretPanelContainer;

    [Header("Turret Settings")]
    [SerializeField] private TurretSettings[] turrets;

    private Node _currentNodeSelected;

    private void Start()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            CreateTurretCard(turrets[i]);
        }
        try
        {
            if (PropertiesApplication.RequestLoadGame)
            {
                List<TurretProperties> turretProperties = PropertiesApplication.turretProperties;
                foreach (var turet in turretProperties)
                {
                    GameObject.FindGameObjectWithTag("node" + turet.Node);
                    TurretSettings turret = turrets.ToList().Find(t => t.TurretPrefab.name.Trim().Equals(turet.TypeTurret.Trim()));
                    GameObject turretInstance = Instantiate(turret.TurretPrefab);
                    Node node = GameObject.FindGameObjectWithTag("node" + turet.Node).GetComponent<Node>();

                    turretInstance.transform.localPosition = node.transform.position;
                    turretInstance.transform.parent = node.transform;

                    Turret turretPlaced = turretInstance.GetComponent<Turret>();
                    TurretUpgrade turretUpgrade = turretPlaced.GetComponent<TurretUpgrade>();

                    turretUpgrade.InitDamage = turet.Damage;
                    turretUpgrade.InitDelayShot = turet.DelayPerShot;
                    turretUpgrade.Level = turet.Level;
                    turretUpgrade.SellPerc = turet.SellPerc;
                    turretUpgrade.UpgradeCost = turet.UpgradeCost;

                    turretPlaced.NameTypePrefab = turret.TurretPrefab.name;
                    node.SetTurret(turretPlaced);
                }
            }
        }catch(System.Exception ex)
        {
         Debug.Log("an error happened while load game : error "+ex.Message);
        }
        finally
        {
            PropertiesApplication.RequestLoadGame = false;
        }
    }

    private void CreateTurretCard(TurretSettings turretSettings)
    {
        GameObject newInstance = Instantiate(turretCardPrefab, turretPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(turretPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TurretCard cardButton = newInstance.GetComponent<TurretCard>();
        cardButton.SetupTurretButton(turretSettings);
    }
    
    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
    }
    
    private void PlaceTurret(TurretSettings turretLoaded)
    {
        if (_currentNodeSelected != null)
        {
            GameObject turretInstance = Instantiate(turretLoaded.TurretPrefab);
            turretInstance.transform.localPosition = _currentNodeSelected.transform.position;
            turretInstance.transform.parent = _currentNodeSelected.transform;

            Turret turretPlaced = turretInstance.GetComponent<Turret>();
            turretPlaced.NameTypePrefab = turretLoaded.TurretPrefab.name;
            _currentNodeSelected.SetTurret(turretPlaced);
        }
    }

    private void TurretSold()
    {
        _currentNodeSelected = null;
    }
    
    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
        Node.OnTurretSold += TurretSold;
        TurretCard.OnPlaceTurret += PlaceTurret;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
        Node.OnTurretSold -= TurretSold;
        TurretCard.OnPlaceTurret -= PlaceTurret;
    }
}
