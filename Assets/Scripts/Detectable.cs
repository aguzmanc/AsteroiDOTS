using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectable : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    GameObject _parentObject;

    public GameObject parentObject => _parentObject;
}
