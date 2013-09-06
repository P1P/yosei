#ifndef ROCK_H
#define ROCK_H

#include <world/tile_tileobject.h>

class Rock : public TileObject
{
    public:
        Rock(std::string, Tile*);
        virtual ~Rock();

        void start();
        void update();

        std::string to_string() const;
        std::string short_to_string() const;
    protected:
    private:
};

#endif // ROCK_H
