
using Assets.Scripts.SaveLoad;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad 
{

    public static void LoadGame()
    {
        if (!File.Exists("data.json"))
        {
            return;
        }
        string jsonData = File.ReadAllText("data.json");
        Properties properties = JsonConvert.DeserializeObject<Properties>(jsonData);

        PropertiesApplication.turretProperties = properties.turretProperties;
        PropertiesApplication.enemyProperties = properties.enemyProperties;
        PropertiesApplication.currentWave = properties.currentWave;
        PropertiesApplication.TotalLives = properties.TotalLives;
        PropertiesApplication.TotalCoins = properties.TotalCoins;
        PropertiesApplication.RequestLoadGame = true;
        PropertiesApplication.RequestLoadEnemy = true;
        PropertiesApplication.enemiesSpawned = properties.enemiesSpawned;
        PropertiesApplication.enemiesRemain = properties.enemiesRemain;
    }
    // Start is called before the first frame update
   public static void SaveGame()
    {
        StreamWriter file = File.CreateText("data.json");
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        List<EnemyProperties> enemyProperties1 = new List<EnemyProperties>();
        List<TurretProperties> turretProperties = new List<TurretProperties>();

        foreach (GameObject gameObject in gameObjects)
        {
            EnemyProperties enemyProperties = new EnemyProperties();
            enemyProperties.NameType = gameObject.GetComponent<SpriteRenderer>().name;
            enemyProperties.posx = gameObject.transform.position.x;
            enemyProperties.posy = gameObject.transform.position.y;
            enemyProperties.healthCur = gameObject.GetComponent<EnemyHealth>().CurrentHealth;
            enemyProperties.CurrentWaypointIndex = gameObject.GetComponent<Enemy>().CurrentWaypointIndex();

            enemyProperties1.Add(enemyProperties);
        }

        for(int i = 1;i <= 6; i++)
        {

            Node node = GameObject.FindGameObjectWithTag("node"+i).GetComponent<Node>();
            if (node != null)
            {
                if(node.Turret != null)
                {
                    TurretProperties properties = new TurretProperties();
                    properties.TypeTurret = node.Turret.NameTypePrefab;
                    properties.Node = i;

                    TurretUpgrade upgrade = node.Turret.TurretUpgrade;
                    if (upgrade != null)
                    {
                        
                        TurretProjectile projectile = upgrade.GetComponent<TurretProjectile>();

                        properties.Damage = projectile.Damage;
                        properties.UpgradeCost = upgrade.UpgradeCost;
                        properties.SellPerc = upgrade.SellPerc;
                        properties.Level = upgrade.Level;
                        properties.DelayPerShot = projectile.DelayPerShot;

                    }
                    turretProperties.Add(properties);

                }
            }
        }
        Properties properties1 = new Properties();

        properties1.enemiesSpawned = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>().EnemySpawned();
        properties1.enemiesRemain = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>().EnemyRemain(); 
        properties1.TotalCoins = CurrencySystem.Instance.TotalCoins;
        properties1.currentWave = LevelManager.Instance.CurrentWave;
        properties1.turretProperties = turretProperties;
        properties1.enemyProperties = enemyProperties1;
        properties1.TotalLives = LevelManager.Instance.TotalLives;

        file.WriteLine(JsonConvert.SerializeObject(properties1));
        file.Close();

    }

}
public class Properties
{
    public List<EnemyProperties> enemyProperties { set; get; }
    public List<TurretProperties> turretProperties { set; get; }
    public int TotalCoins { set; get; }
    public int currentWave { set; get; }    
    public int TotalLives { set; get; }
    public int enemiesSpawned { set; get; }
    public int enemiesRemain { set; get; }
}

public class EnemyProperties
{
    public int CurrentWaypointIndex { get; set; }
    public string NameType { get; set; }
    public float posx { get; set; }
    public float posy { get; set; }
    public float healthCur { get; set; }

}
public class TurretProperties
{
    public int Node { get; set; }
    public string TypeTurret { get; set; }
    public float SellPerc { get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }
    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

}
