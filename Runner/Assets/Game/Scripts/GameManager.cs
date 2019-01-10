using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
    public GameObject Player;
    public Transform[] railListLeftToRight;
    public Text scoreText;
    public Text levelText;
    public Slider levelBar;
    [HideInInspector]public bool paused = false;

    void Awake()
	{
        instance = this;
	}

    private void Start()
    {
        LevelAndScoreManager.instance.Init();
    }

}
