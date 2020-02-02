using UnityEngine;

public class TentacleScript : MonoBehaviour
{
    [SerializeField] int limbNo =0;
    Ocotpus_Script OctoScript;
    public GameObject[] holes;
    //public int rand;
    //public spawnwater water; 
    private void Start()
    {
        //water = GetComponent<spawnwater>();
        OctoScript = FindObjectOfType<Ocotpus_Script>();
    }


    //make debris a child of tentacle to move
    public void MoveDebris()
    {
        OctoScript.FixTransformDebris(limbNo);
    }

    public void DisableDebris()
    {
        if(holes.Length>0){
        if(holes[0].activeSelf)
        {
            holes[0].SetActive(false);
        } else if(holes[1].activeSelf)
        {
            holes[1].SetActive(false);
        }
        }


        //rand = Random.Range(0, 9);
        //holes[rand].SetActive(true);
        OctoScript.DisableDebris(limbNo);

    }
}
