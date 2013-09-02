#ifndef YOSEI_H
#define YOSEI_H

#include <map>
#include <cstdlib>

#include <gameplay/tile_tileobject.h>
#include <gameplay/action.h>
#include <gameplay/opinion.h>
#include <gameplay/mentalstate.h>
#include <perception/perception.h>

class Yosei : public TileObject
{
    public:
        Yosei(std::string, Tile*, float, float, float);
        Yosei();
        virtual ~Yosei();

        void start();
        void update();

        Perception* get_perception();

        void learn_action(const Action&);

        float get_score() const;
        float judge(const Yosei*, float) const;
        void reflect_upon(const Action*, const Yosei*, float);
        void immediate_reflect_upon(Action*, Yosei*, float, float);

        bool should_do(Opinion::INCLINATION);

        void please(float);
        void sadden(float);

        std::string to_string() const;
        std::string short_to_string() const;

        Yosei* m_other;
    protected:
    private:
        std::map<Action*, Opinion> m_knowledge;
        Perception* m_perception;
        float m_compassion;
        float m_boldness;
        float m_anti_conformism;
        MentalState m_mental_state;
};

#endif // YOSEI_H
