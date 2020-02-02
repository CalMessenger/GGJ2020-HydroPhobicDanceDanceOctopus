using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        speed += 0.1f;
        if(speed > 300f)
        {
            speed -= 50f;
        }
    }
}
