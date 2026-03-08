using UnityEngine;
using UnityEngine.Events;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance;
    public int totalGems { get; private set; }

    public UnityEvent<GemManager> OnGemCollected;

    void Awake()
    {
        Instance = this; 
    }

    // Update is called once per frame
    public void AddGem()
    {
        totalGems++;
        OnGemCollected.Invoke(this);
    }
    
}
