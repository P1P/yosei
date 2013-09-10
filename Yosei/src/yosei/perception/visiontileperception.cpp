#include "yosei/perception/visiontileperception.h"

VisionTilePerception::VisionTilePerception(Coordinates::CARDINAL_DIRECTION p_cadir, Tile* p_tile)
{
    m_cadir = p_cadir;
    m_tile = p_tile;
}

VisionTilePerception::~VisionTilePerception()
{

}

// Returns the Tile perceived
Tile* VisionTilePerception::get_tile()
{
    return m_tile;
}

// Returns the cadir where the Tile is perceived
Coordinates::CARDINAL_DIRECTION VisionTilePerception::get_cadir()
{
    return m_cadir;
}
