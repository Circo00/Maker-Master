using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    
    [SerializeField] private Image healthbarsprite;

    private Camera cam;
    [SerializeField] private float reducespeed = 2f;
    private float target = 1;
    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
    }
    public void UpdateHealthbar(float maxhealth, int currenthealth)
    {
        target = (currenthealth / maxhealth);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthbarsprite.fillAmount = Mathf.MoveTowards(healthbarsprite.fillAmount, target, reducespeed * Time.deltaTime);
    }
}
