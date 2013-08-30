#ifndef MAP_H
#define MAP_H

#include <game/component.h>
#include <gameplay/coordinates.h>

class Tile;

class Map : Component
{
    public:
        Map(unsigned short, unsigned short);
        virtual ~Map();

        Tile* get_tile(const Coordinates&) const;
        Tile* get_tile_neighbor(const Coordinates&, Coordinates::CARDINAL_DIRECTION) const;
        unsigned char get_nb_neighbors(const Coordinates& p_coords) const;
    protected:
    private:
        unsigned short m_width;
        unsigned short m_length;

        Tile*** m_matrix;
};

#endif // MAP_H
