#ifndef IDENTIFIABLE_H
#define IDENTIFIABLE_H

#include <helpers/observer.h>

class Identifiable
{
    public:
        Identifiable(std::string);
        Identifiable();
        virtual ~Identifiable();

        unsigned int get_id() const;

        virtual std::string to_string() const;

        bool operator==(const Identifiable&);
    protected:
        std::string m_name;
        unsigned int m_unique_id;
    private:
        bool m_active;

        static unsigned int m_static_id_increment;
};

#endif // IDENTIFIABLE_H
