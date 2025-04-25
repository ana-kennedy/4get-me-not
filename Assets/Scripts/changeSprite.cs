using UnityEngine;
using System.Collections;

public class ChangeSprite : MonoBehaviour
{
    public Sprite sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10;
    private SpriteRenderer spriteRenderer;
    public bool isLeft;
    public bool isJumping;
    public bool gButton;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
            GetComponent<SpriteRenderer>().sprite = sp3;
            if (gButton && isLeft == true)
            {
                GetComponent<SpriteRenderer>().sprite = sp6;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isLeft = false;
            GetComponent<SpriteRenderer>().sprite = sp2;
            if (gButton == true)
            {
                GetComponent<SpriteRenderer>().sprite = sp7;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            GetComponent<SpriteRenderer>().sprite = sp4;
        }
        if (isLeft && isJumping == true){
            GetComponent<SpriteRenderer>().sprite = sp5;
        }
        if (gButton && isLeft && isJumping == true)
        {
            GetComponent<SpriteRenderer>().sprite = sp8;
        }
        if (gButton == true && isLeft == false && isJumping == true)
        {
          GetComponent<SpriteRenderer>().sprite = sp9;  
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            gButton = !gButton;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            GetComponent<SpriteRenderer>().sprite = sp1;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping == false && isLeft == true)
            {
                GetComponent<SpriteRenderer>().sprite = sp10;
            }
        }
    }
}

