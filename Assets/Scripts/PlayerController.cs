using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables del movimiento
    public float jumpForce = 6f;
    Rigidbody2D rb;
    Animator animator;
    public float runningSpeed = 2f;
    float inputHorizontal;
    Vector3 startPosition;
    [SerializeField]
    private int manaPoints, healthPoints;

    public const int INITIAL_MANA = 15, INITIAL_HEALTH = 100, MAX_MANA = 30, MAX_HEALTH = 200, MIN_MANA = 0, MIN_HEALT = 10;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const int SUPERJUMP_COST = 5;
    const float SUPERJUMP_FORCE = 1.5f;

    public float jumpRaycastDistance = 1.5f;

    public LayerMask groundMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        Invoke("RestartPosition", 0.2f);
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.rb.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            Jump(false);
        }
        if (Input.GetButtonDown("Superjump"))
        {
            Jump(true);
        }

        animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround());

        //Debug.DrawRay(this.transform.position, Vector2.down*1.5f, Color.red);
    }

    private void FixedUpdate()
    {
        /*if(rb.velocity.x < runningSpeed)
        {
            rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
        }*/

        inputHorizontal = Input.GetAxis("Horizontal") * runningSpeed * Time.deltaTime;

        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (inputHorizontal != 0)
            {
                transform.Translate(inputHorizontal, 0, 0);
            }
            if (inputHorizontal > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (inputHorizontal < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void Jump(bool superJump)
    {
        float jumpForceFactor = jumpForce;
        if (superJump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (isTouchingTheGround())
            {
                GetComponent<AudioSource>().Play();
                rb.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }

    bool isTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, jumpRaycastDistance, groundMask))
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    public void Die()
    {
        float traveledDistance = GetTravelledDistance();
        float previusMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if(traveledDistance > previusMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", traveledDistance);
        }
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.instance.GameOver();
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if(healthPoints >= MAX_HEALTH)
        {
           this.healthPoints = MAX_HEALTH;
        }
        if(healthPoints <= 0)
        {
            Die();
        }
    }
    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
        if (manaPoints <= 0)
        {
            Die();
        }
    }
    public int GetHealth()
    {
        return healthPoints;
    }
    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "movingPlatform")
        {
            transform.parent = collision.transform;
        }
    }*/
}
