#include "yosei/will/will.h"

Will::Will()
{

}

Will::~Will()
{

}

// Pops and returns the oldest Motor Action in the queue
// Returns nullptr if no Motor Action available
MotorAction* Will::perceive_action_motor()
{
    if (m_action_motor.empty())
    {
        return nullptr;
    }
    MotorAction* res = m_action_motor.front();
    m_action_motor.pop();
    return res;
}

// Pushes a Motor Action in the queue
void Will::push_action_motor(MotorAction* p_motor_action)
{
    m_action_motor.push(p_motor_action);
}
