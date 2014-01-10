#pragma strict
var prefabBullet:Rigidbody;
var bullet_speed:float;
//var shootPosition:Transform;
var firingTimer:float;
var firingRate:float;

function Update () {
	firingTimer++;
	// Mouse1 was pressed, launch a projectile
	if (Input.GetMouseButton(0))
	 	{	
	 		if(firingTimer > firingRate){
	 		var mouse = Input.mousePosition;
           	mouse.z = 1.0; //diobonino
           	var mouseworldpos : Vector3 = Camera.main.ScreenToWorldPoint(mouse);
           	mouseworldpos.y = transform.position.y;
           	Debug.Log(mouseworldpos);
           	var bullet_dir : Vector3 = mouseworldpos - transform.position;
           	bullet_dir = bullet_dir.normalized;
           	var clone = Instantiate(prefabBullet,transform.position + bullet_dir * 2,Quaternion.LookRotation(mouseworldpos, Vector3.forward));
           	clone.GetComponent(Rigidbody).transform.rotation = Quaternion.LookRotation(mouseworldpos, Vector3.forward);
           	clone.GetComponent(Rigidbody).AddForce(bullet_dir * bullet_speed);
			//var instanceBullet = Instantiate(prefabBullet, transform.position, shootPosition.rotation);
			//instanceBullet.velocity = transform.TransformDirection (Vector3.forward * shootForce);
			firingTimer = 0;
			}
		}

}