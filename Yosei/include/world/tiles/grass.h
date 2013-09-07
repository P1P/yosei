#ifndef GRASS_H
#define GRASS_H

#include <world/tile_tileobject.h>

class Grass : public Tile
{
    public:
        Grass(std::string, Coordinates*);
        virtual ~Grass();

        void start();
        void update();

        std::string to_string() const;
        std::string short_to_string() const;
        std::string base_decoration() const;
    protected:
    private:
};

#endif // GRASS_H
