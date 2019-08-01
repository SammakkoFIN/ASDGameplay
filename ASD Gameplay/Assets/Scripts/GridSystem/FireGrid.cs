using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGrid : MonoBehaviour
{
    [SerializeField] private GridSystem gridGO;
    public GridSystem GridGO { get => gridGO; set => gridGO = value; }

    [SerializeField] private Node node;
    public Node Node { get => node; set => node = value; }

    [SerializeField] private Fire fireRules;
    public Fire FireRules { get => fireRules; set => fireRules = value; }

    private int health = 30;
    private int currentFire = 1;                            // References to the list in fireRules

    private Transform localSmoke;                           // Access to smoke when its instantiated when losing health
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Sets grid system's node on fire
    /// </summary>
    public void StartFire()
    {
        GridGO = GameObject.Find("GridSystem").GetComponent<GridSystem>();
        Node = GridGO.NodeFromWorldPoint(transform.position);
        Node.SetOnFire();

        float time = Random.Range(FireRules.SpreadTimeInSeconds, FireRules.SpreadTimeInSeconds * 2f);
        Invoke("GrowFire", time);
    }

    /// <summary>
    /// Spread fire by getting neighbour grids, take some out from the list and spawn fire
    /// </summary>
    private void SpreadFire()
    {
        List<Node> nodes = GridGO.GetNeighbours(Node);
        int random = Random.Range(4, 6);
        if (nodes.Count < random)
            random /= random;

        for (int i = 0; i < random; i++)
        {
            int r = Random.Range(0, nodes.Count);
            if (nodes[r] != null)
                nodes.Remove(nodes[r]);
        }

        foreach (Node n in nodes)
        {
            if (n.OnFire || n.Type == NodeType.Empty || n == null)
                continue;

            GameObject GO = Instantiate(FireRules.Fires[0].gameObject, GridGO.transform);
            GO.transform.position = n.WorldPoint;
            GO.transform.localScale = RandomSize();

            GO.GetComponent<FireGrid>().StartFire();
            n.SetOnFire();
        }
    }

    /// <summary>
    /// Set different size for the transform
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomSize()
    {
        float random = Random.Range(1f, 1.5f);
        return transform.localScale = new Vector3(random, random, random);
    }

    /// <summary>
    /// Grow fire by instantiating the bigger fire
    /// </summary>
    private void GrowFire()
    {
        if (currentFire != FireRules.Fires.Length)
        {
            currentFire++;
            GameObject GO = Instantiate(FireRules.Fires[currentFire - 1].gameObject, GridGO.transform);
            GO.transform.position = transform.position;

            var fire = GO.GetComponent<FireGrid>();
            fire.currentFire = this.currentFire;
            fire.Node = Node;

            fire.StartFire();

            Destroy(this.gameObject, 2f);
        }
        else if (currentFire == FireRules.Fires.Length)
        {
            SpreadFire();
        }
    }

    /// <summary>
    /// Decrease the fire by making it smaller
    /// </summary>
    public void DecreaseFire()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// On collision with particles. Decrease damage
    /// </summary>
    /// <param name="other"></param>
    void OnParticleCollision(GameObject other)
    {
        // Avoid getting collision with the tool itself
        if (!other.transform.CompareTag("Interactable"))
        {
            health--;

            if (localSmoke == null)
            {
                GameObject GO = Instantiate(FireRules.Smoke.gameObject);
                GO.transform.position = transform.position;
                localSmoke = GO.transform;
            }

            if (health <= 0)
            {
                CancelInvoke();
                StartCoroutine(StopFire());

                Destroy(gameObject, 2f);
                Node.FireOut();
            }
        }
    }

    /// <summary>
    /// Get currentfire number based on the fire list (in firerules)
    /// </summary>
    /// <returns></returns>
    public int GetCurrentFire()
    {
        return currentFire;
    }

    /// <summary>
    /// Change localScale when the fire is going out
    /// </summary>
    /// <returns></returns>
    IEnumerator StopFire()
    {
        while (true)
        {
            particle.Stop();
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0f, 0f, 0f), 0.05f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
