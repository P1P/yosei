#include "yosei/psyche/mentalstate.h"

MentalState::MentalState(std::string p_name) : Identifiable(p_name)
{
    m_value = 0;
}

MentalState::MentalState()
{
    m_value = 0;
}

MentalState::~MentalState()
{

}

MentalState::HAPPINESS MentalState::get_happiness() const
{
    if (m_value <= -MentalState::HIGH_VALUE)
    {
        return HAPPINESS::DEPRESSED;
    }
    if (m_value <= -MentalState::MID_VALUE)
    {
        return HAPPINESS::SAD;
    }
    if (m_value <= -MentalState::LOW_VALUE)
    {
        return HAPPINESS::TROUBLED;
    }

    if (m_value <= MentalState::LOW_VALUE)
    {
        return HAPPINESS::NEUTRAL;
    }
    if (m_value <= MentalState::MID_VALUE)
    {
        return HAPPINESS::CONTENT;
    }
    if (m_value <= MentalState::HIGH_VALUE)
    {
        return HAPPINESS::HAPPY;
    }

    return HAPPINESS::EUPHORIC;
}

void MentalState::offset_value(float p_offset_value)
{
    m_value += p_offset_value;

    // Clamping
    m_value = m_value > MentalState::VALUE_LIMIT ? MentalState::VALUE_LIMIT : m_value;
    m_value = m_value < -MentalState::VALUE_LIMIT ? -MentalState::VALUE_LIMIT : m_value;
}

float MentalState::get_value() const
{
    return m_value;
}

std::string MentalState::to_string() const
{
    std::string label = "";

    switch (get_happiness())
    {
        case HAPPINESS::DEPRESSED:
            label = "Depressed";
            break;
        case HAPPINESS::SAD:
            label = "Sad";
            break;
        case HAPPINESS::TROUBLED:
            label = "Troubled";
            break;
        case HAPPINESS::NEUTRAL:
            label = "Whatever";
            break;
        case HAPPINESS::CONTENT:
            label = "Content";
            break;
        case HAPPINESS::HAPPY:
            label = "Happy!";
            break;
        case HAPPINESS::EUPHORIC:
            label = "Euphoric";
            break;
    }

    return (label + " (" + SSTR(m_value) + ")");
}
