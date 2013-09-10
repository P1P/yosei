#ifndef YOSEI_H
#define YOSEI_H

#include <map>
#include <cstdlib>

#include <world/tile_tileobject.h>
#include <yosei/determination/action.h>
#include <yosei/determination/opinion.h>
#include <yosei/determination/memory.h>
#include <yosei/psyche/mentalstate.h>
#include <yosei/psyche/personality.h>
#include <yosei/will/will.h>
#include <yosei/perception/perception.h>

class Yosei : public TileObject
{
    public:
        Yosei(std::string, Tile*, Personality*);
        Yosei();
        virtual ~Yosei();

        void start();
        void update();

        Perception* get_perception();
        Will* get_will();
        MentalState* get_mental_state();
        Memory* get_memory();

        void learn_action(const Action&);

        float get_score() const;
        float judge(const Yosei*, float) const;
        void reflect_upon(const Action*, const Yosei*, float);
        void immediate_reflect_upon(Action*, Yosei*, float, float);

        void burn();

        std::string to_string() const;
        std::string short_to_string() const;

        Yosei* m_other;
    protected:
    private:
        std::map<Action*, Opinion> m_knowledge;
        Personality* m_personality;
        Perception* m_perception;
        Will* m_will;
        MentalState* m_mental_state;
        Memory* m_memory;
};

#endif // YOSEI_H
