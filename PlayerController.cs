using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int speed;
    private int count;
    public Text countText;
    public Text winText;
    private int pickupCount = 0;
    private Transform pickups2;
    private Transform enemies2;
    private Transform barrier1;
    private Transform barrier2;
    private int lives = 3;
    public Text livesText;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pickups2 = GameObject.Find("Pickup2").GetComponent<Transform>();
        enemies2 = GameObject.Find("Enemies2").GetComponent<Transform>();
        barrier1 = GameObject.Find("Barrier").GetComponent<Transform>();
        barrier2 = GameObject.Find("Barrier1").GetComponent<Transform>();
        pickups2.gameObject.SetActive(false);
        enemies2.gameObject.SetActive(false);
        count = 0;
        winText.text = "";
        SetCountText(false);
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }
    private void Update()
    {
        if (lives < 1)
        {
            Gameover(false);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false);
            count++;
            SetCountText(true);
        }else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            if (count > 0)
            {
                count--;
            }
            lives--;
            livesText.text = "Lives: " + lives.ToString();
            SetCountText(false);
        }
    }
    void SetCountText (bool pickup)
    {
        countText.text = "Score: " + count.ToString();
        if(pickup == true)
        {
            pickupCount++;
        }
        if (pickupCount == 16)
        {
            rb2d.velocity = Vector2.zero;
            transform.position = Vector3.zero;
            pickups2.gameObject.SetActive(true);
            enemies2.gameObject.SetActive(true);
            barrier1.gameObject.SetActive(false);
            barrier2.gameObject.SetActive(false);
        }
        if (pickupCount == 28)
        {
            Gameover(true);
        }
    }
    void Gameover (bool something)
    {

        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
        if (something == true)
        {
            winText.text = "Good job, you got all the gems! Alex Grant";
        }
        else if (something == false)
        {
            winText.text = "Sad, you lost. Try again!";
            Destroy(this.gameObject);
        }
    }
}
