using UnityEngine;

public class RandomCoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 5;

    BoxCollider2D[] spawnAreas;

    void Awake()
    {
        // Automatically get all BoxCollider2D from children
        spawnAreas = GetComponentsInChildren<BoxCollider2D>();
    }

    void Start()
    {
        if (spawnAreas.Length == 0)
        {
            Debug.LogError("No spawn areas found!");
            return;
        }

        for (int i = 0; i < coinCount; i++)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        BoxCollider2D area = spawnAreas[Random.Range(0, spawnAreas.Length)];
        Bounds bounds = area.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(coinPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
