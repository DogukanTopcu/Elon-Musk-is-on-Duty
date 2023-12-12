using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float hor;
    private float vert;
    private int speed;
    private int jumpSpeed;
    private Vector2 lastPosition;

    private GameManager gameManager;
    private Rigidbody2D rb;

    private GameObject LosePanel;
    private GameObject WinPanel;

    void Start()
    {
        hor = 0;
        vert = 0;
        speed = 40;
        jumpSpeed = 100;
        lastPosition = Vector2.zero;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        LosePanel = gameManager.FindInActiveObjectByName("LosePanel");
        WinPanel = gameManager.FindInActiveObjectByName("WinPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            hor = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");

            lastPosition = new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
        }
        else
        {
            rb.gravityScale = 0;
            this.GetComponent<Transform>().position = lastPosition;
        }
    }

    private void FixedUpdate()
    {
        if (hor > 0.1f || hor < -0.1f)
        {
            float x = hor * speed * Time.deltaTime;
            rb.AddForce(new Vector2(x, 0), ForceMode2D.Impulse);
        }


        if (vert > 0.1f) 
        {
            float y = vert * jumpSpeed * Time.deltaTime;
            rb.AddForce(new Vector2(0, y), ForceMode2D.Impulse);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            LosePanel.SetActive(true);
            gameManager.isGameOver = true;
        }
        else if (collision.gameObject.CompareTag("Star"))
        {
            Destroy(collision.gameObject);
            gameManager.totalScore += 10;
        }
        else if (collision.gameObject.CompareTag("Satellite"))
        {
            Destroy(collision.gameObject);
            gameManager.isTakeSatellite = true;
        }
        else if (collision.gameObject.CompareTag("Ground") && gameManager.isTakeSatellite)
        {
            string layerName = LayerMask.LayerToName(collision.gameObject.layer);
            gameManager.totalScore += int.Parse(layerName);
            WinPanel.SetActive(true);
            gameManager.isGameOver = true;
        }
    }

}
