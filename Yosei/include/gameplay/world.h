#ifndef WORLD_H
#define WORLD_H

#include <map>
#include <utility>

#include <game/component.h>

class World : public Component
{
    public:
        World();
        virtual ~World();

        void start();
        void update();
    private:

};

#endif // WORLD_H
