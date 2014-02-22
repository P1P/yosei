using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EntityTrigger))]

public class Goal : Entity
{
    public delegate void GoalReachedHandler();

    private EntityTrigger _entity_trigger;
    private List<Yosei> _lst_valid_yosei;
    private event GoalReachedHandler _goal_reached;

	public void Awake()
    {
        _entity_trigger = GetComponent<EntityTrigger>();
        _entity_trigger.SubscribeEntityEnter(EntityTriggerEnterHandler);
    }

    /// <summary>
    /// Activates the Goal with the provided list of Yosei as valid triggers
    /// Only the Yosei within this list will fire an event when entering the Goal
    /// </summary>
    /// <param name="_lst_valid_yosei">The list of Yosei that can activate the goal</param>
    /// <param name="p_lst_valid_yosei">The handler for the Goal reached event</param>
    public void ActivateForTeam(List<Yosei> p_lst_valid_yosei, GoalReachedHandler p_handler)
    {
        _lst_valid_yosei = p_lst_valid_yosei;
        _goal_reached += p_handler;
    }

    /// <summary>
    /// Sets the Goal to an inactive state
    /// Will not fire events when reached by a Yosei
    /// </summary>
    public void Disable()
    {
        _lst_valid_yosei = null;
        _goal_reached = null;
    }

    public void OnDestroy()
    {
        _entity_trigger.UnsubscribeEntityEnter(EntityTriggerEnterHandler);
    }

    /// <summary>
    /// If the Goal is active, fire a valid event if the entering Entity is a valid Yosei
    /// </summary>
    /// <param name="p_entity">The Entity to reach the Goal</param>
    private void EntityTriggerEnterHandler(Entity p_entity)
    {
        if (_lst_valid_yosei == null)
        {
            return;
        }

        Yosei yosei = p_entity.GetComponent<Yosei>();
        if (yosei != null && _lst_valid_yosei.Contains(yosei))
        {
            if (_goal_reached != null)
            {
                _goal_reached();
            }
        }
    }
}
