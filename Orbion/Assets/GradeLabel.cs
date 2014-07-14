using UnityEngine;
using System.Collections;

public class GradeLabel : MonoBehaviour {
	public dfLabel gradeLabel;
	private string playerGrade;

	// Use this for initialization
	void Start () {

		playerGrade = MetricManager.getGrade;
	
	}
	
	// Update is called once per frame
	void Update () {

		gradeLabel.Text = playerGrade;
	
	}
}
