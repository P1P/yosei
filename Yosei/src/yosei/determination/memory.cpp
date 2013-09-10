#include "yosei/determination/memory.h"

Memory::Memory()
{

}

Memory::~Memory()
{

}

void Memory::add_motor_action(MotorAction* p_motor_action)
{
    //delete(p_motor_action);
    m_history_motor_actions.push_back(p_motor_action);
}

void Memory::age()
{
    // Make every memory age
    for (std::list<MotorAction*>::iterator it = m_history_motor_actions.begin(); it != m_history_motor_actions.end(); ++it)
    {
        (*it)->age();
    }

    // Get rid of old memories. Harsh
    bool relaunch;
    do
    {
        relaunch = false;
        for (std::list<MotorAction*>::iterator it = m_history_motor_actions.begin(); it != m_history_motor_actions.end(); ++it)
        {
            if ((*it)->get_age() > 10)
            {
                delete(*it);
                m_history_motor_actions.erase(it);
                relaunch = true;
                break;
            }
        }
    } while (relaunch);
}

std::string Memory::to_string()
{
    std::string str = "I remember:\n";
    for (std::list<MotorAction*>::iterator it = m_history_motor_actions.begin(); it != m_history_motor_actions.end(); ++it)
    {
        str += "Age " + SSTR((*it)->get_age()) + " Going from " + (*it)->get_from()->to_string() + " to " + (*it)->get_to()->to_string() + "\n";
    }
    return str;
}
