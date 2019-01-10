using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelAndScoreManager : MonoBehaviour
{
    [HideInInspector]public int score = 0;
    public int level = 1;
    public static LevelAndScoreManager instance;
    public int timeOfOneLevel = 60;
    float time = 0;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);

    }

    public void Init()
    {
        if (GameManager.instance != null && GameManager.instance.levelText != null)
            GameManager.instance.levelText.text = "Level " + level.ToString();
        Time.timeScale = 1.5f + (level * 0.05f);
        TrapAndPieceManager.instance.trapDensity = Mathf.Min(0.05f * level, 0.5f);
        time = 0;
    }

    private void Update()
    {
        if (GameManager.instance.paused)
            return;
        time += Time.deltaTime / Time.timeScale;
        if (time > timeOfOneLevel)
        {
            level++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (GameManager.instance == null)
            return;
        if (GameManager.instance.scoreText != null)
            GameManager.instance.scoreText.text = score.ToString();
        if (GameManager.instance.levelBar != null)
            GameManager.instance.levelBar.value = (time / timeOfOneLevel);
    }
}
