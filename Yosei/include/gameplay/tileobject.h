#ifndef TILEOBJECT_H
#define TILEOBJECT_H

#include <helpers/observer.h>
#include <game/component.h>

class Tile;

class TileObject : public Component
{
    public:
        TileObject(std::string, Tile*);
        virtual ~TileObject();

        virtual void start()=0;
        virtual void update()=0;

        void set_tile(Tile*);
        Tile* get_tile() const;

        virtual std::string to_string() const=0;
        virtual std::string short_to_string() const=0;
    protected:
    private:
        Tile* m_tile;
};

#endif // TILEOBJECT_H
