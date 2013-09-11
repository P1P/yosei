#ifndef ACTION_H
#define ACTION_H

#include <game/identifiable.h>

class Yosei;

class Action : public Identifiable
{
    public:
        Action();

        unsigned int age();
        void memorize();
        unsigned int get_age() const;
        bool is_memorized() const;
        virtual bool less_comparer(Action*)=0;
        static bool is_old(const Action*);

        virtual std::string to_string() const;
    protected:
    private:
        unsigned int m_age;
        bool m_memorize;
};

#endif // ACTION_H
