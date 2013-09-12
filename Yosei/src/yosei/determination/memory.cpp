#include "yosei/determination/memory.h"

Memory::Memory()
{

}

Memory::~Memory()
{

}

void Memory::historize_action(Action* p_action)
{
    m_history_actions.push_front(p_action);
}

void Memory::remember_action(Action* p_action, float p_feedback)
{
    std::map<Action*, Opinion*>::iterator it = m_knowledge.find(p_action);

    // This Action is already known; merge its opinion with the new one
    if (it != m_knowledge.end())
    {
        it->second->offset_value(p_feedback);
    }
    // This Action isn't known. Use a new entry
    else
    {
        m_knowledge.insert(std::pair<Action*, Opinion*>(p_action, new Opinion("Opinion", p_feedback)));
        p_action->memorize();
    }

    //m_knowledge.insert(std::pair<Action*, Opinion*>(new Action(p_action), Opinion("Opinion of " + p_action.to_string())));
}

void Memory::remember_actions(float p_feedback)
{
    int i = 2;
    for (std::list<Action*>::iterator it = m_history_actions.begin(); it != m_history_actions.end(); ++it, --i)
    {
        //Observer::getInstance().out(Observer::GAMEPLAY, "Adding feedback " + SSTR(p_feedback) + " on " + (*it)->get_to()->to_string());
        remember_action((*it), p_feedback * i * i);
    }
}

bool Memory::should_do(Action* p_motor_action, const Personality* p_personality)
{
    std::map<Action*, Opinion*>::iterator it = m_knowledge.find(p_motor_action);

    if (it == m_knowledge.end())
    {
        return true; // Try
    }

    return it->second->should_do(p_personality);
}

bool Memory::should_do_over(Action* p_first, Action* p_second, const Personality* p_personality)
{
    std::map<Action*, Opinion*>::iterator it_first = m_knowledge.find(p_first);
    std::map<Action*, Opinion*>::iterator it_second = m_knowledge.find(p_second);

    if (it_first == m_knowledge.end())
    {
        if (it_second == m_knowledge.end())
        {
            // Coin toss, both are unknown
            Observer::getInstance().out(Observer::GAMEPLAY, "Both unknown");
            return (rand() < (RAND_MAX / 2));
        }
        else
        {
            // Do second if we want to, otherwise try first
            return should_do(p_second, p_personality);
        }
    }
    else
    {
        if (it_second == m_knowledge.end())
        {
            // Do first if we want to, otherwise try second
            return should_do(p_first, p_personality);
        }
        else
        {
            // We know both, let's see which one is better
            return (it_first->second > it_second->second);
        }
    }
}

void Memory::age()
{
    // Make every memory age
    for (std::list<Action*>::iterator it = m_history_actions.begin(); it != m_history_actions.end(); ++it)
    {
        (*it)->age();

        // Deallocate old memories
        if (Action::is_old(*it))
        {
            // Only if memory isn't used in knowledge
            if (!(*it)->is_memorized())
            {
                delete (*it);
            }
        }
    }

    // Get rid of old memories. Harsh
    m_history_actions.remove_if(Action::is_old);

    /*
    bool relaunch;
    do
    {
        relaunch = false;
        for (std::list<Action*>::iterator it = m_history_actions.begin(); it != m_history_actions.end(); ++it)
        {
            if ((*it)->get_age() > 10)
            {
                delete(*it);
                m_history_actions.erase(it);
                relaunch = true;
                break;
            }
        }
    } while (relaunch);
    */
}

std::string Memory::to_string()
{
    std::string str = "I remember:\n";
    for (std::list<Action*>::iterator it = m_history_actions.begin(); it != m_history_actions.end(); ++it)
    {
        str += (*it)->to_string() + "\n";
    }

    str += "My opinions are:" + SSTR("") + ((m_knowledge.size() != 0) ? "\n" : "");

    for (std::map<Action*, Opinion*>::iterator it = m_knowledge.begin(); it != m_knowledge.end(); ++it)
    {
        str += it->first->to_string() + " is " + it->second->to_string() + ((std::next(it) == m_knowledge.end()) ? "" : "\n");
    }
    return str;
}
