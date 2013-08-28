#ifndef MENTALSTATE_H
#define MENTALSTATE_H

#include <game/identifiable.h>

class MentalState : Identifiable
{
    public:
        MentalState(std::string);
        MentalState();
        virtual ~MentalState();

        enum HAPPINESS { DEPRESSED = 0, SAD = 1, TROUBLED = 2, NEUTRAL = 3, CONTENT = 4, HAPPY = 5, EUPHORIC = 6 };

        HAPPINESS get_happiness() const;
        float get_value() const;

        void offset_value(float);

        std::string to_string() const;
    protected:
    private:
        float m_value;

        static const int VALUE_LIMIT = 10;

        static const int HIGH_VALUE = 8;
        static const int MID_VALUE = 5;
        static const int LOW_VALUE = 2;
};

#endif // MENTALSTATE_H
