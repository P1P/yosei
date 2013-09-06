#include "yosei/psyche/personality.h"

Personality::Personality(char p_compassion, char p_boldness, char p_anti_conformism)
{
    m_compassion = p_compassion;
    m_boldness = p_boldness;
    m_anti_conformism = p_anti_conformism;
}

Personality::~Personality()
{

}

char Personality::get_compassion() const
{
    return m_compassion;
}

char Personality::get_boldness() const
{
    return m_boldness;
}

char Personality::get_anti_conformism() const
{
    return m_anti_conformism;
}
