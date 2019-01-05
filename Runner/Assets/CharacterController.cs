using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterController : MonoBehaviour
{
    Transform[] railListLeftToRight;
    bool changingRail = false;
    int indexOfRail = 0;
    public float speed = 15;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        railListLeftToRight = GameManager.instance.railListLeftToRight;
        if (railListLeftToRight == null || railListLeftToRight.Length < 1)
            Debug.LogWarning("No rail set in PlayerController of " + name);
    }

    IEnumerator ChangeRail(int X)
    {
        animator.SetTrigger((X > 0) ? "JumpingRight" : "JumpingLeft");
        int newIndexOfRail = indexOfRail + X;
        if (newIndexOfRail >= 0 && newIndexOfRail < railListLeftToRight.Length)
        {
            indexOfRail = newIndexOfRail;
            float timer = 0;
            float speed = (railListLeftToRight[newIndexOfRail].position.x - transform.position.x) / 0.833f / (16f / 24f); // the speed by second you have to achieve to go to the next rail
            while ((timer = timer + Time.deltaTime) / 0.833f < 8f / 24f ) // wait for 8 frame
                yield return new WaitForEndOfFrame();
            while (Mathf.Sign(railListLeftToRight[newIndexOfRail].position.x - transform.position.x) == Mathf.Sign(speed))
            {
                transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
                yield return new WaitForEndOfFrame();
            }
            transform.position = new Vector3(railListLeftToRight[newIndexOfRail].position.x, transform.position.y, transform.position.z); // Making sure that the dwarf is on the rail

        }
        changingRail = false;
        animator.SetBool((X > 0) ? "JumpingRight" : "JumpingLeft", false);
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

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
