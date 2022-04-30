using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private bool isTurning = false;
    private Quaternion targetAngle;
    private Quaternion startAngle;
    private float startTime;
    public bool isActive = false;
    private bool checkCollisions = false;
    private float rotateAngle = 60f;
    public GameObject[] mySpheres;
    public GameObject[] myRays;
    public Color[] sphereColors;
    public int numSpheres = 6;
    public LayerMask platformLayer;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.numberOfSpheres += 6;
        GameManager.isGameRunning = true;
        var randomNum = Random.Range(0, 5);
        foreach (var item in mySpheres)
        {
            item.GetComponent<Renderer>().material.SetColor("_Color", sphereColors[randomNum%6]);
            item.GetComponent<SphereCollisionCheck>().SetSphereColor(sphereColors[randomNum % 6]);
            randomNum++;
        }
    }
    void TurnPlatform()
    {
        if (Time.time - startTime < 1)
        {
            transform.rotation = Quaternion.Lerp(startAngle, targetAngle, (Time.time - startTime) / 1);
        }
        else
        {
            transform.rotation = targetAngle;
            checkCollisions = true;
            isTurning = false;
            GameManager.isAvailable = true;
        }
    }
    void CheckIsFailed()
    {
        GameManager.isAvailable = false;
        var check = 0;
        foreach(var item in myRays)
        {
            RaycastHit hit;
            Vector3 fwd = item.transform.TransformDirection(new Vector3(1.3f, 0, 1.3f));
            if (Physics.Raycast(item.transform.position, fwd, out hit, 1f, platformLayer))
            {
                if (hit.collider.tag == mytags.PLATFORM_TAG)
                {
                    foreach(var sphere in hit.collider.GetComponent<PlatformManager>().mySpheres)
                    {
                        if(sphere.active)
                        {
                            foreach(var mysphere in mySpheres)
                            {
                                if(mysphere.GetComponent<SphereCollisionCheck>().GetSphereColor()== sphere.GetComponent<SphereCollisionCheck>().GetSphereColor())
                                {
                                    check = 1;
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("Check Value : " + check);
        if(check==0)
        {
            GameManager.isFailed = true;
        }
        
    }
    void checkChildrenAdjacents()
    {
        foreach(var item in mySpheres)
        {
            if(item.active)
            {
                item.GetComponent<SphereCollisionCheck>().startCollisionCheck = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if(Input.touchCount==1)
            {
                Touch screenTouch = Input.GetTouch(0);
                if(screenTouch.phase==TouchPhase.Ended)
                {
                    if (!isTurning)
                    {
                        isActive = false;
                        startAngle = transform.rotation;
                        targetAngle = startAngle * Quaternion.Euler(0, 0, rotateAngle);
                        startTime = Time.time;
                        isTurning = true;
                    }
                }
            }
        }
        if (isTurning)
        {
            TurnPlatform();
        }
        if(checkCollisions)
        {
            checkChildrenAdjacents();
            checkCollisions = false;
        }
        if(GameManager.isAvailable)
        {
            CheckIsFailed();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        var check = 0;
        foreach(var item in mySpheres)
        {
            foreach(var item1 in other.GetComponent<PlatformManager>().mySpheres)
            {
                if(item.GetComponent<Renderer>().material.color==item1.GetComponent<Renderer>().material.color)
                {
                    check = 1;
                }
            }
        }
        if(check==0)
        {
            GameManager.isDisplayGO = true;
        }
    }
}
