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
        Rotate(GameObject.FindObjectOfType<PolygonHero>().transform.position);
        agent.abilityList[0].Activate(agent.transform, agent);
        if (lordGameObject) {
			Follow(lordGameObject, 3);
		}
	}
}
