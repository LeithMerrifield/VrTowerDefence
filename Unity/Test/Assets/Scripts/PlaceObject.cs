using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public GameObject m_tower = null;
    public Material m_previewValid = null;
    public Material m_previewInvalid = null;

    private GameObject m_object = null;
    private GameObject m_objectMaterial = null;
    private Material m_originalMaterial = null;
    private Vector3 m_mousePreviousPosition;
    private Vector3 m_mousePosition;
    private bool m_held = false;
    private bool m_validPlacement = false;
    private bool m_hit = false;

    public void InstatiateTower()
    {
        if (m_tower != null && m_previewValid != null)
        {
            m_object = Instantiate(m_tower);
            try
            {
                m_objectMaterial = m_object.transform.Find("Surface").gameObject;
            }
            catch
            {
                m_originalMaterial = m_object.GetComponent<MeshRenderer>().material;
            }

            try
            {
                m_originalMaterial = m_objectMaterial.GetComponent<MeshRenderer>().material;
                SetMaterial(m_previewValid);
            }
            catch
            {
                SetMaterial(m_originalMaterial);
            }
            m_object.layer = 2;
            m_object.GetComponent<TowerShoot>().Resize();
            m_held = true;
        }
    }

    void Update()
    {
        m_hit = false;
        RaycastHit currentMouse;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out currentMouse))
        {
            m_mousePosition = currentMouse.point;
            m_hit = true;
            if (currentMouse.transform.gameObject.tag == "Ground")
            {
                m_validPlacement = true;
            }
            else
            {
                m_validPlacement = false;
            }
            //Debug.Log(m_validPlacement);
        }

        if(m_object != null)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (!m_held)
                {
                    //RaycastHit hit;
                    //if (Physics.Raycast(ray, out hit))
                    //{
                    //    m_object = hit.collider.gameObject;
                    //    m_object.layer = 0;
                    //    m_held = true;
                    //}
                }
                else
                {
                    if (m_validPlacement)
                    {
                        SetMaterial(m_originalMaterial);
                        m_held = false;
                        m_object.layer = 0;
                        m_object.GetComponent<TowerShoot>().Enable();
                        var temp = m_object.GetComponentsInChildren<Transform>();
                        foreach(var i in temp)
                        {
                            if(i.name == "Radius")
                            {
                                i.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }

            var myMaterial = m_objectMaterial.GetComponent<MeshRenderer>().material;

            if (m_held && m_hit)
            {
                //m_mousePosition.y += (m_object.transform.localScale.y / 2);
                m_object.transform.position = m_mousePosition;
                if (m_validPlacement && m_previewValid != myMaterial)
                {
                    try
                    {
                        SetMaterial(m_previewValid);
                    }
                    catch
                    {
                        SetMaterial(m_originalMaterial);
                    }
                }
                if (!m_validPlacement && m_previewInvalid != myMaterial)
                {
                    try
                    {
                        SetMaterial(m_previewInvalid);
                    }
                    catch
                    {
                        SetMaterial(m_originalMaterial);
                    }
                }
            }
        }
    }
    void SetMaterial(Material material)
    {
        var children = m_object.GetComponentsInChildren<MeshRenderer>();
        foreach(var i in children)
        {
            i.material = material;
        }
        m_objectMaterial.GetComponent<MeshRenderer>().material = material;
    }
}
