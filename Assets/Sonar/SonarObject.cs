// GlowOnTouch scripts and shaders were written by Drew Okenfuss.
// Any object with this script must have the GlowOnTouch/Single shader on it

using UnityEngine;
using System.Collections;

public class SonarObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // StartGlowing on the contact point
        SonarParent.instance.StartScan(collision.contacts[0].point, 2.5f);
    }

    
}
