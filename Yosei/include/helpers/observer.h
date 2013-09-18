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
            static Observer instance;
            return instance;
        }

        enum LOG_TYPE { MUTED = 0, GAMEPLAY = 1, VERBOSE = 2, SUPER_VERBOSE = 3};

        void out(LOG_TYPE, std::string);
        void out_continued(LOG_TYPE, std::string);
        void out_highlight(LOG_TYPE, std::string);
        void out_say(LOG_TYPE, std::string);
        void set_log_type(LOG_TYPE);

    private:
        Observer() {};
        Observer(Observer const&);
        void operator=(Observer const&);

        LOG_TYPE m_current_log_type;
};

#endif // LOG_H
