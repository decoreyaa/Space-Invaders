using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float topBound = 4f;
    public float bottomBound = -4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = 0f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;

        Vector2 movement = Vector2.up * v * speed * Time.deltaTime;
        transform.Translate(movement);

        float posY = transform.position.y;
        float clamp = Mathf.Clamp(transform.position.y, -4f, 4f);

        transform.position = new Vector3(transform.position.x, clamp, 0);
    }

    }

    

