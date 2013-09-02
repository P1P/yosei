#ifndef VISION_H
#define VISION_H

#include <gameplay/tile_tileobject.h>

class VisionTile
{
    public:
        VisionTile(Coordinates::CARDINAL_DIRECTION, Tile*);
        virtual ~VisionTile();

        Tile* get_tile();
        Coordinates::CARDINAL_DIRECTION get_cadir();
    protected:
    private:
        Tile* m_tile;
        Coordinates::CARDINAL_DIRECTION m_cadir;
};

#endif // VISION_H
