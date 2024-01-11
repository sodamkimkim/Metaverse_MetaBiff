using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerClick : MonoBehaviour
{
    private MarketManager marketManager = null;

    private void Awake()
    {
      
    }
    private void Start()
    {
       // marketManager = GameObject.FindWithTag("Market").gameObject.GetComponent<MarketManager>();
    }
    public void SetInitialValues(MarketManager marketManager_)
    {
        marketManager = marketManager_;
    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //       Debug.Log("Hit: " + hit.transform.name);
                    if (hit.transform.CompareTag("Market"))
                    {
                        Debug.Log("Market click");
                        marketManager.OpenMarketPan();
                    }
                }
            }
        }
    }
} // end of class
