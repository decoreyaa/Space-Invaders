using UnityEngine;

public class MenuBulletSpawner : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public GameObject bulletPrefab;
    public float spawnInterval = 0.5f;
    public float spawnerOffset = 2f;
    private float nextShootTime;

    void Start()
    {
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextShootTime)
        {
           if (Random.value < 0.5f)
        {
            SpawnBullet(enemyBulletPrefab, ShootLeft());
        }
        else
        {
            SpawnBullet(bulletPrefab, ShootRight());
        }
         
        }


    }

 void SpawnBullet(GameObject prefab, Vector2 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
        nextShootTime = Time.time + Random.Range(spawnInterval * 0.5f, spawnInterval * 1.5f);
    }

    

    Vector2 ShootLeft()
    {
        float startX = ScreenBounds.Right + spawnerOffset ;
        float startY = Random.Range(ScreenBounds.Bottom, ScreenBounds.Top);
        return new Vector2(startX, startY);
    }

    Vector2 ShootRight()
    {
        float startX = ScreenBounds.Left + (spawnerOffset * -1);
        float startY = Random.Range(ScreenBounds.Bottom, ScreenBounds.Top);
        return new Vector2(startX, startY);
    }
   
}
