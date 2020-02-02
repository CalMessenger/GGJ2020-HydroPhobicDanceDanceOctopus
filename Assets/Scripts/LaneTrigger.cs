using UnityEngine;

public class LaneTrigger : MonoBehaviour
{
    [SerializeField] int LaneNo = 0;
    Ocotpus_Script OctoScript;
    private void Start()
    {
        OctoScript = FindObjectOfType<Ocotpus_Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Plank"){
        OctoScript.DebrisLane(true, other.gameObject, LaneNo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Plank"){
        OctoScript.DebrisLane(false, null, LaneNo);
        }
    }
}
