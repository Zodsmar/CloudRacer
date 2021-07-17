using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10f;
    public float sensitivity = 30f;
    private Vector3 offset = new Vector3(-2, 1, 0);

    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10.0f;

    public AudioClip notPlaying;
    public AudioClip playing;

    private AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.clip = notPlaying;
        audio.Play();
    }

    void Update()
    {
        if(GameManager.Instance.isDriving){
            //Follow the player
            Vector3 wantedPosition;
            Transform target = GameManager.Instance.getVehicle().transform;
            if(followBehind)
                    wantedPosition = target.TransformPoint(0, height, -distance);
            else
                    wantedPosition = target.TransformPoint(0, height, distance);
    
            transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

            if (smoothRotation) {
                    Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                    transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else transform.LookAt (target, target.up);
        
        } else {
            float side = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * Time.deltaTime * speed * side, Space.World);

            float forward = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * speed * forward, Space.World);

            float up = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            transform.Translate(Vector3.forward * Time.deltaTime * speed * up); 

        }
    }

    public void changeAudio() {
        if(audio.clip == notPlaying){
            audio.clip = playing;
        } else {
            audio.clip = notPlaying;
        }
        audio.Play();
    }
}
