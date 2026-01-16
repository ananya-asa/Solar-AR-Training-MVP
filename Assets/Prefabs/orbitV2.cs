using UnityEngine;

public class OrbitV2 : MonoBehaviour {
    public Transform[] components = new Transform[5];
    public float radius = 3f;
    public float speed = 100f;  // FASTER!
    
    void Update() {
        for(int i = 0; i < components.Length; i++) {
            if(components[i] == null) continue;
            float angle = Time.time * speed * Mathf.Deg2Rad + i * 1.256f;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius,
                0.5f,
                Mathf.Sin(angle) * radius + 2f
            );
            components[i].position = pos;
            components[i].rotation = Quaternion.LookRotation(-Vector3.forward);
        }
    }
}
