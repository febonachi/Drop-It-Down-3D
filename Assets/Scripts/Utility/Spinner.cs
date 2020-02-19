﻿using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float speed;
    public float maxSpeed = 2.5f;
    public bool constantSpeed = false;
    public bool spinOnlyOnStart = false;
    public float durationOnStart = 1f;
    public bool isBonus = false;
    public Vector3 vector = Vector3.up;

    private int clockwise;

    private void Start() {
        if (!isBonus) {
            maxSpeed = GameController.instance.Score / 400f + PlayerPrefs.GetFloat("difficultySpinSpeed");
        }

        RotateRandomAngle();
        clockwise = Random.Range(0, 2) == 0 ? 1 : -1;
        if (constantSpeed) {
            speed = maxSpeed;
        } else {
            speed = Random.Range(0f, maxSpeed);
        }
        
        speed = Mathf.Clamp(maxSpeed, 0, 2.5f) * clockwise;

        if (spinOnlyOnStart) {
            StartCoroutine(SpinOnce());
        }
    }

    private void Update() {
        if (!spinOnlyOnStart) {
            transform.Rotate(vector.x * speed, vector.y * speed, vector.z * speed);
        }
    }

    private IEnumerator SpinOnce() {
        float elapsed = .0f;
        while(elapsed < durationOnStart) {
            elapsed += Time.deltaTime;
            transform.Rotate(0, speed, 0);
            yield return null;
        }
    }

    private void RotateRandomAngle() {
        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
    }

    public void StopSpin() {
        speed = Mathf.Lerp(speed, 0, Time.deltaTime);
    }
}
