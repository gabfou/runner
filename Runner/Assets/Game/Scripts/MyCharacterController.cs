using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyCharacterController : MonoBehaviour
{
    Transform[] railListLeftToRight;
    bool changingRail = false;
    public int indexOfRail = 1;
    public float speed = 15;
    Animator animator;
    SphereCollider col;
    bool jumping = false;
    bool crouching = false;
    bool isDead = false;
    public GameObject textPaused;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<SphereCollider>();
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
            float timer = 0;
            float speed = (railListLeftToRight[newIndexOfRail].position.x - transform.position.x) / 0.833f / (16f / 24f); // the speed by second you have to achieve to go to the next rail
            while ((timer = timer + Time.deltaTime) / 0.833f < 8f / 24f) // wait for 8 frame
                yield return new WaitForEndOfFrame();
            indexOfRail = newIndexOfRail;
            while (Mathf.Sign(railListLeftToRight[newIndexOfRail].position.x - transform.position.x) == Mathf.Sign(speed))
            {
                transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
                yield return new WaitForEndOfFrame();
            }
            transform.position = new Vector3(railListLeftToRight[newIndexOfRail].position.x, transform.position.y, transform.position.z); // Making sure that the dwarf is on the rail

        }
        changingRail = false;
    }

    IEnumerator Jump()
    {
        animator.SetBool("Jumping", true);
        float timer = 0;

        while ((timer = timer + Time.deltaTime) / 1.667f < 11f / 40f && crouching == false) // wait for 11 frame
            yield return new WaitForEndOfFrame();
        col.center = new Vector3(col.center.x, col.center.y + col.radius, col.center.z); // moving the collider on a distance coresponding of 3/4 of it's radius0

        while ((timer = timer + Time.deltaTime) / 1.667f < 35f / 40f && crouching == false) // wait for the 35th frame
            yield return new WaitForEndOfFrame();
        col.center = new Vector3(col.center.x, col.center.y - col.radius, col.center.z);

        animator.SetBool("Jumping", false);
        jumping = false;

    }

    IEnumerator Crouch()
    {
        animator.SetBool("Crouching", true);
        float timer = 0;

        while ((timer = timer + Time.deltaTime) / 1.583f < 4f / 38f && jumping == false) // wait for 4 frame
            yield return new WaitForEndOfFrame();
        col.center = new Vector3(col.center.x, col.center.y - col.radius, col.center.z);

        while ((timer = timer + Time.deltaTime) / 1.583f < 34f / 38f && jumping == false) // wait for the 34th frame
            yield return new WaitForEndOfFrame();
        col.center = new Vector3(col.center.x, col.center.y + col.radius, col.center.z);

        animator.SetBool("Crouching", false);
        crouching = false;

    }

    float oldTimeScale;
    
    void Update()
    {
        if (isDead)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameManager.instance.paused = !(GameManager.instance.paused);
            if (GameManager.instance.paused)
            {
                oldTimeScale = Time.timeScale;
                Time.timeScale = 0;
                textPaused.SetActive(true);
            }
            else
            {
                Time.timeScale = oldTimeScale;
                textPaused.SetActive(false);
            }
        }
        if (GameManager.instance.paused)
            return;
        if (changingRail == false)
        {
            float Xaxis = Input.GetAxisRaw("Horizontal");
            if (Xaxis != 0)
            {
                changingRail = true;
                StartCoroutine(ChangeRail((int)Mathf.Sign(Xaxis)));
            }
        }

        float Yaxis = Input.GetAxisRaw("Vertical");

        if (Yaxis > 0 && jumping == false)
        {
            jumping = true;
            StartCoroutine(Jump());
        }
        else if (Yaxis < 0 && crouching == false)
        {
            crouching = true;
            StartCoroutine(Crouch());
        }

        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "piece")
        {
            LevelAndScoreManager.instance.score += 1;
            GameObject.Destroy(other.gameObject);
        }
        if (other.tag == "trap")
        {
            GameManager.instance.paused = true;
            Trap trap = other.GetComponentInParent<Trap>();
            if (trap.CanPutPiece[indexOfRail])
                animator.SetTrigger("death_hight");
            else
                animator.SetTrigger("death_low");
            GameObject.Destroy(other.gameObject);
            StartCoroutine(Death());
        }
    }
}
