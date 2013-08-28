#ifndef TILE_H
#define TILE_H

#include <game/component.h>

class Tile : Component
{
    public:
        Tile(std::string);
        virtual ~Tile();

        virtual void start();
        virtual void update();
    protected:
    private:
        Tile* m_neighbors;
};

#endif // TILE_H
