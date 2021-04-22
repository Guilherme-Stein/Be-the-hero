using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDificuldade : MonoBehaviour
{

    #region Váriaveis staticas
    //Velocidade prédios Run surgindo
    public static float VelocidadePrediosRun;
    //numero de inimigos na mecanica run
    public static int QuantidadeInimigos;
    //numero de predios spawnados na mecanica Run
    public static int QuantidadePrediosRun;
    //Velocidade da caida na mecanica fall
    public static float VelocidadeCaidaFall;
    //numero de predios spawnados na mecanica fall
    public static int QuantidadePrediosFall;
    //Velocidade do inimigo ir em direção ao player
    public static float VelocidadeInimigo;
    //Tempo até trocar para mecanica de falling
    public static float TempoTroca;
    //Tempo até ser spawnado outro carro
    public static float TempoCarros;
    //Tempo até ser spawnado outro carro
    public static float VelocidadeCarros;
    //Verificar a troca de mecanica
    public static bool TrocaRun;
    public static bool TrocaFall;
    #endregion

    [SerializeField] [Header("Prédios Corrida")] [Range(1.0f, 50.0f)] private float velPrediosRun;
    [SerializeField] [Range(1.0f, 10.0f)] private int qtdInimigos;
    [SerializeField] [Range(1.0f, 10.0f)] private int qtdPrediosRun;
    [SerializeField] [Range(1.0f, 100.0f)] private float tempoTroca;
    [SerializeField] [Header("Prédios Caida")] [Range(1.0f, 30.0f)] private float velCaidaFall;
    [SerializeField] [Range(1.0f, 120.0f)] private int qtdPrediosFall;
    [SerializeField] [Header("Carros")] [Range(1.0f, 10.0f)] private float tempoCarros;
    [SerializeField] [Range(1.0f, 50.0f)] private float velCarros;
    [SerializeField] [Header("Inimigos")] [Range(1.0f, 30.0f)] private float velInimigo;

    void Start(){
        VelocidadePrediosRun = velPrediosRun;
        QuantidadeInimigos = qtdInimigos;
        QuantidadePrediosRun = qtdPrediosRun;
        VelocidadeCaidaFall = velCaidaFall;
        QuantidadePrediosFall = qtdPrediosFall;
        VelocidadeInimigo = velInimigo;
        TempoTroca = tempoTroca;
        TempoCarros = tempoCarros;
        VelocidadeCarros = velCarros;
        TrocaRun = false;
        TrocaFall = false;
        
    }

    void Update(){
        if (TrocaRun){
            //Velocidade Do Player
            if (VelocidadePrediosRun < 40.0f){
                    VelocidadePrediosRun += 15f;
                    print("Velocidade: " + VelocidadePrediosRun);
            }else{
                VelocidadePrediosRun = 45.0f;
            }
            //Tempo De troca FPS
            if (TempoTroca < 30.0f){
                TempoTroca += 5f;
            }else{
                TempoTroca = 35.0f;
            }
            //Velocidade Ciborg
            if (VelocidadeInimigo < 15.5f)
            {
                VelocidadeInimigo += 8f;
            }
            else
            {
                VelocidadeInimigo = 17f;
            }
            //Tempo Carros Respawn
            if (TempoCarros > 3f)
            {
                TempoCarros -= 5f;
            
            }
            else
            {
                TempoCarros = 2f;
            }

            //Quantidade de Inimigo
            //if (QuantidadeInimigos < 2)
            //{
            //    QuantidadeInimigos += 1;
            //}
            //else
            //{
            //    QuantidadeInimigos = 2;
            //}
            
            TrocaRun = false;

            //Velocidade Da Queda
        }else if(TrocaFall){
            if(VelocidadeCaidaFall < 21.0f){
                    VelocidadeCaidaFall += 5f;
            }else{
                VelocidadeCaidaFall = 24.0f;
            }

            //Quantidade de Predios 
            if(QuantidadePrediosFall < 110){
                    QuantidadePrediosFall += 15;
            }else{
                QuantidadePrediosFall = 120;
            }

            TrocaFall = false;
        }
    }
}
