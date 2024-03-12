using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public Transform[] movePos;
    public float Speed;
    private int currentState;
    public Can can;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (can == null)
        {
            Debug.Log("Null");
        }else if (can != null)
        {
            Debug.Log("Full");
        }
        transform.position = Vector3.MoveTowards(transform.position, movePos[currentState].position,Speed);
        /*if (can.canFollow)
        {
            GetComponentInChildren<Transform>().position = Vector3.MoveTowards(transform.position, movePos[currentState].position, Speed);
        }*/
        if (Vector2.Distance(transform.position, movePos[currentState].position) <0.07f)
        {
            currentState++;
        }
        if (currentState==movePos.Length)
        {
            currentState = 0;
        }
    }
}
