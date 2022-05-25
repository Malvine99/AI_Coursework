using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIVariables : MonoBehaviour
{
    [SerializeField] protected int health_;
    [SerializeField] protected float hunger_;
    [SerializeField] protected int hunger_threshold_;
    [SerializeField] public int run_speed;

    private Text health_and_hunger_;

    public Vector2 min_position;
    public Vector2 max_position;
    public float near_distance;
    public float damage_distance;

    protected bool has_goal_ = false;
    protected Vector3 shelter_goal_;
    private bool in_shelter_ = false;

    private Rigidbody2D self_rigid_body_;
    private Vector2 new_velocity_;

    protected bool wandering;
    protected bool running_chance_0;
    protected bool running_chance_1;

    public int id;

    public void Start()
    {
        self_rigid_body_ = GetComponent<Rigidbody2D>();
        health_and_hunger_ = GetComponent<Text>();
    }

    public void Update()
    {
        //limit AI position
        if (transform.position.x < min_position.x)
        {
            transform.position = new Vector3(min_position.x, transform.position.y, transform.position.z);
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

        health_and_hunger_.text = "Health: " + health_ + "\n Hunger: " + hunger_;
    }

    public void FixedUpdate()
    {
         self_rigid_body_.velocity = new_velocity_;
    }

    public void setVelocity(Vector2 velocity)
    {
        new_velocity_ = velocity;
    }

    public void updateHealth(int modifier)
    {
        health_ += modifier;
        if (health_ > 100)
        {
            health_ = 100;
        }
        if (health_ <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("AI_Manager").GetComponent<AIManager>().AI_instances.RemoveAt(id);
            GameObject.Find("AI_Manager").GetComponent<AIManager>().available_id = id;
        }
    }

    public void updateHunger(float modifier)
    {
        hunger_ += modifier;
        if(hunger_ > 100)
        {
            hunger_ = 100;
        } 
        if (hunger_ <= 0)
        {
            hunger_ = 0;
            updateHealth(-1);
        }
    }

    public bool isInRange(Vector2 location, Vector2 goal, float range)
    {
        if (Vector2.Distance(location, goal) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isPathClear(Vector3Int shelter_position, Vector3 self_position, Vector3 player_position)
    {
        if (Vector3.Distance(shelter_position, self_position) < Vector3.Distance(shelter_position, player_position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 makeNewGoal(Transform instance, float range)
    {
        setVelocity(new Vector2(0, 0));
        setWandering(true);

        Vector2 new_goal_;
        new_goal_.x = Random.Range(instance.position.x - range, instance.position.x + range);
        new_goal_.y = Random.Range(instance.position.y - range, instance.position.y + range);

        while (!isInRange(transform.position, new_goal_, range))
        {
            new_goal_.x = Random.Range(instance.position.x - range, instance.position.x + range);
            new_goal_.y = Random.Range(instance.position.y - range, instance.position.y + range);
        }

        return new_goal_;
    }

    public void resolveWander(Transform instance, bool is_grazing)
    {
        setWandering(false);
        if (is_grazing)
        {
            updateHunger(10);
        }
        else
        {
            updateHunger(-5);
        }

    }

    public bool isHungry()
    {
        if(hunger_ < hunger_threshold_)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setHasGoal(bool has)
    {
        has_goal_ = has;
    }

    public bool getHasGoal()
    {
        return has_goal_;
    }

    public void setShelterGoal(Vector3 goal)
    {
        shelter_goal_ = goal;
    }

    public Vector3 getShelterGoal()
    {
        return shelter_goal_;
    }

    public void setInShelter(bool shelter)
    {
        in_shelter_ = shelter;
    }

    public bool getInShelter()
    {
        return in_shelter_;
    }

    public void setWandering(bool w)
    {
        wandering = w;
    }

    public bool getWandering()
    {
        return wandering;
    }

    public void setRunningChance0(bool c0)
    {
        running_chance_0 = c0;
    }

    public bool getRunningChance0()
    {
        return running_chance_0;
    }

    public void setRunningChance1(bool c1)
    {
        running_chance_1 = c1;
    }

    public bool getRunningChance1()
    {
        return running_chance_1;
    }

}
