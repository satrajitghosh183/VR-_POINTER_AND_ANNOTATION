using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Annotation 
{ 
    public enum HandToTrack
    {
        Left ,

        Right
    }
}
public class VRDraw : MonoBehaviour
{
    [SerializeField]

    private Annotation.HandToTrack handToTrack =Annotation.HandToTrack.Left;

    [SerializeField]
    private GameObject objectToTrackMovement;

    private Vector3 prevPointDistance=Vector3.zero;

    [SerializeField]

    private float minFingerPinchStrength=0.5f;
    [SerializeField, Range(0,1.0f)]
    private float minDistanceBeforeNewPoint=0.2f;

    [SerializeField, Range(0,1.0f)]

    private float lineDefaultWidth=0.010f;

    private int positionCount=0;

    private List<LineRenderer> lines =new List<LineRenderer>(); // a list of line renderers? so maybe "pop"?
    
    private LineRenderer currentLineRenderer;

    [SerializeField]

    private Color defaultColor =Color.white;

    [SerializeField]

    private GameObject editorObjectToTrackMovement ;

    [SerializeField]

    private bool allowEditorControls= true;

    [SerializeField]

    private Material defaultLineMaterial;

    private bool IsPinchDown= false ;


#region Oculus Types 
    
    private OVRHand ovrHand;
    private OVRSkeleton ovrSkeleton;
    private OVRBone boneToTrack;

#endregion

Vector3 origin = new Vector3(0.0f,0.0f,0.0f);

    void Awake()
    {
        ovrHand=objectToTrackMovement.GetComponent<OVRHand>();
        ovrSkeleton= objectToTrackMovement.GetComponent<OVRSkeleton>();

        #if UNITY_EDITOR
        
        if(allowEditorControls)
        {
            objectToTrackMovement=editorObjectToTrackMovement !=null ? editorObjectToTrackMovement:objectToTrackMovement;
        }

        #endif
        boneToTrack= ovrSkeleton.Bones
            .Where(b => b.Id ==OVRSkeleton.BoneId.Hand_Index1)
            .SingleOrDefault();
        
        AddNewLineRenderer();


    }
    
    //Refer to https://forum.unity.com/threads/deleting-a-line-renderer.172007/ (specifically deleting gameobjs)

    void AddNewLineRenderer()
    {
        positionCount=0;

        GameObject go = new GameObject($"Line Renderer_{handToTrack.ToString()}_{lines.Count}");
        go.transform.parent=objectToTrackMovement.transform.parent;
        go.transform.position-=objectToTrackMovement.transform.position;

        LineRenderer goLineRenderer =go.AddComponent<LineRenderer>();


        goLineRenderer.startWidth=lineDefaultWidth;
        goLineRenderer.endWidth=lineDefaultWidth;
        goLineRenderer.useWorldSpace=true;
        goLineRenderer.material=defaultLineMaterial;
        goLineRenderer.positionCount=1;
        goLineRenderer.numCapVertices=5;

        currentLineRenderer=goLineRenderer;

        lines.Add(goLineRenderer);


    }

    void Update()
    {
        if(boneToTrack==null)
        {

            boneToTrack=ovrSkeleton.Bones
             .Where(b =>b.Id==OVRSkeleton.BoneId.Hand_Index3)
             .SingleOrDefault();

            objectToTrackMovement=boneToTrack.Transform.gameObject;

        }
        CheckPinchState();
    }

    private void CheckPinchState()
    {
        bool isIndexFingerPinching=ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        float indexFingerPinchStrength=ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        if (isIndexFingerPinching && indexFingerPinchStrength >=minFingerPinchStrength)
        {
          UpdateLine();
          IsPinchDown=false;

        }
        else 
        {
            if(!IsPinchDown)
            {
                AddNewLineRenderer() ;
                IsPinchDown=true;

            }
        }
    }
    void UpdateLine()
    {
        if (prevPointDistance==null)
        {
            prevPointDistance=objectToTrackMovement.transform.position;
        }
        if (prevPointDistance!=null && Mathf.Abs(Vector3.Distance(prevPointDistance,objectToTrackMovement.transform.position))>=minDistanceBeforeNewPoint)
        {
           Vector3 dir =(objectToTrackMovement.transform.position-Camera.main.transform.position).normalized;
           prevPointDistance= objectToTrackMovement.transform.position;
           AddPoint(prevPointDistance,dir);

        }
      
    }
    public void DeletePoints()
    {
        currentLineRenderer.positionCount = 0; // experiment with - currentLineRenderer.positionCount = currentLineRenderer.positionCount-1;
    }//GoLineRenderer is not Global

    void AddPoint(Vector3 position ,Vector3 direction )
    {
        currentLineRenderer.SetPosition(positionCount,position);
        positionCount++;
        currentLineRenderer.positionCount=positionCount+1;//change the value and remove +1 
        currentLineRenderer.SetPosition(positionCount,position);

    }
    public void UpdateLineWidth(float newValue)
    {
        currentLineRenderer.startWidth=newValue;
        currentLineRenderer.endWidth=newValue;
        lineDefaultWidth=newValue;
    }

    public void UpdateLineColor(Color color)
    {
        if(currentLineRenderer.positionCount==1)
        {
            currentLineRenderer.material.color=color;
            currentLineRenderer.material.EnableKeyword("_EMISSION");
            currentLineRenderer.material.SetColor("EmissionColor" ,color);

        }
        defaultColor=color;

    }
    public void UpdateLineMinDistance(float newValue)
    {
        minDistanceBeforeNewPoint=newValue;
    }


}


       