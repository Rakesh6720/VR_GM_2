using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {

    public GameObject preFab;
    GameObject preFabClone;
    List<GameObject> clones = new List<GameObject>();

    // Use this for initialization
    void Start() {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(i, (i + .25f), 0f);
            preFabClone = new GameObject();
            preFabClone.transform.position = pos;
            clones.Add(preFabClone);

        }

        Vector3 max = (clones[clones.Count - 1]).transform.position; //this is the position of the last item in the list

        Vector3 mid = (clones[clones.Count / 2].transform.position); //this is the position of the midpoint clone

        Vector3 origin = (clones[0].transform.position); //this is the position of the origin clone

        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, mid);
        lineRenderer.SetPosition(2, max);

      
    }
}
