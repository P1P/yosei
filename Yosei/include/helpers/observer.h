#ifndef LOG_H
#define LOG_H

#include <string>
#include <sstream>
#include <assert.h>

#define SSTR( x ) dynamic_cast< std::ostringstream & >( \
        ( std::ostringstream() << std::dec << x ) ).str()

class Observer
{
    public:
        static Observer& getInstance()
        {
            static Observer instance; // Guaranteed to be destroyed.
                                 // Instantiated on first use.
            return instance;
        }

        enum LOG_TYPE { MUTED = 0, GAMEPLAY = 1, VERBOSE = 2, SUPER_VERBOSE = 3};

        void out(LOG_TYPE, std::string);
        void out_highlight(LOG_TYPE, std::string);
        void out_say(LOG_TYPE, std::string);
        void set_log_type(LOG_TYPE);

    private:
        Observer() {};                   // Constructor? (the {} brackets) are needed here.
        // Dont forget to declare these two. You want to make sure they
        // are unaccessable otherwise you may accidently get copies of
        // your singleton appearing.
        Observer(Observer const&);            // Don't Implement
        void operator=(Observer const&); // Don't implement

        LOG_TYPE m_current_log_type;
};

#endif // LOG_H
