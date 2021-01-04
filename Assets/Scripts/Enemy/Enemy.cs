using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseHealth;
    public float baseSpeed;

    private Transform target;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public virtual void Attack()
    { 
    }

    public virtual void Move()
    {

    }

    public void Spawn(int area)
    {
        target = GameManager.instance.initialEnemyTargets[area];
    }
}
