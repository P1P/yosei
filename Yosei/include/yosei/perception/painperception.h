#ifndef PAINPERCEPTION_H
#define PAINPERCEPTION_H


class PainPerception
{
    public:
        PainPerception(unsigned char);
        virtual ~PainPerception();

        unsigned char get_pain();
    protected:
    private:
        unsigned char m_pain;
};

#endif // PAINPERCEPTION_H
