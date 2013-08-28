#ifndef YOSEI_H
#define YOSEI_H

#include <map>
#include <cstdlib>

#include <game/component.h>
#include <gameplay/action.h>
#include <gameplay/opinion.h>
#include <gameplay/mentalstate.h>

class Yosei : public Component
{
    public:
        Yosei(std::string, float, float, float);
        Yosei();
        virtual ~Yosei();

        void start();
        void update();

        void learn_action(const Action&);

        float get_score() const;
        float judge(const Yosei*, float) const;
        void reflect_upon(const Action*, const Yosei*, float);
        void immediate_reflect_upon(Action*, Yosei*, float, float);

        bool should_do(Opinion::INCLINATION);

        void please(float);
        void sadden(float);

        std::string to_string() const;

        Yosei* m_other;
    protected:
    private:
        std::map<Action*, Opinion> m_knowledge;
        float m_compassion;
        float m_boldness;
        float m_anti_conformism;
        MentalState m_mental_state;
};

#endif // YOSEI_H
