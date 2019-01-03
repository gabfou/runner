using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
    public GameObject Player;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null)
			instance = this;
		else 
			GameObject.Destroy(gameObject);
	}
}
