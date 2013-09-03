#ifndef TILE_H
#define TILE_H

#include <game/component.h>
#include <world/coordinates.h>

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

        void set_direction(Coordinates::CARDINAL_DIRECTION);
        Coordinates::CARDINAL_DIRECTION get_direction() const;
        Coordinates::CARDINAL_DIRECTION* get_ordered_directions() const;

        virtual std::string to_string() const=0;
        virtual std::string short_to_string() const=0;
    protected:
    private:
        Coordinates::CARDINAL_DIRECTION m_direction;
        Tile* m_tile;
};

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
