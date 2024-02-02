using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyVPrefab;
    public GameObject enemyHPrefab;

    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletFollowerPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
    public GameObject explosionPrefab;


    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;
    GameObject[] enemyB;
    GameObject[] enemyV;
    GameObject[] enemyH;


    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] explosion;

    GameObject[] targetPool;



    private void Awake()
    {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[10];
        enemyB = new GameObject[10];
        enemyV = new GameObject[10];
        enemyH = new GameObject[10];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];
        explosion= new GameObject[20];

        Generate();
    }

    void Generate() 
    {
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }
        for (int index = 0; index < enemyL.Length; index++) 
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
       
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }
        for (int index = 0; index < enemyV.Length; index++)
        {
            enemyV[index] = Instantiate(enemyVPrefab);
            enemyV[index].SetActive(false);
        }
        for (int index = 0; index < enemyH.Length; index++)
        {
            enemyH[index] = Instantiate(enemyHPrefab);
            enemyH[index].SetActive(false);
        }

        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }
        
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        
        for (int index = 0; index < enemyS.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }
        

        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }
        
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }
        
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
        
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }

        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }
        for (int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }


    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyV":
                targetPool = enemyV;
                break;
            case "EnemyH":
                targetPool = enemyH;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;
            case "explosion":
                targetPool = explosion;
                break;


        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyV":
                targetPool = enemyV;
                break;
            case "EnemyH":
                targetPool = enemyH;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;
            case "explosion":
                targetPool = explosion;
                break;

        }
        return targetPool;
    }
}
