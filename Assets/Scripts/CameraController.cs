using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //variables for camera movement
    public Transform player;
    public float damp_time = 0.4f;
    private Vector3 camera_goal;
    private Vector3 velocity = Vector3.zero;

    //variables for camera movement clamp
    public Vector2 min_position;
    public Vector2 max_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera_goal = new Vector3(player.position.x, player.position.y, -10);
        //camera_goal = new Vector3(GameObject.Find("AI").transform.position.x, GameObject.Find("AI").transform.position.y, -10);

        camera_goal.x = Mathf.Clamp(camera_goal.x, min_position.x, max_position.x);
        camera_goal.y = Mathf.Clamp(camera_goal.y, min_position.y, max_position.y);

        transform.position = Vector3.SmoothDamp(gameObject.transform.position, camera_goal, ref velocity, damp_time);
    }
}
