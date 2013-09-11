#include "world/coordinates.h"

Coordinates::Coordinates(unsigned short* p_dimensions, const unsigned short* p_lengths, unsigned char p_nb_dimensions)
{
    m_dimensions = p_dimensions;
    m_lengths = p_lengths;
    m_nb_dimensions = p_nb_dimensions;

    assert(is_valid(m_dimensions));
}

Coordinates::~Coordinates()
{
    delete m_dimensions;
}

// Returns a new peek towards p_cadir
// Returns nullptr if Direction is invalid
Coordinates* Coordinates::operator+(CARDINAL_DIRECTION p_cadir) const
{
    return peek(p_cadir);
}

// Attempts to go towards p_cadir
// Returns this if operation was successful, nullptr if coord didn't move
Coordinates* Coordinates::operator+=(CARDINAL_DIRECTION p_cadir)
{
    if (go(p_cadir))
    {
        return this;
    }
    return nullptr;
}

// Comparison operator
bool Coordinates::operator==(const Coordinates& p_other) const
{
    if (this->m_nb_dimensions != p_other.m_nb_dimensions)
    {
        return false;
    }
    for (int i = 0; i < m_nb_dimensions; ++i)
    {
        if (this->m_dimensions[i] != p_other.m_dimensions[i])
        {
            return false;
        }
        if (this->m_lengths[i] != p_other.m_lengths[i])
        {
            return false;
        }
    }
    return true;
}

// Modifies the Coordinates towards p_cadir
// Return false if Direction is invalid
bool Coordinates::go(CARDINAL_DIRECTION p_cadir)
{
    if (unsigned short* candidate_dimensions = peek_dimensions(p_cadir))
    {
        delete m_dimensions;
        m_dimensions = candidate_dimensions;
        return true;
    }
    return false;
}

// Returns a new pointer to the Coordinates towards p_cadir
// Returns nullptr if Direction is invalid
Coordinates* Coordinates::peek(CARDINAL_DIRECTION p_cadir) const
{
    if (unsigned short* candidate_dimensions = peek_dimensions(p_cadir))
    {
        return new Coordinates(candidate_dimensions, m_lengths, m_nb_dimensions);
    }
    return nullptr;
}

// Returns a new pointer to the dimensions towards p_cadir
// Returns nullptr if Direction is invalid
unsigned short* Coordinates::peek_dimensions(CARDINAL_DIRECTION p_cadir) const
{
    unsigned short* candidate_dimensions = new unsigned short[m_nb_dimensions];

    for (int i = 0; i < m_nb_dimensions; ++i)
    {
        candidate_dimensions[i] = m_dimensions[i] + cadir_to(p_cadir, i);
    }

    if (is_valid(candidate_dimensions))
    {
        return candidate_dimensions;
    }
    delete candidate_dimensions;
    return nullptr;
}

// Returns whether the Direction is valid
bool Coordinates::peek_dimensions_valid(CARDINAL_DIRECTION p_cadir) const
{
    for (int i = 0; i < m_nb_dimensions; ++i)
    {
        if (!is_valid(m_dimensions[i] + cadir_to(p_cadir, i), i)) return false;
    }

    return true;
}

// Returns whether p_dimensions is within lengths
bool Coordinates::is_valid(unsigned short* p_dimensions) const
{
    for (int i = 0; i < m_nb_dimensions; ++i)
    {
        if (!is_valid(p_dimensions[i], i)) return false;
    }

    return true;
}

// Returns whether a single dimension is within length
bool Coordinates::is_valid(unsigned short p_dimension_value, unsigned char p_dimension) const
{
    if (p_dimension_value < 0) return false;
    if (p_dimension_value >= m_lengths[p_dimension]) return false;

    return true;
}

// Returns the offset of a Cardinal Direction for p_dimension
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
            case NONE: return 0;
            default: assert(0);
        }
    }
    else if (p_dimension == 1)
    {
        switch (p_cadir)
        {
            case NORTH: return -1;
            case EAST: return 0;
            case SOUTH: return 1;
            case WEST: return 0;
            case NONE: return 0;
            default: assert(0);
        }
    }

    assert(0);
    return 0;
}

std::string Coordinates::to_string() const
{
    std::string res = "[";

    for (int i = 0; i < m_nb_dimensions - 1; ++i)
    {
        res += SSTR(m_dimensions[i]) + ", ";
    }
    res += SSTR(m_dimensions[m_nb_dimensions - 1]) + " / ";

    for (int i = 0; i < m_nb_dimensions - 1; ++i)
    {
        res += SSTR(m_lengths[i]) + ", ";
    }
    res += SSTR(m_lengths[m_nb_dimensions - 1]) + "]";

    return res;
}

std::string Coordinates::short_to_string() const
{
    std::string res = "[";

    for (int i = 0; i < m_nb_dimensions - 1; ++i)
    {
        res += SSTR(m_dimensions[i]) + ", ";
    }
    res += SSTR(m_dimensions[m_nb_dimensions - 1]) + "]";

    return res;
}
