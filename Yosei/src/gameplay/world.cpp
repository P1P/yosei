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

    unsigned char nb_d = 2;
    unsigned short* dimensions = new unsigned short[nb_d];
    unsigned short* lengths = new unsigned short[nb_d];

    dimensions[0] = 5;
    dimensions[1] = 5;
    lengths[0] = 10;
    lengths[1] = 10;

    m_coords = new Coordinates(dimensions, lengths, nb_d);
}

void World::update()
{
    Observer::getInstance().out(Observer::SUPER_VERBOSE, "Update " + to_string());

    Observer::getInstance().out(Observer::GAMEPLAY, "To north! " + m_coords->to_string());

    Coordinates* peek = *m_coords + Coordinates::NORTH;

    Observer::getInstance().out(Observer::GAMEPLAY, peek->to_string());

    delete peek;
}
