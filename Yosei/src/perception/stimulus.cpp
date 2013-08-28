#include "perception/stimulus.h"

Stimulus::Stimulus()
{
    //ctor
}

Stimulus::~Stimulus()
{
    //dtor
}

void* Stimulus::get_target()
{
    return m_target;
}

bool Stimulus::get_value()
{
    return m_value;
}
