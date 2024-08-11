using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
 
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            LoadScene();
        }
    }

    // Function to load the specified scene
    void LoadScene()
    {
        SceneManager.LoadScene("angryteeth");
    }
}
