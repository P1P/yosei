#include "game/identifiable.h"

int Identifiable::m_static_id_increment = 0;

Identifiable::Identifiable(std::string p_name)
{
    m_name = p_name;
    m_unique_id = Identifiable::m_static_id_increment++;
    Observer::getInstance().out(Observer::VERBOSE, "Constructing " + to_string());
}

Identifiable::Identifiable()
{
    m_name = "Dummy Identifiable";
    m_unique_id = Identifiable::m_static_id_increment++;
    Observer::getInstance().out(Observer::VERBOSE, "Constructing " + to_string());
}

Identifiable::~Identifiable()
{
    //dtor
}

std::string Identifiable::to_string() const
{
    return ("[" + m_name + "]");
}

bool Identifiable::operator==(const Identifiable& other)
{
    if (this->m_unique_id == other.m_unique_id)
    {
        return true;
    }
    return false;
}
