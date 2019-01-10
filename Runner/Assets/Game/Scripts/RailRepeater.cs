using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RailRepeater : MonoBehaviour
{
    public float distPlayerRBeforeRepeat = 20;
    public float distBetweenRail = 8;
    [Min(0)]
    public int nbOfRail = 1;
    public List<Transform> listMesh = new List<Transform>();
    public GameObject rail;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - listMesh.First().position.z > distPlayerRBeforeRepeat)
        {
            Transform t = listMesh.First();
            listMesh.RemoveAt(0);
            PlaceNewRail(t);
        }
        // Debug.Log(listMesh.Count);
    }

    void PlaceNewRail(Transform newRail)
    {
        Vector3 lastRailPos = (listMesh.LastOrDefault() == null) ? transform.position : listMesh.LastOrDefault().position;
        newRail.position = lastRailPos + new Vector3(0,0,distBetweenRail);
        newRail.parent = transform;
        listMesh.Add(newRail);
        // Debug.Log(listMesh.Count);
    }
 

    public void AddRail(int i)
    {
        GameObject newRail = GameObject.Instantiate(rail);
        PlaceNewRail(newRail.transform);
    }
}
