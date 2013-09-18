#include "helpers/observer.h"

#include <iostream>

void Observer::out(LOG_TYPE p_log_type, std::string p_message)
{
    if (m_current_log_type >= p_log_type)
    {
        std::cout << p_message << std::endl;
    }
}

void Observer::out_continued(LOG_TYPE p_log_type, std::string p_message)
{
    if (m_current_log_type >= p_log_type)
    {
        std::cout << p_message;
    }
}

void Observer::out_highlight(LOG_TYPE p_log_type, std::string p_message)
{
    if (m_current_log_type >= p_log_type)
    {
        std::cout << std::endl << p_message << std::endl;
    }
}

void Observer::out_say(LOG_TYPE p_log_type, std::string p_message)
{
    if (m_current_log_type >= p_log_type)
    {
        std::cout << "\"" + p_message + "\"" << std::endl;
    }
}

void Observer::set_log_type(LOG_TYPE p_log_type)
{
    m_current_log_type = p_log_type;
}
