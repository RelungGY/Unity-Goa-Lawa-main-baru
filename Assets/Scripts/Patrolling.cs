using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{

    public Transform[] waypoints; // Array untuk menyimpan titik-titik tujuan
    public int currentWaypoint = 0; // Indeks titik tujuan saat ini
    public float idleTimer; // Penghitung waktu untuk perilaku idle
    public float idleDuration; // Durasi idle dalam detik
    public bool isIdle = true; // Status apakah sedang idle atau tidak
    public float speed; // Kecepatan pergerakan
    // Start is called before the first frame update

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        patrolling();
    }


    void patrolling()
    {
        // Jika NPC sedang idle
        if (isIdle)
        {
            if (animator != null)
            {
                animator.SetBool("idle", true);
            }

            idleTimer += Time.deltaTime; // Tambahkan waktu ke penghitung idle

            // Jika durasi idle telah tercapai
            if (idleTimer >= idleDuration)
            {
                isIdle = false; // Atur status NPC menjadi tidak idle
                idleTimer = 0f; // Reset penghitung idle
            }
        }
        else if (!isIdle) // Jika NPC tidak idle
        {
            if (animator != null)
            {
                animator.SetBool("idle", false);
            }

            // Dapatkan posisi titik tujuan saat ini
            Vector2 targetPosition = waypoints[currentWaypoint].position;

            // Hitung arah pergerakan ke titik tujuan
            Vector2 direction = targetPosition - (Vector2)transform.position;

            // Ubah arah menghadap kiri atau kanan berdasarkan arah pergerakan
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;// Menghadap ke kanan
            }
            else if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;// Menghadap ke kiri
            }
            // Gerakkan  menuju titik tujuan
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, targetPosition.x, speed * Time.deltaTime), Mathf.MoveTowards(transform.position.y, targetPosition.y, speed * Time.deltaTime));


            // Jika telah mencapai titik tujuan
            if (Vector2.Distance(transform.position, targetPosition) < 0.25f)
            {
                isIdle = true; // Atur status menjadi idle
                currentWaypoint++; // Pindah ke titik tujuan berikutnya

                // Jika telah mencapai titik tujuan terakhir, kembali ke titik awal
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
        }
    }

}
