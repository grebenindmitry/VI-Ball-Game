using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SetHightSessionOrigin : MonoBehaviour
{
    public ARSessionOrigin aRSessionOrigin;
    float playerEyeYPos;
    Vector3 aRSessionOriginNewPos;

    // Start is called before the first frame update
    void Start()
    {
        int temp = PlayerPrefs.GetInt("playerHeight", 15);
        playerEyeYPos = ((temp - 10 ) / 100.0f);
        aRSessionOriginNewPos.Set(aRSessionOrigin.transform.position.x, aRSessionOrigin.transform.position.y + playerEyeYPos, aRSessionOrigin.transform.position.z);
        aRSessionOrigin.transform.SetPositionAndRotation(aRSessionOriginNewPos, aRSessionOrigin.transform.rotation);        
    }


}
