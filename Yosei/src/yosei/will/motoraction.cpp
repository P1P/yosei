#include "yosei/will/motoraction.h"

MotorAction::MotorAction(TileObject* p_tobject, Coordinates::CARDINAL_DIRECTION p_cadir)
{
    m_tobject = p_tobject;
    m_cadir = p_cadir;
}

MotorAction::~MotorAction()
{

}

// Returns the TileObject source of the Motor Action
TileObject* MotorAction::get_tobject()
{
    return m_tobject;
}

// Returns the Direction the TileObject wills to go
Coordinates::CARDINAL_DIRECTION MotorAction::get_cadir()
{
    return m_cadir;
}
