#ifndef OPINION_H
#define OPINION_H

#include <cstdlib>

#include <game/identifiable.h>
#include <yosei/psyche/personality.h>

class Opinion : Identifiable
{
    public:
        Opinion(std::string, float);
        Opinion();
        virtual ~Opinion();

        enum INCLINATION { NO_NO = 0, NO = 1, SHOULDNT = 2, NEUTRAL = 3, SHOULD = 4, YES = 5, YES_YES = 6};

        INCLINATION get_inclination() const;
        bool should_do(const Personality*) const;
        bool operator>(const Opinion& p_other) const;
        bool operator<(const Opinion& p_other) const;

        void offset_value(float);
        void absorb(Opinion*);
        void memorize();

        std::string to_string() const;
    protected:
    private:
        float m_value;

        static const int VALUE_LIMIT = 10;

        static const int DETERMINED_VALUE = 8;
        static const int CONVINCED_VALUE = 5;
        static const int SHOULD_VALUE = 2;
};

#endif // OPINION_H
