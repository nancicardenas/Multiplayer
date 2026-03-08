using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;
    [SerializeField] private GameObject winPanel;

  
    void Awake()
    {
        Instance = this;
        winPanel.SetActive(false);
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    
}
