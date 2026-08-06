using UnityEngine;


public static class ScreenBounds
{
    public static float Left
    {
        get
        {
            float height = Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;
            return Camera.main.transform.position.x - width;
        }
    }

    public static float Right
    {
        get
        {
            float height = Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;
            return Camera.main.transform.position.x + width;
        }
    }

    public static float Top
    {
        get
        {
            return Camera.main.transform.position.y + Camera.main.orthographicSize;
        }
    }

    public static float Bottom
    {
        get
        {
            return Camera.main.transform.position.y - Camera.main.orthographicSize;
        }
    }
}