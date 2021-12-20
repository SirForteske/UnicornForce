using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class ShooterPlayer : MonoBehaviour
{
    public GameObject destructionFX;
    public int maxHP = 5;

    public static ShooterPlayer instance; 

    public int HP { get; private set; }

    private void Awake()
    {
        if (instance == null) 
            instance = this;

        HP = maxHP;
    }

    private void Update()
    {
        if(HP == 0)
        {
            Destruction();
        }
    }

    //method for damage processing by 'Player'
    public void Damage(int damage)
    {
        HP = Mathf.Max(0, HP - damage);
    }    

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }
}
















