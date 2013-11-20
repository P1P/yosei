using UnityEngine;
using System.Collections;

public class Gaugeable : MonoBehaviour
{
	// Returns the degree of likeliness with another Item of the same type
	float Calc_Likeliness(dynamic other)
	{
		if (other is Yosei)
		{
			return 1f;
		}
		return 0f;
	}
}
