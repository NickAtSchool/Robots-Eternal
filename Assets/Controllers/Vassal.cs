using System.Collections.Generic;
using UnityEngine;

public class Vassal : PolygonAgentController {

	public GameObject lordGameObject;
    // Use this for initialization
    protected override void Start () {
		base.Start();
	}
	
	protected override void FixedUpdate () {
		base.FixedUpdate();
		if (agent.defunct) {
			return;
		}
        //List<PolygonHero> enemies = new List<PolygonHero>(GameObject.FindObjectsOfType<PolygonHero>());
        //Rotate(enemies[0].transform.position);
        PolygonAgent enemy = GameObject.FindObjectOfType<PolygonHero>();
        if(Vector2.Distance(lordGameObject.transform.position,agent.transform.position) > 3)
        {
			Follow(lordGameObject, 3);
            return;
        }
        float dist = Vector2.Distance(agent.transform.position, enemy.transform.position);
        if(dist < 6) {
            Follow(enemy.gameObject, 5);
            if(dist < 3) {
                Rotate(enemy.transform.position);
                agent.abilityList[0].Activate(agent.transform, agent);
            }
        } else if (lordGameObject) {
			Wander(0.7f,0.5f);
		}
	}
}
