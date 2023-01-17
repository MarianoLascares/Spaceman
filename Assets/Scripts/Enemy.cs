using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    public int enemyDamage = 20;
    private Rigidbody2D rb;
    public bool facingRigth = false;
    
    //private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if (facingRigth)
        {
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if(GameManager.instance.currentGameState == GameState.inGame)
        {
            rb.velocity = new Vector2(currentRunningSpeed, rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }
        if(collision.tag == "Rock")
        {
            facingRigth = !facingRigth;
        }
    }
}
