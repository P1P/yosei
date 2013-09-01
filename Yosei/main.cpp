// Include standard headers
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <limits>
#include <cstdlib>
#include <time.h>

#include "gameplay/world.h"

int main (void)
{
    srand(time(NULL));

    Observer::getInstance().set_log_type(Observer::GAMEPLAY);

    int frame_count = 0;

    World world = World();
    world.start();

    do
    {
        Observer::getInstance().out_highlight(Observer::GAMEPLAY, "Frame " + SSTR(frame_count++));
        std::cin.ignore(std::numeric_limits <std::streamsize>::max(), '\n');

        world.update();
    }
    while (true);

    return 0;
}

