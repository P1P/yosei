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

        virtual void burn();

        virtual std::string to_string() const=0;
        virtual std::string short_to_string() const=0;
    protected:
        Coordinates::CARDINAL_DIRECTION m_direction;
        Tile* m_tile;
    private:
};

class Tile : public Component
{
    public:
        Tile(std::string, Coordinates*);
        virtual ~Tile();

        virtual void start()=0;
        virtual void update()=0;

        const Coordinates& get_coordinates() const;
        TileObject* get_tobject() const;
        void place_tobject(TileObject*);
        void remove_tobject();

        virtual std::string to_string() const;
        virtual std::string short_to_string() const;
        virtual std::string base_decoration() const=0;
    protected:
        Coordinates* m_coordinates;
        TileObject* m_tobject;
    private:
};

#endif // TILE_H
