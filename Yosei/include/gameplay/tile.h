#ifndef TILE_H
#define TILE_H

#include <game/component.h>
#include <gameplay/map.h>

class Tile : Component
{
    public:
        Tile(std::string);
        virtual ~Tile();

        virtual void start();
        virtual void update();

        Tile* operator+(Coordinates::CARDINAL_DIRECTION) const;
        unsigned char get_nb_neighbors() const;
    protected:
    private:
        Tile* get_neighbor(Coordinates::CARDINAL_DIRECTION) const;

        Coordinates* m_coordinates;
        Map* m_map;
        //Tile* m_neighbors;
};

#endif // TILE_H
