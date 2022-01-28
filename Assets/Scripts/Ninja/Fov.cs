using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov : MonoBehaviour
{
    [SerializeField] float distance = 10f;
    [SerializeField] float angle = 30;
    [SerializeField] float height = 1.0f;
    [SerializeField] Color meshColor = Color.red;

    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask occlusionLayers;
    public List<GameObject> Objects = new List<GameObject>();

    Collider[] colliders = new Collider[50];
    Mesh mesh;
    int count;
    float scanInterval;
    float scanTimer;
    public bool inTheView;

    private void Start() 
    {
        scanInterval = 1.0f / scanFrequency;       
    }
    private void Update() 
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0) 
        {
            scanTimer += scanInterval;
            inTheView = Scan();           
        }
    }

    private bool Scan() 
    {
        bool inSite = false;
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        Objects.Clear();
        for(int i = 0; i < count; ++i) 
        {
            GameObject obj = colliders[i].gameObject;
            if(isInSight(obj))
            {
                inSite = true;
                Objects.Add(obj);
                //Debug.Log("Object In Sight", obj);
            }
        }
        return inSite;
    }

    public bool isInSight(GameObject obj) {
        
        // HEIGHT CORRECTION WHILE KEEPING THE SPHERE
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if(direction.y < 0 || direction.y > height) {
            return false;
        }

        //RADIUS OF DETECTION CORRECTION
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if(deltaAngle > angle) {
            return false;
        }

        //FOR OCCLUSION LAYERS
        origin.y += height / 2;
        dest.y = origin.y;
        if(Physics.Linecast(origin, dest, occlusionLayers)) 
        {
            return false;
        }

        return true;
    }
    private void OnValidate() 
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos() 
    {
        if(mesh)
        {
            Gizmos.color = meshColor;
            // Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
            Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation);
        }
        // Gizmos.DrawWireSphere(transform.position, distance);
        // for(int i = 0; i < count; ++i )
        // {
        //     Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);           
        // }
    }


    Mesh CreateWedgeMesh()
    {
        mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCentre = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCentre = bottomCentre + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height; 

        int vert = 0;

        //Left Side
        vertices[vert++] = bottomCentre;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCentre;
        vertices[vert++] = bottomCentre;

        //Right Side

        vertices[vert++] = bottomCentre;
        vertices[vert++] = topCentre;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCentre;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for(int i = 0; i < segments; ++i) 
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            //Far Side

            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top

            vertices[vert++] = topCentre;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //bottom

            vertices[vert++] = bottomCentre;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft; 

            currentAngle += deltaAngle;
        }

        

        for(int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;                    
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }


}
