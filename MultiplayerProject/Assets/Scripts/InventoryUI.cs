using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI gemText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gemText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateGemText()
    {
        gemText.text = GemManager.Instance.totalGems.ToString();
    }
}
