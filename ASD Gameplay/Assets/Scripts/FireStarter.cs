using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    public Fire fireData;

    // Start is called before the first frame update
    void Start()
    {
        if (fireData != null)
        {
            GameObject GO = Instantiate(fireData.Fires[0].gameObject, transform.position, transform.rotation);
            var position = transform.position;
            position.y = 0f;
            GO.transform.position = position;
            GO.GetComponent<FireGrid>().StartFire();
        }
}

}
