using UnityEngine;

public class GemCollection : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GemManager.Instance.AddGem();
            Destroy(gameObject);
        }
    }
}
