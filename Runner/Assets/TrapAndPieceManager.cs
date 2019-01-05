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
    public GameObject[] piege;
    public float PiegeDensity = 0.5f;
    List<GameObject>[] listOfGameObject;
    public float distPlayerRBeforeRepeat = 20;
    Transform player;

    void Start()
    {
        listOfGameObject = new List<GameObject>[GameManager.instance.railListLeftToRight.Length];
        for (int i = 0; i < listOfGameObject.Length; i++)
            listOfGameObject[i] = new List<GameObject>();
        player = GameManager.instance.Player.transform;
    }

    int searchForPiege()
    {
        int i = 0;
        while (i < 3)
        {
            for (int j = 0; j < listOfGameObject.Length; j++)
            {
                if (listOfGameObject[j][listOfGameObject[j].Count - i]?.tag == "piege")
                    return i;
            }
            i++;
        }
        return i;
    }

    void PlaceNewTrapAndPiece()
    {
        int lastpiegedist = searchForPiege();


    }

    void Update()
    {
        if (player.position.z - listOfGameObject[0].First().transform.position.z > distPlayerRBeforeRepeat)
        {
            for (int i = 0; i < listOfGameObject.Length; i++)
                listOfGameObject[i].RemoveAt(0);
            PlaceNewTrapAndPiece();
        }
    }
}
