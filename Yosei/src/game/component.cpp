#include "game/component.h"

Component::Component(std::string p_name) : Identifiable(p_name)
{
    m_active = true;
}

Component::Component()
{
    m_active = true;
}

Component::~Component()
{
    Observer::getInstance().out(Observer::VERBOSE, "Destroyed component " + to_string());
}

bool Component::is_active()
{
    return m_active;
}

std::string Component::to_string() const
{
    return m_name;
}
