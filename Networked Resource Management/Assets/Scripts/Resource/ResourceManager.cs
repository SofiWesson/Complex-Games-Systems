using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    protected List<Resource> resources = new List<Resource>();
    protected List<Attributes> attributes = new List<Attributes>();

    private Resource m_resource;
    private Attributes m_attributes;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddResource(Resource resource)
    {

    }

    private void RemoveResource(Resource resource)
    {

    }

    private void AddAttribute(Attributes attribute)
    {

    }

    private void RemoveAttribute(Attributes attribute)
    {

    }
}