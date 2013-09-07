#include "world\tiles\grass.h"

Grass::Grass(std::string p_name, Coordinates* p_coords) : Tile(p_name, p_coords)
{

}

Grass::~Grass()
{

}

void Grass::start()
{

}

void Grass::update()
{

}

std::string Grass::to_string() const
{
    return "Grass " + m_coordinates->to_string() + " " + m_name + (m_tobject != nullptr ? " " + m_tobject->to_string() : "");
}

std::string Grass::short_to_string() const
{
    return m_tobject != nullptr ? m_tobject->short_to_string() : base_decoration();
}

std::string Grass::base_decoration() const
{
    return "-";
}
