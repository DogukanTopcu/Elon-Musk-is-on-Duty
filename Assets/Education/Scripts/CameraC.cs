using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    private GameObject target;
    private float lateTime;

    private Manager manager;

    void Start()
    {
        target = GameObject.Find("rocket");
        lateTime = 0.2f;

        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    void Update()
    {
        if (!manager.isGameOver)
        {
            float y = target.GetComponent<Transform>().position.y;
            this.GetComponent<Transform>().position = 
                Vector3.Lerp(
                    this.transform.position, 
                    new Vector3(0, y, -10), 
                    lateTime);    
        }
    }
}
