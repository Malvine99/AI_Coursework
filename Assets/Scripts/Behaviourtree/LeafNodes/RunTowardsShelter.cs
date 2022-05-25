using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RunTowardsShelter : LeafNode
{
    [SerializeField] private Tilemap shelter_map_;
    [SerializeField] private float shelter_range_;

    private void Start()
    {
        shelter_map_ = GameObject.Find("Shelter").GetComponent<Tilemap>();

    }

    protected override void NodeFunction()
    {
        root_transform_.GetComponent<AIVariables>().setRunningChance0(false);
        root_transform_.GetComponent<AIVariables>().setRunningChance1(false);
        root_transform_.GetComponent<AIVariables>().setWandering(false);

        if (shelter_map_.GetComponent<TilemapCollider2D>().IsTouching(root_transform_.GetComponent<CircleCollider2D>()))
        {
            Debug.Log("collided");
            root_transform_.GetComponent<AIVariables>().setInShelter(true);
        }
        else
        {
            root_transform_.GetComponent<AIVariables>().setInShelter(false);
            float goalX = (root_transform_.GetComponent<AIVariables>().getShelterGoal().x + 1) - root_transform_.GetComponent<Rigidbody2D>().position.x;
            float goalY = (root_transform_.GetComponent<AIVariables>().getShelterGoal().y + 1) - root_transform_.GetComponent<Rigidbody2D>().position.y;
            Vector2 direction = new Vector2(goalX, goalY);
            direction.Normalize();

            root_transform_.GetComponent<AIVariables>().setVelocity(direction * root_transform_.GetComponent<AIVariables>().run_speed * Time.deltaTime);

        }
        this.node_state_ = TreeNodeState.SUCCESS;
    }
}
