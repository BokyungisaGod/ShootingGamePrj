using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public ObjectManager objectManager;

    //public Vector3 followPos;
    //public Transform parent;

    //void Update()
    //{

    //}
    public GameObject bar;
    public float health;
    public float maxHealth;

    public void SetHealth(float _health, float _maxHealth)
    {
        health = _health;
        maxHealth = _maxHealth;
        float healthPercentage = (float)health / maxHealth;
        bar.transform.localScale = new Vector3(healthPercentage, 1f, 1f);
    }
}