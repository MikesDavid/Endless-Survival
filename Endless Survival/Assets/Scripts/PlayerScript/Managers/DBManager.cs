using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager 
{
    public static string username; //
    //public static string save; //
    public static int szint; //
    public static string tulelesido; //
    public static int death; //
    public static string primary; //
    public static string secondary; //
    public static int damageTagen; //
    public static int kills; // 
    public static DateTime date; //

    public static bool LoggedIn { get { return username != null; } }

}
