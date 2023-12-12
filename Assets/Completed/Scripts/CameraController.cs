using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject target;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.Find("rocket");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            float y = target.transform.position.y;
            this.GetComponent<Transform>().position = Vector3.Lerp(this.transform.position, new Vector3(0, y, -10), 0.2f);
        }
    }
}
