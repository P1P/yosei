#ifndef MOTORACTION_H
#define MOTORACTION_H

#include <world/tile_tileobject.h>

class MotorAction
{
    public:
        MotorAction(TileObject*, Coordinates::CARDINAL_DIRECTION);
        virtual ~MotorAction();

        TileObject* get_tobject();
        Coordinates::CARDINAL_DIRECTION get_cadir();
    protected:
    private:
        TileObject* m_tobject;
        Coordinates::CARDINAL_DIRECTION m_cadir;
};

#endif // MOTORACTION_H
