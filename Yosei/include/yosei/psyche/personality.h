#ifndef PERSONALITY_H
#define PERSONALITY_H


class Personality
{
    public:
        Personality(char, char, char);

        char get_compassion() const;
        char get_boldness() const;
        char get_anti_conformism() const;

        virtual ~Personality();
    protected:
    private:
        char m_compassion;
        char m_boldness;
        char m_anti_conformism;
};

#endif // PERSONALITY_H
