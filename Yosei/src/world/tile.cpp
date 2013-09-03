#include "world//tile_tileobject.h"

Tile::Tile(std::string p_name, Coordinates* p_coords) : Component(p_name)
{
    m_coordinates = p_coords;
    m_tobject = nullptr;
}

Tile::~Tile()
{
    delete m_coordinates;
}

void Tile::start()
{

}

void Tile::update()
{

}

// Returns the coordinates of the Tile
const Coordinates& Tile::get_coordinates() const
{
    return *m_coordinates;
}

// Returns the TileObject placed on the Tile
// Returns nullptr if there is no TileObject
TileObject* Tile::get_tobject() const
{
    return m_tobject;
}

// Places a TileObject on the Tile
// Requires the Tile not to have a TileObject on it
void Tile::place_tobject(TileObject* p_tobject)
{
    assert(m_tobject == nullptr);
    m_tobject = p_tobject;
    m_tobject->set_tile(this);
}

// Removes the TileObject on the Tile
// Has no effect if Tile doesn't have a TileObject
void Tile::remove_tobject()
{
    if (m_tobject)
    {
        m_tobject->set_tile(nullptr);
        m_tobject = nullptr;
    }
}

std::string Tile::to_string() const
{
    return m_coordinates->to_string() + " " + m_name + (m_tobject != nullptr ? " " + m_tobject->to_string() : "");
}

std::string Tile::short_to_string() const
{
    return m_tobject != nullptr ? m_tobject->short_to_string() : " ";
}
