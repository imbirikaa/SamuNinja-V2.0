using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : MonoBehaviour
{
    [SerializeField]
    private Transform Player;

    PlayerMovement pl;

    void Start()
    {
        pl = Player.GetComponent<PlayerMovement>();

    }
    public void AssignPlayer(Transform playerTransform)
    {
        Player = playerTransform;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (pl != null)
            {

                pl.reFill();
                Destroy(gameObject);
            }
        }
    }
}
