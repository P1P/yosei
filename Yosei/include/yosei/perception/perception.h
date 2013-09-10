#ifndef PERCEPTION_H
#define PERCEPTION_H

#include <queue>

#include <yosei/perception/visiontileperception.h>
#include <yosei/perception/painperception.h>

class Perception
{
    public:
        Perception();
        virtual ~Perception();

        VisionTilePerception* perceive_stimulus_vision_tile();
        PainPerception* perceive_stimulus_pain();
        void push_stimulus_vision_tile(VisionTilePerception*);
        void push_stimulus_pain(PainPerception*);
    protected:
    private:
        std::queue<VisionTilePerception*> m_stimuli_vision_tile;
        std::queue<PainPerception*> m_stimuli_pain;
};

#endif // PERCEPTION_H
