using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Virtual_Keyboard))]
public class TextSync : MonoBehaviour
{

    public TextMesh Mesh;

    public Virtual_Keyboard kb;

    void Start()
    {
        this.kb = this.GetComponent<Virtual_Keyboard>();
    }
	// Update is called once per frame
	void Update ()
	{
	    this.Mesh.text = this.kb.character;
	}
}
