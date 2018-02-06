using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {

    public void Die(){
        transform.parent.gameObject.SetActive(false);
    }
}
