#ifndef MEMORY_H
#define MEMORY_H

#include <list>
#include <map>

#include <yosei/determination/opinion.h>
#include <yosei/determination/action.h>

struct LessComparer
{
    bool operator()(Action* p_left, Action* p_right) const
    {
        return p_left->less_comparer(p_right);
    }
};

class Memory
{
    public:
        Memory();
        virtual ~Memory();

        void historize_action(Action*);
        void remember_actions(float, int);
        void age();

        bool should_do(Action*, const Personality*);
        bool should_do_over(Action*, Action*, const Personality*);

        Opinion* get_opinion(Action*) const;

        std::string to_string();
        std::string knowledge_to_string();
        std::string opinions_to_string();
    protected:
    private:
        std::list<Action*> m_history_actions;
        std::map<Action*, Opinion*, LessComparer> m_knowledge;

        void remember_action(Action*, float);
};

#endif // MEMORY_H
