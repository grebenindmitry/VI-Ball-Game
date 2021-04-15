using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SetHeightSessionOrigin : MonoBehaviour
{
    public ARSessionOrigin aRSessionOrigin;
    private float _playerEyeYPos;

    // Start is called before the first frame update
    private void Start()
    {
        var temp = PlayerPrefs.GetInt("playerHeight", 175);
        _playerEyeYPos = (temp - 10) / 100.0f;
        
        var arSessionOriginNewPos = aRSessionOrigin.transform.position;
        arSessionOriginNewPos.y = _playerEyeYPos;
        aRSessionOrigin.transform.SetPositionAndRotation(arSessionOriginNewPos, aRSessionOrigin.transform.rotation);
    }
}
