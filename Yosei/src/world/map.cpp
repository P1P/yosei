#include "world/map.h"

Map::Map(unsigned short* p_lenghts) : Component("Map")
{
    m_lengths = p_lenghts;

    m_matrix = new Tile**[m_lengths[0]];

    for (int x = 0; x < m_lengths[0]; ++x)
    {
        m_matrix[x] = new Tile*[m_lengths[1]];

        for (int y = 0; y < m_lengths[1]; ++y)
        {
            m_matrix[x][y] = nullptr;
        }
    }
}

Map::~Map()
{
    for (int y = 0; y < m_lengths[0]; ++y)
    {
        delete m_matrix[y];
    }

    delete m_matrix;
}

void Map::start()
{

}

void Map::update()
{

}

// Inserts a tile to an empty slot in the matrix
void Map::insert_tile(Tile& p_tile)
{
    const Coordinates& coords = p_tile.get_coordinates();

    Tile* slot = get_tile(coords);
    if (slot != nullptr)
    {
         remove_tile(slot->get_coordinates());
    }

    slot = &p_tile;
    m_matrix[coords.m_dimensions[0]][coords.m_dimensions[1]] = &p_tile;
}

// Removes the tile at p_coords and sets the pointer in the matrix to nullptr
// Returns null if no tile to remove
bool Map::remove_tile(const Coordinates& p_coords)
{
    Tile* slot = get_tile(p_coords);
    if (slot == nullptr)
    {
        return false;
    }
    delete slot;
    slot = nullptr;
    return true;
}

// Returns the tile at p_tile's coords
// Returns nullptr otherwise
Tile* Map::get_tile(const Coordinates& p_coords) const
{
    return m_matrix[p_coords.m_dimensions[0]][p_coords.m_dimensions[1]];
}

// Returns the tile at p_tile's coords + p_cadir
// Returns nullptr otherwise
Tile* Map::get_tile_neighbor(const Tile& p_tile, Coordinates::CARDINAL_DIRECTION p_cadir) const
{
    return get_tile_neighbor(p_tile.get_coordinates(), p_cadir);
}

// Returns the tile at p_coords + p_cadir
// Returns nullptr otherwise
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

// Returns the number of neighbor tiles of p_coords
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

// Returns the lengths of the map
const unsigned short* Map::get_lengths() const
{
    return m_lengths;
}

std::string Map::to_string() const
{
    std::string res = "";

    for (int y = 0; y < m_lengths[1]; ++y)
    {
        for (int x = 0; x < m_lengths[0]; ++x)
        {
            res += m_matrix[x][y]->left_bracket() + m_matrix[x][y]->short_to_string() + m_matrix[x][y]->right_bracket();
        }
        res += "\n";
    }

    return res;
}
