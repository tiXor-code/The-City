using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _interactRadius;

    [SerializeField] private TextMeshProUGUI _textBox;
    [SerializeField] private string _text;

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
    }

    IEnumerator NPCInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                Chat();

                yield return new WaitForSeconds(1f);
            }
            else if(_textBox.enabled == true && Input.GetKeyDown(KeyCode.E))
            {
                DisableChat();
            }
        }
        
    }

    void Chat()
    {
        _textBox.enabled = true;
        _textBox.text = _text;
    }

    void DisableChat()
    {
        _textBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(NPCInteract());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}