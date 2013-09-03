#include "yosei/perception/visiontile.h"

VisionTile::VisionTile(Coordinates::CARDINAL_DIRECTION p_cadir, Tile* p_tile)
{
    m_cadir = p_cadir;
    m_tile = p_tile;
}

VisionTile::~VisionTile()
{

}

// Returns the Tile perceived
Tile* VisionTile::get_tile()
{
    return m_tile;
}

// Returns the cadir where the Tile is perceived
Coordinates::CARDINAL_DIRECTION VisionTile::get_cadir()
{
    return m_cadir;
}
