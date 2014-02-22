using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]

public class EntityTrigger : MonoBehaviour
{
    public delegate void EntityTriggerHandler(Entity p_entity);

    private Collider _collider;
    private event EntityTriggerHandler _on_trigger_enter;
    private event EntityTriggerHandler _on_trigger_leave;

	public void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
	}

    public void SubscribeEntityEnter(EntityTriggerHandler p_handler)
    {
        _on_trigger_enter += p_handler;
    }

    public void SubscribeEntityLeave(EntityTriggerHandler p_handler)
    {
        _on_trigger_leave += p_handler;
    }
    
    public void UnsubscribeEntityEnter(EntityTriggerHandler p_handler)
    {
        _on_trigger_enter -= p_handler;
    }

    public void UnsubscribeEntityLeave(EntityTriggerHandler p_handler)
    {
        _on_trigger_leave -= p_handler;
    }

    public void OnTriggerEnter(Collider p_collider)
    {
        Entity entity = p_collider.gameObject.GetComponent<Entity>();

        if (entity != null)
        {
            if (_on_trigger_enter != null)
            {
                _on_trigger_enter(entity);
            }
        }
    }

    public void OnTriggerExit(Collider p_collider)
    {
        Entity entity = p_collider.gameObject.GetComponent<Entity>();

        if (entity != null)
        {
            if (_on_trigger_leave != null)
            {
                _on_trigger_leave(entity);
            }
        }
    }
}
