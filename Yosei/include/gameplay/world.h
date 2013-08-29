#ifndef WORLD_H
#define WORLD_H

#include <map>
#include <utility>

#include <game/component.h>
#include <gameplay/coordinates.h>

class World : public Component
{
    public:
        World();
        virtual ~World();

        void start();
        void update();
    private:
        Coordinates* m_coords;
};

#endif // WORLD_H
