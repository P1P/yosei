#ifndef WILL_H
#define WILL_H

#include <queue>

#include <yosei/will/motoraction.h>

class Will
{
    public:
        Will();
        virtual ~Will();

        MotorAction* perceive_action_motor();
        void push_action_motor(MotorAction*);
    protected:
    private:
        std::queue<MotorAction*> m_action_motor;
};

#endif // WILL_H
