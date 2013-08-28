#include "gameplay/world.h"

World::World() : Component("World")
{

}

World::~World()
{

}

void World::start()
{
    Observer::getInstance().out(Observer::VERBOSE, "Start " + to_string());
}

void World::update()
{
    Observer::getInstance().out(Observer::SUPER_VERBOSE, "Update " + to_string());
}
