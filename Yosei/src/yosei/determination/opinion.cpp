#include "yosei/determination/opinion.h"

Opinion::Opinion(std::string p_name, float p_value) : Identifiable(p_name)
{
    m_value = p_value;
}

Opinion::Opinion()
{
    m_value = 0;
}

Opinion::~Opinion()
{

}

void Opinion::offset_value(float p_offset)
{
    m_value += p_offset;

    // Clamping
    m_value = m_value > Opinion::VALUE_LIMIT ? Opinion::VALUE_LIMIT : m_value;
    m_value = m_value < -Opinion::VALUE_LIMIT ? -Opinion::VALUE_LIMIT : m_value;
}

void Opinion::absorb(Opinion* p_other)
{
    this->offset_value(p_other->m_value);
}

Opinion::INCLINATION Opinion::get_inclination() const
{
    if (m_value <= -Opinion::DETERMINED_VALUE)
    {
        return INCLINATION::NO_NO;
    }
    if (m_value <= -Opinion::CONVINCED_VALUE)
    {
        return INCLINATION::NO;
    }
    if (m_value <= -Opinion::SHOULD_VALUE)
    {
        return INCLINATION::SHOULDNT;
    }
    if (m_value <= Opinion::SHOULD_VALUE)
    {
        return INCLINATION::NEUTRAL;
    }
    if (m_value <= Opinion::CONVINCED_VALUE)
    {
        return INCLINATION::SHOULD;
    }
    if (m_value <= Opinion::DETERMINED_VALUE)
    {
        return INCLINATION::YES;
    }

    return INCLINATION::YES_YES;
}

bool Opinion::should_do(const Personality* p_personality) const
{
    INCLINATION inclination = get_inclination();

    switch (inclination)
    {
        case (Opinion::YES_YES) :
        {
            if (rand() % 100 < 100 - p_personality->get_anti_conformism()) return true; break;
        }
        case (Opinion::YES) :
        {
            if (rand() % 100 < 80 - p_personality->get_anti_conformism()) return true; break;
        }
        case (Opinion::SHOULD) :
        {
            if (rand() % 100 < 70 - p_personality->get_anti_conformism()) return true; break;
        }
        case (Opinion::NEUTRAL) :
        {
            if (rand() % 100 < 50) return true; break;
        }
        case (Opinion::SHOULDNT) :
        {
            if (rand() % 100 < 5 + p_personality->get_boldness()) return true; break;
        }
        case (Opinion::NO) :
        {
            if (rand() % 100 < -5 + p_personality->get_boldness()) return true; break;
        }
        case (Opinion::NO_NO) :
        {
            if (rand() % 100 < -15 + p_personality->get_boldness()) return true; break;
        }
        return false;
    }
    //assert(0)
    return false;
}

bool Opinion::operator>(const Opinion& p_other) const
{
    return this->m_value > p_other.m_value;
}

bool Opinion::operator<(const Opinion& p_other) const
{
    return this->m_value < p_other.m_value;
}

std::string Opinion::to_string() const
{
    std::string label = "";

    switch (get_inclination())
    {
        case INCLINATION::NO_NO:
            label = "No-no";
            break;
        case INCLINATION::NO:
            label = "No";
            break;
        case INCLINATION::SHOULDNT:
            label = "Shouldn't";
            break;
        case INCLINATION::NEUTRAL:
            label = "Whatever";
            break;
        case INCLINATION::SHOULD:
            label = "Should";
            break;
        case INCLINATION::YES:
            label = "Yes!";
            break;
        case INCLINATION::YES_YES:
            label = "Absolutely";
            break;
    }

    return (label + " (" + SSTR(m_value) + ")");
}
