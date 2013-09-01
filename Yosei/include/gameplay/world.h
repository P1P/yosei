#ifndef WORLD_H
#define WORLD_H

#include <map>
#include <list>
#include <utility>

#include <game/component.h>
#include <gameplay/tile.h>
#include <gameplay/coordinates.h>
#include <gameplay/map.h>
#include <yosei/yosei.h>

class World : public Component
{
    public:
        World();
        virtual ~World();

        void start();
        void update();

        void add_component(Component*);
        void remove_component(Component*);

        void build_world(unsigned int);

        bool move_tobject(TileObject*, Coordinates::CARDINAL_DIRECTION);

        std::string to_string() const;
    private:
        Map* m_map;
        TileObject* m_tobject;

        std::list<Component*> m_lst_components;
};

#endif // WORLD_H
