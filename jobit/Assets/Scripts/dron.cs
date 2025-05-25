using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class dron : MonoBehaviour
{
    public gamemanager gamemanager;
    public GameObject baza;
    public NavMeshAgent agent;
    public GameObject targetore;
    public GameObject particles;
    public ore targetorescr;
    public int gamemode;
    public int idbase;
    public int myid;
    public Image mappoint;
    public RectTransform mappointpos;
    // Start is called before the first frame update
    void Start()
    {
        mappointpos = mappoint.GetComponent<RectTransform>();
        mappoint.color = new Color(1, 0, 0);
        updTargetOre();
    }

    // Update is called once per frame
    void Update()
    {
        mappointpos.anchoredPosition = new Vector2(transform.position.x / 10, transform.position.z / 10);
        if (gamemode == 0)
        {
            if(targetore == null)
            {
                updTargetOre();
            }
            if (targetore != null)
            {
                float dist = Mathf.Pow(Mathf.Pow(gameObject.transform.position.x - targetore.transform.position.x, 2) + Mathf.Pow(gameObject.transform.position.z - targetore.transform.position.z, 2), 0.5f);
                if (dist <= 2)
                {
                    particles.SetActive(true);
                    targetorescr.setup();
                    gamemode = 1;
                    mappoint.color = new Color(1, 1, 0);
                    StartCoroutine(sleeping(2));
                }
            }
        }
        if(gamemode == 2)
        {
            float dist = Mathf.Pow(Mathf.Pow(gameObject.transform.position.x - baza.transform.position.x, 2) + Mathf.Pow(gameObject.transform.position.z - baza.transform.position.z, 2), 0.5f);
            if (dist <= 2)
            {
                mappoint.color = new Color(1, 1, 1);
                gamemode = 3;
                StartCoroutine(sleeping(2));
            }
        }
    }
    public IEnumerator sleeping(float tm)
    {
        yield return new WaitForSeconds(tm);
        if (gamemode == 1)
        {
            gamemode = 2;
            particles.SetActive(false);
            gamemanager.clearOre(targetorescr.myid);
            agent.SetDestination(baza.transform.position);
            mappoint.color = new Color(0, 1, 0);
            targetore = null;
            targetorescr = null;
            yield break;
        }
        if (gamemode == 3)
        {
            mappoint.color = new Color(1, 0, 0);
            gamemanager.orehub[idbase]++;
            gamemanager.txorehub[idbase].text = gamemanager.orehub[idbase].ToString();
            updTargetOre();
            gamemode = 0;
            yield break;
        }
    }
    public void updTargetOre()
    {
        targetorescr = gamemanager.getOre(gameObject);
        if (targetorescr != null)
        {
            targetore = targetorescr.gameObject;
            agent.SetDestination(targetore.transform.position);
        }
    }
}
