#include "world/world.h"

#include <cstdlib>
#include <time.h>

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
    lengths[0] = 10;
    lengths[1] = 10;

    m_map = new Map(lengths);

    build_world(time(NULL));

    simple_add_yosei(5, 5, "A");

    for (int y = 0; y < m_map->get_lengths()[1]; ++y)
    {
        for (int x = 0; x < m_map->get_lengths()[0]; ++x)
        {
            if (x % 2 == 0 && y % 2 == 0)
            {
                if (!((x == lengths[0] / 4 || y == lengths[1] / 4)))
                {
                    simple_add_rock(x, y, "R");
                }
            }
        }
    }

    Observer::getInstance().out(Observer::GAMEPLAY, m_map->to_string());

}

void World::update()
{
    Observer::getInstance().out(Observer::SUPER_VERBOSE, "Update " + to_string());

    // Yosei-specific updates
    for (std::list<Yosei*>::iterator it = m_yosei_population.begin(); it != m_yosei_population.end(); ++it)
    {
        // Realizes a Yosei's will for Motor Actions
        while (MotorAction* motor_action = (*it)->get_will()->perceive_action_motor())
        {
            move_tobject((*it), motor_action->get_cadir());
            delete motor_action;
        }

        // Provides a Yosei's perception with its neighboring tiles
        Coordinates::CARDINAL_DIRECTION* directions = (*it)->get_ordered_directions();
        for (int i = 0; i < Coordinates::CARDINAL_DIRECTION::COUNT; ++i)
        {
            Coordinates::CARDINAL_DIRECTION cadir = directions[i];//static_cast<Coordinates::CARDINAL_DIRECTION>(i);

            if (Tile* neigh_tile = m_map->get_tile_neighbor(*((*it)->get_tile()), cadir))
            {
                (*it)->get_perception()->push_stimulus_vision_tile(new VisionTile(cadir, neigh_tile));
            }
        }

        delete directions;
    }

    // Updates every component
    for (std::list<Component*>::iterator it = m_lst_components.begin(); it != m_lst_components.end(); it++)
    {
        if ((*it)->is_active())
        {
            (*it)->update();
        }
    }

    Observer::getInstance().out(Observer::GAMEPLAY, m_map->to_string());
}

// Moves a tobject to a Tile towards cadir
// Returns false if Tile is invalid or already has a TileObject
bool World::move_tobject(TileObject* p_tobject, Coordinates::CARDINAL_DIRECTION p_cadir)
{
    if (Tile* next_tile = m_map->get_tile_neighbor(*(p_tobject->get_tile()), p_cadir))
    {
        if (next_tile->get_tobject() == nullptr)
        {
            p_tobject->get_tile()->remove_tobject();
            next_tile->place_tobject(p_tobject);
            p_tobject->set_direction(p_cadir);
            return true;
        }
    }
    return false;
}

// Starts a component and ensures its update every frame
void World::add_component(Component* p_component)
{
    m_lst_components.push_back(p_component);
    p_component->start();
}

// Finds, removes and deletes a component
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

// Builds the world for the given seed
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

            if (x == lengths[0] / 4 || y == lengths[1] / 4)
            {
                Lava* lava = new Lava(SSTR(rand() % 10), coords);
                m_map->insert_tile(*lava);
                add_component(lava);
            }
            else
            {
                Grass* grass = new Grass(SSTR(rand() % 10), coords);
                m_map->insert_tile(*grass);
                add_component(grass);
            }
        }
    }
}

// Macro function to add a dummy Yosei at Coord(p_x, p_y) nammed p_name
bool World::simple_add_yosei(unsigned short p_x, unsigned short p_y, std::string p_name)
{
    unsigned short* dimensions = new unsigned short[2];
    dimensions[0] = p_x;
    dimensions[1] = p_y;
    Coordinates* coords = new Coordinates(dimensions, m_map->get_lengths(), 2);
    Tile* tile = m_map->get_tile(*coords);

    if (tile->get_tobject() == nullptr)
    {
        Yosei* yosei = new Yosei(p_name, tile, new Personality(0, 0, 0));

        add_component(yosei);
        tile->place_tobject(yosei);
        m_yosei_population.push_back(yosei);

        return true;
    }
    delete coords;

    return false;
}

// Macro function to add a dummy Rock at Coord(p_x, p_y) nammed p_name
bool World::simple_add_rock(unsigned short p_x, unsigned short p_y, std::string p_name)
{
    unsigned short* dimensions = new unsigned short[2];
    dimensions[0] = p_x;
    dimensions[1] = p_y;
    Coordinates* coords = new Coordinates(dimensions, m_map->get_lengths(), 2);
    Tile* tile = m_map->get_tile(*coords);

    if (tile->get_tobject() == nullptr)
    {
        Rock* rock = new Rock(p_name, tile);

        add_component(rock);
        tile->place_tobject(rock);

        return true;
    }
    delete coords;

    return false;
}

std::string World::to_string() const
{
    return m_name;
}
