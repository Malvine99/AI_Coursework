using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_HungryAction : FSM_State
{
    [SerializeField] private float movement_range_;
    [SerializeField] private float cooldown;
    private bool grazing_ = true;
    private float elapsed_time_ = 0f;
    private float lerp_time_ = 0;
    private Vector2 startPosition;
    private Vector2 goal;

    public override FSM.MachineState StateAction()
    {
        GetComponent<AIVariables>().setInShelter(false);

        if (isDistanceNear(GetComponent<AIVariables>().near_distance))
        {
            return FSM.MachineState.CloseToPlayer;
        }

        else
        {

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

                    GetComponent<AIVariables>().resolveWander(this.transform, grazing_);
                }
            }
            if (GetComponent<AIVariables>().isHungry())
            {
                return FSM.MachineState.Hungry;
            }
            else
            {
                return FSM.MachineState.Idle;
            }
        }
    }

}
