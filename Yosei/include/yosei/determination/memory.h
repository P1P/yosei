#ifndef MEMORY_H
#define MEMORY_H

#include <list>

#include <yosei/will/motoraction.h>

class Memory
{
    public:
        Memory();
        virtual ~Memory();

        void add_motor_action(MotorAction*);
        void age();

        std::string to_string();
    protected:
    private:
        std::list<MotorAction*> m_history_motor_actions;
};

#endif // MEMORY_H
