using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int DAT_1180; //gp+1180h
    public int DAT_1184; //gp+1184h
    public int DAT_118C; //gp+118Ch
    public List<JUNC_DB> juncList = new List<JUNC_DB>(); //gp+1198h
    public List<XOBF_DB> xobfList = new List<XOBF_DB>(); //0xC6220

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
