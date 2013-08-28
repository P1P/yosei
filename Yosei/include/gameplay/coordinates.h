#ifndef COORDINATES_H
#define COORDINATES_H

#include <assert.h>

class Coordinates
{
    public:
        Coordinates(unsigned short, unsigned short, unsigned short, unsigned short);
        virtual ~Coordinates();

        enum CARDINAL_DIRECTION { NORTH = 0, EAST = 1, SOUTH = 2, WEST = 3 };

        bool go(CARDINAL_DIRECTION);
        bool is_within_bounds(CARDINAL_DIRECTION) const;
        bool is_valid(unsigned short*) const;
        unsigned short cadir_to(CARDINAL_DIRECTION, unsigned char) const;
    protected:
    private:
        static const unsigned char DIMENSIONS = 2;

        unsigned short* m_dimensions;
        unsigned short* m_lenghts;

        friend class Map;
};

#endif // COORDINATES_H
