#include "yosei/yosei.h"

Yosei::Yosei(std::string p_name, Tile* p_tile, Personality* p_personality) : TileObject(p_name, p_tile)
{
    m_personality = p_personality;
    m_mental_state = new MentalState(p_name + "'s mental state");
    m_perception = new Perception();
    m_will = new Will();
    m_memory = new Memory();
}

Yosei::Yosei() : TileObject("Dummy", nullptr)
{

}

Yosei::~Yosei()
{
    /*
    for (std::map<Action*, Opinion>::iterator it = m_knowledge.begin(); it != m_knowledge.end(); ++it)
    {
        delete it->first;
    }
    */
}

void Yosei::start()
{
    Observer::getInstance().out(Observer::VERBOSE, "Start " + to_string());
}

void Yosei::update()
{
    MotorAction* motor_action_decision = nullptr;
    while (VisionTilePerception* stimulus_vision_tile = m_perception->perceive_stimulus_vision_tile())
    {
        if (motor_action_decision == nullptr)
        {
            Tile* tile = stimulus_vision_tile->get_tile();
            if (tile->get_tobject() == nullptr)
            {
                motor_action_decision = new MotorAction(this->get_tile(), tile, stimulus_vision_tile->get_cadir());
                if (!m_memory->should_do(motor_action_decision, m_personality))
                {
                    delete motor_action_decision;
                    motor_action_decision = nullptr;
                }
            }
        }
        delete stimulus_vision_tile;
    }

    if (motor_action_decision)
    {
        m_will->push_action_motor(motor_action_decision);
    }

    while (PainPerception* stimulus_pain = m_perception->perceive_stimulus_pain())
    {
        Observer::getInstance().out(Observer::GAMEPLAY, "Ouch");
        m_memory->remember_actions(-stimulus_pain->get_pain());

        delete stimulus_pain;
    }

    Observer::getInstance().out_say(Observer::GAMEPLAY, m_memory->to_string());

    get_memory()->age();

    /*
    Observer::getInstance().out_highlight(Observer::GAMEPLAY, to_string() + " is thinking");
    Observer::getInstance().out_say(Observer::GAMEPLAY, "My mental state is " + m_mental_state.to_string());

    for (std::map<Action*, Opinion>::iterator it = m_knowledge.begin(); it != m_knowledge.end(); ++it)
    {
        Yosei* target = m_other;
        Observer::getInstance().out_say(Observer::GAMEPLAY, "I am going to " + (it->first)->to_string() + " " + m_other->to_string() + ". My " + (it->second).to_string());
        if (should_do((it->second).get_inclination()))
        {
            float target_pre_score = target->get_score();
            float own_pre_score = this->get_score();

            (it->first)->Do(this, target);

            immediate_reflect_upon((it->first), target, target_pre_score, own_pre_score);

            Observer::getInstance().out_say(Observer::GAMEPLAY, "I now have an " + (it->second).to_string());
        }
        else
        {
            Observer::getInstance().out_say(Observer::GAMEPLAY, "I shouldn't do " + (it->first)->to_string() + ".");
        }
    }

    Observer::getInstance().out_say(Observer::GAMEPLAY, "I am " + m_mental_state.to_string());
    */
}

Perception* Yosei::get_perception()
{
    return m_perception;
}

Will* Yosei::get_will()
{
    return m_will;
}

MentalState* Yosei::get_mental_state()
{
    return m_mental_state;
}

Memory* Yosei::get_memory()
{
    return m_memory;
}
/*
void Yosei::immediate_reflect_upon(Action* p_action, Yosei* p_yosei, float p_target_pre_score, float p_own_pre_score)
{
    // Reflect upon the results for the target
    reflect_upon(p_action, p_yosei, judge(p_yosei, p_target_pre_score));

    // Reflect upon the results for myself, if I wasn't the target
    if (p_yosei != this)
    {
        reflect_upon(p_action, this, judge(this, p_own_pre_score));
    }
}

void Yosei::learn_action(const Action& p_action)
{
    m_knowledge.insert(std::pair<Action*, Opinion>(new Action(p_action), Opinion("Opinion of " + p_action.to_string())));
    Observer::getInstance().out(Observer::VERBOSE, to_string() + " has learned to " + p_action.to_string());
}
*/
float Yosei::get_score() const
{
    return m_mental_state->get_value();
}

float Yosei::judge(const Yosei* p_target, float p_pre_score) const
{
    return (p_target->get_score() - p_pre_score);
}
/*
void Yosei::reflect_upon(const Action* p_action, const Yosei* p_target, float p_judgement)
{
    bool self_action = p_target == this;

    if (p_judgement == 0)
    {
        Observer::getInstance().out_say(Observer::GAMEPLAY, "I think doing " + p_action->to_string() + " has done nothing for " + (self_action ? "me" : "him") + " (" + SSTR(p_judgement) + ")");
    }
    else if (p_judgement > 0)
    {
        Observer::getInstance().out_say(Observer::GAMEPLAY, "I think doing " + p_action->to_string() + " was good for " + (self_action ? "me" : "him") + " (" + SSTR(p_judgement) + ")");
    }
    else
    {
        Observer::getInstance().out_say(Observer::GAMEPLAY, "I think doing " + p_action->to_string() + " was bad for " + (self_action ? "me" : "him") + " (" + SSTR(p_judgement) + ")");
    }

    m_knowledge.at(const_cast<Action*>(p_action)).offset_value(self_action ? p_judgement : p_judgement * m_personality->get_compassion());
}
*/

void Yosei::burn()
{
    get_perception()->push_stimulus_pain(new PainPerception(1));
}

std::string Yosei::to_string() const
{
    return ("Yosei " + Component::to_string());
}

std::string Yosei::short_to_string() const
{
    return (Component::to_string());
}
