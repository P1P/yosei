#ifndef MOTORACTION_H
#define MOTORACTION_H

#include <world/tile_tileobject.h>
#include <yosei/determination/action.h>

class MotorAction : public Action
{
    public:
        MotorAction(Tile*, Tile*, Coordinates::CARDINAL_DIRECTION);
        virtual ~MotorAction();

        Tile* get_from() const;
        Tile* get_to() const;
        Coordinates::CARDINAL_DIRECTION get_cadir() const;
        bool less_comparer(Action*);

        float like(void*) const;

        std::string to_string() const;
    protected:
    private:
        Tile* m_from;
        Tile* m_to;
        Coordinates::CARDINAL_DIRECTION m_cadir;
};

#endif // MOTORACTION_H
