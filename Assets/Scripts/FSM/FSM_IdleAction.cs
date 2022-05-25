using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_IdleAction : FSM_State
{
    [SerializeField] private float movement_range_;
    [SerializeField] private float cooldown;
    [SerializeField] private bool grazing_ = false;
    private float elapsed_time_ = 0f;
    private float lerp_time_ = 0;
    private Vector2 startPosition;
    private Vector2 goal;        

    public override FSM.MachineState StateAction()
    {
        if (isDistanceNear(GetComponent<AIVariables>().near_distance))
        {
            return FSM.MachineState.CloseToPlayer;
        }

        else if (GetComponent<AIVariables>().isHungry())
        {
            GetComponent<AIVariables>().setHasGoal(false);
            GetComponent<AIVariables>().setInShelter(false);
            return FSM.MachineState.Hungry;
        }

        else
        {
            GetComponent<AIVariables>().setHasGoal(false);
            GetComponent<AIVariables>().setInShelter(false);

            if (!GetComponent<AIVariables>().getWandering())
            {

                startPosition = this.transform.position;

                goal = GetComponent<AIVariables>().makeNewGoal(this.transform, movement_range_);
            }


            if (goal.x != 0 && goal.y != 0)
            {
                elapsed_time_ += Time.deltaTime;
                lerp_time_ += Time.deltaTime / cooldown;
                this.transform.position = Vector2.Lerp(startPosition, goal, lerp_time_);

                if (elapsed_time_ > cooldown)
                {
                    elapsed_time_ = 0f;
                    lerp_time_ = 0f;

                    int rand_ = Random.Range(0, 100);
                    if(rand_ <= 50)
                    {
                        grazing_ = true;
                    }
                    else
                    {
                        grazing_ = false;
                    }

                    GetComponent<AIVariables>().resolveWander(this.transform, grazing_);
                }
            }

            return FSM.MachineState.Idle;
        }
    }

}
