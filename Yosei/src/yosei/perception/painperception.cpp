#include "yosei/perception/painperception.h"

PainPerception::PainPerception(unsigned char p_pain)
{
    m_pain = p_pain;
}

PainPerception::~PainPerception()
{

}

unsigned char PainPerception::get_pain()
{
    return m_pain;
}
