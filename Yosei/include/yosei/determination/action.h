#ifndef ACTION_H
#define ACTION_H

#include <game/identifiable.h>

class Yosei;

class Action : public Identifiable
{
    public:
        Action(std::string, void (Yosei*, Yosei*));
        Action();
        Action(const Action&);

        virtual ~Action();

        void Do(Yosei*, Yosei*);
    protected:
    private:
        void (*m_fct_action) (Yosei*, Yosei*);
};

#endif // ACTION_H
