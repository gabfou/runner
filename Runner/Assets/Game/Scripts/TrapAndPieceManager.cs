using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrapAndPieceManager : MonoBehaviour
{
	public float SpaceBetweenCase = 8;
	public int nbOfCase = 300;
	public GameObject piece;
	public float pieceHeight = 2;
	public Trap[] trap;
	public float trapDensity = 0.1f;
	List<GameObject>[] listOfPiece;
	List<Trap> listOfTrap;
	public float distPlayerRBeforeRepeat = 20;
	Transform player;
	public float offset = 25;
    public static TrapAndPieceManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
	{
		listOfPiece = new List<GameObject>[GameManager.instance.railListLeftToRight.Length];
		for (int i = 0; i < listOfPiece.Length; i++)
			listOfPiece[i] = new List<GameObject>();
		listOfTrap = new List<Trap>();
		player = GameManager.instance.Player.transform;
	}

	int searchForPiece(out int Railnumber, int limit = 3)
	{
		int i = 0;
		Railnumber = -1;

		while (i < limit)
		{
			for (int j = 0; j < listOfPiece.Length; j++)
			{
				if (listOfPiece[j].Count - i - 1 >= 0 && listOfPiece[j][listOfPiece[j].Count - i - 1] != null)
				{
					Railnumber = j;
					return i;
				}
			}
			i++;
		}
		return i;
	}

	int searchForTrap()
	{
		int i = 0;

		while (i < 10)
		{
			if ((listOfTrap.Count - i - 1) >= 0 && listOfTrap[listOfTrap.Count - i - 1] != null)
				return i;
			i++;
		}
		return i;
	}

	void destroyLastNpece(int n)
	{

		for (int i = 0 ; i < n && listOfPiece[0].Count - i - 1 >= 0; i++)
		{
			for (int j = 0; j < listOfPiece.Length; j++)
				if (listOfPiece[j][listOfPiece[j].Count - i - 1] != null)
				{
					GameObject.Destroy(listOfPiece[j][listOfPiece[j].Count - i - 1]);
					listOfPiece[j][listOfPiece[j].Count - i - 1] = null;
				}
		}
	}

	bool PieceTrapColision(Trap trap, int railPieceNumber)
	{
		return (trap == null || (railPieceNumber > -1 && trap.CanPutPiece[railPieceNumber]));
	}

	void PlaceNewTrapAndPiece()
	{
		
		Trap newTrap = null;
		int lastTrapDist = searchForTrap();

		if (lastTrapDist > 6 && Random.Range(0f, 1f) < trapDensity)
		{
			newTrap = GameObject.Instantiate(trap[Random.Range(0, trap.Length)], new Vector3(0, 0, offset + listOfPiece[0].Count() * SpaceBetweenCase), Quaternion.identity);
			destroyLastNpece(5);
		}
		listOfTrap.Add(newTrap);

		

	// with the system below the total number of piece is random maybe make an insurance;

		for (int i = 0; i < listOfPiece.Length; i++)
		{
			listOfPiece[i].Add(null);
		}

		int RailPieceNumber = -1;
		int lastPieceDist = searchForPiece(out RailPieceNumber, 10);

		float rand = Random.Range(0f, 1f);
		if (rand < 0.8f && lastPieceDist == 1 && listOfPiece[(int)RailPieceNumber].Count > 0 && PieceTrapColision(newTrap, RailPieceNumber))
			listOfPiece[(int)RailPieceNumber][listOfPiece[(int)RailPieceNumber].Count - 1] = GameObject.Instantiate(piece, new Vector3(GameManager.instance.railListLeftToRight[RailPieceNumber].transform.position.x, 0, offset + (listOfPiece[(int)RailPieceNumber].Count - 1) * SpaceBetweenCase), Quaternion.identity);
		else if (rand < 0.8f && lastPieceDist > 6)
		{
			int rand2 = Random.Range(0, GameManager.instance.railListLeftToRight.Count());
			if (PieceTrapColision(newTrap, RailPieceNumber))
				listOfPiece[rand2][listOfPiece[rand2].Count - 1] = GameObject.Instantiate(piece, new Vector3(GameManager.instance.railListLeftToRight[rand2].transform.position.x, 0, offset + (listOfPiece[rand2].Count - 1) * SpaceBetweenCase), Quaternion.identity);
		}
	}

	

	void Update()
	{
		if (player.position.z - offset > distPlayerRBeforeRepeat)
		{
			GameObject g;
			for (int i = 0; i < listOfPiece.Length; i++)
			{
				g = listOfPiece[i].FirstOrDefault();
				listOfPiece[i].RemoveAt(0);
				if (g)
					GameObject.Destroy(g);
			}
			g = listOfTrap.FirstOrDefault()?.gameObject;
			listOfTrap.RemoveAt(0);
			if (g)
				GameObject.Destroy(g);
			offset += SpaceBetweenCase;
		}
		if (listOfPiece[0].Count < nbOfCase)
		{
			PlaceNewTrapAndPiece();
		}
	}
}
