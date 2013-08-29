#ifndef MAP_H
#define MAP_H

#include <game/component.h>
#include <gameplay/tile.h>
#include <gameplay/coordinates.h>

class Map : Component
{
    public:
        Map(unsigned short, unsigned short);
        virtual ~Map();

        Tile* get_tile(Coordinates);
        Tile* get_tile_neighbors(Tile*);
    protected:
    private:
        unsigned short m_width;
        unsigned short m_length;

        Tile*** m_matrix;
};

#endif // MAP_H
