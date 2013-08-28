// Include standard headers
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <limits>
#include <cstdlib>
#include <time.h>

#include "include/game/game.h"

int main (void)
{
    srand(time(NULL));

    Observer::getInstance().set_log_type(Observer::VERBOSE);

    int frame_count = 0;

    Game game = Game();
    game.start();

    do
    {
        Observer::getInstance().out_highlight(Observer::VERBOSE, "Frame " + SSTR(frame_count++));
        std::cin.ignore( std::numeric_limits <std::streamsize> ::max(), '\n' );

        game.update();
    }
    while (true);

    return 0;
}

