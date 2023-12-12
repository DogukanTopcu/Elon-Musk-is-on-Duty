using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private float hor;
    private float vert;
    private int speed;
    private int acceleration;

    private Rigidbody2D rb;

    private Manager manager;
    private GameObject LosePanel;
    private GameObject WinPanel;


    void Awake()
    {
        hor = 0;
        vert = 0;   
        speed = 40;
        acceleration = 100;

        rb = this.GetComponent<Rigidbody2D>();

        manager = GameObject.Find("GameManager").GetComponent<Manager>();

        LosePanel = manager.FindInActiveObjectByName("LosePanel");
        WinPanel = manager.FindInActiveObjectByName("WinPanel");
    }

    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        if (!manager.isGameOver)
        {
            if (hor > 0.1f || hor < -0.1f)
            {
                float x = hor * speed * Time.deltaTime;
                rb.AddForce(new Vector2(x, 0), ForceMode2D.Impulse);
            }
            if (vert > 0.1f)
            {
                float y = vert * acceleration * Time.deltaTime;
                rb.AddForce(new Vector2(0, y), ForceMode2D.Impulse);
            }
        }
        else
        {
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            Destroy(collision.gameObject);
            manager.totalScore += 10;
        }
        else if (collision.gameObject.CompareTag("Satellite"))
        {
            Destroy(collision.gameObject);
            manager.isTakeSatellite = true;
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            LosePanel.SetActive(true);
            manager.isGameOver = true;
        }
        else if(collision.gameObject.CompareTag("Ground") && manager.isTakeSatellite)
        {
            string point = LayerMask.LayerToName(collision.gameObject.layer);
            manager.totalScore += int.Parse(point);

            WinPanel.SetActive(true);
            // manager.isGameOver = true;
        }
    }


}
