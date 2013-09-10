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
    return "Lava " + m_coordinates->to_string() + " " + m_name + (m_tobject != nullptr ? " " + m_tobject->to_string() : "");
}

std::string Lava::short_to_string() const
{
    return m_tobject != nullptr ? m_tobject->short_to_string() : base_decoration();
}

std::string Lava::base_decoration() const
{
    return "~";
}
