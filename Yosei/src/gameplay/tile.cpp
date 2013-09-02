#include "gameplay/tile_tileobject.h"

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

const Coordinates& Tile::get_coordinates() const
{
    return *m_coordinates;
}

TileObject* Tile::get_tobject() const
{
    return m_tobject;
}

void Tile::place_tobject(TileObject* p_tobject)
{
    assert(m_tobject == nullptr);
    m_tobject = p_tobject;
    m_tobject->set_tile(this);
}

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
