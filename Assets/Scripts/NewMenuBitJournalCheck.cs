using UnityEngine;
using System.Collections.Generic;

public class NewMenuBitJournalCheck : MonoBehaviour
{

    //Lil Town
    public GameObject LT1;
    public GameObject LT2;
    public GameObject LT3;
    public GameObject LT4;
    public GameObject LT5;
    public GameObject LT6;
    public GameObject LT7;
    public GameObject LT8;
    public GameObject LT9;
    public GameObject LT10;

    //Girdwood

    public GameObject G1;
    public GameObject G2;
    public GameObject G3;
    public GameObject G4;
    public GameObject G5;
    
    //Nome

    public GameObject N1;
    public GameObject N2;
    public GameObject N3;
    public GameObject N4;
    public GameObject N5;
    public GameObject N6;

    //Iliamna

    public GameObject I1;
    public GameObject I2;
    public GameObject I3;
    public GameObject I4;
    public GameObject I5;

    //Denali

    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;

    //Utqiagvik

    public GameObject U1;
    public GameObject U2;
    public GameObject U3;
    public GameObject U4;
    public GameObject U5;
    public GameObject U6;
    public GameObject U7;
    public GameObject U8;
    public GameObject U9;
    public GameObject U10;
    public GameObject U11;

    private Dictionary<string, GameObject> bitObjects = new Dictionary<string, GameObject>();

    void Start()
    {
        // Lil Town
        bitObjects.Add("LTBit1", LT1); bitObjects.Add("LTBit2", LT2); bitObjects.Add("LTBit3", LT3);
        bitObjects.Add("LTBit4", LT4); bitObjects.Add("LTBit5", LT5); bitObjects.Add("LTBit6", LT6);
        bitObjects.Add("LTBit7", LT7); bitObjects.Add("LTBit8", LT8); bitObjects.Add("LTBit9", LT9);
        bitObjects.Add("LTBit10", LT10);

        // Girdwood
        bitObjects.Add("GBit1", G1); bitObjects.Add("GBit2", G2); bitObjects.Add("GBit3", G3);
        bitObjects.Add("GBit4", G4); bitObjects.Add("GBit5", G5);

        // Nome
        bitObjects.Add("NBit1", N1); bitObjects.Add("NBit2", N2); bitObjects.Add("NBit3", N3);
        bitObjects.Add("NBit4", N4); bitObjects.Add("NBit5", N5); bitObjects.Add("NBit6", N6);

        // Iliamna
        bitObjects.Add("IBit1", I1); bitObjects.Add("IBit2", I2); bitObjects.Add("IBit3", I3);
        bitObjects.Add("IBit4", I4); bitObjects.Add("IBit5", I5);

        // Denali
        bitObjects.Add("DBit1", D1); bitObjects.Add("DBit2", D2); bitObjects.Add("DBit3", D3);
        bitObjects.Add("DBit4", D4); bitObjects.Add("DBit5", D5); bitObjects.Add("DBit6", D6);

        // Utqiagvik
        bitObjects.Add("UBit1", U1); bitObjects.Add("UBit2", U2); bitObjects.Add("UBit3", U3);
        bitObjects.Add("UBit4", U4); bitObjects.Add("UBit5", U5); bitObjects.Add("UBit6", U6);
        bitObjects.Add("UBit7", U7); bitObjects.Add("UBit8", U8); bitObjects.Add("UBit9", U9);
        bitObjects.Add("UBit10", U10); bitObjects.Add("UBit11", U11);
    }

    void Update()
    {
        foreach (var pair in bitObjects)
        {
            pair.Value.SetActive(GameManager.Instance.IsBitCollected(pair.Key));
        }
    }
}
