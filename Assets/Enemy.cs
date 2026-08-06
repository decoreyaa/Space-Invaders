using UnityEngine;

public class Enemy : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        // what should happen when the enemy collide with a "Bullet" tagged object
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy Hit");
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
