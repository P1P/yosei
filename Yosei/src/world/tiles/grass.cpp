#include "world\tiles\grass.h"

Grass::Grass(std::string p_name, Coordinates* p_coords) : Tile(p_name, p_coords)
{
    this->m_appearance = "grass";
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
    return "Grass    " + Tile::to_string();
}

std::string Grass::base_decoration() const
{
    return "-";
}
