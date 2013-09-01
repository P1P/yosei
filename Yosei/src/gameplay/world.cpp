#include "gameplay/world.h"

#include <cstdlib>

World::World() : Component("World")
{

}

World::~World()
{

}

void World::start()
{
    Observer::getInstance().out(Observer::VERBOSE, "Start " + to_string());

    unsigned short* lengths = new unsigned short[2];
    lengths[0] = 5;
    lengths[1] = 5;

    m_map = new Map(lengths);

    build_world(0);

    unsigned short* dimensions = new unsigned short[2];
    dimensions[0] = 2;
    dimensions[1] = 2;
    Coordinates* coords = new Coordinates(dimensions, lengths, 2);
    Tile* tile = m_map->get_tile(*coords);

    add_component(m_tobject = new Yosei("A", tile, 0, 0, 0));

    tile->place_tobject(m_tobject);

    Observer::getInstance().out(Observer::GAMEPLAY, m_map->to_string());
}

void World::update()
{
    Observer::getInstance().out(Observer::SUPER_VERBOSE, "Update " + to_string());

    for (std::list<Component*>::iterator it = m_lst_components.begin(); it != m_lst_components.end(); it++)
    {
        if ((*it)->is_active())
        {
            (*it)->update();
        }
    }

    Observer::getInstance().out(Observer::SUPER_VERBOSE, "Update " + to_string());

    move_tobject(m_tobject, static_cast<Coordinates::CARDINAL_DIRECTION>(rand() % Coordinates::CARDINAL_DIRECTION::COUNT));

    Observer::getInstance().out(Observer::GAMEPLAY, m_map->to_string());
}

bool World::move_tobject(TileObject* p_tobject, Coordinates::CARDINAL_DIRECTION p_cadir)
{
    if (Tile* next_tile = m_map->get_tile_neighbor(*(p_tobject->get_tile()), p_cadir))
    {
        p_tobject->get_tile()->remove_tobject();
        next_tile->place_tobject(p_tobject);
        return true;
    }
    return false;
}

void World::add_component(Component* p_component)
{
    m_lst_components.push_back(p_component);
    p_component->start();
}

void World::remove_component(Component* p_component)
{
    Observer::getInstance().out(Observer::VERBOSE, "Attempting to remove " + p_component->to_string());
    for (std::list<Component*>::iterator it = m_lst_components.begin(); it != m_lst_components.end(); it++)
    {
        if ((*it) == p_component)
        {
            Observer::getInstance().out(Observer::VERBOSE, "Removing " + p_component->to_string());
            m_lst_components.erase(it);
            delete(*it);
            break;
        }
    }
}

void World::build_world(unsigned int p_seed)
{
    srand(p_seed);

    const unsigned short* const lengths = m_map->get_lengths();

    for (int x = 0; x < lengths[0]; ++x)
    {
        for (int y = 0; y < lengths[1]; ++y)
        {
            unsigned short* dimensions = new unsigned short[2];
            dimensions[0] = x;
            dimensions[1] = y;
            Coordinates* coords = new Coordinates(dimensions, lengths, 2);
            Tile* tile = new Tile(SSTR(rand() % 10), coords);
            m_map->insert_tile(*tile);
            add_component(tile);
        }
    }
}

std::string World::to_string() const
{
    return m_name;
}
