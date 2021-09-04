using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : Editor{

    private void OnSceneGUI() {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle/2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngle02 * fov.radius);

        if (fov.canSeeResource) { 
            Handles.color = Color.green;
            if (fov.resourceTypeSeen == "Food")
            {
                GameObject resourceFound = fov.ResourceFoodRef[fov.resourceTypeIndex];
                Handles.DrawLine(fov.transform.position, resourceFound.transform.position);
            }
            else if (fov.resourceTypeSeen == "Wood")
            {
                GameObject resourceFound = fov.ResourceWoodRef[fov.resourceTypeIndex];
                Handles.DrawLine(fov.transform.position, resourceFound.transform.position);
            }
            else if (fov.resourceTypeSeen == "Iron")
            {
                GameObject resourceFound = fov.ResourceIronRef[fov.resourceTypeIndex];
                Handles.DrawLine(fov.transform.position, resourceFound.transform.position);
            }
            else if (fov.resourceTypeSeen == "Water")
            {
                GameObject resourceFound = fov.ResourceWaterRef[fov.resourceTypeIndex];
                Handles.DrawLine(fov.transform.position, resourceFound.transform.position);
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
