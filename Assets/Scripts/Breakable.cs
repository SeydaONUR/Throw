using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject broke;
    [SerializeField] private float breakForce;
    [SerializeField] private float collsiionMultiplier;
    public Rigidbody[] rbs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="Ball")
        {
            var replacement = Instantiate(broke, transform.position, transform.rotation);
            rbs = replacement.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rbs)
            {
                rb.AddExplosionForce(collision.relativeVelocity.magnitude * collsiionMultiplier,
                    collision.contacts[0].point, 2);
            }
            Destroy(gameObject);
        }
        
    }
}
