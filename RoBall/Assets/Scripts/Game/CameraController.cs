using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 _offset;
    public Vector3 Offset;
    public Transform[] Obstructions;

    private int _oldHitsNumber;

    private void Start() {
        _oldHitsNumber = 0;
        _offset = transform.position - Player.transform.position;
    }

    private void LateUpdate() {
        transform.position = Player.transform.position + _offset;
        ViewObstructed();
    }

	private void ViewObstructed() {
		float characterDistance = Vector3.Distance(transform.position, Player.transform.position);
		int layerNumber = LayerMask.NameToLayer("Obstacles");
		int layerMask = 1 << layerNumber;

		RaycastHit[] hits = Physics.RaycastAll(transform.position, Player.transform.position - transform.position, characterDistance, layerMask);

		if (hits.Length > 0) {   // Means that some stuff is blocking the view
			int newHits = hits.Length - _oldHitsNumber;

			if (Obstructions != null && Obstructions.Length > 0 && newHits < 0) {
				// Repaint all the previous obstructions. Because some of the stuff might be not blocking anymore
				for (int i = 0; i < Obstructions.Length; i++) {
					Obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				}
			}
			Obstructions = new Transform[hits.Length];
			// Hide the current obstructions 
			for (int i = 0; i < hits.Length; i++) {
				Transform obstruction = hits[i].transform;
				obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
				Obstructions[i] = obstruction;
			}
			_oldHitsNumber = hits.Length;
		}
		else {   // Mean that no more stuff is blocking the view and sometimes all the stuff is not blocking as the same time
			if (Obstructions != null && Obstructions.Length > 0) {
				for (int i = 0; i < Obstructions.Length; i++) {
					Obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				}
				_oldHitsNumber = 0;
				Obstructions = null;
			}
		}
	}
}
