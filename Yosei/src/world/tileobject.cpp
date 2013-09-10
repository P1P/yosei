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

// Returns an array of Directions ordered depending on the direction faced by the TileObject
// e.g. when facing NORTH, SOUTH always comes last. The rest is random
Coordinates::CARDINAL_DIRECTION* TileObject::get_ordered_directions() const
{
    int* res_shuffle = new int[Coordinates::CARDINAL_DIRECTION::COUNT - 1];
    res_shuffle[0] = ((int)m_direction + 0 + Coordinates::CARDINAL_DIRECTION::COUNT) % Coordinates::CARDINAL_DIRECTION::COUNT;
    res_shuffle[1] = ((int)m_direction + 1 + Coordinates::CARDINAL_DIRECTION::COUNT) % Coordinates::CARDINAL_DIRECTION::COUNT;
    res_shuffle[2] = ((int)m_direction - 1 + Coordinates::CARDINAL_DIRECTION::COUNT) % Coordinates::CARDINAL_DIRECTION::COUNT;

    ContainerHelper::shuffle(res_shuffle, Coordinates::CARDINAL_DIRECTION::COUNT - 1);

    Coordinates::CARDINAL_DIRECTION* res = new Coordinates::CARDINAL_DIRECTION[Coordinates::CARDINAL_DIRECTION::COUNT];

    res[0] = static_cast<Coordinates::CARDINAL_DIRECTION>(res_shuffle[0]);
    res[1] = static_cast<Coordinates::CARDINAL_DIRECTION>(res_shuffle[1]);
    res[2] = static_cast<Coordinates::CARDINAL_DIRECTION>(res_shuffle[2]);
    res[3] = static_cast<Coordinates::CARDINAL_DIRECTION>(((int)m_direction + 2) % Coordinates::CARDINAL_DIRECTION::COUNT);

    delete res_shuffle;

    return res;
}

void TileObject::burn()
{

}
