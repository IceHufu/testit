using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public Hashtable bases;
    public Hashtable droids;
    public Hashtable ores;
    public GameObject prefbase;
    public GameObject prefdroid;
    public dron prefdroidscr;
    public GameObject prefore;
    public ore preforescr;
    public GameObject prefmappoint;
    public GameObject map;
    public int countdroids;
    public int[] droidstobase;
    public int currentiddroid;
    public int currentidore;
    public cameraMove cameraMove;
    public float currentTmSpaunOre;
    public float tmSpaunOre;
    public Text txcountdroids;
    public float speeddroids;
    public Text txspeeddroids;
    public int spaunOrePerSecond;
    public Text txSpaunOrePerSecond;
    public int[] orehub;
    public Text[] txorehub;
    public float speedGame;
    public Text txSpeedGame;
    void Start()
    {
        if(spaunOrePerSecond == 0)
        {
            spaunOrePerSecond = 1;
        }
        if (countdroids == 0)
        {
            countdroids = 5;
        }
        if(speedGame == 0)
        {
            speedGame = 1;
        }
        speeddroids = 20;
        currentTmSpaunOre = 1f / spaunOrePerSecond;
        txSpeedGame.text = $"Скорость игры: {speedGame}X";
        txSpaunOrePerSecond.text = $"Спавн руды в секунду: {spaunOrePerSecond}";
        txspeeddroids.text = $"Скорость дроидов: {speeddroids}";
        txcountdroids.text = $"Дроны: {countdroids}";
        prefdroidscr = prefdroid.GetComponent<dron>();
        prefdroidscr.gamemanager = this;
        preforescr = prefore.GetComponent<ore>();
        bases = new Hashtable();
        droids = new Hashtable();
        ores = new Hashtable();
        droidstobase = new int[2];
        orehub = new int[2];

        createAllBases();
        createAllDroids();
    }
    public void Update()
    {
        tmSpaunOre -= Time.deltaTime * speedGame;
        if (tmSpaunOre <= 0)
        {
            createOre();
            tmSpaunOre = currentTmSpaunOre;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) == true)
        {
            setTrackBot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            setTrackBot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            setTrackBot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            setTrackBot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) == true)
        {
            setTrackBot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) == true)
        {
            setTrackBot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) == true)
        {
            setTrackBot(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) == true)
        {
            setTrackBot(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) == true)
        {
            setTrackBot(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) == true)
        {
            setTrackBot(9);
        }
    }
    public void setTrackBot(int i)
    {
        int i2 = 0;
        foreach (DictionaryEntry entry in droids)
        {
            if (i2 == i)
            {
                GameObject NDroid = entry.Value as GameObject;
                cameraMove.Target = NDroid.transform;
            }
            i2++;
        }
    }
    public void createAllBases()
    {
        GameObject NBase = Instantiate(prefbase, new Vector3(450, 2, 0), Quaternion.identity);
        NBase.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        bases.Add("0", NBase);
        NBase = Instantiate(prefbase, new Vector3(-450, 2, 0), Quaternion.identity);
        NBase.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
        bases.Add("1", NBase);
    }
    public void createAllDroids()
    {
        for (int i = 1; i <= countdroids; i++)
        {
            for (int idbase = 0; idbase <= 1; idbase++)
            {
                GameObject targetbase = bases[$"{idbase}"] as GameObject;
                prefdroidscr.baza = targetbase;
                prefdroidscr.idbase = idbase;
                prefdroidscr.myid = currentiddroid;
                GameObject NMappoint = Instantiate(prefmappoint, map.transform);
                prefdroidscr.mappoint = NMappoint.GetComponent<Image>();
                GameObject NDroid = Instantiate(prefdroid, targetbase.transform.position, Quaternion.identity);
                if (idbase == 0)
                {
                    NDroid.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                }
                if (idbase == 1)
                {
                    NDroid.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
                }
                droidstobase[idbase]++;
                droids.Add($"{currentiddroid}", NDroid);
                currentiddroid++;
            }
        }
    }
    public void createOre()
    {
        preforescr.myid = currentidore;
        GameObject NOre = Instantiate(prefore, new Vector3(Random.Range(-300, 300), 2, Random.Range(-300, 300)), Quaternion.identity);
        ores.Add($"{currentidore}", NOre.GetComponent<ore>());
        currentidore++;
    }
    public ore getOre(GameObject dron)
    {
        ore retOre = null;
        float dist = 0;
        foreach (DictionaryEntry entry in ores)
        {
            ore NOrescr = entry.Value as ore;
            GameObject NOre = NOrescr.gameObject;
            float ndist = Vector3.Distance(NOre.transform.position, dron.transform.position);
            if ((retOre == null || ndist < dist) & NOrescr.inprivate == 0)
            {
                dist = ndist;
                retOre = NOrescr;
            }
        }
        if (retOre != null)
        {
            retOre.inprivate = 1;
        }
        return retOre;
    }
    public void clearOre(int id)
    {
        if (ores.ContainsKey($"{id}") == true)
        {
            ore TOre = ores[$"{id}"] as ore;
            Destroy(TOre.gameObject);
            ores.Remove($"{id}");
        }
    }
    public void updCountBots(Slider slider)
    {
        countdroids = Mathf.RoundToInt(slider.value);
        txcountdroids.text = $"Дроны: {countdroids}";
        List<string> delbots = new List<string>();
        foreach (DictionaryEntry entry in droids)
        {
            dron NDroidscr = (entry.Value as GameObject).GetComponent<dron>();
            if (droidstobase[NDroidscr.idbase] > countdroids)
            {
                delbots.Add(entry.Key.ToString());
                droidstobase[NDroidscr.idbase]--;
            }
        }
        foreach (string onedelbot in delbots)
        {
            if (droids.ContainsKey($"{onedelbot}") == true)
            {
                dron NDroidscr = (droids[$"{onedelbot}"] as GameObject).GetComponent<dron>();
                Destroy(NDroidscr.mappoint.gameObject);
                Destroy(NDroidscr.gameObject);
                droids.Remove(onedelbot);
            }
        }
        for (int idbase = 0; idbase <= 1; idbase++)
        {
            while(droidstobase[idbase] < countdroids)
            {
                GameObject targetbase = bases[$"{idbase}"] as GameObject;
                prefdroidscr.baza = targetbase;
                prefdroidscr.idbase = idbase;
                prefdroidscr.myid = currentiddroid;
                GameObject NMappoint = Instantiate(prefmappoint, map.transform);
                prefdroidscr.mappoint = NMappoint.GetComponent<Image>();
                GameObject NDroid = Instantiate(prefdroid, targetbase.transform.position, Quaternion.identity);
                if (idbase == 0)
                {
                    NDroid.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                }
                if (idbase == 1)
                {
                    NDroid.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
                }
                droidstobase[idbase]++;
                droids.Add($"{currentiddroid}", NDroid);
                currentiddroid++;
            }
        }
    }
    public void setResPerSecond(Slider slider)
    {
        spaunOrePerSecond = Mathf.RoundToInt(slider.value);
        txSpaunOrePerSecond.text = $"Спавн руды в секунду: {spaunOrePerSecond}";
        currentTmSpaunOre = 1f / spaunOrePerSecond;
    }
    public void setSpeedDroids(Slider slider)
    {
        if (slider != null)
        {
            speeddroids = slider.value;
            txspeeddroids.text = $"Скорость дроидов: {speeddroids}";
        }
        NavMeshAgent agent;
        foreach (DictionaryEntry entry in droids)
        {
            dron NDroidscr = (entry.Value as GameObject).GetComponent<dron>();
            agent = NDroidscr.GetComponent<NavMeshAgent>();
            agent.speed = speeddroids * speedGame;
            agent.acceleration = speeddroids * speedGame;
        }
        agent = prefdroid.GetComponent<NavMeshAgent>();
        agent.speed = speeddroids * speedGame;
        agent.acceleration = speeddroids * speedGame;
    }
    public void setSpeedGame(Slider slider)
    {
        speedGame = slider.value;
        txSpeedGame.text = $"Скорость игры: {speedGame}X";
        setSpeedDroids(null);
    }
}
