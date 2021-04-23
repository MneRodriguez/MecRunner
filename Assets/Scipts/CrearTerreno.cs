using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrearTerreno : MonoBehaviour
{
    public Camera mainCam;

    public Transform PuntoPartida;
    public GenrcnPlatfrm prefabMapa;

    public float VelMovto = 12;
    public int mapaDePreSpawn = 15;
    public int mapaSinObstc = 3;

    List<GenrcnPlatfrm> mapaSpawneado = new List<GenrcnPlatfrm>();
    //int sigMapaAgenerar = -1;

    [HideInInspector] // esto sirve para ocultar en un principio las variables declaradas debajo
    public bool gameOver = false;
    public bool gameStarted = false;
    float Puntaje = 0;

    public static GenrcnPlatfrm instance;
    void Start()
    {
        //instance = this; EL THIS ME DABA ERROR, VER LUEGO SI AFECTA DEJARLO DESACTIVADO

        Vector3 PosDeSpawn = PuntoPartida.position;
        int mapaSinObstcTemp = mapaSinObstc;

        for (int i=0; i<mapaDePreSpawn; i++)
        {
            PosDeSpawn -= prefabMapa.PuntoDePartida.localPosition;
            GenrcnPlatfrm MapaDelJuego = Instantiate(prefabMapa, PosDeSpawn, Quaternion.identity);
            if (mapaSinObstcTemp > 0)
            {
                MapaDelJuego.DesactivarTodosLosObst();
                mapaSinObstcTemp--;
            }
            else
            {
                MapaDelJuego.ActivarObstAlAzar();
            }

            PosDeSpawn = MapaDelJuego.PuntoDeFin.position;
            MapaDelJuego.transform.SetParent(transform);
            mapaSpawneado.Add(MapaDelJuego);
        }
    }

    
    void Update()
    {
        if (!gameOver && gameStarted)
        {
            transform.Translate(-mapaSpawneado[0].transform.forward * Time.deltaTime * (VelMovto + (Puntaje / 500)), Space.World);
            Puntaje += Time.deltaTime * VelMovto;
        }

        if (mainCam.WorldToViewportPoint(mapaSpawneado[0].PuntoDeFin.position).z <= 0)
        {
            GenrcnPlatfrm SueloTemp = mapaSpawneado[0];
            mapaSpawneado.RemoveAt(0);
            SueloTemp.transform.position = mapaSpawneado[mapaSpawneado.Count - 1].PuntoDeFin.position - SueloTemp.PuntoDePartida.localPosition;
            SueloTemp.ActivarObstAlAzar();
            mapaSpawneado.Add(SueloTemp);
        }

        if (gameOver || !gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // TOMA EL INPUT DEL ENTER PARA EMPEZAR A JUGAR
            {
                if (gameOver)
                {
                    Scene escena = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(escena.name);
                }
                else
                {
                    gameStarted = true;
                }
            }
        }
    }

    public void OnGUI()
    {
        if (gameOver)
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Perdiste!\n Tu puntaje: " + ((int)Puntaje) + "\n Presiona ENTER para reiniciar");
        }
        else
        {
            if (!gameStarted)
            {
                GUI.color = Color.yellow;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Presiona ENTER para empezar a jugar");
            }
        }

        GUI.color = Color.green;
        GUI.Label(new Rect(5, 5, 200, 25), "Tu puntaje: " + ((int)Puntaje));
    }
}
