#include "game/game.h"

Game::Game() : Component("Game")
{
    m_lst_components.clear();
}

Game::~Game()
{
    //dtor
}

void DummyActionA(Yosei* p_doing, Yosei* p_target)
{
    p_doing->please(1);
    p_target->please(1);
}

void DummyActionB(Yosei* p_doing, Yosei* p_target)
{
    p_doing->please(1);
    p_target->sadden(1);
}

void DummyActionC(Yosei* p_doing, Yosei* p_target)
{
    p_doing->please(0.25);
    p_target->please(0.25);
}

void Game::start()
{
    Observer::getInstance().out(Observer::VERBOSE, "Start " + to_string());

    m_lst_components.clear();

    m_world = new World();
    add_component(m_world);

    /*
    Yosei* c1 = new Yosei("Niceguy", 2, 0, 0);
    Yosei* c2 = new Yosei("Notsogood", 0.5, 25, 20);

    c1->m_other = c2;
    c2->m_other = c1;

    add_component(c1);
    add_component(c2);

    Action a1("Play", &DummyActionA);
    Action a2("Hurt", &DummyActionB);
    Action a3("Talk", &DummyActionC);

    c1->learn_action(a1);
    c1->learn_action(a2);
    c1->learn_action(a3);

    c2->learn_action(a1);
    c2->learn_action(a2);
    c2->learn_action(a3);
    */
}

void Game::update()
{
    Observer::getInstance().out(Observer::SUPER_VERBOSE, "Update " + to_string());

    for (std::list<Component*>::iterator it = m_lst_components.begin(); it != m_lst_components.end(); it++)
    {
        if ((*it)->is_active())
        {
            (*it)->update();
        }
    }
}

void Game::add_component(Component* p_component)
{
    m_lst_components.push_back(p_component);
    p_component->start();
}

void Game::remove_component(Component* p_component)
{
    Observer::getInstance().out(Observer::VERBOSE, "Attempting to remove " + p_component->to_string());
    for (std::list<Component*>::iterator it = m_lst_components.begin(); it != m_lst_components.end(); it++)
    {
        if ((*it) == p_component)
        {
            Observer::getInstance().out(Observer::VERBOSE, "Removing " + p_component->to_string());
            m_lst_components.erase(it);
            delete (*it);
            break;
        }
    }
}
