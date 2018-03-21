using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour {

    private GameObject buildingExteriorPrefab;
    private GameObject buildingInteriorPrefab;

    private Transform interior;
    private Transform exterior;

    void Awake()
    {
        //EventManager.AddListener(EventType.CreateBuildings, OnCreateBuildings);
        LoadPrefabs();
        GetInteriorAndExterior();
    }

    void LoadPrefabs()
    {
        buildingExteriorPrefab = Resources.Load<GameObject>("Building Exterior");
        buildingInteriorPrefab = Resources.Load<GameObject>("Building Interior");
    }

    void GetInteriorAndExterior()
    {
        interior = GameObject.Find("Interior").transform;
        exterior = GameObject.Find("Exterior").transform;
    }

    void OnCreateBuildings(object _)
    {
        Instantiate<GameObject>(buildingExteriorPrefab, new Vector3(-100, -100, 0), Quaternion.identity, exterior);
        Instantiate<GameObject>(buildingInteriorPrefab, new Vector3(-100, -100, 0), Quaternion.identity, interior);
    }
}
