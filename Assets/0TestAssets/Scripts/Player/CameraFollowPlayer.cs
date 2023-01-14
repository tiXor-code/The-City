using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Vector3 offset = new(0,6,-3);
    public Quaternion rotation = Quaternion.Euler(50, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FollowPlayer()
    {
        Camera.main.transform.position = PlayerMovement.instance.transform.position + offset;
        Camera.main.transform.rotation = rotation;
    }    

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
