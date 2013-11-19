#include "yosei/will/motoraction.h"

MotorAction::MotorAction(Tile* p_from, Tile* p_to, Coordinates::CARDINAL_DIRECTION p_cadir)
{
    m_from = p_from;
    m_to = p_to;
    m_cadir = p_cadir;
}

MotorAction::~MotorAction()
{

}

// Returns the Tile where the TileObject was
Tile* MotorAction::get_from() const
{
    return m_from;
}

// Returns the Tile the TileObject wants to reach
Tile* MotorAction::get_to() const
{
    return m_to;
}

// Returns the Direction the TileObject wills to go
Coordinates::CARDINAL_DIRECTION MotorAction::get_cadir() const
{
    return m_cadir;
}

// Determines whether this and p_other are similar actions
bool MotorAction::less_comparer(Action* p_other)
{
    return this->get_to()->get_id() < ((static_cast<MotorAction*>(p_other))->get_to()->get_id());
}

// Returns the ressemblance of this and another MotorAction
float MotorAction::like(void* p_other) const
{
    MotorAction* other_motor_action = static_cast<MotorAction*>(p_other);

    // Both objects have the same appearance
    if (other_motor_action->get_to()->get_appearance().compare(this->get_to()->get_appearance()) == 0)
    {
        return 1;
    }

    return 0;
}

std::string MotorAction::to_string() const
{
    return Action::to_string() + (m_cadir == Coordinates::STILL ? "Stay at " : "Go to   ") + get_to()->to_string();
}
