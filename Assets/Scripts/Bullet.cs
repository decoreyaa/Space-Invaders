using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private bool hasHit = false;

    void Update()
    {
        // move the bullet to the right in this horizontal-oriented game
        transform.position += Vector3.right * speed * Time.deltaTime;

        // destroy the bullet if it flies off the right edge of the screen
        if (transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
     
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy is deferred to the end of the frame, so a spent bullet still gets
        // callbacks this step. Without the guard one shot could take out two enemies.
        if (hasHit)
        {
            return;
        }

        // what should happen when the bullet hits something?
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasHit = true;
            Debug.Log("Enemy Hit");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
