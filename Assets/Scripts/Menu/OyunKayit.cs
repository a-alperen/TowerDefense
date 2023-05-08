using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OyunKayit
{
    public static string kolay = "Kolay";
    public static string orta = "Orta";
    public static string zor = "Zor";

    public static string kolayPuan = "KolayPuan";
    public static string ortaPuan = "OrtaPuan";
    public static string zorPuan = "ZorPuan";

    public static string kolayAltin = "KolayAltin";
    public static string ortaAltin = "OrtaAltin";
    public static string zorAltin = "ZorAltin";

    public static string muzikAcik = "MuzikAcik";

    #region Zorluk icin kayit islemi
    public static void KolayDegerAta(int kolay)
    {
        PlayerPrefs.SetInt(OyunKayit.kolay, kolay);
    }
    public static int KolayDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.kolay);
    }

    public static void OrtaDegerAta(int orta)
    {
        PlayerPrefs.SetInt(OyunKayit.orta, orta);
    }
    public static int OrtaDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.orta);
    }

    public static void ZorDegerAta(int zor)
    {
        PlayerPrefs.SetInt(OyunKayit.zor, zor);
    }
    public static int ZorDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.zor);
    }
    #endregion


    #region Puan için kayıt islemi
    public static void KolayPuanDegerAta(int kolayPuan)
    {
        PlayerPrefs.SetInt(OyunKayit.kolayPuan, kolayPuan);
    }
    public static int KolayPuanDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.kolayPuan);
    }

    public static void OrtaPuanDegerAta(int ortaPuan)
    {
        PlayerPrefs.SetInt(OyunKayit.ortaPuan, ortaPuan);
    }
    public static int OrtaPuanDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.ortaPuan);
    }

    public static void ZorPuanDegerAta(int zorPuan)
    {
        PlayerPrefs.SetInt(OyunKayit.zorPuan, zorPuan);
    }
    public static int ZorPuanDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.zorPuan);
    }
    #endregion


    #region Altin icin kayit islemi
    public static void KolayAltinDegerAta(int kolayAltin)
    {
        PlayerPrefs.SetInt(OyunKayit.kolayAltin, kolayAltin);
    }
    public static int KolayAltinDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.kolayAltin);
    }

    public static void OrtaAltinDegerAta(int ortaAltin)
    {
        PlayerPrefs.SetInt(OyunKayit.ortaAltin, ortaAltin);
    }
    public static int OrtaAltinDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.ortaAltin);
    }

    public static void ZorAltinDegerAta(int zorAltin)
    {
        PlayerPrefs.SetInt(OyunKayit.zorAltin, zorAltin);
    }
    public static int ZorAltinDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.zorAltin);
    }
    #endregion


    public static void MuzikAcikDegerAta(int muzikAcik)
    {
        PlayerPrefs.SetInt(OyunKayit.muzikAcik, muzikAcik);
    }
    public static int MuzikAcikDegerOku()
    {
        return PlayerPrefs.GetInt(OyunKayit.muzikAcik);
    }

    /// <summary>
    /// Zorluk icin daha onceden bir kayit var mi kontrolu yapiyor.
    /// </summary>
    /// <returns></returns>
    public static bool ZorlukKayitVarmi()
    {
        if(PlayerPrefs.HasKey(OyunKayit.kolay) || PlayerPrefs.HasKey(OyunKayit.orta) || PlayerPrefs.HasKey(OyunKayit.zor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool MuzikAcikKayitVarmi()
    {
        if (PlayerPrefs.HasKey(OyunKayit.muzikAcik))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
