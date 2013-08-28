#ifndef COMPONENT_H
#define COMPONENT_H

#include <game/identifiable.h>

class Component : public Identifiable
{
    public:
        Component(std::string);
        Component();
        virtual ~Component();

        virtual void start()=0;
        virtual void update()=0;
        bool is_active();
    protected:
    private:
        bool m_active;
};

#endif // COMPONENT_H
