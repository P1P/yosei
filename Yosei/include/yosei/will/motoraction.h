#ifndef MOTORACTION_H
#define MOTORACTION_H

#include <world/tile_tileobject.h>

class MotorAction
{
    public:
        MotorAction(Tile*, Tile*, Coordinates::CARDINAL_DIRECTION);
        virtual ~MotorAction();

        Tile* get_from();
        Tile* get_to();
        Coordinates::CARDINAL_DIRECTION get_cadir();

        unsigned int age();
        unsigned int get_age();
    protected:
    private:
        Tile* m_from;
        Tile* m_to;
        Coordinates::CARDINAL_DIRECTION m_cadir;

        unsigned int m_age;
};

#endif // MOTORACTION_H
