using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterController : MonoBehaviour
{
    public Transform[] railListLeftToRight;
    bool changingRail = false;
    int indexOfRail = 0;
    public float speed = 15;
    void Start()
    {
        if (railListLeftToRight == null || railListLeftToRight.Length < 1)
            Debug.LogWarning("No rail set in PlayerController of " + name);
    }

    IEnumerator ChangeRail(int X)
    {
        int newIndexOfRail = indexOfRail + X;
        if (newIndexOfRail >= 0 && newIndexOfRail < railListLeftToRight.Length)
        {
            indexOfRail = newIndexOfRail;
            float speed = (railListLeftToRight[newIndexOfRail].position.x - transform.position.x) / 0.2f; // the speed by second you have to achieve to go on the next rail in 0.2f second
            while (Mathf.Sign(railListLeftToRight[newIndexOfRail].position.x - transform.position.x) ==  Mathf.Sign(speed))
            {
                transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
                yield return new WaitForEndOfFrame();
            }
            transform.position = new Vector3(railListLeftToRight[newIndexOfRail].position.x, transform.position.y, transform.position.z); // Making sure that the dwarf is on the rail
        }
        changingRail = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changingRail == false)
        {
            float Xaxis = Input.GetAxisRaw("Horizontal");
            if (Xaxis != 0)
            {
                changingRail = true;
                StartCoroutine(ChangeRail((int)Mathf.Sign(Xaxis)));
            }
        }
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    }
}
