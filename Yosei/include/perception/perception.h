#ifndef PERCEPTION_H
#define PERCEPTION_H

#include <queue>

#include <perception/visiontile.h>

class Perception
{
    public:
        Perception();
        virtual ~Perception();

        VisionTile* perceive_stimulus_vision_tile();
        void push_stimulus_vision_tile(VisionTile*);
    protected:
    private:
        std::queue<VisionTile*> m_stimuli_vision_tile;
};

#endif // PERCEPTION_H
