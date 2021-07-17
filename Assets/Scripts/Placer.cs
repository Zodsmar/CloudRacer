using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Placer : MonoBehaviour
{
    private Grid grid;

    //public List<GameObject> roads;
    //public List<GameObject> obstacles;

    public GameObject selected;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        //Check if over the UI if so don't do anything at all
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (GameManager.Instance.isDriving) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.transform.tag == "Ground"){
                    if(selected.tag == "Road" || selected.tag == "Start"|| selected.tag == "Checkpoint"){
                        PlaceRoadNear(hitInfo.point);
                    } else if(selected.tag == "Obstacle") {
                        PlaceObstacle(hitInfo.point);
                    }     
                }
                 if(hitInfo.transform.tag == "Road"){
                     if(selected.tag == "Obstacle") {
                        PlaceObstacle(hitInfo.point);
                    }     
                 }
            }
        }

        if(Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(2)){
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.transform.tag == "Road" || hitInfo.transform.tag == "Obstacle" 
                || hitInfo.transform.tag == "Start"|| hitInfo.transform.tag == "Checkpoint"){
                    RotateObject90(hitInfo.transform.gameObject);
                }

            }
        }

         if(Input.GetMouseButtonDown(1)){
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.transform.tag == "Road" || hitInfo.transform.tag == "Obstacle" 
                    || hitInfo.transform.tag == "Start"|| hitInfo.transform.tag == "Checkpoint"){
                    if(hitInfo.transform.gameObject.GetComponent<Destroyable>().getDestroyable() == true){
                    Destroy(hitInfo.transform.gameObject);
                    }
                }
            }
        }

    }

    public void setSpawnable(GameObject selected){
        this.selected = selected;
    }

    private void PlaceRoadNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        if(selected.name == "Start"){
            if(GameObject.FindGameObjectWithTag("Start") == null){
                Instantiate(selected, finalPosition, selected.transform.rotation);
            }
        } else {
            Instantiate(selected, finalPosition, selected.transform.rotation);
        }
    }

    private void PlaceObstacle(Vector3 clickPoint)
    {
        Instantiate(selected, clickPoint, selected.transform.rotation);
    }

    private void RotateObject90(GameObject gameObject){
        gameObject.transform.Rotate(0, 90, 0);
    }
}
