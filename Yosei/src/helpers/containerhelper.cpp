#include "helpers/containerhelper.h"

#include <cstdlib>

void ContainerHelper::shuffle(int* p_array, int n)
{
    if (n > 1)
    {
        int i;
        for (i = 0; i < n - 1; i++)
        {
          int j = i + rand() / (RAND_MAX / (n - i) + 1);
          int t = p_array[j];
          p_array[j] = p_array[i];
          p_array[i] = t;
        }
    }
}
