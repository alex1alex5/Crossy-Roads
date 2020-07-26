
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //This will be named whatever name the Player GameObject is, in my case mine is named Player
    [SerializeField] private GameObject Player;
    //Your offset and smoothness may need to be reconfigured in Unity while running
    //Clipping Plane on the main camera may also need to be reconfiugred in Unity
    [SerializeField] private Vector3 offset; //try setting the offset: x by -5, y by 7, z by -2
    [SerializeField] private float smoothness; //try setting smoothness to 0.1


    // Update is called once per frame
    void Update()
    {
        if(Player!=null)
            transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, smoothness);
    }
}
