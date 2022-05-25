using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FSM_CloseToPlayerAction : FSM_State
{
    [SerializeField] private float shelter_range_radius_;
    [SerializeField] private Tilemap shelter_map_;

    [SerializeField] private float block_time_;
    private float time_left_ = 0;

    private void Start()
    {
        shelter_map_ = GameObject.Find("Shelter").GetComponent<Tilemap>();
    }

    private void Update()
    {
        time_left_ -= Time.deltaTime;
        if (time_left_ < 0)
        {
            time_left_ = 0;
        }
    }

    public override FSM.MachineState StateAction()
    {
        GetComponent<AIVariables>().setWandering(false);

        if (isDamageDistance(GetComponent<AIVariables>().damage_distance))
        {
            takeDamage();
        }

        if (GetComponent<AIVariables>().getHasGoal())
        {
            //run to shelter if we're not already there
            if (shelter_map_.GetComponent<TilemapCollider2D>().IsTouching(GetComponent<CircleCollider2D>()))
            {
                GetComponent<AIVariables>().setInShelter(true);

                return FSM.MachineState.InShelter;
            }
            else
            {
                GetComponent<AIVariables>().setInShelter(false);
                float goalX = (GetComponent<AIVariables>().getShelterGoal().x + 1) - GetComponent<Rigidbody2D>().position.x;
                float goalY = (GetComponent<AIVariables>().getShelterGoal().y + 1) - GetComponent<Rigidbody2D>().position.y;
                Vector2 shelter_direction_ = new Vector2(goalX, goalY);
                shelter_direction_.Normalize();

                GetComponent<AIVariables>().setVelocity(shelter_direction_ * GetComponent<AIVariables>().run_speed * Time.deltaTime);

                if (isDistanceNear(GetComponent<AIVariables>().near_distance))
                {
                    return FSM.MachineState.CloseToPlayer;
                }
                else
                {
                    return FSM.MachineState.Idle;
                }
            }
        }
        else
        {
            //check for viable shelter
            foreach (var position in shelter_map_.cellBounds.allPositionsWithin)
            {
                if (shelter_map_.HasTile(position))
                {
                    if (GetComponent<AIVariables>().isInRange(new Vector2(position.x, position.y), this.transform.position, shelter_range_radius_))
                    {

                        if (GetComponent<AIVariables>().isPathClear(position, this.transform.position, GameObject.Find("Player").transform.position))
                        {
                            GetComponent<AIVariables>().setShelterGoal(position);
                            GetComponent<AIVariables>().setHasGoal(true);

                            if (isDistanceNear(GetComponent<AIVariables>().near_distance))
                            {
                                return FSM.MachineState.CloseToPlayer;
                            }
                            else
                            {
                                return FSM.MachineState.Idle;
                            }

                        }
                    }
                }
            }
        }



        //if there is no shelter run away

        Vector2 direction_ = (GameObject.Find("Player").transform.position - this.transform.position);
        direction_.Scale(new Vector2(-1, -1));
        direction_.Normalize();

        GetComponent<AIVariables>().setVelocity(direction_ * GetComponent<AIVariables>().run_speed * Time.deltaTime);

        if (isDistanceNear(GetComponent<AIVariables>().near_distance))
        {
            return FSM.MachineState.CloseToPlayer;
        }
        else
        {
            return FSM.MachineState.Idle;
        }
    }

    public void takeDamage()
    {
        if (time_left_ <= 0)
        {
            GetComponent<AIVariables>().updateHealth(-50);
            time_left_ = block_time_;
        }
    }

}
