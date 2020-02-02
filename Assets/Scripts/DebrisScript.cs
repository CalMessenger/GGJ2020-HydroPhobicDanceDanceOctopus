using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * 10, Space.World);
    }
}
