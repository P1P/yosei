#include "world\tiles\lava.h"

Lava::Lava(std::string p_name, Coordinates* p_coords) : Tile(p_name, p_coords)
{

}

Lava::~Lava()
{

}

void Lava::start()
{

}

void Lava::update()
{
    if (m_tobject != nullptr)
    {
        m_tobject->burn();
    }
}

std::string Lava::to_string() const
{
    return "Lava     " + Tile::to_string();
}

std::string Lava::base_decoration() const
{
    return "~";
}
