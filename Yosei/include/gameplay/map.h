#ifndef MAP_H
#define MAP_H

#include <gameplay/tile.h>

class Map : Component
{
    public:
        Map(unsigned short*);
        virtual ~Map();

        void start();
        void update();

        void insert_tile(Tile&);
        bool remove_tile(const Coordinates&);

        Tile* get_tile(const Coordinates&) const;
        Tile* get_tile_neighbor(const Tile&, Coordinates::CARDINAL_DIRECTION) const;
        Tile* get_tile_neighbor(const Coordinates&, Coordinates::CARDINAL_DIRECTION) const;
        unsigned char get_nb_neighbors(const Coordinates& p_coords) const;
        const unsigned short* get_lengths() const;

        std::string to_string() const;
    protected:
    private:
        unsigned short* m_lengths;

        Tile*** m_matrix;
};

#endif // MAP_H
