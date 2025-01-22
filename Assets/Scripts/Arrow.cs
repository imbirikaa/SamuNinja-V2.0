using System;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float maxDistance = 2f;


    PlayerMovement pl;


    private Vector3 startPosition;

    
    void Start()
    {

        startPosition = transform.position;
    }

    
    void Update()
    {

        
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void FlipSprite()
    {

        Vector3 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Enemy1")
        {
            
            if (Mathf.Abs(transform.position.x - other.transform.position.x) < 0.8f)
            {

                
                EnemyControls enemy = other.GetComponent<EnemyControls>();
                if (enemy != null)
                {
                    enemy.death();
                }

                
                Destroy(gameObject);
            }
        }
    }
}
