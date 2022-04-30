using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionCheck : MonoBehaviour
{
    public bool startCollisionCheck = false;
    public LayerMask sphereLayer;
    private Color sphereColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(startCollisionCheck)
        {
            collisionCheck();
        }
    }
    public void SetSphereColor(Color color)
    {
        sphereColor = color;
    }
    public Color GetSphereColor()
    {
        return sphereColor;
    }
    void collisionCheck()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(new Vector3(1.3f, 0, 1.3f));
        if (Physics.Raycast(transform.position,fwd,out hit,1f,sphereLayer))
        {
            if(hit.collider.tag==mytags.SPHERE_TAG)
            {
                if (hit.collider.gameObject.GetComponent<Renderer>().material.color==gameObject.GetComponent<Renderer>().material.color)
                {
                    GameObject go = GameObject.FindGameObjectWithTag(mytags.MATCHED_PARTICLE);
                    go.GetComponent<ParticleSystem>().Play();
                    hit.collider.gameObject.GetComponent<SphereCollisionCheck>().sphereDisable();
                    sphereDisable();
                }
                
            }
        }
        startCollisionCheck = false;
    }
    public void sphereDisable()
    {
        GameManager.numberOfSpheres -= 1;
        StartCoroutine(scaleSphere());
    }
    IEnumerator scaleSphere()
    {
        if(transform.localScale.x<0.006f)
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(transform.localScale.x + 0.0005f, transform.localScale.y + 0.0005f, transform.localScale.z + 0.0005f);
            StartCoroutine(scaleSphere());
        }
        else
        {
            gameObject.GetComponentInParent<PlatformManager>().numSpheres--;
            StopCoroutine(scaleSphere());
            gameObject.SetActive(false);
        }
    }

}
