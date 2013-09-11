#include "yosei/determination/action.h"

Action::Action()
{
    m_age = 0;
    m_memorize = false;
}

// Add one unit of time to the duration since the Action was executed
// Returns the current age after incrementation
unsigned int Action::age()
{
    return ++m_age;
}

// Gets the duration since the Action was executed
unsigned int Action::get_age() const
{
    return m_age;
}

// Returns whether the Action is used in one's knowledge and is protected
bool Action::is_memorized() const
{
    return m_memorize;
}

// Returns true if the p_action has expired
bool Action::is_old(const Action* p_action)
{
    return (p_action->get_age() > 3);
}

// States the Action as used in one's knowledge, and as such it musn't be deleted
void Action::memorize()
{
    m_memorize = true;
}

std::string Action::to_string() const
{
    return "";
}
