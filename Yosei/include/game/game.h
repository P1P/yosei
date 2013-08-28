#ifndef GAME_H
#define GAME_H

#include <list>

#include <game/component.h>
#include <yosei/yosei.h>
#include <gameplay/world.h>

class Game : public Component
{
    public:
        Game();
        virtual ~Game();

        void start();
        void update();

        void add_component(Component*);
        void remove_component(Component*);
    protected:
    private:
        World* m_world;
        std::list<Component*> m_lst_components;
};

#endif // GAME_H
