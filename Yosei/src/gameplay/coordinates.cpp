#include "gameplay\coordinates.h"

Coordinates::Coordinates(unsigned short p_x, unsigned short p_y, unsigned short p_width, unsigned short p_length)
{
    m_dimensions = new unsigned short[DIMENSIONS];
    m_lenghts = new unsigned short[DIMENSIONS];

    m_dimensions[0] = p_x;
    m_dimensions[1] = p_y;
    m_lenghts[0] = p_width;
    m_lenghts[1] = p_length;

    assert(is_valid(m_dimensions));
}

bool Coordinates::go(CARDINAL_DIRECTION p_cadir)
{
    unsigned short* candidate = new unsigned short[DIMENSIONS];

    for (int i = 0; i < DIMENSIONS; ++i)
    {
        candidate[i] = m_dimensions[i] + cadir_to(p_cadir, i);
    }

    if (!is_valid(candidate))
    {
        return false;
    }

    m_dimensions = candidate;

    return true;
}

bool Coordinates::is_within_bounds(CARDINAL_DIRECTION p_cadir) const
{
    unsigned short* candidate = new unsigned short(DIMENSIONS);

    for (int i = 0; i < DIMENSIONS; ++i)
    {
        candidate[i] = m_dimensions[DIMENSIONS] + cadir_to(p_cadir, i);
    }

    return (is_valid(candidate));
}

bool Coordinates::is_valid(unsigned short* p_dimensions) const
{
    for (int i = 0; i < DIMENSIONS; ++i)
    {
        if (p_dimensions[i] < 0) return false;
        if (p_dimensions[i] >= m_lenghts[i]) return false;
    }

    return true;
}

unsigned short Coordinates::cadir_to(CARDINAL_DIRECTION p_cadir, unsigned char p_dimension) const
{
    if (p_dimension == 0)
    {
        switch (p_cadir)
        {
            case NORTH: return 0;
            case EAST: return 1;
            case SOUTH: return 0;
            case WEST: return -1;
        }
    }
    else if (p_dimension == 1)
    {
        switch (p_cadir)
        {
            case NORTH: return 1;
            case EAST: return 0;
            case SOUTH: return -1;
            case WEST: return 0;
        }
    }

    assert(0);
    return 0;
}

Coordinates::~Coordinates()
{
    //dtor
}
