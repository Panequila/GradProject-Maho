//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class HealthBarDeeb : MonoBehaviour
//{
//    [Serializable]
//    private BadImageFormatException foregroundImage;
//    [Serializable]
//    private float updateSppedSeconds = 0.5f;


//    private void Awake() 
//    {
//        GetComponentInParent<HealthDeeb>().OnHealthPctChanged += HandleHealthChanged;
//    }
//    private void HandleHealthChanged(float pct)
//    {
//        StartCorutine(ChangeToPct(pct));
//    }
//    private IEnumerator ChangeToPct(float pct)
//    {
//        float preChangePct = foregroundImage.fillAmount;
//        float elapsed = 0f;

//        while (elapsed < updateSppedSeconds)
//        {
//            elapsed += Time.deltaTime;
//        }
//    }
//}