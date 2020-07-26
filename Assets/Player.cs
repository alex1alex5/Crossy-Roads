using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //private Animator animator;
    private bool isHopping;
    public Text countText;
    private int count;
    private int update_score = 0;
    Vector3 p_pos;
    float radius = 1f;
    private void Start()
    {
        //animator = GetComponent<Animator>();
        count = 0;
        SetCountText();


    }

    private void Update()
    {
        p_pos = transform.position;

        if (Physics.CheckSphere(p_pos, radius))
        {
            if (transform.name == "Tree(Clone)")
            {
                Debug.Log("PO");
            }
        }
        else
        {
            Debug.Log("PISSSSSS");
        }

        if (Input.GetKeyDown(KeyCode.W)) //&& !isHopping)
        {
            //animator.SetTrigger("hop");
            //isHopping = true;
            //     float zDiff = 0;
            Debug.Log(p_pos);

            if (update_score != 0)
            {
                update_score--;
                SetCountText();
            }
            else
            {
                count++;
                SetCountText();
            }
            //SetCountText();
            //     if (transform.position.z % 1 != 0)
            //     {
            //         zDiff = (transform.position.z - Mathf.Round(transform.position.z) - transform.position.z);
            //     }

            MoveCharacter(new Vector3(1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.A)) // && !isHopping)
        {
            MoveCharacter(new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.S)) // && !isHopping)
        {
            MoveCharacter(new Vector3(-1, 0, 0));
            //count--;
            if (count > 0)
            {
                update_score++;
            }
            //SetCountText();
        }
        else if (Input.GetKeyDown(KeyCode.D)) // && !isHopping)
        {
            MoveCharacter(new Vector3(0, 0, -1));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Log>() != null || collision.collider.GetComponent<LogVariant>() != null)
        {
            transform.parent = collision.collider.transform;
        }
        else
            transform.parent = null;
    }

    private void MoveCharacter(Vector3 difference)
    {
        //animator.SetTrigger("hop");
        isHopping = true;
        transform.position = (transform.position + difference);
    }
    public void FinishHop()

    {
        isHopping = false;
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }
    //When the Primitive collides with the walls, it will reverse direction

}
