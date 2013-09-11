#ifndef LAVA_H
#define LAVA_H

#include <world/tile_tileobject.h>

class Lava : public Tile
{
    public:
        Lava(std::string, Coordinates*);
        virtual ~Lava();

        void start();
        void update();

        std::string to_string() const;
        std::string base_decoration() const;
    protected:
    private:
};


#endif // LAVA_H
