#include "gameplay/action.h"

Action::Action(std::string p_name, void (*p_fct_action)(Yosei*, Yosei*)) : Identifiable(p_name)
{
    m_fct_action = p_fct_action;
}

Action::Action()
{

}

Action::Action(const Action& p_other) : Identifiable(p_other.m_name)
{
    m_fct_action = p_other.m_fct_action;
}

Action::~Action()
{
    //dtor
}

void Action::Do(Yosei* doing, Yosei* target)
{
    m_fct_action(doing, target);
}
