#ifndef OPINION_H
#define OPINION_H

#include <game/identifiable.h>

class Opinion : Identifiable
{
    public:
        Opinion(std::string);
        Opinion();
        virtual ~Opinion();

        enum INCLINATION { NO_NO = 0, NO = 1, SHOULDNT = 2, NEUTRAL = 3, TRY = 4, SHOULD = 5, YES = 6, YES_YES = 7};

        INCLINATION get_inclination() const;
        void offset_value(float);

        std::string to_string() const;
    protected:
    private:
        float m_value;
        bool m_known;

        static const int VALUE_LIMIT = 10;

        static const int DETERMINED_VALUE = 8;
        static const int CONVINCED_VALUE = 5;
        static const int SHOULD_VALUE = 2;
};

#endif // OPINION_H
