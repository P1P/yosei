#include "world/tileobjects/rock.h"

Rock::Rock(std::string p_name, Tile* p_tile) : TileObject(p_name, p_tile)
{

}

Rock::~Rock()
{

}

void Rock::start()
{

}

void Rock::update()
{

}

std::string Rock::to_string() const
{
    return "Rock " + m_name;
}

std::string Rock::short_to_string() const
{
    return m_name;
}
