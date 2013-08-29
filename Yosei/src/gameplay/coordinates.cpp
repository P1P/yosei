#include "gameplay\coordinates.h"

Coordinates::Coordinates(unsigned short* p_dimensions, unsigned short* p_lengths, unsigned char p_nb_dimensions)
{
    m_dimensions = p_dimensions;
    m_lengths = p_lengths;
    m_nb_dimensions = p_nb_dimensions;

    assert(is_valid(m_dimensions));
}

Coordinates::~Coordinates()
{
    delete m_dimensions;
    delete m_lengths;
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

// Returns a new peek towards p_cadir
Coordinates* Coordinates::operator+(CARDINAL_DIRECTION p_cadir) const
{
    return peek(p_cadir);
}

// Attempts to go towards p_cadir
// Always returns a reference to the object
Coordinates* Coordinates::operator+=(CARDINAL_DIRECTION p_cadir)
{
    go(p_cadir);
    return this;
}

// Comparison operator
bool Coordinates::operator==(Coordinates p_other) const
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

// Returns a new pointer to the Coordinates towards p_cadir
// Returns nullptr if Direction is invalid
Coordinates* Coordinates::peek(CARDINAL_DIRECTION p_cadir) const
{
    if (unsigned short* candidate_dimensions = peek_dimensions(p_cadir))
    {
        // Todo: memcpy
        unsigned short* candidate_lengths = new unsigned short[m_nb_dimensions];
        for (int i = 0; i < m_nb_dimensions; ++i)
        {
            candidate_lengths[i] = m_lengths[i];
        }

        return new Coordinates(candidate_dimensions, candidate_lengths, m_nb_dimensions);
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

// Returns whether p_dimensions is within lengths
bool Coordinates::is_valid(unsigned short* p_dimensions) const
{
    for (int i = 0; i < m_nb_dimensions; ++i)
    {
        if (p_dimensions[i] < 0) return false;
        if (p_dimensions[i] >= m_lengths[i]) return false;
    }

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
    res += SSTR(m_lengths[m_nb_dimensions - 1]) + " / ";

    res += SSTR((int)m_nb_dimensions) + "D]";

    return res;
}
