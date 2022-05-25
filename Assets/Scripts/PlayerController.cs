using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D player_rigid_body;
    private Vector2 movement;
    [SerializeField] private float player_speed;
    public Vector2 min_position;
    public Vector2 max_position;

    // Start is called before the first frame update
    void Start()
    {
        player_rigid_body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x_movement_ = Input.GetAxisRaw("Horizontal") * player_speed;
        float y_movement = Input.GetAxisRaw("Vertical") * player_speed;
        movement = new Vector2(x_movement_, y_movement) * Time.deltaTime;

        //limit player position
        if(transform.position.x < min_position.x)
        {
            transform.position = new Vector3 (min_position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > max_position.x)
        {
            transform.position = new Vector3(max_position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y < min_position.y)
        {
            transform.position = new Vector3(transform.position.x, min_position.y, transform.position.z);
        }
        if (transform.position.y > max_position.y)
        {
            transform.position = new Vector3(transform.position.x, max_position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        //in fixed update for smoother movement
        player_rigid_body.velocity = movement;

    }

}
