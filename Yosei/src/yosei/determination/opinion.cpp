#include "yosei/determination/opinion.h"

Opinion::Opinion(std::string p_name) : Identifiable(p_name)
{
    m_value = 0;
    m_known = false;
}

Opinion::Opinion()
{
    m_value = 0;
    m_known = false;
}

Opinion::~Opinion()
{
    //dtor
}

Opinion::INCLINATION Opinion::get_inclination() const
{
    if (!m_known)
    {
        return INCLINATION::TRY;
    }

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

void Opinion::offset_value(float p_offset)
{
    m_value += p_offset;

    // Clamping
    m_value = m_value > Opinion::VALUE_LIMIT ? Opinion::VALUE_LIMIT : m_value;
    m_value = m_value < -Opinion::VALUE_LIMIT ? -Opinion::VALUE_LIMIT : m_value;

    m_known = true;
}

std::string Opinion::to_string() const
{
    std::string label = "";

    switch (get_inclination())
    {
        case INCLINATION::TRY:
            label = "Let's try";
            break;
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

    return (m_name + ": " + label + " (" + SSTR(m_value) + ")");
}
