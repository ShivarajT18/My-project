using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            LevelManager.instance.CoinCollected();
            Destroy(gameObject);
        }
    }
}
