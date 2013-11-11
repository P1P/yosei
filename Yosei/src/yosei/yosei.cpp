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
    Observer::getInstance().out_highlight(Observer::GAMEPLAY, "I am at " + get_tile()->to_string());

    MotorAction* best_motor_action_decision = nullptr;

    // Let's try and see which direction is the best

    std::vector<VisionTilePerception*> lst_stimuli_vision_tile;
    std::vector<MotorAction*> lst_motor_actions;

    // Parse the neighboring tiles we can see
    while (VisionTilePerception* stimulus_vision_tile = m_perception->perceive_stimulus_vision_tile())
    {
        lst_stimuli_vision_tile.push_back(stimulus_vision_tile);
        if ((stimulus_vision_tile->get_tile()->get_tobject() == nullptr) || (stimulus_vision_tile->get_cadir() == Coordinates::STILL))
        {
            lst_motor_actions.push_back(new MotorAction(this->get_tile(), stimulus_vision_tile->get_tile(), stimulus_vision_tile->get_cadir()));
        }
    }

    Observer::getInstance().out_highlight(Observer::GAMEPLAY, "I can see");
    for (std::vector<VisionTilePerception*>::iterator it_vision = lst_stimuli_vision_tile.begin(); it_vision != lst_stimuli_vision_tile.end(); ++it_vision)
    {
        display_vision((*it_vision), lst_motor_actions);
    }

    // The first option is standing still
    best_motor_action_decision = new MotorAction(this->get_tile(), this->get_tile(), Coordinates::STILL);

    Observer::getInstance().out_highlight(Observer::GAMEPLAY, "I think that");
    for (std::vector<MotorAction*>::iterator it = lst_motor_actions.begin(); it != lst_motor_actions.end(); ++it)
    {
        // See whether this action is better than the previous best
        if (m_memory->should_do_over((*it), best_motor_action_decision, m_personality))
        {
            Observer::getInstance().out(Observer::GAMEPLAY,(*it)->to_string() + " > " + best_motor_action_decision->to_string());
            delete(best_motor_action_decision);
            best_motor_action_decision = (*it);
        }
        else
        {
            Observer::getInstance().out(Observer::GAMEPLAY,(*it)->to_string() + " < " + best_motor_action_decision->to_string());
            delete(*it);
        }
    }

    for (std::vector<VisionTilePerception*>::iterator it = lst_stimuli_vision_tile.begin(); it != lst_stimuli_vision_tile.end(); ++it)
    {
        delete(*it);
    }
    lst_stimuli_vision_tile.clear();
    lst_motor_actions.clear();

    // If applicable, put the Motor Action in Will
    if (best_motor_action_decision)
    {
        Observer::getInstance().out_highlight(Observer::GAMEPLAY, "I choose to");
        Observer::getInstance().out_highlight(Observer::GAMEPLAY, best_motor_action_decision->to_string());

        // NB: standing still is an action, because it needs to be feedbacked too
        m_will->push_action_motor(best_motor_action_decision);
    }

    // Add feedback to last actions
    while (PainPerception* stimulus_pain = m_perception->perceive_stimulus_pain())
    {
        Observer::getInstance().out(Observer::GAMEPLAY, "Ouch");
        m_memory->remember_actions(-stimulus_pain->get_pain(), 1);

        delete stimulus_pain;
    }

    Observer::getInstance().out_highlight(Observer::GAMEPLAY, "I did");
    Observer::getInstance().out(Observer::GAMEPLAY, m_memory->knowledge_to_string());

    Observer::getInstance().out_highlight(Observer::GAMEPLAY, "I think that");
    Observer::getInstance().out(Observer::GAMEPLAY, m_memory->opinions_to_string());

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

void Yosei::display_vision(VisionTilePerception* p_vision, std::vector<MotorAction*> p_actions) const
{
    bool found = false;
    Observer::getInstance().out_continued(Observer::GAMEPLAY, Coordinates::cadir_short_to_string(p_vision->get_cadir()) + " " + p_vision->get_tile()->to_string() + " ");
    for (std::vector<MotorAction*>::iterator it_motor = p_actions.begin(); it_motor != p_actions.end(); ++it_motor)
    {
        if ((*it_motor)->get_cadir() == (p_vision)->get_cadir())
        {
            found = true;
            if (Opinion* opinion = m_memory->get_opinion((*it_motor)))
            {
                Observer::getInstance().out(Observer::GAMEPLAY, opinion->to_string());
            }
            else
            {
                Observer::getInstance().out(Observer::GAMEPLAY, "No opinion");
            }
        }
    }
    if (!found)
    {
        Observer::getInstance().out(Observer::GAMEPLAY, "Unable to go");
    }
}

std::string Yosei::to_string() const
{
    return ("Yosei " + Component::to_string());
}

std::string Yosei::short_to_string() const
{
    return (Component::to_string());
}
