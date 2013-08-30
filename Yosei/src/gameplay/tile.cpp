#include "gameplay/tile.h"

Tile::Tile(std::string p_name) : Component(p_name)
{
    //ctor
}

Tile::~Tile()
{
    //dtor
}

void Tile::start()
{

}

void Tile::update()
{

}

Tile* Tile::operator+(Coordinates::CARDINAL_DIRECTION p_cadir) const
{
    return get_neighbor(p_cadir);
}

Tile* Tile::get_neighbor(Coordinates::CARDINAL_DIRECTION p_cadir) const
{
    return m_map->get_tile_neighbor((*m_coordinates), p_cadir);
}

unsigned char Tile::get_nb_neighbors() const
{
    return m_map->get_nb_neighbors(*m_coordinates);
}
