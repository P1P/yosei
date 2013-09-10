#include "yosei/will/motoraction.h"

MotorAction::MotorAction(Tile* p_from, Tile* p_to, Coordinates::CARDINAL_DIRECTION p_cadir)
{
    m_from = p_from;
    m_to = p_to;
    m_cadir = p_cadir;
    m_age = 0;
}

MotorAction::~MotorAction()
{

}

// Returns the Tile where the TileObject was
Tile* MotorAction::get_from()
{
    return m_from;
}

// Returns the Tile the TileObject wants to reach
Tile* MotorAction::get_to()
{
    return m_to;
}

// Returns the Direction the TileObject wills to go
Coordinates::CARDINAL_DIRECTION MotorAction::get_cadir()
{
    return m_cadir;
}

// Add one unit of time to the duration since the MotorAction was executed
// Returns the current age after incrementation
unsigned int MotorAction::age()
{
    return ++m_age;
}

// Gets the duration since the MotorAction was executed
unsigned int MotorAction::get_age()
{
    return m_age;
}
