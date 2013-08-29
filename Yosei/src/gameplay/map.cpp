#include "gameplay/map.h"

Map::Map(unsigned short p_width, unsigned short p_length) : Component("Map")
{
    m_matrix = new Tile**[p_width];

    for (int x = 0; x < p_width; ++x)
    {
        m_matrix[x] = new Tile*[p_length];

        for (int y = 0; y < p_length; ++y)
        {
            m_matrix[x][y] = new Tile("Dummy Tile");
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

Tile* Map::get_tile(Coordinates p_coords)
{
    return m_matrix[p_coords.m_dimensions[0]][p_coords.m_dimensions[1]];
}

Tile* Map::get_tile_neighbors(Tile* tile)
{
    return new Tile("bob");
}
