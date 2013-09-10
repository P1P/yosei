#ifndef VISIONTILEPERCEPTION_H
#define VISIONTILEPERCEPTION_H

#include <world/tile_tileobject.h>

class VisionTilePerception
{
    public:
        VisionTilePerception(Coordinates::CARDINAL_DIRECTION, Tile*);
        virtual ~VisionTilePerception();

        Tile* get_tile();
        Coordinates::CARDINAL_DIRECTION get_cadir();
    protected:
    private:
        Tile* m_tile;
        Coordinates::CARDINAL_DIRECTION m_cadir;
};

#endif // VISIONTILEPERCEPTION_H
