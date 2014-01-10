#pragma strict
var prefabBuilding:Rigidbody;

function Start () {

}

function Update () {
	if(Input.GetKeyDown('b')){
	var instanceBullet = Instantiate(prefabBuilding, transform.position, Quaternion.identity);

	}
}