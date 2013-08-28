#ifndef STIMULUS_H
#define STIMULUS_H


class Stimulus
{
    public:
        Stimulus();
        virtual ~Stimulus();

        void* get_target();
        bool get_value();
    protected:
    private:
        // The Item subject of the Stimulus
        void* m_target;

        // The state of the target
        bool m_value;
};

#endif // STIMULUS_H
