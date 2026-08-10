using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;


    void Update()
    {

        // move the bullet to the left in this horizontal-oriented game
        transform.position += Vector3.left * speed * Time.deltaTime;
        // destroy the bullet if it flies off the right edge of the screen
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
     
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // what should happen when the bullet hits something?
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            GameManager.Instance.SetGameOver();
            //Destroy(gameObject);
        }
    }
  
}