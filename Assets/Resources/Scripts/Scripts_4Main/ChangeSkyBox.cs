using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSkyBox : MonoBehaviour
{
    //[SerializeField]
    private Material[] mts = null;
    [SerializeField]
    private bool shuffle;
    [SerializeField]
    private float interval = 10f;

    private MeshRenderer mr = null;

    [SerializeField]
    private Material rainnyDay = null;
    //[SerializeField]
    //private GameObject rainPrefab = null;

    private Material beforeMat = null;

    private void Awake()
    {
        mts = Resources.LoadAll<Material>("Materials\\Mat_Main2\\M_Skybox");
        mr = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        RenderSettings.skybox = mts[Random.Range(0, mts.Length - 1)];
        StartCoroutine(ChangeSkyCoroutine());
    }

    private IEnumerator ChangeSkyCoroutine()
    {
        int idx = 0;

        while (true)
        {
            if (!shuffle)
            {
                RenderSettings.skybox = mts[idx];
                ++idx;
                idx %= mts.Length;
                beforeMat = mts[idx];
            }
            else
            {
                // RenderSettings.skybox = mts[Random.Range(0, mts.Length - 1)];

                Material mt = mts[Random.Range(0, mts.Length - 1)];

                //  float lerp = Mathf.PingPong(Time.time, 5000);
                //float lerp = 0;
                //lerp += Time.deltaTime;

                //float lerp = Mathf.PingPong(Time.time, 10000) / 10000;

                //RenderSettings.skybox.Lerp(beforeMat, mt, lerp);

                //beforeMat = mt;
            }


            if (RenderSettings.skybox.name == "RainnyDay")
            {

               // Instantiate(rainPrefab);


            }
            else
            {
                //Destroy(GameObject.Find("RainEffect(Clone)"));
            }

            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator ShowRainCoroutine()
    {
        yield return new WaitForSeconds(interval);
    }


    ////////////////////////////////////////////////////////////////////
    ///

    //public Material[] skyboxMaterials;

    //private int currentIndex = 0;
    //private int nextIndex;

    //private float lerpTime = 1f;
    //private float currentLerpTime;

    //private void Start()
    //{
    //    RenderSettings.skybox = skyboxMaterials[currentIndex];
    //    nextIndex = (currentIndex + 1) % skyboxMaterials.Length;
    //}

    //private void Update()
    //{
    //    currentLerpTime += Time.deltaTime;

    //    if(currentLerpTime > lerpTime)
    //    {
    //        currentLerpTime = 0f;
    //        currentIndex = nextIndex;
    //        nextIndex = (nextIndex + 1) % skyboxMaterials.Length;
    //    }

    //    float t = currentLerpTime / lerpTime;
    //    RenderSettings.skybox.Lerp(skyboxMaterials[currentIndex], skyboxMaterials[nextIndex], t);
    //}

}// end of class
