using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using System;

public class BlinkerProjector : MonoBehaviour {

    /* NOTE: Y val determines if robot is turning or not */ 
    /* 0 is the baseline, positive is skewed to the left, negative skewed to the right */

    /* Recieve Messages */
    public BlinkerReceiver message;
    public PathReceiver fullpath_message; // full path
    public PathReceiver path_message; // global path 

    /* Particles system variables */
    private ParticleSystem mySystem;
    private ParticleSystem.Particle[] particles;
    private int numParticles = 2; // 2 blinkers

    /* Variables for on/off switch */ 
    public bool right_blinker_on = false;
    public bool left_blinker_on = false;

    /* Variables for blinker positions */
    public float x_left = -0.1f;
    public float y_left = 0.22f;
    public float z_left = -0.4f;
    public float x_right = -0.1f;
    public float y_right = -0.22f;
    public float z_right = -0.4f;
    private float blinking_size = 0.12f;

    /* Thresholds */
    public uint numpoints_thresh = 10; 
    public uint outlook_thresh = 15;
    public float deviation_thresh = 2.2f;

    /* Use this for initialization */
    void Start () {
        mySystem = GetComponent<ParticleSystem> ();
    }
    
    // Update is called once per frame
    void Update () {

        if (path_message != null)
        {
            particles = new ParticleSystem.Particle[numParticles];
            // Reset values 
            right_blinker_on = false;
            left_blinker_on = false; 
            updateBlinker(); // Determine if a blinker should be on
            Display(); // Spawn Blinkers 
        }

    }
    void updateBlinker(){

        int left_outliers = 0;
        int right_outliers = 0;
        /* Calculate number of outliers */
        for (int i = 0; i < outlook_thresh; i++){
            if(path_message.path[i].y > deviation_thresh){
                Debug.Log("LEFT OUTLIER at value");
                Debug.Log(path_message.path[i].y);
                left_outliers++;
            }
            if (path_message.path[i].y < (-1f * deviation_thresh)){
                Debug.Log("RIGHT OUTLIER at value");
                Debug.Log(path_message.path[i].y);
                right_outliers++;
            }
        }

        /* Determine if a blinker should be on */
        if(left_outliers >= numpoints_thresh ){
            Debug.Log("Left Blinker ON");
            left_blinker_on = true;
        }
        if(right_outliers >= numpoints_thresh ){
            Debug.Log("Right Blinker ON");
            right_blinker_on = true;
        }
    }
    void Display(){
        /* Set position for left blinker */
         if(left_blinker_on){
            particles[0].position = new Vector3(x_left, y_left, z_left);
            particles[0].startColor = Color.yellow;
            particles[0].startSize = blinking_size;
        }

        /* Set position for right blinker */
        if(right_blinker_on){
            particles[1].position = new Vector3(x_right, y_right, z_right);
            particles[1].startColor = Color.yellow;
            particles[1].startSize = blinking_size;
        }

        /* Set particles */
        mySystem.SetParticles(particles, particles.Length);
    }
}
