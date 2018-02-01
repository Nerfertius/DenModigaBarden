using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathH {

	public static float Normalize(float input) {
        if(input < 0) {
            input *= -1;
        }

        return input;
    }
}
