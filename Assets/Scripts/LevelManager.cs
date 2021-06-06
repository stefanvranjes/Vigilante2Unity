using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int DAT_1180; //gp+1180h
    public int DAT_1184; //gp+1184h
    public int DAT_118C; //gp+118Ch
    public List<XRTP_DB> xrtpList = new List<XRTP_DB>(); //gp+1194h
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

    //FUN_7E6C (LOAD.DLL)
    public void FUN_7E6C()
    {
        RSEG_DB dbVar1;
        int iVar2;
        JUNC_DB dbVar3;
        int iVar4;
        JUNC_DB dbVar5;

        iVar4 = 0;

        if (0 < DAT_1184)
        {
            for (int i = 0; i < DAT_1184; i++)
            {
                dbVar5 = juncList[i];

                if (dbVar5.DAT_11 != 0)
                {
                    for (int j = 0; j < dbVar5.DAT_11; j++)
                    {
                        dbVar1 = dbVar5.DAT_1C[j];

                        //if (dbVar5=dbVar1.DAT_00 && (dbVar1.DAT_0C & 0x10) == 0 && 
                        //    (uint)dbVar1.DAT_0A * 0x34 + )
                    }
                }
            }
        }
    }
}
