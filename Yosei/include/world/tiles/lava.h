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
        std::string short_to_string() const;
        std::string left_bracket() const;
        std::string base_decoration() const;
        std::string right_bracket() const;
    protected:
    private:
};


#endif // LAVA_H
