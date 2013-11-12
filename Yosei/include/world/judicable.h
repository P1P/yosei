#ifndef JUDICABLE_H
#define JUDICABLE_H

#include <game/identifiable.h>

class Judicable : public Identifiable
{
    public:
        Judicable();
        ~Judicable();

        virtual float like(void*) const =0;
    protected:
    private:
};

#endif // JUDICABLE_H
