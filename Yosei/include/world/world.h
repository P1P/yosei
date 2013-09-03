#ifndef WORLD_H
#define WORLD_H

#include <map>
#include <list>
#include <utility>

#include <world/map.h>
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
        std::list<Yosei*> m_yosei_population;
        std::list<Component*> m_lst_components;

        bool simple_add_yosei(unsigned short, unsigned short, std::string p_name);
};

#endif // WORLD_H
