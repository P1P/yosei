#ifndef COORDINATES_H
#define COORDINATES_H

#include <helpers/observer.h>

class Coordinates
{
    public:
        Coordinates(unsigned short*, const unsigned short*, unsigned char);
        virtual ~Coordinates();

        enum CARDINAL_DIRECTION { NORTH, EAST, SOUTH, WEST, COUNT };

        Coordinates* operator+=(CARDINAL_DIRECTION p_cadir);
        Coordinates* operator+(CARDINAL_DIRECTION p_cadir) const;
        bool operator==(const Coordinates& p_other) const;

        std::string to_string() const;
    protected:
    private:
        bool go(CARDINAL_DIRECTION);
        bool is_valid(unsigned short*) const;
        bool is_valid(unsigned short, unsigned char) const;
        unsigned short cadir_to(CARDINAL_DIRECTION, unsigned char) const;
        unsigned short* peek_dimensions(CARDINAL_DIRECTION) const;
        bool peek_dimensions_valid(CARDINAL_DIRECTION p_cadir) const;
        Coordinates* peek(CARDINAL_DIRECTION) const;

        unsigned char m_nb_dimensions;

        unsigned short* m_dimensions;
        const unsigned short* m_lengths;

        friend class Map;
};

#endif // COORDINATES_H
