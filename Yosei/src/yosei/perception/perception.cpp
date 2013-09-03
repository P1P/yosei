#include "yosei/perception/perception.h"

Perception::Perception()
{
    //ctor
}

Perception::~Perception()
{
    //dtor
}

// Pops and returns the oldest Vision Tile Stimulus in the queue
// Returns nullptr if no Vision Tile Stimulus available
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

// Pushes a Vision Tile Stimulus in the queue
void Perception::push_stimulus_vision_tile(VisionTile* p_stimulus)
{
    m_stimuli_vision_tile.push(p_stimulus);
}
