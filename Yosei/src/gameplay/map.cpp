#include "gameplay/map.h"

Map::Map(unsigned short p_width, unsigned short p_length) : Component("Map")
{
    m_matrix = new Tile**[p_width];

    for (int x = 0; x < p_width; ++x)
    {
        m_matrix[x] = new Tile*[p_length];

        for (int y = 0; y < p_length; ++y)
        {
            //m_matrix[x][y] = new Tile("Dummy Tile");
        }
    }
}

Map::~Map()
{
    for (int y = 0; y < m_width; ++y)
    {
        delete m_matrix[y];
    }

    delete m_matrix;
}

Tile* Map::get_tile(const Coordinates& p_coords) const
{
    return m_matrix[p_coords.m_dimensions[0]][p_coords.m_dimensions[1]];
}

Tile* Map::get_tile_neighbor(const Coordinates& p_coords, Coordinates::CARDINAL_DIRECTION p_cadir) const
{
    if (Coordinates* peek = p_coords + p_cadir)
    {
        Tile* res = get_tile(*peek);
        delete(peek);
        return res;
    }
    return nullptr;
}

unsigned char Map::get_nb_neighbors(const Coordinates& p_coords) const
{
    unsigned char count = 0;
    for (int i = 0; i < Coordinates::CARDINAL_DIRECTION::COUNT; ++i)
    {
        if (p_coords.peek_dimensions_valid((Coordinates::CARDINAL_DIRECTION)(i)))
        {
            count++;
        }
    }
    return count;
}
