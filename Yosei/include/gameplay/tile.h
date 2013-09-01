#ifndef TILE_H
#define TILE_H

#include <game/component.h>
#include <gameplay/coordinates.h>
#include <gameplay/tileobject.h>

class Tile : public Component
{
    public:
        Tile(std::string, Coordinates*);
        virtual ~Tile();

        virtual void start();
        virtual void update();

        const Coordinates& get_coordinates() const;
        TileObject* get_tobject() const;
        void place_tobject(TileObject*);
        void remove_tobject();

        std::string to_string() const;
        std::string short_to_string() const;
    protected:
    private:
        Coordinates* m_coordinates;
        TileObject* m_tobject;
};

#endif // TILE_H
