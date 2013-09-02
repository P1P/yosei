#include "gameplay/tile_tileobject.h"

TileObject::TileObject(std::string p_name, Tile* p_tile) : Component(p_name)
{
    set_tile(p_tile);
}

TileObject::~TileObject()
{
    //dtor
}

void TileObject::set_tile(Tile* p_tile)
{
    m_tile = p_tile;
}

Tile* TileObject::get_tile() const
{
    return m_tile;
}
