using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data 
{
    public static string gold = "Gold";

    public static string normalStudentHealth = "NormalStudentHealth";
    public static string normalStudentSpeed = "NormalStudentSpeed";
    public static string normalStudentCount = "NormalStudentCount";

    public static string lazyStudentHealth = "NormalStudentHealth";
    public static string lazyStudentSpeed = "NormalStudentSpeed";
    public static string lazyStudentCount = "NormalStudentCount";

    public static string hardworkingStudentHealth = "NormalStudentHealth";
    public static string hardworkingStudentSpeed = "NormalStudentSpeed";
    public static string hardworkingStudentCount = "NormalStudentCount";

    #region Para degerini guncelleme
    public static void SetGoldValue(int gold)
    {
        PlayerPrefs.SetInt(Data.gold, gold);
    }
    public static int GetGoldValue()
    {
        return PlayerPrefs.GetInt(Data.gold);
    }
    #endregion

    #region NormalStudent degiskenlerini kaydetme
    public static void SetNormalStudentHealthValue(int health)
    {
        PlayerPrefs.SetInt(Data.normalStudentHealth, health);
    }
    public static int GetNormalStudentHealthValue()
    {
        return PlayerPrefs.GetInt(Data.normalStudentHealth);
    }
    public static void SetNormalStudentSpeedValue(int speed)
    {
        PlayerPrefs.SetInt(Data.normalStudentSpeed, speed);
    }
    public static int GetNormalStudentSpeedValue()
    {
        return PlayerPrefs.GetInt(Data.normalStudentSpeed);
    }
    public static void SetNormalStudentCountValue(int count)
    {
        PlayerPrefs.SetInt(Data.normalStudentCount, count);
    }
    public static int GetNormalStudentCountValue()
    {
        return PlayerPrefs.GetInt(Data.normalStudentCount);
    }
    #endregion

    #region LazyStudent degiskenlerini kaydetme
    public static void SetLazyStudentHealthValue(int health)
    {
        PlayerPrefs.SetInt(Data.lazyStudentHealth, health);
    }
    public static int GetLazyStudentHealthValue()
    {
        return PlayerPrefs.GetInt(Data.lazyStudentHealth);
    }
    public static void SetLazyStudentSpeedValue(int speed)
    {
        PlayerPrefs.SetInt(Data.lazyStudentSpeed, speed);
    }
    public static int GetLazyStudentSpeedValue()
    {
        return PlayerPrefs.GetInt(Data.lazyStudentSpeed);
    }
    public static void SetLazyStudentCountValue(int count)
    {
        PlayerPrefs.SetInt(Data.lazyStudentCount, count);
    }
    public static int GetLazyStudentCountValue()
    {
        return PlayerPrefs.GetInt(Data.lazyStudentCount);
    }
    #endregion

    #region HardworkingStudent degiskenlerini kaydetme
    public static void SetHardworkingStudentHealthValue(int health)
    {
        PlayerPrefs.SetInt(Data.hardworkingStudentHealth, health);
    }
    public static int GetHardworkingStudentHealthValue()
    {
        return PlayerPrefs.GetInt(Data.hardworkingStudentHealth);
    }
    public static void SetHardworkingStudentSpeedValue(int speed)
    {
        PlayerPrefs.SetInt(Data.hardworkingStudentSpeed, speed);
    }
    public static int GetHardworkingStudentSpeedValue()
    {
        return PlayerPrefs.GetInt(Data.hardworkingStudentSpeed);
    }
    public static void SetHardworkingStudentCountValue(int count)
    {
        PlayerPrefs.SetInt(Data.hardworkingStudentCount, count);
    }
    public static int GetHardworkingStudentCountValue()
    {
        return PlayerPrefs.GetInt(Data.hardworkingStudentCount);
    }
    #endregion
}
