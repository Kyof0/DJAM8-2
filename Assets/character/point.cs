using UnityEngine;

public class VulnerablePoint : MonoBehaviour
{
    public float appearTime = 1.0f; // Time the point remains visible
    private bool isVisible = false;

    void Start()
    {
        gameObject.SetActive(false); // Hide the point initially
    }

    public void Show()
    {
        isVisible = true;
        gameObject.SetActive(true);
        Invoke("Hide", appearTime);
    }

    private void Hide()
    {
        isVisible = false;
        gameObject.SetActive(false);
    }

    public bool IsVisible()
    {
        return isVisible;
    }
}
