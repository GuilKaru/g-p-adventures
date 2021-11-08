using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement Variables
    private Rigidbody2D playerRb;
    [SerializeField]
    private float speed;
   
    private Vector2 lastMoveDir;
    private Vector2 moveDirection;

    //Dash Variables
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;
    bool isDashing = false;

    //Sprite Renderer Variables
    private SpriteRenderer playerSprite;
    
    private void Start()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInputs()
    {
        
        //take the input methods
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash());
            }

            if ((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0)
            {
                lastMoveDir = moveDirection;
            }

            moveDirection = new Vector2(moveX, moveY).normalized; //normalize for diagonals
        }
    }

    private void MovePlayer()
    {
        if (!isDashing)
        {
            playerRb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerRb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        //playerRb.AddForce(new Vector2(dashDistance * moveDirection.x, dashDistance * moveDirection.y), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Auchie");
        if (collision.gameObject.CompareTag("Layer0"))
        {
            Debug.Log("Layer0");
            playerSprite.sortingOrder = 1;
        } 
        else if (collision.gameObject.CompareTag("Layer1"))
        {
            Debug.Log("Layer1");
            playerSprite.sortingOrder = 2;
        } 
        else if (collision.gameObject.CompareTag("Layer2"))
        {
            playerSprite.sortingOrder = 3;
        }
    }
}
