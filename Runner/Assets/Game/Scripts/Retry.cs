using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Game");
    }
}
