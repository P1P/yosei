#include "perception/perception.h"

Perception::Perception()
{
    //ctor
}

Perception::~Perception()
{
    //dtor
}

VisionTile* Perception::perceive_stimulus_vision_tile()
{
    if (m_stimuli_vision_tile.empty())
    {
        return nullptr;
    }
    VisionTile* res = m_stimuli_vision_tile.front();
    m_stimuli_vision_tile.pop();
    return res;
}

void Perception::push_stimulus_vision_tile(VisionTile* p_stimulus)
{
    m_stimuli_vision_tile.push(p_stimulus);
}
