using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shout : MonoBehaviour
{
    public Button callBtn;
    public ParticleSystem particle;
    void Start()
    {
        Button call = callBtn.GetComponent<Button>();
        call.onClick.AddListener(Shout);
    }

    private void Shout()
    {
        particle.Play();
    }

   
}
