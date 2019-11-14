using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Log : MonoBehaviour {

    private string File_in;
    private System.IO.StreamReader file;
    string File_log_out, cs_str;
    private const string STORAGE_PERMISSION = "android.permission.WRITE_EXTERNAL_STORAGE";

    void Awake() {

        // prise en compte de l'heure afin de générer un fichier dont le nom changera automatiquement
        System.DateTime localDate = System.DateTime.Now;
        var culture = new System.Globalization.CultureInfo("de-DE");
        string str_date = localDate.ToString(culture);
        str_date = str_date.Replace(" ", ".");
        str_date = str_date.Replace(":", ".");


        if (Application.platform == RuntimePlatform.Android) {
            // il s'agit là du chemin sur votre Android qui permet d'atteindre le répertoire Download
            File_log_out = "/storage/emulated/0/Download/data/log_out_" + str_date + ".txt";//File_log_out
            AndroidPermissionsManager.RequestPermission(new[] { STORAGE_PERMISSION }, new AndroidPermissionCallback(
                grantedPermission => {
                    // The permission was successfully granted, restart the change avatar routine
                    //OnBrowseGalleryButtonPress();
                },
                deniedPermission => {
                    // The permission was denied
                },
                deniedPermissionAndDontAskAgain => {
                    // The permission was denied, and the user has selected "Don't ask again"
                    // Show in-game pop-up message stating that the user can change permissions in Android Application Settings
                    // if he changes his mind (also required by Google Featuring program)
                }));

            cs_print_out("coucou android");
        }
        else {
            cs_print_out("coucou edit");
        }

        if (Application.platform == RuntimePlatform.Android) {
            File_in = "/storage/emulated/0/Download/data/DataIn.dat";
        }
        else {
            // Le chemin qui correspond à votre projet sur votre PC
            File_in = "C:\\Users\\ailso\\Documents\\Unity\\maze\\data\\DataIn.dat";
        }

        // Procédure pour lire un fichier texte en C#
        file = new System.IO.StreamReader(File_in);
        string line_in;
        int i = 0;
        while ((line_in = file.ReadLine()) != null) {
            float[] Positions = rs_line2values(line_in);
            cs_str = "ligne" + i.ToString() + "valeur = " + Positions[0].ToString();
            print(cs_str);
            Vector3 origin = new Vector3(1.0f, 0.0f, 0.0f);
            // L'objet auquel est associé votre .cs va se positionner à la coordonnée "origin"
            transform.localPosition = origin;
            // Pour vérifier que votre application lit bien le fichier DataIn
            cs_print_out(cs_str);
            i++;
        }

    }
    //
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void cs_print_out(string cs_str) {
        if (Application.platform == RuntimePlatform.Android) {
            // un fichier texte va etre créé dans download/data dans lequel sera sauvegardé cs_str
            System.IO.File.AppendAllText(File_log_out, cs_str + "\r\n");
        }
        else {
            // l’affichage se fera dans la fenêtre “Console” en bas à gauche dans Unity
            Debug.Log(cs_str);
        }
    }

    // Une petite fonction pour lire plusieures lignes
    float[] rs_line2values(string line_in) {
        string[] StringSeparator = new string[] { " " };
        string[] valeurs = line_in.Split(StringSeparator, StringSplitOptions.RemoveEmptyEntries);
        float[] Positions = new float[3];
        Positions[0] = System.Convert.ToSingle(valeurs[0]);
        Positions[1] = System.Convert.ToSingle(valeurs[1]);
        Positions[2] = System.Convert.ToSingle(valeurs[2]);

        return (Positions);
    }

}

