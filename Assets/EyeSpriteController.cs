using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpriteController : MonoBehaviour
{
    #region Prefab constants
    Vector3 bone1Reference;
    Vector3 bone2Reference;
    #endregion

    #region Instance variables
    Vector3 bone1Instance;
    Vector3 bone2Instance;
    #endregion

    #region editor
    [SerializeField]
    GameObject eye1;
    [SerializeField]
    GameObject eye2;
    
    [SerializeField]
    GameObject bone1;
    [SerializeField]
    GameObject bone2;
    #endregion
    
    [SerializeField]
    float scaleUnit = 0.1f;
    [SerializeField]
    float scaleMin = 1f;
    [SerializeField]
    float scaleMax = 1.5f;

    [SerializeField]
    float maxStress = 10;
    [SerializeField]
    float minStress = 0;

    float stressUnit;

    private void Awake()
    {
        bone1Reference = bone1.transform.localPosition;
        bone2Reference = bone2.transform.localPosition;
    }
    void Start()
    {
        bone1Instance = bone1Reference;
        bone2Instance = bone2Reference;

        stressUnit = maxStress/((scaleMax - scaleMin)/scaleUnit);
    }

    void Update()
    {
        bone1Instance = bone1.transform.localPosition;
        bone2Instance = bone2.transform.localPosition;

        float bone1Stress = StressSteps(calculateBoneStress(bone1Instance, bone1Reference));
        float bone2Stress = StressSteps(calculateBoneStress(bone2Instance, bone2Reference));
        
        EyeSizeController(bone1Stress, eye1);
        EyeSizeController(bone2Stress, eye2);
    }

    //calculates stress of bone based on relative position in a range between minStress and maxStress
    float calculateBoneStress(Vector3 instance, Vector3 reference)
    {
        //number returned is 
        return Mathf.Clamp(Mathf.Abs((instance.x - reference.x) / reference.x) +
                           Mathf.Abs((instance.y - reference.y) / reference.y), 
                           minStress, maxStress);
    }

    float StressSteps(float stress)
    {        
        return stress - (stress % stressUnit);
    }
    
    //change size of eye based on scale
    void EyeSizeController(float boneStress, GameObject eye)
    {
        float eyescale = scaleMin + (scaleUnit * (boneStress / stressUnit));
        
        Vector3 scale = new Vector3(eyescale, eyescale);
        eye.transform.localScale = scale;
    }
    
}
