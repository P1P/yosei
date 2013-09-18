#include "world/tile_tileobject.h"
#include "helpers/containerhelper.h"

#include <cstdlib>

TileObject::TileObject(std::string p_name, Tile* p_tile) : Component(p_name)
{
    set_tile(p_tile);
    m_direction = static_cast<Coordinates::CARDINAL_DIRECTION>(rand() % Coordinates::CARDINAL_DIRECTION::COUNT);
}

TileObject::~TileObject()
{

}

// Sets the Tile upon which the TileObject is placed
void TileObject::set_tile(Tile* p_tile)
{
    m_tile = p_tile;
}

// Returns the Tile upon which the TileObject is placed
Tile* TileObject::get_tile() const
{
    return m_tile;
}

// Sets the Direction the TileObject is facing
void TileObject::set_direction(Coordinates::CARDINAL_DIRECTION p_cadir)
{
    m_direction = p_cadir;
}

// Gets the Direction the TileObject is facing
Coordinates::CARDINAL_DIRECTION TileObject::get_direction() const
{
    return m_direction;
}

void TileObject::burn()
{

}
