#include "yosei/perception/perception.h"

Perception::Perception()
{

}

Perception::~Perception()
{

}

// Pops and returns the oldest Vision Tile Stimulus in the queue
// Returns nullptr if no Vision Tile Stimulus available
VisionTilePerception* Perception::perceive_stimulus_vision_tile()
{
    if (m_stimuli_vision_tile.empty())
    {
        return nullptr;
    }
    VisionTilePerception* res = m_stimuli_vision_tile.front();
    m_stimuli_vision_tile.pop();
    return res;
}

// Pushes a Vision Tile Stimulus in the queue
void Perception::push_stimulus_vision_tile(VisionTilePerception* p_stimulus)
{
    m_stimuli_vision_tile.push(p_stimulus);
}

// Pops and returns the oldest Pain Stimulus in the queue
// Returns nullptr if no Pain Stimulus available
PainPerception* Perception::perceive_stimulus_pain()
{
    if (m_stimuli_pain.empty())
    {
        return nullptr;
    }
    PainPerception* res = m_stimuli_pain.front();
    m_stimuli_pain.pop();
    return res;
}

// Pushes a Pain Stimulus in the queue
void Perception::push_stimulus_pain(PainPerception* p_stimulus)
{
    m_stimuli_pain.push(p_stimulus);
}
