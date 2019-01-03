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

    // Start is called before the first frame update
    void Start()
    {
         Debug.Log(listMesh.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - listMesh.First().position.z > distPlayerRBeforeRepeat)
        {
            Transform t = listMesh.First();
            listMesh.RemoveAt(0);
            PlaceRail(t, nbOfRail - 1);
        }
        // Debug.Log(listMesh.Count);
    }

    void PlaceRail(Transform newRail, int i)
    {
        newRail.position = transform.position + new Vector3(0,0,distBetweenRail * i);
        newRail.parent = transform;
        listMesh.Add(newRail);
        Debug.Log(listMesh.Count);
    }
 

    public void AddRail(int i)
    {
        GameObject newRail = GameObject.Instantiate(rail);
        PlaceRail(newRail.transform, i);
    }
}
