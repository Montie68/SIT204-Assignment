using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QUADS { I, II, III, IV, NONE };


public class CalaculateQuadrant : MonoBehaviour {
    public Vector3 Pos;
    public float h; // play Area Hieght
    public float w; // play Area Width
    public QUADS Quads = QUADS.NONE;


    Vector3 Centre;
    Vector3 CentreToMouse;

    public static Vector3 Origin;

     float AngleToCorner_TL;
     float AngleToCorner_TR;
     float AngleToCorner_BL;
     float AngleToCorner_BR;

    float AngleToCentre;



	// Use this for initialization
	void Start () {
        // get h and w
        h = Camera.main.orthographicSize;
        w = h * Screen.width/Screen.height;
        // the first task is to the determine the centre.
        Centre = new Vector3(w / 2, h / 2);
        // Set Origin
        Origin =Vector3.zero;

        //The next task is to get the vectors to the corners
        Vector3 vector_CO = Origin - Centre;
        Vector3 vector_CTR = new Vector3(w,0) - Centre;
        Vector3 vector_CBL = new Vector3(0, h) - Centre;
        Vector3 vector_CBR = new Vector3(w, h) - Centre;

        // From there we Get the angles that define the boundaries of each sector. 	θ_n=〖cos〗^(-1)⁡((v_nx ) /|(v_n )  | )

        AngleToCorner_TL = Mathf.Acos(vector_CO.x / vector_CO.magnitude) * Mathf.Rad2Deg ;
        AngleToCorner_TR = Mathf.Acos(vector_CTR.x / vector_CTR.magnitude) * Mathf.Rad2Deg ;
        AngleToCorner_BL = Mathf.Acos(vector_CBL.x / vector_CBL.magnitude) * Mathf.Rad2Deg;
        AngleToCorner_BR = Mathf.Acos(vector_CBR.x / vector_CBR.magnitude) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update () {
        //get pos from transform
        Pos = transform.localPosition;

        // get the vector from the centre to the mouse
        CentreToMouse = Pos - Centre ;
        CalcQuad();
	}

    private void CalcQuad()
    {
        // reset quads
        Quads = QUADS.NONE;

        //define angle and oppisit side an Hypotonus
        float θ_Theta;
        float Adj = CentreToMouse.x;

        // could ust Vector3.magnitude here but showing workigns for understanding.
        float Hyp = Mathf.Sqrt(Mathf.Pow(CentreToMouse.x, 2) + Mathf.Pow(CentreToMouse.y, 2));
        // Get the angle to the mouse
        θ_Theta = (Mathf.Acos(Adj / Hyp) * Mathf.Rad2Deg);

        // workout which sector the mouse is in.
    
        if (CentreToMouse.y < 0 && θ_Theta > AngleToCorner_TR && θ_Theta < AngleToCorner_TL)
            Quads = QUADS.I;
        else if ((CentreToMouse.y < 0 && θ_Theta < AngleToCorner_TR) || (CentreToMouse.y >= 0 && θ_Theta < AngleToCorner_BR))
            Quads = QUADS.II;
        else if (CentreToMouse.y >= 0 && θ_Theta > AngleToCorner_BR && θ_Theta < AngleToCorner_BL)
            Quads = QUADS.III;
        else if ((CentreToMouse.y >= 0 && θ_Theta < AngleToCorner_TL) || (CentreToMouse.y <= 0 && θ_Theta >= AngleToCorner_BL))
            Quads = QUADS.IV;

        AngleToCentre = θ_Theta;
        Debug.Log(Quads.ToString());
    }
}
