using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCan : MonoBehaviour
{
    public GameObject poisonText;

    // Start is called before the first frame update
    void Start()
    {
        poisonText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DestroyZone")
        {
            poisonText.SetActive(true);
            StartCoroutine(changeActive());
        }
    }
    private IEnumerator changeActive()
    {
        yield return new WaitForSeconds(1f);
        poisonText.SetActive(false);
    }
}
