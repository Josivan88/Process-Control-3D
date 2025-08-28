

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Globalization;

namespace NeuralNetwork
{

    using UnityEngine;
    using System.Collections;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Reusable;

    [System.Serializable]
    public class NeuralNet
    {
        public string Name;                 //Nome
        public float InputMax = 1f;           //Valor maximo de entrada
        public float InputMin = -1f;          //Valor minimo de entrada
        public float OutputMax = 1f;          //Valor maximo de saida
        public float OutputMin = -1f;         //Valor minimo de saida
        public bool Initialized = false;      //Inicializada
        public bool Trainning = false;        //treinando
        public int[] Structure;             //Estrutura. Ex: {1,2,1} 1 entrada, 2 neuronios na primeira camada oculta, e 1 neuronio de saida
        public int[] NumberOfWeights;       //quantos pesos em cada camada
        public int TotalFreeParameters;     //Total de parametros livres, pesos e bias
        public string ActivationFunction;   //Tipo de função de ativação
        public float alpha;                 //Valor de Alpha na sigmoid function
        public float[,] Weights;            //linhas: Pesos Colunas:Camadas de neuronios
        public float[,] TempWeights;        //linhas: Pesos Temporarios Colunas:Camadas de neuronios
        public float[,] NeuronsBias;        //linhas: Bias Colunas:Camadas de neuronios
        public float[,] TempNeuronsBias;    //linhas: Bias Temporarios Colunas:Camadas de neuronios
        public float[,] NeuronsValues;      //linhas: Valores nos Neuronios Colunas:Camadas de neuronios
        public float[,] NeuronsWeightSum;   //linhas: Valores da soma dos pesos e bias Colunas:Camadas de neuronios
        public float[] Output;              //Saida da rede
        public string Status = "Untouched";   //Status da rede: Untouched,Initialized,Learning,NotTrainedEnough,Trained
        public float Advance = 1;             //Avanço no treinamento
        public float NetworkError;          //Erro
        public float TrainNetworkError;     //Erro no ultimo treinamento
        public float RSquare;               //R2
        public float RSquareAdju;           //R2 ajustado
        public float TrainningTime;         //Tempo de treinamento
        public int NumberIterations;        //Numero de iteraçoes
    }

    [System.Serializable]
    public class GAStatistics
    {
        public NeuralNet[] Population;
    }

    [System.Serializable]
    public class FPSStat
    {
        public float FPS;
        public float FrameTime;
    }

    [System.Serializable]
    public class UIdata
    {
        public int FontSize;
        public int SquareSize;
        public int Casos;
    }

    [System.Serializable]
    public class DrawNetInfo
    {
        public float FontSize = 1;
        public float WeightPos = 5f;
        public bool VisibleParam = true;
        public bool EditParam;
        public string[,] WeightString = new string[1, 1];
        public string[,] BiasString = new string[1, 1];
    }

    public class Functions
    {

        public static Texture2D ScreenshotIcon = Resources.Load<Texture2D>("Textures/ScreenshotIcon");

        public static Texture2D gray12Texture = CreateTexture(Color.gray / 4);
        public static Texture2D gray25Texture = CreateTexture((Color.gray + Color.black) / 2);
        public static Texture2D gray50Texture = CreateTexture(Color.gray);
        public static Texture2D whiteTexture = CreateTexture(Color.white);
        public static Texture2D blackTexture = CreateTexture(Color.black);
        public static Texture2D RedTexture = CreateTexture((Color.red + Color.white) / 2);
        public static Texture2D YellowTexture = CreateTexture((Color.yellow + Color.white) / 2);
        public static Texture2D gray20Texture = CreateTexture((4 * Color.black + Color.white) / 5);
        public static Texture2D gray75Texture = CreateTexture((Color.gray + Color.white) / 2);

        //Inicialiar uma rede neural
        public static NeuralNet CreateNetwork(NeuralNet PointedNetwork, int[] NetworkStructure, string ActivationFun, string Name)
        {
            PointedNetwork.Name = Name;                                     //Nome
            PointedNetwork.Initialized = false;                             //Inicializada
            PointedNetwork.ActivationFunction = ActivationFun;            //Define o tipo de função de ativação pelo usuário
            PointedNetwork.alpha = 1f;
            PointedNetwork.Structure = new int[NetworkStructure.Length];    //Estrutura. Ex: {1,2,1} 1 entrada, 2 neuronios na primeira camada oculta, e 1 neuronio de saida
            for (int i = 0; i < NetworkStructure.Length; i++)
            {
                PointedNetwork.Structure[i] = NetworkStructure[i];
            }
            int MaxWeight = 0;
            PointedNetwork.NumberOfWeights = new int[NetworkStructure.Length];
            for (int i = 0; i < NetworkStructure.Length - 1; i++)
            {
                int Temp = NetworkStructure[i] * NetworkStructure[i + 1];
                PointedNetwork.NumberOfWeights[i] = Temp;
                if (Temp > MaxWeight)
                    MaxWeight = NetworkStructure[i] * NetworkStructure[i + 1];
            }

            PointedNetwork.TotalFreeParameters = 0;

            for (int i = 1; i < NetworkStructure.Length; i++)
            {//contabiliza os parametros livres
                PointedNetwork.TotalFreeParameters += NetworkStructure[i] * NetworkStructure[i - 1] + NetworkStructure[i];
            }
            PointedNetwork.TotalFreeParameters += NetworkStructure[NetworkStructure.Length - 1];

            PointedNetwork.NumberOfWeights[PointedNetwork.NumberOfWeights.Length - 1] = NetworkStructure[NetworkStructure.Length - 1];
            PointedNetwork.Weights = new float[MaxWeight, NetworkStructure.Length];        //linhas: Pesos Colunas:Camadas de neuronios
            PointedNetwork.TempWeights = new float[MaxWeight, NetworkStructure.Length];    //linhas: Pesos Temporarios Colunas:Camadas de neuronios
            PointedNetwork.NeuronsBias = new float[MaxInt(NetworkStructure), NetworkStructure.Length - 1];     //linhas: Bias Colunas:Camadas de neuronios
            PointedNetwork.TempNeuronsBias = new float[MaxInt(NetworkStructure), NetworkStructure.Length - 1]; //linhas: Bias Temporarios Colunas:Camadas de neuronios

            PointedNetwork.Initialized = true;

            for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                {
                    //System.Threading.Thread.Sleep(1);
                    PointedNetwork.Weights[i, j] = Random.Range(-1f, 1f);
                    PointedNetwork.TempWeights[i, j] = Random.Range(-1f, 1f);
                }
            }
            for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.TempNeuronsBias.GetLength(1); j++)
                {
                    //System.Threading.Thread.Sleep(1);
                    PointedNetwork.NeuronsBias[i, j] = Random.Range(-1f, 1f);
                    PointedNetwork.TempNeuronsBias[i, j] = Random.Range(-1f, 1f);
                }
            }
            //Definir como 1 os pesos finais
            for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
            {
                PointedNetwork.TempWeights[i, PointedNetwork.Weights.GetLength(1) - 1] = 1f;
                PointedNetwork.Weights[i, PointedNetwork.Weights.GetLength(1) - 1] = 1f;
            }

            PointedNetwork.NeuronsValues = new float[MaxInt(NetworkStructure), NetworkStructure.Length];      //linhas: Valores nos Neuronios Colunas:Camadas de neuronios
            PointedNetwork.NeuronsWeightSum = new float[MaxInt(NetworkStructure), NetworkStructure.Length];   //linhas: Valores de entrada nos Neuronios Colunas:Camadas de neuronios
            PointedNetwork.Output = new float[NetworkStructure[NetworkStructure.Length - 1]];                   //Saida da rede
            PointedNetwork.Status = "Initialized";
            PointedNetwork.NetworkError = float.PositiveInfinity;                    //Erro da rede
            PointedNetwork.TrainNetworkError = float.PositiveInfinity;               //Erro no ultimo treinamento
            PointedNetwork.NumberIterations = 0;
            return DeepClone(PointedNetwork);
        }

        //Reinicializar pesos e bias
        public static NeuralNet ReinitializeParameters(NeuralNet PointedNetwork, float range, string method)
        {
            PointedNetwork.Initialized = true;
            if (method == "Normalization")
            {
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                    {
                        PointedNetwork.Weights[i, j] = PointedNetwork.Weights[i, j] + Random.Range(-range, range);
                        PointedNetwork.TempWeights[i, j] = PointedNetwork.TempWeights[i, j] + Random.Range(-range, range);
                    }
                }
                for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.TempNeuronsBias.GetLength(1); j++)
                    {
                        PointedNetwork.NeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] / (1 + Random.Range(-range, range));
                        PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.TempNeuronsBias[i, j] / (1 + Random.Range(-range, range));
                    }
                }
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    PointedNetwork.TempWeights[i, PointedNetwork.Weights.GetLength(1) - 1] = PointedNetwork.TempWeights[i, PointedNetwork.Weights.GetLength(1) - 1] / (1 + Random.Range(-range, range));
                    PointedNetwork.Weights[i, PointedNetwork.Weights.GetLength(1) - 1] = PointedNetwork.Weights[i, PointedNetwork.Weights.GetLength(1) - 1] / (1 + Random.Range(-range, range));
                }
            }
            if (method == "Random")
            {
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                    {
                        PointedNetwork.Weights[i, j] = Random.Range(-range, range);
                        PointedNetwork.TempWeights[i, j] = Random.Range(-range, range);
                    }
                }
                for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.TempNeuronsBias.GetLength(1); j++)
                    {
                        PointedNetwork.NeuronsBias[i, j] = Random.Range(-range, range);
                        PointedNetwork.TempNeuronsBias[i, j] = Random.Range(-range, range);
                    }
                }
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    PointedNetwork.TempWeights[i, PointedNetwork.Weights.GetLength(1) - 1] = Random.Range(-range, range);
                    PointedNetwork.Weights[i, PointedNetwork.Weights.GetLength(1) - 1] = Random.Range(-range, range);
                }
            }
            PointedNetwork.Advance = 1f;
            PointedNetwork.NetworkError = float.PositiveInfinity;             //Erro da rede
            PointedNetwork.TrainNetworkError = float.PositiveInfinity;        //Erro no ultimo treinamento
            PointedNetwork.RSquare = 0f;                                      //R2 da rede
            PointedNetwork.RSquareAdju = 0f;                                  //R2 ajustado
            PointedNetwork.NumberIterations = 0;
            return DeepClone(PointedNetwork);
        }

        //Copiar uma rede neural
        public static NeuralNet CopyNetwork(NeuralNet OriginalNetwork, NeuralNet PointedNetwork)
        {

            PointedNetwork.Name = OriginalNetwork.Name;                               //Nome
            PointedNetwork.Initialized = OriginalNetwork.Initialized;                 //Inicializada
            PointedNetwork.ActivationFunction = OriginalNetwork.ActivationFunction;
            PointedNetwork.alpha = OriginalNetwork.alpha;
            PointedNetwork.InputMin = OriginalNetwork.InputMin;
            PointedNetwork.InputMax = OriginalNetwork.InputMax;
            PointedNetwork.OutputMin = OriginalNetwork.OutputMin;
            PointedNetwork.OutputMax = OriginalNetwork.OutputMax;
            PointedNetwork.Trainning = OriginalNetwork.Trainning;

            PointedNetwork.Structure = new int[OriginalNetwork.Structure.Length];    //Estrutura. Ex: {1,2,1} 1 entrada, 2 neuronios na primeira camada oculta, e 1 neuronio de saida
            for (int i = 0; i < OriginalNetwork.Structure.Length; i++)
            {
                PointedNetwork.Structure[i] = OriginalNetwork.Structure[i];
            }
            int MaxWeight = 0;
            PointedNetwork.NumberOfWeights = new int[OriginalNetwork.Structure.Length];
            for (int i = 0; i < OriginalNetwork.Structure.Length; i++)
            {
                PointedNetwork.NumberOfWeights[i] = OriginalNetwork.NumberOfWeights[i];
            }
            MaxWeight = OriginalNetwork.Weights.GetLength(0);

            PointedNetwork.TotalFreeParameters = OriginalNetwork.TotalFreeParameters;

            PointedNetwork.Weights = new float[MaxWeight, OriginalNetwork.Structure.Length];        //linhas: Pesos Colunas:Camadas de neuronios
            PointedNetwork.TempWeights = new float[MaxWeight, OriginalNetwork.Structure.Length];    //linhas: Pesos Temporarios Colunas:Camadas de neuronios
            PointedNetwork.NeuronsBias = new float[MaxInt(OriginalNetwork.Structure), OriginalNetwork.Structure.Length - 1];     //linhas: Bias Colunas:Camadas de neuronios
            PointedNetwork.TempNeuronsBias = new float[MaxInt(OriginalNetwork.Structure), OriginalNetwork.Structure.Length - 1]; //linhas: Bias Temporarios Colunas:Camadas de neuronios

            for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                {
                    PointedNetwork.Weights[i, j] = OriginalNetwork.Weights[i, j];
                    PointedNetwork.TempWeights[i, j] = OriginalNetwork.TempWeights[i, j];
                }
            }
            for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.TempNeuronsBias.GetLength(1); j++)
                {
                    PointedNetwork.NeuronsBias[i, j] = OriginalNetwork.NeuronsBias[i, j];
                    //PointedNetwork.TempNeuronsBias[i,j]=OriginalNetwork.NeuronsBias[i,j];
                }
            }

            PointedNetwork.Advance = OriginalNetwork.Advance;
            PointedNetwork.TrainningTime = OriginalNetwork.TrainningTime;
            PointedNetwork.NeuronsValues = OriginalNetwork.NeuronsValues;     //linhas: Valores nos Neuronios Colunas:Camadas de neuronios
            PointedNetwork.NeuronsWeightSum = OriginalNetwork.NeuronsWeightSum;  //linhas: Valores de entrada nos Neuronios Colunas:Camadas de neuronios
            PointedNetwork.Output = OriginalNetwork.Output;                  //Saida da rede
            PointedNetwork.Status = OriginalNetwork.Status;
            PointedNetwork.NetworkError = OriginalNetwork.NetworkError;                   //Erro da rede
            PointedNetwork.TrainNetworkError = OriginalNetwork.TrainNetworkError;         //Erro no ultimo treinamento
            PointedNetwork.RSquare = OriginalNetwork.RSquare;                             //R2 da rede
            PointedNetwork.RSquareAdju = OriginalNetwork.RSquareAdju;                     //R2 ajustado
            PointedNetwork.NumberIterations = OriginalNetwork.NumberIterations;
            return DeepClone(PointedNetwork);
        }

        //Calcular uma rede neural
        public static float[] ComputeNetwork(NeuralNet PointedNetwork, float[] InputValues, bool learning)
        {

            float[] FunctionOutput = new float[PointedNetwork.Output.Length];

            //Se a entrada nao for compativel...
            if (PointedNetwork.Structure[0] != InputValues.Length)
            {
                Log("Dimensoes de entrada e da rede nao coincidem.");
            }
            //Se a entrada for compativel...
            if (PointedNetwork.Structure[0] == InputValues.Length)
            {

                int NetworkLength = PointedNetwork.Structure.Length;

                InputValues = NormalizeByTwoValues1D(InputValues, PointedNetwork.InputMin, PointedNetwork.InputMax);

                //zerando os neuronios
                for (int i = 0; i < PointedNetwork.NeuronsValues.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.NeuronsValues.GetLength(1); j++)
                    {
                        PointedNetwork.NeuronsValues[i, j] = 0f;
                    }
                }
                for (int i = 0; i < PointedNetwork.Structure[0]; i++)
                {
                    PointedNetwork.NeuronsValues[i, 0] = InputValues[i];
                }

                for (int i = 1; i < PointedNetwork.NeuronsValues.GetLength(1); i++)
                {//camada
                    for (int j = 0; j < PointedNetwork.Structure[i]; j++)
                    {//Neuronio
                        float IncomingSignal = 0f;
                        for (int k = 0; k < PointedNetwork.Structure[i - 1]; k++)
                        {//camada anterior
                            if (!learning) { IncomingSignal += PointedNetwork.NeuronsValues[k, i - 1] * PointedNetwork.Weights[j * PointedNetwork.Structure[i - 1] + k, i - 1]; }
                            if (learning) { IncomingSignal += PointedNetwork.NeuronsValues[k, i - 1] * PointedNetwork.TempWeights[j * PointedNetwork.Structure[i - 1] + k, i - 1]; }
                        }
                        if (!learning)
                        {
                            PointedNetwork.NeuronsWeightSum[j, i] = IncomingSignal + PointedNetwork.NeuronsBias[j, i - 1];
                            PointedNetwork.NeuronsValues[j, i] = Function(IncomingSignal + PointedNetwork.NeuronsBias[j, i - 1], PointedNetwork.ActivationFunction, PointedNetwork.alpha);
                        }
                        if (learning)
                        {
                            PointedNetwork.NeuronsWeightSum[j, i] = IncomingSignal + PointedNetwork.TempNeuronsBias[j, i - 1];
                            PointedNetwork.NeuronsValues[j, i] = Function(IncomingSignal + PointedNetwork.TempNeuronsBias[j, i - 1], PointedNetwork.ActivationFunction, PointedNetwork.alpha);
                        }
                    }
                }

                for (int i = 0; i < PointedNetwork.Structure[PointedNetwork.Structure.Length - 1]; i++)
                {
                    if (!learning)
                    {
                        FunctionOutput[i] = PointedNetwork.NeuronsValues[i, NetworkLength - 1] * PointedNetwork.Weights[i, NetworkLength - 1];
                        PointedNetwork.Output[i] = PointedNetwork.NeuronsValues[i, NetworkLength - 1] * PointedNetwork.Weights[i, NetworkLength - 1];
                    }
                    if (learning)
                    {
                        FunctionOutput[i] = PointedNetwork.NeuronsValues[i, NetworkLength - 1] * PointedNetwork.TempWeights[i, NetworkLength - 1];
                        PointedNetwork.Output[i] = PointedNetwork.NeuronsValues[i, NetworkLength - 1] * PointedNetwork.TempWeights[i, NetworkLength - 1];
                    }
                }
                //Denormaliza
                for (int i = 0; i < PointedNetwork.Output.Length; i++)
                {
                    PointedNetwork.Output[i] = ((PointedNetwork.OutputMax - PointedNetwork.OutputMin) / 2f) * PointedNetwork.Output[i] + (PointedNetwork.OutputMin + PointedNetwork.OutputMax) / 2f;
                    FunctionOutput[i] = ((PointedNetwork.OutputMax - PointedNetwork.OutputMin) / 2f) * FunctionOutput[i] + (PointedNetwork.OutputMin + PointedNetwork.OutputMax) / 2f;
                }

            }
            return FunctionOutput;
        }

        //Converte uma série em dados de entrada e saida
        public static void SerieToInputOutput(float[,] Serie, int NumInputs, int NumOutputs, out float[,] InputValues, out float[,] OutputValues)
        {

            int SerieInput = Serie.GetLength(1);

            if (NumInputs % Serie.GetLength(1) != 0) { Log("O numero de entradas nao e multiplo do numero de linhas na serie"); }
            if (NumOutputs % Serie.GetLength(1) != 0) { Log("O numero de saidas nao e multiplo do numero de linhas na serie"); }

            int NumAntecesors = NumInputs / Serie.GetLength(1); //quantos casos anteriores a rede utiliza
            int NumSucessors = NumOutputs / Serie.GetLength(1); //quantos casos posteriores a rede gera

            int NumTrainInput = Serie.GetLength(0) - NumAntecesors - NumSucessors + 1;//Numero de casos para o treinamento

            InputValues = new float[NumTrainInput, NumInputs];
            OutputValues = new float[NumTrainInput, NumOutputs];

            if (NumInputs % Serie.GetLength(1) == 0 && NumOutputs % Serie.GetLength(1) == 0)
            {

                for (int i = 0; i < NumTrainInput; i++)
                {//casos do conjunto de treinamento
                    for (int j = 0; j < NumAntecesors; j++)
                    {//antecessores
                        for (int k = 0; k < SerieInput; k++)
                        {
                            InputValues[i, j * SerieInput + k] = Serie[i + j, k];
                        }
                    }
                    for (int j = 0; j < NumSucessors; j++)
                    {//sucessores
                        for (int k = 0; k < SerieInput; k++)
                        {
                            OutputValues[i, j * SerieInput + k] = Serie[i + j + NumAntecesors, k];
                        }
                    }
                }

            }
        }

        //pega os ultimos dados para predição em séries
        public static float[] LastInputToPredict(float[,] Serie, int NumInputs)
        {
            int SerieInput = Serie.GetLength(1);

            if (NumInputs % Serie.GetLength(1) != 0) { Log("O numero de entradas nao e multiplo do numero de linhas na serie"); }

            int NumAntecesors = NumInputs / Serie.GetLength(1); //quantos casos anteriores a rede utiliza

            float[] Result = new float[NumInputs];

            int start = Serie.GetLength(0) - NumAntecesors;

            if (NumInputs % Serie.GetLength(1) == 0)
            {
                for (int j = start; j < Serie.GetLength(0); j++)
                {
                    for (int k = 0; k < SerieInput; k++)
                    {
                        Result[(j - (start)) * SerieInput + k] = Serie[j, k];
                    }
                }
            }
            return Result;
        }

        //vetor de float para vetor de string
        public static string[] ArrayFloatToString(float[] Input)
        {
            string[] Output = new string[Input.Length];
            for (int i = 0; i < Input.Length; i++)
            {
                Output[i] = Input[i].ToString();
            }
            return Output;
        }

        public static float[,] filter(float[,] Input, string Method, int param)
        {

            float[,] Result = new float[Input.GetLength(0), Input.GetLength(1)];
            int end = Input.GetLength(0);

            if (Method == "Moving Average")
            {
                for (int k = 0; k < param; k++)
                {
                    for (int j = 0; j < Input.GetLength(1); j++)
                    {
                        Result[0, j] = (3 * Input[0, j] + Input[1, j]) / 4;
                        Result[1, j] = (Input[0, j] + 2 * Input[1, j] + Input[2, j]) / 4f;
                        Result[end - 2, j] = (Input[end - 3, j] + 2 * Input[end - 2, j] + Input[end - 1, j]) / 4f;
                        Result[end - 1, j] = (Input[end - 2, j] + 3 * Input[end - 1, j]) / 4;
                    }
                }
                for (int i = 2; i < Input.GetLength(0) - 2; i++)
                {
                    for (int j = 0; j < Input.GetLength(1); j++)
                    {
                        Result[i, j] = (Input[i - 1, j] + 2 * Input[i, j] + Input[i + 1, j]) / 4f;
                    }
                }

                for (int k = 0; k < param - 1; k++)
                {
                    for (int i = 2; i < Input.GetLength(0) - 2; i++)
                    {
                        for (int j = 0; j < Input.GetLength(1); j++)
                        {
                            Result[i, j] = (Result[i - 1, j] + 2 * Result[i, j] + Result[i + 1, j]) / 4f;
                        }
                    }
                }

            }

            if (Method == "Savitzky Golay")
            {
                for (int k = 0; k < param; k++)
                {
                    for (int j = 0; j < Input.GetLength(1); j++)
                    {
                        Result[0, j] = (3 * Input[0, j] + Input[1, j]) / 4;
                        Result[1, j] = (Input[0, j] + 2 * Input[1, j] + Input[2, j]) / 4f;
                        Result[end - 2, j] = (Input[end - 3, j] + 2 * Input[end - 2, j] + Input[end - 1, j]) / 4f;
                        Result[end - 1, j] = (Input[end - 2, j] + 3 * Input[end - 1, j]) / 4;
                    }
                }
                for (int i = 2; i < Input.GetLength(0) - 2; i++)
                {
                    for (int j = 0; j < Input.GetLength(1); j++)
                    {
                        Result[i, j] = (-3f * Input[i - 2, j] + 12 * Input[i - 1, j] + 17 * Input[i, j] + 12 * Input[i + 1, j] + -3f * Input[i + 2, j]) / 35f;
                    }
                }

                for (int k = 0; k < param - 1; k++)
                {
                    for (int i = 2; i < Input.GetLength(0) - 2; i++)
                    {
                        for (int j = 0; j < Input.GetLength(1); j++)
                        {
                            Result[i, j] = (-3f * Result[i - 2, j] + 12 * Result[i - 1, j] + 17 * Result[i, j] + 12 * Result[i + 1, j] + -3f * Result[i + 2, j]) / 35f;
                        }
                    }
                }

            }
            return Result;
        }

        //Treinamento da rede para series
        public static IEnumerator TrainNetworkSeriePSO(NeuralNet PointedNetwork, float[,] Serie, float LearningRate, float InteratingTime, float TolError)
        {
            //Normalizaçao
            PointedNetwork.InputMin = Min2D(Serie);
            PointedNetwork.InputMax = Max2D(Serie);
            PointedNetwork.OutputMin = Min2D(Serie);
            PointedNetwork.OutputMax = Max2D(Serie);
            PointedNetwork.Status = "Learning";
            PointedNetwork.Trainning = true;
            float InitialTrainningTime = PointedNetwork.TrainningTime;
            float TempoInicial = Time.time;
            int Iteration = 0;
            int NumInputs = PointedNetwork.Structure[0];
            int NumOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];

            float[,] InputValues = new float[1, 1];
            float[,] OutputValues = new float[1, 1];
            SerieToInputOutput(Serie, NumInputs, NumOutputs, out InputValues, out OutputValues);

            bool GoodDirection = false;
            bool BadDirection = false;

            float[,] WeightDirection = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
            float[,] BiasDirection = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];
            //Media acumulada
            float[,] MeanWeight = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
            float[,] MeanBias = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];

            for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                {
                    MeanWeight[i, j] = PointedNetwork.Weights[i, j];
                }
            }
            for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                {
                    MeanBias[i, j] = PointedNetwork.NeuronsBias[i, j];
                }
            }

            float Advance;
            float FatorEscala;
            float RelError;
            float MeanOrRandom;


            int NumberInputs = PointedNetwork.Structure[0];
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];
            float[,] AllNetworkOutputs = new float[OutputValues.GetLength(0), OutputValues.GetLength(1)];
            float PartialError = 0f;

            //Laço de treinamento
            while (PointedNetwork.Trainning)
            {
                //Fator de escala
                Advance = PointedNetwork.Advance;
                FatorEscala = 0;
                RelError = PointedNetwork.NetworkError / Range2D(InputValues);
                MeanOrRandom = Random.Range(0f, 1f);
                if (!GoodDirection & !BadDirection)
                {
                    float RandomicNumber = Random.Range(0f, 1f);
                    if (RelError <= 0.04f)
                    {
                        if (RandomicNumber >= 0f && RandomicNumber < 0.1f)
                        {
                            FatorEscala = LearningRate;
                        }
                        if (RandomicNumber >= 0.1f && RandomicNumber < 0.2f)
                        {
                            FatorEscala = LearningRate * (2 * Mathf.Sin((float)Iteration) + 2f) * 0.5f;
                        }
                        if (RandomicNumber >= 0.2f && RandomicNumber < 1f)
                        {
                            FatorEscala = LearningRate * Mathf.Pow(PointedNetwork.NetworkError / Range2D(InputValues), 1.2f) * (2 * Mathf.Sin((float)Iteration) + 2f) * 0.5f;
                        }
                    }
                    if (RelError > 0.04f && RelError <= 0.3f)
                    {
                        if (RandomicNumber >= 0f && RandomicNumber < 0.3f)
                        {
                            FatorEscala = LearningRate;
                        }
                        if (RandomicNumber >= 0.3f && RandomicNumber < 0.66f)
                        {
                            FatorEscala = LearningRate * (2 * Mathf.Sin((float)Iteration) + 2f) * 0.5f;
                        }
                        if (RandomicNumber >= 0.66f && RandomicNumber < 1f)
                        {
                            FatorEscala = LearningRate * Mathf.Pow(PointedNetwork.NetworkError / Range2D(InputValues), 1.2f);
                        }
                    }
                    if (RelError > 0.3f)
                    {
                        FatorEscala = LearningRate;
                    }
                }
                //Mudando os bias e os pesos a procura de algo melhor
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                    {
                        if (!GoodDirection & !BadDirection & MeanOrRandom > 0.1f)
                        {
                            PointedNetwork.TempWeights[i, j] = PointedNetwork.Weights[i, j] + Random.Range(-1f, 1f) * FatorEscala / (((float)j) * 0.5f + 1f);
                        }
                        if (!GoodDirection & !BadDirection & MeanOrRandom <= 0.1f)
                        {
                            PointedNetwork.TempWeights[i, j] = MeanWeight[i, j];
                        }
                        if (GoodDirection)
                        {
                            PointedNetwork.TempWeights[i, j] = PointedNetwork.Weights[i, j] + Advance * WeightDirection[i, j];
                        }
                        if (BadDirection)
                        {
                            PointedNetwork.TempWeights[i, j] = PointedNetwork.Weights[i, j] + Advance * WeightDirection[i, j];
                        }
                    }
                }
                for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                    {
                        if (!GoodDirection & !BadDirection & MeanOrRandom > 0.1f)
                        {
                            PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] + Random.Range(-1f, 1f) * FatorEscala / (((float)j) * 0.5f + 1f);
                        }
                        if (!GoodDirection & !BadDirection & MeanOrRandom <= 0.1f)
                        {
                            PointedNetwork.TempNeuronsBias[i, j] = MeanBias[i, j];
                        }
                        if (GoodDirection)
                        {
                            PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] + Advance * BiasDirection[i, j];
                        }
                        if (BadDirection)
                        {
                            PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] + Advance * BiasDirection[i, j];
                        }
                    }
                }

                GoodDirection = false;
                BadDirection = false;

                PartialError = 0f;

                if (InputValues.GetLength(0) != OutputValues.GetLength(0))
                    Log("O numero de dados de entrada e saida nao coincidem, dim 0");
                if (NumberInputs != InputValues.GetLength(1))
                    Log("O numero de entradas da rede e de entradas de dados nao coincidem, dim 1");
                if (NumberOutputs != OutputValues.GetLength(1))
                    Log("O numero de saidas da rede e de saida de dados nao coincidem, dim 1");

                for (int i = 0; i < InputValues.GetLength(0); i++)
                {
                    for (int j = 0; j < InputValues.GetLength(1); j++)
                    {
                        TempInput[j] = InputValues[i, j];
                    }
                    TempOutput = ComputeNetwork(PointedNetwork, TempInput, true);
                    for (int j = 0; j < OutputValues.GetLength(1); j++)
                    {
                        AllNetworkOutputs[i, j] = TempOutput[j];
                        PartialError += Mathf.Abs(TempOutput[j] - OutputValues[i, j]);
                    }
                }
                PartialError = PartialError / (OutputValues.GetLength(0) * OutputValues.GetLength(1));

                //Se o erro for maior va pelo caminho oposto
                if (PartialError >= PointedNetwork.NetworkError)
                {
                    PointedNetwork.Advance = 0.92f * PointedNetwork.Advance;
                    if (PointedNetwork.Advance < 0.001)
                    {
                        PointedNetwork.Advance = (PointedNetwork.Advance + 0.1f) / 2f;
                    }
                    if (Random.Range(0f, 1f) > 0.5f)
                    {
                        BadDirection = true;
                        for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                        {
                            for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                            {
                                WeightDirection[i, j] = -PointedNetwork.TempWeights[i, j] + PointedNetwork.Weights[i, j];
                            }
                        }
                        for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                        {
                            for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                            {
                                BiasDirection[i, j] = -PointedNetwork.TempNeuronsBias[i, j] + PointedNetwork.NeuronsBias[i, j];
                            }
                        }

                    }
                }

                //Se o erro for menor atualize os pesos e os bias
                if (PartialError < PointedNetwork.NetworkError)
                {
                    GoodDirection = true;
                    PointedNetwork.Advance = 1.4f * PointedNetwork.Advance;
                    if (PointedNetwork.Advance > 20)
                    {
                        PointedNetwork.Advance = (PointedNetwork.Advance + 1f) / 2f;
                    }
                    for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                    {
                        for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                        {
                            WeightDirection[i, j] = PointedNetwork.TempWeights[i, j] - PointedNetwork.Weights[i, j];
                            PointedNetwork.Weights[i, j] = PointedNetwork.TempWeights[i, j];
                            MeanWeight[i, j] = (MeanWeight[i, j] + PointedNetwork.Weights[i, j]) / 2f;
                        }
                    }
                    for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                    {
                        for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                        {
                            BiasDirection[i, j] = PointedNetwork.TempNeuronsBias[i, j] - PointedNetwork.NeuronsBias[i, j];
                            PointedNetwork.NeuronsBias[i, j] = PointedNetwork.TempNeuronsBias[i, j];
                            MeanBias[i, j] = (MeanBias[i, j] + PointedNetwork.NeuronsBias[i, j]) / 2f;
                        }
                    }
                    R2(PointedNetwork.TotalFreeParameters, OutputValues, AllNetworkOutputs, out PointedNetwork.RSquare, out PointedNetwork.RSquareAdju);

                    PointedNetwork.NetworkError = PartialError;
                }

                PointedNetwork.NumberIterations += 1;
                PointedNetwork.TrainningTime = InitialTrainningTime + Time.time - TempoInicial;
                if (Time.time - TempoInicial >= InteratingTime || PointedNetwork.NetworkError <= TolError)
                {
                    if (PointedNetwork.NetworkError < (PointedNetwork.OutputMax - PointedNetwork.OutputMin) / (InputValues.GetLength(0) + 1))
                    {
                        PointedNetwork.Status = "Trained";
                    }
                    else
                    {
                        PointedNetwork.Status = "NotTrainedEnough";
                    }
                    PointedNetwork.Trainning = false;
                    break;
                }
                yield return new WaitForSeconds(0.00001f);
            }
        }


        //Treinamento da rede por particle swarm optimization
        public static IEnumerator TrainNetworkPSO(NeuralNet PointedNetwork, float[,] InputValues, float[,] OutputValues, float LearningRate, float InteratingTime, float TolError)
        {
            //Normalizaçao
            PointedNetwork.InputMin = Min2D(InputValues);
            PointedNetwork.InputMax = Max2D(InputValues);
            PointedNetwork.OutputMin = Min2D(OutputValues);
            PointedNetwork.OutputMax = Max2D(OutputValues);
            PointedNetwork.Status = "Learning";
            PointedNetwork.Trainning = true;
            float InitialTrainningTime = PointedNetwork.TrainningTime;
            float TempoInicial = Time.time;
            int Iteration = 0;

            bool GoodDirection = false;
            bool BadDirection = false;

            float[,] WeightDirection = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
            float[,] BiasDirection = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];
            //Media acumulada
            float[,] MeanWeight = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
            float[,] MeanBias = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];

            for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                {
                    MeanWeight[i, j] = PointedNetwork.Weights[i, j];
                }
            }
            for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
            {
                for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                {
                    MeanBias[i, j] = PointedNetwork.NeuronsBias[i, j];
                }
            }

            float Advance;
            float FatorEscala;
            float RelError;
            float MeanOrRandom;

            int NumberInputs = PointedNetwork.Structure[0];
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];
            float[,] AllNetworkOutputs = new float[OutputValues.GetLength(0), OutputValues.GetLength(1)];
            float PartialError = 0f;

            float RangeOutput = Range2D(OutputValues);

            //Laço de treinamento
            while (PointedNetwork.Trainning)
            {
                //Fator de escala
                Advance = PointedNetwork.Advance;
                FatorEscala = 0;
                RelError = PointedNetwork.NetworkError / RangeOutput;
                MeanOrRandom = Random.Range(0f, 1f);
                if (!GoodDirection & !BadDirection)
                {
                    float RandomicNumber = Random.Range(0f, 1f);
                    if (RelError <= 0.04f)
                    {
                        if (RandomicNumber >= 0f && RandomicNumber < 0.1f) { FatorEscala = 0.1f * LearningRate; }
                        if (RandomicNumber >= 0.1f && RandomicNumber < 0.2f) { FatorEscala = 0.1f * LearningRate * (2 * Mathf.Sin((float)Iteration) + 2f) * 0.5f; }
                        if (RandomicNumber >= 0.2f && RandomicNumber < 1f) { FatorEscala = 0.1f * LearningRate * Mathf.Pow(PointedNetwork.NetworkError / Range2D(InputValues), 1.2f) * (2 * Mathf.Sin((float)Iteration) + 2f) * 0.5f; }
                    }
                    if (RelError > 0.04f && RelError <= 0.3f)
                    {
                        if (RandomicNumber >= 0f && RandomicNumber < 0.3f) { FatorEscala = LearningRate; }
                        if (RandomicNumber >= 0.3f && RandomicNumber < 0.66f) { FatorEscala = LearningRate * (2 * Mathf.Sin((float)Iteration) + 2f) * 0.5f; }
                        if (RandomicNumber >= 0.66f && RandomicNumber < 1f) { FatorEscala = LearningRate * Mathf.Pow(PointedNetwork.NetworkError / Range2D(InputValues), 1.2f); }
                    }
                    if (RelError > 0.3f)
                    {
                        FatorEscala = LearningRate;
                    }
                }
                //Mudando os bias e os pesos a procura de algo melhor
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                    {
                        if (!GoodDirection & !BadDirection & MeanOrRandom > 0.1f) { PointedNetwork.TempWeights[i, j] = PointedNetwork.Weights[i, j] + Random.Range(-1f, 1f) * FatorEscala / (((float)j) * 0.5f + 1f); }
                        if (!GoodDirection & !BadDirection & MeanOrRandom <= 0.1f) { PointedNetwork.TempWeights[i, j] = MeanWeight[i, j]; }
                        if (GoodDirection) { PointedNetwork.TempWeights[i, j] = PointedNetwork.Weights[i, j] + Advance * WeightDirection[i, j]; }
                        if (BadDirection) { PointedNetwork.TempWeights[i, j] = PointedNetwork.Weights[i, j] + Advance * WeightDirection[i, j]; }
                    }
                }
                for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                    {
                        if (!GoodDirection & !BadDirection & MeanOrRandom > 0.1f) { PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] + Random.Range(-1f, 1f) * FatorEscala / (((float)j) * 0.5f + 1f); }
                        if (!GoodDirection & !BadDirection & MeanOrRandom <= 0.1f) { PointedNetwork.TempNeuronsBias[i, j] = MeanBias[i, j]; }
                        if (GoodDirection) { PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] + Advance * BiasDirection[i, j]; }
                        if (BadDirection) { PointedNetwork.TempNeuronsBias[i, j] = PointedNetwork.NeuronsBias[i, j] + Advance * BiasDirection[i, j]; }
                    }
                }

                GoodDirection = false;
                BadDirection = false;

                PartialError = 0f;

                if (InputValues.GetLength(0) != OutputValues.GetLength(0)) Log("O numero de dados de entrada e saida nao coincidem, dim 0");
                if (NumberInputs != InputValues.GetLength(1)) Log("O numero de entradas da rede e de entradas de dados nao coincidem, dim 1");
                if (NumberOutputs != OutputValues.GetLength(1)) Log("O numero de saidas da rede e de saida de dados nao coincidem, dim 1");

                for (int i = 0; i < InputValues.GetLength(0); i++)
                {
                    for (int j = 0; j < InputValues.GetLength(1); j++)
                    {
                        TempInput[j] = InputValues[i, j];
                    }
                    TempOutput = ComputeNetwork(PointedNetwork, TempInput, true);
                    for (int j = 0; j < OutputValues.GetLength(1); j++)
                    {
                        AllNetworkOutputs[i, j] = TempOutput[j];
                        PartialError += Mathf.Abs(TempOutput[j] - OutputValues[i, j]);
                    }
                }
                PartialError = PartialError / (OutputValues.GetLength(0) * OutputValues.GetLength(1));


                //Se o erro for maior va pelo caminho oposto
                if (PartialError >= PointedNetwork.NetworkError)
                {
                    PointedNetwork.Advance = 0.92f * PointedNetwork.Advance;
                    //UnityEngine.Debug.Log("ATUALIZOU");
                    if (PointedNetwork.Advance < 0.001)
                    {
                        PointedNetwork.Advance = (PointedNetwork.Advance + 0.1f) / 2f;
                    }
                    if (Random.Range(0f, 1f) > 0.5f)
                    {
                        BadDirection = true;

                        for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                        {
                            for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                            {
                                WeightDirection[i, j] = -PointedNetwork.TempWeights[i, j] + PointedNetwork.Weights[i, j];
                            }
                        }
                        for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                        {
                            for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                            {
                                BiasDirection[i, j] = -PointedNetwork.TempNeuronsBias[i, j] + PointedNetwork.NeuronsBias[i, j];
                            }
                        }

                    }
                }

                //Se o erro for menor atualize os pesos e os bias
                if (PartialError < PointedNetwork.NetworkError)
                {
                    GoodDirection = true;
                    PointedNetwork.Advance = 1.4f * PointedNetwork.Advance;
                    if (PointedNetwork.Advance > 20)
                    {
                        PointedNetwork.Advance = (PointedNetwork.Advance + 1f) / 2f;
                    }
                    for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                    {
                        for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                        {
                            WeightDirection[i, j] = PointedNetwork.TempWeights[i, j] - PointedNetwork.Weights[i, j];
                            PointedNetwork.Weights[i, j] = PointedNetwork.TempWeights[i, j];
                            MeanWeight[i, j] = (MeanWeight[i, j] + PointedNetwork.Weights[i, j]) / 2f;
                        }
                    }
                    for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                    {
                        for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                        {
                            BiasDirection[i, j] = PointedNetwork.TempNeuronsBias[i, j] - PointedNetwork.NeuronsBias[i, j];
                            PointedNetwork.NeuronsBias[i, j] = PointedNetwork.TempNeuronsBias[i, j];
                            MeanBias[i, j] = (MeanBias[i, j] + PointedNetwork.NeuronsBias[i, j]) / 2f;
                        }
                    }
                    R2(PointedNetwork.TotalFreeParameters, OutputValues, AllNetworkOutputs, out PointedNetwork.RSquare, out PointedNetwork.RSquareAdju);

                    PointedNetwork.NetworkError = PartialError;
                }

                PointedNetwork.NumberIterations += 1;
                PointedNetwork.TrainningTime = InitialTrainningTime + Time.time - TempoInicial;
                if (Time.time - TempoInicial >= InteratingTime || PointedNetwork.NetworkError <= TolError)
                {
                    if (PointedNetwork.NetworkError < (PointedNetwork.OutputMax - PointedNetwork.OutputMin) / (InputValues.GetLength(0) + 1)) { PointedNetwork.Status = "Trained"; }
                    else { PointedNetwork.Status = "NotTrainedEnough"; }
                    PointedNetwork.Trainning = false;
                    break;
                }
                yield return new WaitForSeconds(0.00001f);
            }
        }

        //Coeficient de determinaçao
        public static void R2(int Param, float[,] Real, float[,] Teoric, out float CoefDet, out float CoefDetAdju)
        {
            float Media = Media2D(Real);
            float SSres = 0f;
            float SStot = 0f;
            float TotSamp = (float)(Real.GetLength(0) * Real.GetLength(1));
            for (int i = 0; i < Teoric.GetLength(0); i++)
            {
                for (int j = 0; j < Teoric.GetLength(1); j++)
                {
                    SSres += Mathf.Pow(Real[i, j] - Teoric[i, j], 2);
                }
            }
            for (int i = 0; i < Teoric.GetLength(0); i++)
            {
                for (int j = 0; j < Teoric.GetLength(1); j++)
                {
                    SStot += Mathf.Pow(Real[i, j] - Media, 2);
                }
            }
            CoefDet = 1f - SSres / SStot;
            CoefDetAdju = 1f - (1f - CoefDet) * ((TotSamp - 1f) / (TotSamp - (float)Param - 1));
        }

        public static float InitialError(NeuralNet PointedNetwork, float[,] InputValues, float[,] OutputValues)
        {
            float PartialError = 0f;
            int NumberInputs = PointedNetwork.Structure[0];
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];

            for (int i = 0; i < InputValues.GetLength(0); i++)
            {
                for (int j = 0; j < InputValues.GetLength(1); j++)
                {
                    TempInput[j] = InputValues[i, j];
                }
                TempOutput = ComputeNetwork(PointedNetwork, TempInput, false);
                for (int j = 0; j < OutputValues.GetLength(1); j++)
                {
                    PartialError += Mathf.Abs(TempOutput[j] - OutputValues[i, j]);
                }
            }
            PartialError = PartialError / (OutputValues.GetLength(0) * OutputValues.GetLength(1));
            return PartialError;
        }

        public static float Media2D(float[,] InputMatrix)
        {
            float sum = 0;
            for (int i = 0; i < InputMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < InputMatrix.GetLength(1); j++)
                {
                    sum += InputMatrix[i, j];
                }
            }
            return sum / (InputMatrix.GetLength(0) * InputMatrix.GetLength(1));
        }

        public static float DiferencaMedia(float[] InputMatrix, float[] InputMatrix2)
        {
            float sum = 0;
            for (int i = 0; i < InputMatrix.GetLength(0); i++)
            {
                sum += Mathf.Abs(InputMatrix[i] - InputMatrix2[i]);
            }
            return sum / (InputMatrix.GetLength(0));
        }
        //soma dos valores de um vetor
        public static float SomaParc(int[] InputMatrix, int a, int b)
        {
            float sum = 0;
            for (int i = a; i <= b; i++)
            {
                sum += InputMatrix[i];
            }
            return sum / InputMatrix.GetLength(0);
        }

        //Treinamento da rede por backpropagation
        public static IEnumerator TrainNetworkBP(NeuralNet PointedNetwork, float[,] InputValues, float[,] OutputValues, float LearningRate, float InteratingTime, float TolError)
        {
            //Normalizaçao
            PointedNetwork.InputMin = Min2D(InputValues);
            PointedNetwork.InputMax = Max2D(InputValues);
            PointedNetwork.OutputMin = Min2D(OutputValues);
            PointedNetwork.OutputMax = Max2D(OutputValues);
            //float[,] NormInputValues = NormalizeByTwoValues2D(InputValues, PointedNetwork.InputMin, PointedNetwork.InputMax); //cuidado
            float[,] NormOutputValues = NormalizeByTwoValues2D(OutputValues, PointedNetwork.OutputMin, PointedNetwork.OutputMax); //cuidado
            //float OutputRange = Range2D(OutputValues);
            PointedNetwork.Status = "Learning";
            PointedNetwork.Trainning = true;
            float InitialTrainningTime = PointedNetwork.TrainningTime;
            float TempoInicial = Time.time;
            int[] Ordering = new int[InputValues.GetLength(0)]; //vetor com a ordem de treinamento dos dados
            for (int i = 0; i < Ordering.Length; i++)
            {
                Ordering[i] = i;
            }

            float momentum = 0.5f;

            //vetor com os delta dos pesos e dos bias
            //vetor com os delta dos pesos e dos bias
            float[,,] WeightDirection = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1), InputValues.GetLength(0)];
            float[,,] BiasDirection = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1), InputValues.GetLength(0)];
            float[,] WeightDirectionApply = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
            float[,] BiasDirectionApply = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];

            float[,] DeltaWeight = new float[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
            float[,] DeltaBias = new float[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];

            //Definir como 1 os pesos finais
            for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
            {
                PointedNetwork.TempWeights[i, PointedNetwork.Weights.GetLength(1) - 1] = 1f;
                PointedNetwork.Weights[i, PointedNetwork.Weights.GetLength(1) - 1] = 1f;
            }

            //Erro do metodo
            float[,] Errors = new float[OutputValues.GetLength(0), OutputValues.GetLength(1)];

            //camada de saida
            int NumberOfOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            int NumberOfPreOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 2];
            int OutputLayer = PointedNetwork.Weights.GetLength(1) - 2;
            float LocalNeuronValue = 0;
            int ActualNeuron = 0;
            float NeuronIncomingValue = 0;
            int WeightsToOutput = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1] * PointedNetwork.Structure[PointedNetwork.Structure.Length - 2];
            //float SumDeltaWeight = 0;

            int NumberInputs = PointedNetwork.Structure[0];
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];

            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];
            float[] NormTempOutput = new float[NumberOutputs];

            if (InputValues.GetLength(0) != OutputValues.GetLength(0)) Log("O numero de dados de entrada e saida nao coincidem, dim 0");
            if (NumberInputs != InputValues.GetLength(1)) Log("O numero de entradas da rede e de entradas de dados nao coincidem, dim 1");
            if (NumberOutputs != OutputValues.GetLength(1)) Log("O numero de saidas da rede e de saida de dados nao coincidem, dim 1");

            //DebugNetwork(PointedNetwork);

            while (PointedNetwork.Trainning)
            {

                RandomOrdering(Ordering); //Reordenar o vetor de ordenaçao

                float PartialError = 0f;


                for (int i = 0; i < InputValues.GetLength(0); i++)
                {
                    //FOWARD PASS
                    // cada caso
                    for (int j = 0; j < InputValues.GetLength(1); j++)
                    { // cada entrada
                        TempInput[j] = InputValues[i, j];
                    }
                    TempOutput = ComputeNetwork(PointedNetwork, TempInput, true);
                    NormTempOutput = NormalizeByTwoValues1D(TempOutput, PointedNetwork.OutputMin, PointedNetwork.OutputMax);
                    for (int j = 0; j < OutputValues.GetLength(1); j++)
                    { // cada saida de um caso
                        Errors[i, j] = NormTempOutput[j] - NormOutputValues[i, j];
                        PartialError += Mathf.Abs(TempOutput[j] - OutputValues[i, j]);
                    }

                    //BACKPROPAGATION PASS
                    //camada de saida

                    for (int j = 0; j < WeightsToOutput; j++)
                    {//cada peso de entrada nos neuronios de saida
                        ActualNeuron = j / NumberOfPreOutputs;
                        //UnityEngine.Debug.Log("INICIO PESO - Caso: " + i.ToString() + " Weigth: " + j.ToString());
                        LocalNeuronValue = PointedNetwork.NeuronsValues[ActualNeuron, OutputLayer + 1];
                        NeuronIncomingValue = PointedNetwork.NeuronsValues[j / NumberOfOutputs, OutputLayer];

                        //DeltaWeight[j, OutputLayer + 1] = (1f - momentum) * Errors[i, ActualNeuron] + momentum * DeltaWeight[j, OutputLayer+1];
                        //WeightDirection[j, OutputLayer + 1, i] = -LearningRate * DeltaWeight[j, OutputLayer + 1] * LocalNeuronValue;

                        DeltaWeight[j, OutputLayer] = (1f - momentum) * Errors[i, ActualNeuron] * (LocalNeuronValue * (1f - LocalNeuronValue)) / PointedNetwork.Weights[ActualNeuron, OutputLayer + 1] + momentum * DeltaWeight[j, OutputLayer];
                        WeightDirection[j, OutputLayer, i] = -LearningRate * DeltaWeight[j, OutputLayer] * NeuronIncomingValue;

                        /*UnityEngine.Debug.Log("Delta weight peso: " + DeltaWeight[j, OutputLayer].ToString());
                        UnityEngine.Debug.Log("Local Neuron Value peso: " + LocalNeuronValue.ToString());
                        UnityEngine.Debug.Log("Derivada peso: " + (LocalNeuronValue * (1f - LocalNeuronValue)).ToString());
                        UnityEngine.Debug.Log("Erro peso: " + Errors[i, ActualNeuron].ToString());
                        UnityEngine.Debug.Log("Direção peso: " + WeightDirection[j, OutputLayer, i].ToString());
                        UnityEngine.Debug.Log("FIM PESO - Caso: " + i.ToString() + " Weigth: " + j.ToString());*/

                    }

                    for (int j = 0; j < NumberOfOutputs; j++)
                    {//cada bia de entrada nos neuronios de saida
                        //UnityEngine.Debug.Log("INICIO BIA - Caso: " + i.ToString() + " Bias: " + j.ToString());
                        LocalNeuronValue = PointedNetwork.NeuronsValues[j, OutputLayer + 1];
                        DeltaBias[j, OutputLayer] = (1f - momentum) * Errors[i, j] * (LocalNeuronValue * (1f - LocalNeuronValue)) + momentum * DeltaBias[j, OutputLayer];
                        BiasDirection[j, OutputLayer, i] = -LearningRate * DeltaBias[j, OutputLayer];
                        /*UnityEngine.Debug.Log("Local Neuron Value bia: " + LocalNeuronValue.ToString());
                        UnityEngine.Debug.Log("Derivada bia: " + (LocalNeuronValue * (1f - LocalNeuronValue)).ToString());
                        UnityEngine.Debug.Log("Erro bia: " + Errors[i, j].ToString());
                        UnityEngine.Debug.Log("Direção bia: " + BiasDirection[j, OutputLayer, i].ToString());
                        UnityEngine.Debug.Log("FIM BIA - Caso: " + i.ToString() + " bia: " + j.ToString());*/

                    }


                    //camadas ocultas
                    /*
                    for (int j=OutputLayer-1; j>=0; j--) {//cada camada oculta, da saida para a entrada
                            for (int k=0; k<PointedNetwork.NumberOfWeights[j]; k++) {//cada peso em cada camada
                                SumDeltaWeight=0f;
                                for (int l=0; l<PointedNetwork.Structure[j+1]; l++) {//soma dos deltas apartir da camada seguinte
                                    SumDeltaWeight+=PointedNetwork.Weights[l+k/PointedNetwork.Structure[j+1],j+1]*DeltaWeight[l+k/PointedNetwork.Structure[j+1],j+1];
                                }
                                ActualNeuron=k/(PointedNetwork.Structure [j]);
                                LocalNeuronValue=PointedNetwork.NeuronsValues[ActualNeuron,j];
                                if (j > 0) NeuronIncomingValue=PointedNetwork.NeuronsValues[ActualNeuron, j];
                                if (j == 0) NeuronIncomingValue = TempInput[ActualNeuron];
                                WeightDirection[k,OutputLayer]=-LearningRate*SumDeltaWeight*(LocalNeuronValue*(1f-LocalNeuronValue))*NeuronIncomingValue;
                            }
                    }
                    */
                    //					
                    //				for (int j=0; j<NumberOfOutputs; j++) {//cada bia das camadas ocultas
                    //						LocalNeuronValue=PointedNetwork.NeuronsValues[j,OutputLayer];
                    //						BiasDirection[j,OutputLayer]=-LearningRate*Errors[i,j]*(LocalNeuronValue*(1f-LocalNeuronValue));
                    //				}

                    //UnityEngine.Debug.Log(Show2DArray(WeightDirection));
                }

                for (int i = 0; i < InputValues.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.Weights.GetLength(0); j++)
                    {
                        for (int k = 0; k < PointedNetwork.Weights.GetLength(1); k++)
                        {
                            WeightDirectionApply[j, k] += WeightDirection[j, k, i];
                        }
                    }
                    for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(0); j++)
                    {
                        for (int k = 0; k < PointedNetwork.NeuronsBias.GetLength(1); k++)
                        {
                            BiasDirectionApply[j, k] += BiasDirection[j, k, i];
                        }
                    }
                }

                //Aplicaçao da variaçao dos pesos e dos bias

                for (int j = 0; j < PointedNetwork.Weights.GetLength(0); j++)
                {
                    for (int k = 0; k < PointedNetwork.Weights.GetLength(1); k++)
                    {
                        WeightDirectionApply[j, k] = WeightDirectionApply[j, k] / (float)InputValues.GetLength(0);
                        PointedNetwork.TempWeights[j, k] = PointedNetwork.TempWeights[j, k] + WeightDirectionApply[j, k];
                        PointedNetwork.Weights[j, k] = PointedNetwork.TempWeights[j, k];
                    }
                }

                for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(0); j++)
                {
                    for (int k = 0; k < PointedNetwork.NeuronsBias.GetLength(1); k++)
                    {
                        BiasDirectionApply[j, k] = BiasDirectionApply[j, k] / (float)InputValues.GetLength(0);
                        PointedNetwork.TempNeuronsBias[j, k] = PointedNetwork.TempNeuronsBias[j, k] + BiasDirectionApply[j, k];
                        PointedNetwork.NeuronsBias[j, k] = PointedNetwork.TempNeuronsBias[j, k];
                    }
                }

                PartialError = PartialError / InputValues.GetLength(0);
                PointedNetwork.NetworkError = PartialError;
                PointedNetwork.NumberIterations += 1;
                PointedNetwork.TrainningTime = InitialTrainningTime + Time.time - TempoInicial;
                if (Time.time - TempoInicial >= InteratingTime || PointedNetwork.NetworkError <= TolError)
                {
                    if (PointedNetwork.NetworkError < (PointedNetwork.OutputMax - PointedNetwork.OutputMin) / (InputValues.GetLength(0) + 1)) { PointedNetwork.Status = "Trained"; }
                    else { PointedNetwork.Status = "NotTrainedEnough"; }
                    PointedNetwork.Trainning = false;
                    break;
                }
                yield return new WaitForSeconds(0.00001f);
                //PointedNetwork.Trainning = false; ///retirar, apenas para uma so interação!!!!

            }

        }

        //Treinamento de diversas redes por particle swarm optimization
        public static IEnumerator TrainNetworkGA(NeuralNet PointedNetwork, GAStatistics GAStat, float[,] InputValues, float[,] OutputValues, float LearningRate, float InteratingTime, float TolError, int Phenotypes)
        {

            PointedNetwork.Status = "Learning";
            //PointedNetwork.NetworkError = 3f * PointedNetwork.NetworkError;
            //PointedNetwork.TrainNetworkError = 3f * PointedNetwork.TrainNetworkError;
            PointedNetwork.RSquare = 0f;
            PointedNetwork.RSquareAdju = 0f;
            PointedNetwork.TrainningTime = 0f;
            PointedNetwork.NumberIterations = 0;
            PointedNetwork.Trainning = true;
            float TempoInicial = Time.time;
            float UpdateTime = Time.time;
            float ReproductionTime = Time.time;
            float ReproductionInterval = InteratingTime / ((float)Phenotypes);
            //Criando a populaçao
            NeuralNet[] Population = new NeuralNet[Phenotypes];
            //Copias, fenotipos
            for (int i = 0; i < Phenotypes; i++)
            {
                Population[i] = DeepClone(PointedNetwork);
            }
            //Copias com pesos e bias diferentes, a 0 é identica a original
            for (int i = 1; i < Phenotypes; i++)
            {
                ReinitializeParameters(Population[i], 2f, "Random");
            }
            //Pondo todos os individuos da populaçao para treinamento
            for (int i = 0; i < Phenotypes; i++)
            {
                Coroutiner.StartCoroutine(TrainNetworkPSO(Population[i], InputValues, OutputValues, LearningRate, InteratingTime, TolError));
            }

            float MinError = float.PositiveInfinity;
            int IndexOfBestPhenotype = 0;
            float MaxError = float.NegativeInfinity;
            int IndexOfWorstPhenotype = 0;

            while (PointedNetwork.Trainning)
            {
                GAStat.Population = DeepClone(Population);
                //Atualizaçao para a melhor rede
                if (Time.time >= UpdateTime)
                {
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = false;
                    }
                    MinError = float.PositiveInfinity;
                    IndexOfBestPhenotype = 0;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        if (Population[i].NetworkError <= MinError && Population[i].NetworkError <= PointedNetwork.NetworkError)
                        {
                            MinError = Population[i].NetworkError;
                            IndexOfBestPhenotype = i;
                        }
                    }
                    if (MinError < PointedNetwork.NetworkError)
                    {
                        CopyNetwork(Population[IndexOfBestPhenotype], PointedNetwork);
                        PointedNetwork.Trainning = true;
                    }

                    PointedNetwork.Advance = Population[IndexOfBestPhenotype].Advance;
                    PointedNetwork.NumberIterations = Population[IndexOfBestPhenotype].NumberIterations;
                    PointedNetwork.TrainningTime = Population[IndexOfBestPhenotype].TrainningTime;
                    UpdateTime += 0.1f;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = true;
                    }

                    if (Time.time - TempoInicial >= InteratingTime || PointedNetwork.NetworkError <= TolError)
                    {
                        //Log("Valores: "+(Time.time - TempoInicial).ToString() + "   "+ InteratingTime.ToString()+"   "+ PointedNetwork.NetworkError.ToString()+"   "+ TolError.ToString());
                        PointedNetwork.Status = "Trained";
                        PointedNetwork.Trainning = false;
                        DestroyCoroutiners();
                        break;
                    }

                }
                //reproduçao
                if (Time.time >= ReproductionTime)
                {
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = false;
                    }
                    MaxError = float.NegativeInfinity;
                    IndexOfWorstPhenotype = 0;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        if (Population[i].NetworkError > MaxError)
                        {
                            MaxError = Population[i].NetworkError;
                            IndexOfWorstPhenotype = i;
                        }
                    }
                    CopyNetwork(PointedNetwork, Population[IndexOfWorstPhenotype]);
                    ReproductionTime += ReproductionInterval;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = true;
                    }
                }
                yield return new WaitForSeconds(0.00001f);
            }
        }

        //Treinamento da rede por particle swarm optimization
        public static IEnumerator TrainNetworkSerieGA(NeuralNet PointedNetwork, GAStatistics GAStat, float[,] Serie, float LearningRate, float InteratingTime, float TolError, int Phenotypes)
        {

            PointedNetwork.Status = "Learning";
            //PointedNetwork.NetworkError = 3f* PointedNetwork.NetworkError;
            //PointedNetwork.TrainNetworkError = 3f* PointedNetwork.TrainNetworkError;
            PointedNetwork.RSquare = 0f;
            PointedNetwork.RSquareAdju = 0f;
            PointedNetwork.TrainningTime = 0f;
            PointedNetwork.NumberIterations = 0;
            PointedNetwork.Trainning = true;
            float TempoInicial = Time.time;
            float UpdateTime = Time.time;
            float ReproductionTime = Time.time;
            float ReproductionInterval = InteratingTime / ((float)Phenotypes);
            //Criando a populaçao
            NeuralNet[] Population = new NeuralNet[Phenotypes];
            //Copias, fenotipos
            for (int i = 0; i < Phenotypes; i++)
            {
                Population[i] = DeepClone(PointedNetwork);
            }
            //Copias com pesos e bias diferentes, a 0 é identica a original
            for (int i = 1; i < Phenotypes; i++)
            {
                ReinitializeParameters(Population[i], 2f, "Random");
            }
            //Pondo todos os individuos da populaçao para treinamento
            for (int i = 0; i < Phenotypes; i++)
            {
                Coroutiner.StartCoroutine(TrainNetworkSeriePSO(Population[i], Serie, LearningRate, InteratingTime, TolError));
            }

            float MinError = float.PositiveInfinity;
            int IndexOfBestPhenotype = 0;
            float MaxError = float.NegativeInfinity;
            int IndexOfWorstPhenotype = 0;

            while (PointedNetwork.Trainning)
            {
                GAStat.Population = DeepClone(Population);
                //Atualizaçao para a melhor rede
                if (Time.time >= UpdateTime)
                {
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = false;
                    }
                    MinError = float.PositiveInfinity;
                    IndexOfBestPhenotype = 0;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        if (Population[i].NetworkError <= MinError && Population[i].NetworkError <= PointedNetwork.NetworkError)
                        {
                            MinError = Population[i].NetworkError;
                            IndexOfBestPhenotype = i;
                        }
                    }
                    if (MinError < PointedNetwork.NetworkError)
                    {
                        CopyNetwork(Population[IndexOfBestPhenotype], PointedNetwork);
                        PointedNetwork.Trainning = true;
                    }
                    PointedNetwork.Advance = Population[IndexOfBestPhenotype].Advance;
                    PointedNetwork.NumberIterations = Population[IndexOfBestPhenotype].NumberIterations;
                    PointedNetwork.TrainningTime = Population[IndexOfBestPhenotype].TrainningTime;
                    UpdateTime += 0.1f;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = true;
                    }

                    if (Time.time - TempoInicial >= InteratingTime || PointedNetwork.NetworkError <= TolError)
                    {
                        PointedNetwork.Status = "Trained";
                        PointedNetwork.Trainning = false;

                        DestroyCoroutiners();
                        break;
                    }

                }
                //reproduçao
                if (Time.time >= ReproductionTime)
                {
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = false;
                    }
                    MaxError = float.NegativeInfinity;
                    IndexOfWorstPhenotype = 0;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        if (Population[i].NetworkError > MaxError)
                        {
                            MaxError = Population[i].NetworkError;
                            IndexOfWorstPhenotype = i;
                        }
                    }
                    CopyNetwork(PointedNetwork, Population[IndexOfWorstPhenotype]);
                    ReproductionTime += ReproductionInterval;
                    for (int i = 0; i < Phenotypes; i++)
                    {
                        Population[i].Trainning = true;
                    }
                }
                yield return new WaitForSeconds(0.00001f);
            }
        }

        //Destroy todas as redes de uma população em treinamento
        public static void DestroyCoroutiners()
        {

            int i = 0;
            GameObject[] Coroutiners;
            bool b = false;
            while (!b)
            {
                if (GameObject.Find("Coroutiner"))
                {
                    GameObject.Find("Coroutiner").transform.name = "Coroutiner" + i;
                    i++;
                }
                else { b = true; }
            }

            Coroutiners = new GameObject[i];
            while (i > 0)
            {
                i--;
                Coroutiners[i] = GameObject.Find("Coroutiner" + i);
                GameObject.Destroy(Coroutiners[i]);
            }
        }

        //Reordena os termos de um vetor 1D de inteiros
        public static void RandomOrdering(int[] InputArray)
        {
            int r;
            int temp;
            for (int i = 0; i < InputArray.Length; i++)
            {
                r = Mathf.RoundToInt(Random.Range(0f, (float)InputArray.Length - 1f));
                temp = InputArray[i];
                InputArray[i] = InputArray[r];
                InputArray[r] = temp;
            }
        }

        //Computar uma rede multiplas vezes ao longo de dados
        public static float[,] ComputeOverData(NeuralNet PointedNetwork, float[,] InputValues)
        {
            int NumberInputs = PointedNetwork.Structure[0];
            if (NumberInputs != InputValues.GetLength(1)) Log("O numero de entradas da rede e de entradas de dados nao coincidem, dim 1");
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];
            float[,] Result = new float[InputValues.GetLength(0), NumberOutputs];
            for (int i = 0; i < InputValues.GetLength(0); i++)
            {
                for (int j = 0; j < InputValues.GetLength(1); j++)
                {
                    TempInput[j] = InputValues[i, j];
                }
                TempOutput = ComputeNetwork(PointedNetwork, TempInput, false);
                for (int j = 0; j < NumberOutputs; j++)
                {
                    Result[i, j] = TempOutput[j];
                }
            }
            return Result;
        }

        //Computar uma rede multiplas vezes ao longo de uma serie
        public static float[,] ComputeOverDataSerie(NeuralNet PointedNetwork, float[,] Serie)
        {

            int NumInputs = PointedNetwork.Structure[0];
            int NumOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];

            float[,] InputValues = new float[1, 1];
            float[,] OutputValues = new float[1, 1];
            SerieToInputOutput(Serie, NumInputs, NumOutputs, out InputValues, out OutputValues);

            int NumberInputs = PointedNetwork.Structure[0];
            if (NumberInputs != InputValues.GetLength(1)) Log("O numero de entradas da rede e de entradas de dados nao coincidem, dim 1");
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];
            float[,] Result = new float[InputValues.GetLength(0), NumOutputs];
            for (int i = 0; i < InputValues.GetLength(0); i++)
            {
                for (int j = 0; j < InputValues.GetLength(1); j++)
                {
                    TempInput[j] = InputValues[i, j];
                }
                TempOutput = ComputeNetwork(PointedNetwork, TempInput, false);
                for (int j = 0; j < NumberOutputs; j++)
                {
                    Result[i, j] = TempOutput[j];
                }
            }
            return Result;
        }

        //Criar um vetor com os indices
        public static int[] CreateCounter(float[,] Serie)
        {
            int[] Result = new int[Serie.GetLength(0)];
            for (int i = 0; i < Serie.GetLength(0); i++)
            {//apenas contagem
                Result[i] = i + 1;
            }
            return Result;
        }

        //Gera em um vetor a união entre uma serie e um numero definido de previsões de forma recursiva
        public static void PredictedPoints(NeuralNet PointedNetwork, float[,] Serie, int NumPredictedPoints, out float[,] Prediction)
        {
            //Pontos resultantes do treino
            int NumberInputs = PointedNetwork.Structure[0];
            int NumberOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];
            float[] TempInput = new float[NumberInputs];
            float[] TempOutput = new float[NumberOutputs];
            int CasosPorSaida = NumberOutputs / Serie.GetLength(1);
            float[,] ToAppend = new float[CasosPorSaida, NumberOutputs];

            Prediction = new float[Serie.GetLength(0), Serie.GetLength(1)];//dim 0 = casos, dim 1 = saidas
            Copy2D(Serie, Prediction);

            //Log("S dim 0: " + Serie.GetLength(0));
            //Log("S dim 1: " + Serie.GetLength(1));

            for (int i = 0; i < NumPredictedPoints; i += CasosPorSaida)
            {
                TempInput = LastInputToPredict(Prediction, NumberInputs);
                TempOutput = ComputeNetwork(PointedNetwork, TempInput, false);
                ToAppend = ReorganizeOutput(TempOutput, Serie.GetLength(1));
                //Log("ToAppend: " + Show2DArray(ToAppend));
                Prediction = horzcat(Prediction, ToAppend);
            }

        }

        //transforma a saida de uma unica linha em um vetor multidimensional coerente com a serie
        public static float[,] ReorganizeOutput(float[] Output, int OutputEachCase)
        {

            float[,] Result = new float[Output.Length / OutputEachCase, OutputEachCase];
            //Log("dim 0: " + Result.GetLength(0));
            //Log("dim 1: " + Result.GetLength(1));
            for (int i = 0; i <= Result.GetLength(0) - 1; i++)
            {
                //Log("Reached 1");
                for (int j = 0; j < Result.GetLength(1); j++)
                {
                    Result[i, j] = Output[i * OutputEachCase + j];
                    //Log("Reached 2");
                }
            }
            return Result;
        }

        //Concatenação horizontal de dois vetores 2D
        public static float[,] horzcat(float[,] First, float[,] Second)
        {
            int TotalLength = First.GetLength(0) + Second.GetLength(0);
            float[,] Result = new float[TotalLength, Second.GetLength(1)];
            if (First.GetLength(1) != Second.GetLength(1))
            {
                Log("O numero de linhas nos vetores nao coincidem, dim 1");
            }
            else
            {
                for (int i = 0; i < TotalLength; i++)
                {
                    for (int j = 0; j < Second.GetLength(1); j++)
                    {
                        if (i < First.GetLength(0))
                            Result[i, j] = First[i, j];
                        if (i >= First.GetLength(0) && i < TotalLength)
                            Result[i, j] = Second[i - First.GetLength(0), j];
                    }
                }
            }
            return Result;
        }

        //Concatenação horizontal de um vetor 2D e um vetor 1D
        public static float[,] horzcatdif(float[,] First, float[] Second)
        {
            int TotalLength = First.GetLength(0) + 1;
            float[,] Result = new float[TotalLength, Second.GetLength(0)];
            if (First.GetLength(1) != Second.GetLength(0))
            {
                Log("O numero de linhas nos vetores nao coincidem, dim 1 e dim 0");
            }
            else
            {
                for (int i = 0; i < TotalLength; i++)
                {
                    for (int j = 0; j < Second.GetLength(0); j++)
                    {
                        if (i < First.GetLength(0))
                            Result[i, j] = First[i, j];
                        if (i >= First.GetLength(0) && i < TotalLength)
                            Result[i, j] = Second[j];
                    }
                }
            }
            return Result;
        }

        public static void DebugNetwork(NeuralNet RedeNeural)
        {
            UnityEngine.Debug.Log("Pesos");
            UnityEngine.Debug.Log(Show2DArray(RedeNeural.Weights));
            UnityEngine.Debug.Log("Bias");
            UnityEngine.Debug.Log(Show2DArray(RedeNeural.NeuronsBias));
            UnityEngine.Debug.Log("Neuronios");
            UnityEngine.Debug.Log(Show2DArray(RedeNeural.NeuronsValues));
            UnityEngine.Debug.Log("Saidas");
            UnityEngine.Debug.Log(ShowArray(RedeNeural.Output));
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                UnityEngine.Debug.Log("Nao serializavel");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        //Normaliza a entrada entre -1 e 1
        public static float[,] NormalizeLine(float[,] input, int LineNumber)
        {
            float[] inputLine = new float[input.GetLength(1)];
            float[,] output = new float[input.GetLength(0), input.GetLength(1)];
            inputLine = PartOfArray(input, LineNumber);
            float MinValue = Min(inputLine);
            float range = Range(inputLine);
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (i == LineNumber) { output[i, j] = 2f * (inputLine[j] - MinValue) / range - 1f; }
                    if (i != LineNumber) { output[i, j] = input[i, j]; }
                }
            }
            return output;
        }
        //Normaliza o vetor 2D entre -1 e 1
        public static float[,] Normalize2D(float[,] input, NeuralNet PointedNetwork, string Definition)
        {
            float[,] output = new float[input.GetLength(0), input.GetLength(1)];
            float MaxValue = Max2D(input);
            if (Definition == "Input") PointedNetwork.InputMax = MaxValue;
            if (Definition == "Output") PointedNetwork.OutputMax = MaxValue;
            float MinValue = Min2D(input);
            if (Definition == "Input") PointedNetwork.InputMin = MinValue;
            if (Definition == "Output") PointedNetwork.OutputMin = MinValue;
            float range = MaxValue - MinValue;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    output[i, j] = 2f * (input[i, j] - MinValue) / range - 1f;
                }
            }
            return output;
        }

        //Normaliza um vetor entre -1 e 1 a partir de dois valores
        public static float[] NormalizeByTwoValues1D(float[] input, float MinValue, float MaxValue)
        {
            float[] output = new float[input.Length];
            float range = MaxValue - MinValue;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                output[i] = 2f * (input[i] - MinValue) / range - 1f;
            }
            return output;
        }

        //Denormaliza um vetor de -1 e 1 para outra faixa
        public static float[] DenormalizeValues1D(float[] input, float MinValue, float MaxValue)
        {
            float[] output = new float[input.Length];
            float range = MaxValue - MinValue;
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = range * (input[i] + 1f) / 2f + MinValue;
            }
            return output;
        }

        //Normaliza um vetor entre -1 e 1 a partir de dois valores
        public static float[,] NormalizeByTwoValues2D(float[,] input, float MinValue, float MaxValue)
        {
            float[,] output = new float[input.GetLength(0), input.GetLength(1)];
            float range = MaxValue - MinValue;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    output[i, j] = 2f * (input[i, j] - MinValue) / range - 1f;
                }
            }
            return output;
        }

        public static string Show2DArray(float[,] ToShow)
        {
            string Result = "";
            for (int i = 0; i < ToShow.GetLength(0); i++)
            {
                for (int j = 0; j < ToShow.GetLength(1); j++)
                {
                    Result += ToShow[i, j].ToString();
                    Result += " ";
                }
                Result += "\n\r";
            }
            return Result;
        }

        public static string Show2DArrayInt(int[,] ToShow)
        {
            string Result = "";
            for (int i = 0; i < ToShow.GetLength(0); i++)
            {
                for (int j = 0; j < ToShow.GetLength(1); j++)
                {
                    Result += ToShow[i, j].ToString();
                    Result += " ";
                }
                Result += "\n\r";
            }
            return Result;
        }

        public static float[] PartOfArray(float[,] InputArray, int Line)
        {
            float[] array = new float[InputArray.GetLength(1)];
            for (int i = 0; i < InputArray.GetLength(1); i++)
            {
                array[i] = InputArray[Line, i];
            }
            return array;
        }

        public static float[] PartOfArrayLine(float[,] InputArray, int Line)
        {
            float[] array = new float[InputArray.GetLength(0)];
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                array[i] = InputArray[i, Line];
            }
            return array;
        }

        //Sigmoid function
        public static float Function(float x, string type, float alpha)
        {
            float result = 0f;
            if (type == "Seno" || type == "seno")
            {
                result = Mathf.Sin(alpha * x);
            }
            if (type == "Cosseno" || type == "cosseno")
            {
                result = Mathf.Cos(alpha * x);
            }
            if (type == "Linear" || type == "linear")
            {
                result = alpha * x;
            }
            if (type == "Ramp" || type == "ramp")
            {
                result = Ramp(x, alpha);
            }
            if (type == "Exp" || type == "exp")
            {
                if (x <= 0f) result = -Mathf.Log(-x + 1f);
                if (x > 0f) result = Mathf.Log(x + 1f);
            }
            if (type == "Sigmoid" || type == "sigmoid") //entre -1 e 1
            {
                result = (2 / (1 + Mathf.Exp(-alpha * x)) - 1);
            }
            if (type == "RELU" || type == "Relu" || type == "relu")
            {
                if (x <= 0f) result = 0.1f * x;
                if (x > 0f) result = alpha * x;
            }
            if (type == "Swish" || type == "swish")
            {
                result = x / (1 + Mathf.Exp(-alpha * x));
            }
            if (type == "Gaussian" || type == "gaussian")
            {
                result = Mathf.Exp(-alpha * x * x);
            }
            if (type == "ELU" || type == "Elu" || type == "elu")
            {
                if (x <= 0f) result = alpha * (Mathf.Exp(x) - 1f);
                if (x > 0f) result = x;
            }

            return result;
        }

        //Derivative of Activation functions
        public static float DerivFunction(float x, string type, float alpha)
        {
            float result = 0f;
            if (type == "Seno" || type == "seno")
            {
                result = alpha * Mathf.Cos(alpha * x);
            }
            if (type == "Cosseno" || type == "cosseno")
            {
                result = -alpha * Mathf.Sin(alpha * x);
            }
            if (type == "Linear" || type == "linear")
            {
                result = alpha;
            }
            if (type == "Ramp" || type == "ramp")
            {
                result = DerivRamp(x, alpha);
            }
            if (type == "Exp" || type == "exp")
            {
                if (x <= 0f) result = 1f / (1f - x);
                if (x > 0f) result = 1f / (x + 1f);
            }
            if (type == "Sigmoid" || type == "sigmoid") //entre -1 e 1
            {
                result = 2f * alpha * Mathf.Exp(-alpha * x) / Mathf.Pow(1 + Mathf.Exp(-alpha * x), 2f);
            }
            if (type == "Relu" || type == "relu")
            {
                if (x <= 0f) result = 0.1f;
                if (x > 0f) result = alpha;
            }
            if (type == "Swish" || type == "Swish")
            {
                result = (1f + (1f + alpha * x) * Mathf.Exp(-alpha * x)) / Mathf.Pow(1 + Mathf.Exp(-alpha * x), 2f);
            }

            return result;
        }

        //error function
        public static float ErrorF(float x, string precision)
        {
            float result = 0f;
            if (precision == "fast")
            {
                result = (2 / (1 + Mathf.Exp(-2.405f * x)) - 1);
            }
            if (precision == "precise")
            {
                float p = 0.3275911f;
                float t = 1 / (1 + p * Mathf.Abs(x));
                float a1 = 0.254829592f;
                float a2 = -0.284496736f;
                float a3 = 1.421413741f;
                float a4 = -1.453152027f;
                float a5 = 1.061405429f;
                result = 1 - (a1 * t + a2 * t * t + a3 * t * t * t + a4 * t * t * t * t + a5 * t * t * t * t * t) * Mathf.Exp(-(x * x));
                if (x < 0) { result = -result; }
            }

            return result;
        }
        //Inverse error function
        public static float InvErrorF(float x)
        {
            float tt1, tt2, lnx, result;

            x = (1 - x) * (1 + x);
            lnx = Mathf.Log(x);
            tt1 = 2f / (3.141592653f * 0.147f) + 0.5f * lnx;
            tt2 = 1f / (0.147f) * lnx;
            result = Mathf.Sqrt(-tt1 + Mathf.Sqrt(tt1 * tt1 - tt2));
            if (x < 0) { result = -result; }

            return result;
        }

        //Security interval
        public static float SecInterv(float x)
        {
            return Mathf.Pow(2f, 0.5f) * InvErrorF(x);
        }

        //Aproximated linear sigmoid function
        public static float Ramp(float x, float alpha)
        {
            float result = 0f;
            if (x < -4f / alpha) result = -1f;
            if (x > 4f / alpha) result = 1f;
            if (x > -4f / alpha && x < 4f / alpha) result = (alpha / 4f) * x;
            return result;
        }

        //Derivative of ramp function
        public static float DerivRamp(float x, float alpha)
        {
            float result = 0f;
            if (x < -4f / alpha) result = 0;
            if (x > 4f / alpha) result = 0;
            if (x > -4f / alpha && x < 4f / alpha) result = alpha / 4f;
            return result;
        }

        //Maximum value of array
        public static float Max(float[] Entrada)
        {
            float maximo = float.NegativeInfinity;
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (Entrada[i] > maximo) maximo = Entrada[i];
            }
            return maximo;
        }
        //Maximum value of 2D array
        public static float Max2D(float[,] Entrada)
        {
            float maximo = float.NegativeInfinity;
            for (int i = 0; i < Entrada.GetLength(0); i++)
            {
                for (int j = 0; j < Entrada.GetLength(1); j++)
                {
                    if (Entrada[i, j] > maximo) maximo = Entrada[i, j];
                }
            }
            return maximo;
        }

        //Maximum value of 2D array
        public static float Min2D(float[,] Entrada)
        {
            float minimo = float.PositiveInfinity;
            for (int i = 0; i < Entrada.GetLength(0); i++)
            {
                for (int j = 0; j < Entrada.GetLength(1); j++)
                {
                    if (Entrada[i, j] < minimo) minimo = Entrada[i, j];
                }
            }
            return minimo;
        }

        //Maximum value of array
        public static int MaxInt(int[] Entrada)
        {
            int maximo = 0;
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (Entrada[i] > maximo) maximo = Entrada[i];
            }
            return maximo;
        }
        //Minimum value of array
        public static float Min(float[] Entrada)
        {
            float minimo = float.PositiveInfinity;
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (Entrada[i] < minimo) minimo = Entrada[i];
            }
            return minimo;
        }
        //Minimum value of array
        public static int MinInt(int[] Entrada)
        {
            int minimo = 100000000;
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (Entrada[i] < minimo) minimo = Entrada[i];
            }
            return minimo;
        }
        //Range of array
        public static float Range(float[] Entrada)
        {
            float faixa = Max(Entrada) - Min(Entrada);
            return faixa;
        }
        //Range of array
        public static float RangeInt(int[] Entrada)
        {
            float faixa = (float)MaxInt(Entrada) - (float)MinInt(Entrada);
            return faixa;
        }
        //Range of 2D array
        public static float Range2D(float[,] Entrada)
        {
            float faixa = Max2D(Entrada) - Min2D(Entrada);
            return faixa;
        }
        //Copy of 2D array
        public static void Copy2D(float[,] Entrada, float[,] Saida)
        {
            if (Entrada.GetLength(0) > Saida.GetLength(0)) { Log("A dimensão 0 da saida precisa ser igual ou maior do que a entrada"); }
            if (Entrada.GetLength(1) > Saida.GetLength(1)) { Log("A dimensão 1 da saida precisa ser igual ou maior do que a entrada"); }
            if (Entrada.GetLength(0) <= Saida.GetLength(0) && Entrada.GetLength(1) <= Saida.GetLength(1))
            {
                for (int i = 0; i < Entrada.GetLength(0); i++)
                {
                    for (int j = 0; j < Entrada.GetLength(1); j++)
                    {
                        Saida[i, j] = Entrada[i, j];
                    }
                }
            }
        }

        //Copy of 1D array
        public static void Copy1D(float[] Entrada, float[] Saida)
        {
            if (Entrada.GetLength(0) != Saida.GetLength(0))
            {
                Saida = new float[Entrada.Length];
            }
            if (Entrada.GetLength(0) == Saida.GetLength(0))
            {
                for (int i = 0; i < Entrada.GetLength(0); i++)
                {
                    Saida[i] = Entrada[i];
                }
            }
        }

        //Resulta na matriz de erro entre a saída de uma rede neural e uma série temporal
        public static float[,] NetworkErrorDataSerie(NeuralNet PointedNetwork, float[,] Serie)
        {

            float[,] NetworkResult = ComputeOverDataSerie(PointedNetwork, Serie);

            float[,] OutputError = new float[NetworkResult.GetLength(0), NetworkResult.GetLength(1)];

            int NumInputs = PointedNetwork.Structure[0];
            int NumOutputs = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];

            float[,] InputValues = new float[1, 1];
            float[,] OutputValues = new float[1, 1];
            SerieToInputOutput(Serie, NumInputs, NumOutputs, out InputValues, out OutputValues);

            //Comparando os resultados
            for (int i = 0; i < NetworkResult.GetLength(0); i++)
            {
                for (int j = 0; j < NetworkResult.GetLength(1); j++)
                {
                    OutputError[i, j] = NetworkResult[i, j] - OutputValues[i, j];
                }
            }

            return OutputError;

        }

        //Show array
        public static string ShowArrayLine(float[,] InputArray, int Line)
        {
            string text = string.Empty;
            for (int i = 0; i < InputArray.GetLength(1); i++)
            {
                text = text + " " + InputArray[Line, i].ToString();
            }
            return text;
        }

        public static string ShowArray(float[] InputArray)
        {
            string text = string.Empty;
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                text = text + " " + InputArray[i].ToString("G5");
            }
            return text;
        }

        public static string ShowArrayCounter(float[] InputArray)
        {
            string text = string.Empty;
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                text = text + " (" + (i + 1).ToString() + ") " + InputArray[i].ToString("G5");
            }
            return text;
        }

        public static string ShowArrayInt(int[] InputArray)
        {
            string text = string.Empty;
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                text = text + " " + InputArray[i].ToString("f0");
            }
            return text;
        }

        public static Texture2D CreateTexture(Color TextColor)
        {
            Texture2D OutTexture = new Texture2D(1, 1);
            OutTexture.SetPixel(0, 0, TextColor);
            OutTexture.Apply();
            return OutTexture;
        }

        public static IEnumerator FPSCounter(FPSStat FPS)
        {
            float count;
            while (true)
            {
                if (Time.timeScale == 1)
                {
                    yield return new WaitForSeconds(0.1f);
                    count = (1 / Time.deltaTime);
                    FPS.FPS = (Mathf.Round(count));
                    FPS.FrameTime = Time.deltaTime * 1000f;
                }
                else
                {
                    FPS.FPS = 0f;
                    FPS.FrameTime = float.PositiveInfinity;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        //Mostrar a rede
        public static void DrawNetwork(NeuralNet PointedNetwork, int PosX, int PosY, int SizeX, int SizeY, Texture2D InputTexture, Texture2D NeuronTexture, Texture2D texture, DrawNetInfo DrawDetails)
        {

            int[] AdaptedStructure = new int[PointedNetwork.Structure.Length + 1];
            int MaxNumInpuNeu = MaxInt(PointedNetwork.Structure);
            int fontsize = 9 + Mathf.RoundToInt(DrawDetails.FontSize * 24 / PointedNetwork.TotalFreeParameters) + Mathf.RoundToInt(DrawDetails.FontSize * SizeY * Screen.width / 100000);

            if (DrawDetails.EditParam)
            {
                DrawDetails.WeightString = new string[PointedNetwork.Weights.GetLength(0), PointedNetwork.Weights.GetLength(1)];
                for (int i = 0; i < PointedNetwork.Weights.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.Weights.GetLength(1); j++)
                    {
                        DrawDetails.WeightString[i, j] = PointedNetwork.Weights[i, j].ToString();
                    }
                }
                DrawDetails.BiasString = new string[PointedNetwork.NeuronsBias.GetLength(0), PointedNetwork.NeuronsBias.GetLength(1)];
                for (int i = 0; i < PointedNetwork.NeuronsBias.GetLength(0); i++)
                {
                    for (int j = 0; j < PointedNetwork.NeuronsBias.GetLength(1); j++)
                    {
                        DrawDetails.BiasString[i, j] = PointedNetwork.NeuronsBias[i, j].ToString();
                    }
                }
            }

            for (int i = 0; i < AdaptedStructure.Length; i++)
            {
                if (i < PointedNetwork.Structure.Length)
                    AdaptedStructure[i] = PointedNetwork.Structure[i];
                if (i >= PointedNetwork.Structure.Length)
                    AdaptedStructure[i] = PointedNetwork.Structure[i - 1];
            }

            Texture2D gray12Texture = CreateTexture((9 * Color.black + Color.white) / 10);
            Texture2D grayTexture = CreateTexture(Color.gray);
            Texture2D whiteTexture = CreateTexture(Color.white);

            int columns = AdaptedStructure.Length;
            int InputSize = SizeY / 10;
            if (MaxNumInpuNeu > 8)
            {
                InputSize = (SizeY - MaxNumInpuNeu * fontsize) / (MaxNumInpuNeu + 2);
            }
            int BiasPad = (InputSize - fontsize * 4 + 6) / 2;
            if (BiasPad < 0)
            {
                BiasPad = 0;
            }

            Vector2 Start = new Vector2(0, 0);
            Vector2 Finish = new Vector2(0, 0);
            Rect rectText = new Rect(0, 0, 0, 0);
            Rect rectBack = new Rect(0, 0, 0, 0);
            Rect rectDraw = new Rect(0, 0, 0, 0);

            GUI.skin.label.fontSize = fontsize;
            GUI.skin.textField.fontSize = 2 * fontsize / 3;

            int[] DrawPosX = new int[columns];
            int[,] DrawPosY = new int[columns, MaxNumInpuNeu];

            int IntervalX = SizeX / (columns + 1);
            int IntervalY = 0;

            for (int i = 0; i < columns; i++)
            {
                DrawPosX[i] = PosX + (i + 1) * IntervalX - InputSize / 2;
            }

            for (int i = 0; i < columns; i++)
            {
                IntervalY = SizeY / (AdaptedStructure[i] + 1);
                for (int j = 0; j < AdaptedStructure[i]; j++)
                {
                    DrawPosY[i, j] = PosY + (j + 1) * IntervalY - InputSize / 2;
                }
            }
            //desenha as linhas
            int pad = InputSize / 2;
            for (int i = 0; i < columns - 1; i++)
            {
                for (int j = 0; j < AdaptedStructure[i]; j++)
                {
                    for (int k = 0; k < AdaptedStructure[i + 1]; k++)
                    {
                        Start = new Vector2(DrawPosX[i] + pad, DrawPosY[i, j] + pad);
                        if (i < columns - 2)//entradas e camadas
                        {
                            Finish = new Vector2(DrawPosX[i + 1] + pad, DrawPosY[i + 1, k] + pad);
                        }
                        if (i == columns - 2)//saidas
                        {
                            Finish = new Vector2(DrawPosX[i + 1] + pad, DrawPosY[i + 1, j] + pad);
                        }
                        DrawLineStretched(Start, Finish, texture, 1 + pad / 15);
                    }
                }
            }

            //ajusta posição dos pesos

            GUI.skin.label.fontSize = 18;
            GUI.Label(new Rect(PosX + SizeX - 160, PosY + 15, 140, 30), "Erro: " + PointedNetwork.NetworkError.ToString("G3"));
            GUI.skin.label.fontSize = 13;
            if (GUI.Button(new Rect(PosX + SizeX - 160, PosY + 15 * 3 + 4, 140, 45), ScreenshotIcon))
            {
                SavePartOfScreen(PosX, Screen.height - PosY - SizeY, SizeX, SizeY, "Rede neural " + PointedNetwork.Name + " " + System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd-MM-yyyy"));
                Common.Speak("Captura de tela salva com sucesso");
            }
            GUI.Label(new Rect(PosX + SizeX - 160, PosY + 7 * 15, 140, 25), "Posição dos pesos");
            DrawDetails.WeightPos = GUI.HorizontalSlider(new Rect(PosX + SizeX - 160, PosY + 120 + 10, 140, 25), DrawDetails.WeightPos, 0.0F, 10.0F);
            GUI.Label(new Rect(PosX + SizeX - 160, PosY + 9 * 15 + 20, 140, 25), "Tamanho da fonte");
            DrawDetails.FontSize = GUI.HorizontalSlider(new Rect(PosX + SizeX - 160, PosY + 150 + 30, 140, 25), DrawDetails.FontSize, 0.0F, 2.0F);
            DrawDetails.VisibleParam = GUI.Toggle(new Rect(PosX + SizeX - 160, PosY + 11 * 15 + 40, 140, 25), DrawDetails.VisibleParam, "Parametros visiveis");
            DrawDetails.EditParam = GUI.Toggle(new Rect(PosX + SizeX - 160, PosY + 12 * 15 + 50, 140, 25), DrawDetails.EditParam, "Editar parametros");
            GUI.skin.label.fontSize = fontsize;

            //desenha os inputs e neuronios
            for (int i = 0; i < columns - 1; i++)
            {
                for (int j = 0; j < AdaptedStructure[i]; j++)
                {
                    rectDraw = new Rect(DrawPosX[i], DrawPosY[i, j], InputSize, InputSize);
                    if (i == 0)
                        GUI.DrawTexture(rectDraw, InputTexture);
                    if (i != 0)
                        GUI.DrawTexture(rectDraw, NeuronTexture);
                }
            }
            //desenha os bias
            if (DrawDetails.VisibleParam)
            {
                for (int i = 0; i < columns - 2; i++)
                {
                    for (int j = 0; j < AdaptedStructure[i + 1]; j++)
                    {
                        rectText = new Rect(DrawPosX[i + 1] + BiasPad, DrawPosY[i + 1, j] - fontsize - 4, fontsize * 4, fontsize + 6);
                        rectBack = new Rect(DrawPosX[i + 1] + BiasPad - 4, DrawPosY[i + 1, j] - fontsize - 3, fontsize * 4 + 4, fontsize + 3);
                        GUI.DrawTexture(rectBack, gray12Texture);
                        if (DrawDetails.EditParam)
                        {
                            DrawDetails.BiasString[j, i] = GUI.TextField(rectText, DrawDetails.BiasString[j, i], fontsize);
                            float.TryParse(DrawDetails.BiasString[j, i], out PointedNetwork.NeuronsBias[j, i]);
                        }
                        if (!DrawDetails.EditParam)
                        {
                            GUI.Label(rectText, PointedNetwork.NeuronsBias[j, i].ToString("F4"));
                        }
                    }
                }
            }

            //desenha os pesos
            if (DrawDetails.VisibleParam)
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    for (int j = 0; j < AdaptedStructure[i]; j++)
                    {
                        for (int k = 0; k < AdaptedStructure[i + 1]; k++)
                        {
                            Start = new Vector2(DrawPosX[i] + pad, DrawPosY[i, j] + pad);
                            if (i < columns - 2)//entradas e camadas
                            {
                                Finish = new Vector2(DrawPosX[i + 1] + pad, DrawPosY[i + 1, k] + pad);
                            }
                            if (i == columns - 2)//saidas
                            {
                                Finish = new Vector2(DrawPosX[i + 1] + pad, DrawPosY[i + 1, j] + pad);
                            }
                            rectText = new Rect(((10 - DrawDetails.WeightPos) * Start.x + DrawDetails.WeightPos * Finish.x) / 10 - fontsize * 2, ((10 - DrawDetails.WeightPos) * Start.y + DrawDetails.WeightPos * Finish.y) / 10 - fontsize / 2, fontsize * 4, fontsize + 6);
                            rectBack = new Rect(((10 - DrawDetails.WeightPos) * Start.x + DrawDetails.WeightPos * Finish.x) / 10 - fontsize * 2, ((10 - DrawDetails.WeightPos) * Start.y + DrawDetails.WeightPos * Finish.y) / 10 - fontsize / 2, fontsize * 4 + 8, fontsize + 3);
                            GUI.DrawTexture(rectBack, gray12Texture);
                            if (i < columns - 2)//entradas e camadas
                            {
                                if (DrawDetails.EditParam)
                                {
                                    DrawDetails.WeightString[j * AdaptedStructure[i + 1] + k, i] = GUI.TextField(rectText, DrawDetails.WeightString[j * AdaptedStructure[i + 1] + k, i], fontsize);
                                    float.TryParse(DrawDetails.WeightString[j * AdaptedStructure[i + 1] + k, i], out PointedNetwork.Weights[j * AdaptedStructure[i + 1] + k, i]);
                                }
                                if (!DrawDetails.EditParam)
                                {
                                    GUI.Label(rectText, PointedNetwork.Weights[j * AdaptedStructure[i + 1] + k, i].ToString("F4"));
                                }
                            }
                            if (i == columns - 2)//saidas
                            {
                                if (DrawDetails.EditParam)
                                {
                                    DrawDetails.WeightString[j, i] = GUI.TextField(rectText, DrawDetails.WeightString[j, i], fontsize);
                                    float.TryParse(DrawDetails.WeightString[j, i], out PointedNetwork.Weights[j, i]);
                                }
                                if (!DrawDetails.EditParam)
                                {
                                    GUI.Label(rectText, PointedNetwork.Weights[j, i].ToString("F4"));
                                }
                            }
                        }
                    }
                }
            }



            //criar quadro
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), grayTexture, 1);
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), grayTexture, 1);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), grayTexture, 1);
            DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), grayTexture, 1);
            int squareSize = 1 + Mathf.RoundToInt(SizeY * Screen.width / 200000);
            GUI.DrawTexture(new Rect(PosX, PosY, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX, PosY + SizeY - squareSize, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY + SizeY - squareSize, squareSize, squareSize), whiteTexture);

            GUI.skin.label.fontSize = 12;
            GUI.skin.textField.fontSize = 12;


        }

        //Mostrar detalhes de uma rede neural
        public static void TextsNetworkDetails(NeuralNet PointedNetwork, UIdata uidata, int PosX, int PosY, int SizeX, int SizeY, float[,] DataInput, float[,] DataOutput)
        {

            uidata.FontSize = 8 + Mathf.RoundToInt(SizeY * Screen.width / 160000);
            uidata.SquareSize = 1 + Mathf.RoundToInt(SizeY * Screen.width / 200000);
            float Criterea1 = DataInput.GetLength(0) / (SomaParc(PointedNetwork.Structure, 1, PointedNetwork.Structure.Length - 2) * (PointedNetwork.Structure[0] + PointedNetwork.Structure[PointedNetwork.Structure.Length - 1]));
            //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            GUI.skin.label.fontSize = uidata.FontSize;
            int TextPosX = PosX + 2 * uidata.SquareSize;

            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray12Texture);

            int midLine = (2 * uidata.SquareSize + uidata.FontSize) / 2;
            DrawLineStretched(new Vector2(PosX, PosY + midLine), new Vector2(PosX + SizeX, PosY + midLine), gray25Texture, 2 * uidata.SquareSize + uidata.FontSize);

            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray50Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), gray50Texture, uidata.SquareSize - 3);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray50Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), gray50Texture, 1);

            GUI.DrawTexture(new Rect(PosX, PosY, uidata.SquareSize, uidata.SquareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - uidata.SquareSize, PosY, uidata.SquareSize, uidata.SquareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX, PosY + SizeY - uidata.SquareSize, uidata.SquareSize, uidata.SquareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - uidata.SquareSize, PosY + SizeY - uidata.SquareSize, uidata.SquareSize, uidata.SquareSize), whiteTexture);

            GUI.Label(new Rect(PosX + 2 * uidata.SquareSize, PosY + 3, uidata.FontSize * 18, uidata.FontSize * 2), "Detalhes da rede: ");

            GUI.Label(new Rect(TextPosX, 5 + PosY + SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Rede: " + PointedNetwork.Name);
            GUI.Label(new Rect(TextPosX, 5 + PosY + 2 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Inicializada: " + PointedNetwork.Initialized.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 3 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Treinando: " + PointedNetwork.Trainning.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 4 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Estrutura: " + ShowArrayInt(PointedNetwork.Structure));
            GUI.Label(new Rect(TextPosX, 5 + PosY + 5 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Parâmetros da rede: " + PointedNetwork.TotalFreeParameters.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 6 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Função de ativação: " + PointedNetwork.ActivationFunction);
            GUI.Label(new Rect(TextPosX, 5 + PosY + 7 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Alfa: " + PointedNetwork.alpha.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 8 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Estatus: " + PointedNetwork.Status);
            GUI.Label(new Rect(TextPosX, 5 + PosY + 9 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Erro: " + PointedNetwork.NetworkError.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 10 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "R²: " + PointedNetwork.RSquare.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 11 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "R² ajustado: " + PointedNetwork.RSquareAdju.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 12 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Tempo de treinamento: " + PointedNetwork.TrainningTime.ToString());
            GUI.Label(new Rect(TextPosX, 5 + PosY + 13 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Número de iterações: " + PointedNetwork.NumberIterations.ToString());

            GUI.Label(new Rect(TextPosX, 5 + PosY + 15 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Critérios de overfitting: ");
            GUI.Label(new Rect(TextPosX, 5 + PosY + 16 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Critérios 1: " + (Mathf.Exp(-0.09f * Criterea1) * 100).ToString("f2") + " %");
            uidata.Casos = (PointedNetwork.Structure[0]) * (DataInput.GetLength(0) - PointedNetwork.Structure[0] / DataInput.GetLength(1) - PointedNetwork.Structure[PointedNetwork.Structure.Length - 1] / DataInput.GetLength(1));
            GUI.Label(new Rect(TextPosX, 5 + PosY + 17 * SizeY / 19, SizeX - 5 * uidata.SquareSize, SizeY / 7), "Critérios 2: " + ((100f * (float)PointedNetwork.TotalFreeParameters) / ((float)uidata.Casos)).ToString("F2") + " %");

            GUI.skin.label.fontSize = 12;
        }

        //Desenhar estatisticas
        public static void TimeDetails(UIdata uidata, int PosX, int PosY, int SizeX, int SizeY)
        {
            uidata.FontSize = 7 + Mathf.RoundToInt(SizeY * Screen.width / 25000);
            uidata.SquareSize = 2 + Mathf.RoundToInt(SizeY * Screen.width / 100000);
            //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            GUI.skin.label.fontSize = uidata.FontSize;

            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray20Texture);

            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray75Texture, uidata.SquareSize);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray75Texture, uidata.SquareSize);

            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 2, SizeX, 2 * uidata.FontSize), "Tempo");
            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 4 + uidata.FontSize, SizeX, 2 * uidata.FontSize), System.DateTime.Now.ToString("hh.mm.ss"));
            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 6 + 2 * uidata.FontSize, SizeX, 2 * uidata.FontSize), IntuitiveTime(Time.time));

            GUI.skin.label.fontSize = 12;
        }

        //Desenhar estatisticas da taxa de frames
        public static void FPSDetails(UIdata uidata, int PosX, int PosY, int SizeX, int SizeY, FPSStat FPS)
        {
            uidata.FontSize = 7 + Mathf.RoundToInt(SizeY * Screen.width / 25000);
            uidata.SquareSize = 2 + Mathf.RoundToInt(SizeY * Screen.width / 100000);
            //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            GUI.skin.label.fontSize = uidata.FontSize;

            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray20Texture);

            if (FPS.FPS >= 25)
            {
                DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray75Texture, uidata.SquareSize);
                DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray75Texture, uidata.SquareSize);
            }

            if (FPS.FPS < 25 && FPS.FPS >= 10)
            {
                DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), YellowTexture, uidata.SquareSize);
                DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), YellowTexture, uidata.SquareSize);
            }

            if (FPS.FPS < 10)
            {
                DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), RedTexture, uidata.SquareSize);
                DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), RedTexture, uidata.SquareSize);
            }

            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 2, SizeX, 2 * uidata.FontSize), "Desempenho");
            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 4 + uidata.FontSize, SizeX, 2 * uidata.FontSize), "FPS: " + FPS.FPS.ToString());
            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 6 + 2 * uidata.FontSize, SizeX, 2 * uidata.FontSize), "Tempo do quadro: " + FPS.FrameTime.ToString("F2") + " ms");

            GUI.skin.label.fontSize = 12;
        }

        //Desenhar estatisticas
        public static void CPUandRAMUsage(UIdata uidata, int PosX, int PosY, int SizeX, int SizeY)
        {

            uidata.FontSize = 7 + Mathf.RoundToInt(SizeY * Screen.width / 25000);
            uidata.SquareSize = 2 + Mathf.RoundToInt(SizeY * Screen.width / 100000);
            //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            GUI.skin.label.fontSize = uidata.FontSize;

            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray20Texture);

            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray75Texture, uidata.SquareSize);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray75Texture, uidata.SquareSize);

            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 2, SizeX, 2 * uidata.FontSize), "Computador");
            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 4 + uidata.FontSize, SizeX, 2 * uidata.FontSize), "CPU " + CurrentCPUusage() + "%");
            GUI.Label(new Rect(PosX + 3 * uidata.SquareSize, PosY + 6 + 2 * uidata.FontSize, SizeX, 2 * uidata.FontSize), "RAM " + ((float)System.GC.GetTotalMemory(false) / 1048576f).ToString("F2") + " Gb");

            GUI.skin.label.fontSize = 12;
        }

        public static string CurrentCPUusage()
        {
            System.Diagnostics.PerformanceCounter cpuCounter;
            //System.Diagnostics.PerformanceCounter ramCounter;

            System.Diagnostics.PerformanceCounterCategory.Exists("PerformanceCounter");

            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            //ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            return System.Convert.ToInt32(cpuCounter.NextValue()).ToString();
        }

        //Criar grupo de botoes
        public static bool[] ButtonGroup(int PosX, int PosY, int SizeX, int SizeY, int ButtonNum, string[] Names, Texture[] ButtonTexture)
        {
            int fontsize = 10 + Mathf.RoundToInt(SizeY * Screen.width / 15000);
            int squareSize = 1 + Mathf.RoundToInt(SizeY * Screen.width / 100000);
            int IncrementDivision = SizeX / ButtonNum;
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            bool[] output = new bool[ButtonNum];

            GUI.skin.label.fontSize = fontsize;

            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray12Texture);
            for (int i = 0; i < ButtonNum; i++)
            {
                if (mousePosition.x > (PosX + IncrementDivision * (i)) && mousePosition.x < (PosX + IncrementDivision * (i + 1)) && mousePosition.y > (Screen.height - SizeY - PosY) && mousePosition.y < (Screen.height - PosY))
                {
                    GUI.DrawTexture(new Rect(PosX + IncrementDivision * (i), PosY, IncrementDivision, SizeY), gray25Texture);
                    GUI.DrawTexture(new Rect(PosX + IncrementDivision * (i), PosY + SizeY - squareSize * 2, IncrementDivision, squareSize * 2), whiteTexture);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        output[i] = true;
                    }
                }
            }

            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), gray75Texture, squareSize);
            DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), gray75Texture, squareSize);

            GUI.DrawTexture(new Rect(PosX, PosY, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX, PosY + SizeY - squareSize, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY + SizeY - squareSize, squareSize, squareSize), whiteTexture);
            //divisions
            for (int i = 0; i < ButtonNum - 1; i++)
            {
                DrawLineStretched(new Vector2(PosX + IncrementDivision * (i + 1), PosY + SizeY / 5), new Vector2(PosX + IncrementDivision * (i + 1), PosY + 4 * SizeY / 5), gray50Texture, squareSize);
            }
            //botoes
            for (int i = 0; i < ButtonNum; i++)
            {
                GUI.DrawTexture(new Rect(PosX + IncrementDivision * i + SizeY / 4, PosY + SizeY / 4, SizeY / 2, SizeY / 2), ButtonTexture[i]);
                GUI.Label(new Rect(PosX + IncrementDivision * i + SizeY, PosY + SizeY / 2 - 3 * fontsize / 4, fontsize * 8, fontsize * 2), Names[i]);
            }
            GUI.skin.label.fontSize = 12;

            return output;
        }

        //Mostrar detalhes do treinamento da rede
        public static void DrawNetworkDetails(NeuralNet PointedNetwork, int PosX, int PosY, int SizeX, int SizeY, float TotalTrainningTime)
        {

            int fontsize = 8 + Mathf.RoundToInt(SizeY * Screen.width / 80000);
            int squareSize = 1 + Mathf.RoundToInt(SizeY * Screen.width / 100000);

            GUI.skin.label.fontSize = fontsize;

            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray12Texture);

            int midLine = (2 * squareSize + fontsize) / 2;
            DrawLineStretched(new Vector2(PosX, PosY + midLine), new Vector2(PosX + SizeX, PosY + midLine), gray25Texture, 2 * squareSize + fontsize);

            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray50Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), gray50Texture, squareSize - 1);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray50Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), gray50Texture, 1);

            GUI.DrawTexture(new Rect(PosX, PosY, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX, PosY + SizeY - squareSize, squareSize, squareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY + SizeY - squareSize, squareSize, squareSize), whiteTexture);

            GUI.Label(new Rect(PosX + 2 * squareSize, PosY, fontsize * 8, fontsize * 2), "Detalhes: ");
            int TextPosX = PosX + 2 * squareSize;
            //int TextPosY = PosY + 4 * squareSize + fontsize;

            DrawPercBar(TextPosX, 5 + PosY + SizeY / 7, SizeX - 5 * squareSize, SizeY / 7, "Erro: " + PointedNetwork.NetworkError.ToString(), 0f, 0.1f, PointedNetwork.NetworkError);
            DrawPercBar(TextPosX, 5 + PosY + 2 * SizeY / 7, SizeX - 5 * squareSize, SizeY / 7, "Tempo de treinamento: " + IntuitiveTime(PointedNetwork.TrainningTime) + " (" + PointedNetwork.TrainningTime.ToString("F1") + ")", 0f, TotalTrainningTime, PointedNetwork.TrainningTime);
            DrawPercBar(TextPosX, 5 + PosY + 3 * SizeY / 7, SizeX - 5 * squareSize, SizeY / 7, "Iteraçoes: " + PointedNetwork.NumberIterations.ToString(), 0f, Mathf.RoundToInt(TotalTrainningTime * (float)PointedNetwork.NumberIterations / PointedNetwork.TrainningTime), PointedNetwork.NumberIterations);
            DrawPercBar(TextPosX, 5 + PosY + 4 * SizeY / 7, SizeX - 5 * squareSize, SizeY / 7, "Avanço: " + PointedNetwork.Advance.ToString(), 0f, 15f, PointedNetwork.Advance);
            DrawPercBar(TextPosX, 5 + PosY + 5 * SizeY / 7, SizeX - 5 * squareSize, SizeY / 7, "R²:  " + PointedNetwork.RSquare.ToString("F4") + "  R²  ajustado:  " + PointedNetwork.RSquareAdju.ToString("F4"), 0f, 1f, PointedNetwork.RSquareAdju);
            GUI.skin.label.fontSize = fontsize;
            GUI.Label(new Rect(TextPosX, PosY + 6 * SizeY / 7 - fontsize, SizeY, fontsize * 2), "Status: " + PointedNetwork.Status);

            GUI.skin.label.fontSize = 12;

        }

        //mostrar informações dos dados
        public static void DrawDataDetails(string Name, float[,] InputData, float[,] OutputData, string itemPath, int PosX, int PosY, int SizeX, int SizeY, UIdata UIinfo)
        {
            UIinfo.FontSize = 10 + Mathf.RoundToInt(SizeY * Screen.width / 160000);
            UIinfo.SquareSize = 1 + Mathf.RoundToInt(SizeY * Screen.width / 180000);

            GUI.skin.label.fontSize = UIinfo.FontSize;
            //criar quadro
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray12Texture);

            int midLine = (2 * UIinfo.SquareSize + UIinfo.FontSize) / 2;
            DrawLineStretched(new Vector2(PosX, PosY + midLine), new Vector2(PosX + SizeX, PosY + midLine), gray25Texture, 2 * UIinfo.SquareSize + UIinfo.FontSize);

            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray50Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), gray50Texture, UIinfo.SquareSize - 1);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray50Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), gray50Texture, 1);

            GUI.DrawTexture(new Rect(PosX, PosY, UIinfo.SquareSize, UIinfo.SquareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - UIinfo.SquareSize, PosY, UIinfo.SquareSize, UIinfo.SquareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX, PosY + SizeY - UIinfo.SquareSize, UIinfo.SquareSize, UIinfo.SquareSize), whiteTexture);
            GUI.DrawTexture(new Rect(PosX + SizeX - UIinfo.SquareSize, PosY + SizeY - UIinfo.SquareSize, UIinfo.SquareSize, UIinfo.SquareSize), whiteTexture);

            GUI.Label(new Rect(PosX + 2 * UIinfo.SquareSize + 2, PosY, UIinfo.FontSize * 8, UIinfo.FontSize * 2), "Informações: ");
            int TextPosX = PosX + 2 * UIinfo.SquareSize;
            GUI.Label(new Rect(TextPosX, 5 + PosY + SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Nome do dado: " + Name);
            if (Name.Contains("Serie") || Name.Contains("Série") || Name.Contains("serie") || Name.Contains("série") || Name.Contains("Series") || Name.Contains("Serie"))
            {
                GUI.Label(new Rect(TextPosX, 5 + PosY + 2 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Tipo: Série");
                GUI.Label(new Rect(TextPosX, 5 + PosY + 4 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Número de valores a cada instante no tempo: " + OutputData.GetLength(1));
            }
            else
            {
                GUI.Label(new Rect(TextPosX, 5 + PosY + 2 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Tipo: Entrada - Saida");
                GUI.Label(new Rect(TextPosX, 5 + PosY + 4 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Número de entradas: " + InputData.GetLength(1) + "  Número de saídas: " + OutputData.GetLength(1));
            }
            if (InputData.GetLength(0) == OutputData.GetLength(0))
                GUI.Label(new Rect(TextPosX, 5 + PosY + 3 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Número de casos: " + InputData.GetLength(0));
            if (InputData.GetLength(0) != OutputData.GetLength(0))
                GUI.Label(new Rect(TextPosX, 5 + PosY + 3 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Número de casos: Incoerente, entradas e saidas não coincidem: " + InputData.GetLength(0) + " Casos de entrada " + OutputData.GetLength(0) + " Casos de saida");
            GUI.Label(new Rect(TextPosX, 5 + PosY + 5 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Valores máximos e mínimos:");
            if (Name.Contains("Serie") || Name.Contains("Série") || Name.Contains("serie") || Name.Contains("série") || Name.Contains("Series") || Name.Contains("Serie"))
            {
                GUI.Label(new Rect(TextPosX, 5 + PosY + 6 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Série - Min " + Min2D(OutputData).ToString() + " Max " + Max2D(OutputData).ToString());
            }
            else
            {
                GUI.Label(new Rect(TextPosX, 5 + PosY + 6 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Entrada - Min " + Min2D(InputData).ToString() + " Max " + Max2D(InputData).ToString());
                GUI.Label(new Rect(TextPosX, 5 + PosY + 7 * SizeY / 15, SizeX - 5 * UIinfo.SquareSize, SizeY / 15), "Saida - Min " + Min2D(OutputData).ToString() + " Max " + Max2D(OutputData).ToString());
            }

            if (GUI.Button(new Rect(TextPosX, 5 + PosY + 8 * SizeY / 15, SizeX / 3, SizeY / 18), "Abrir local de origem"))
            {
                itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
                System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
            }

        }


        //Tempo intuitivo
        public static string IntuitiveTime(float TimeInSeconds)
        {
            string Result = "algum tempo";
            if (TimeInSeconds >= 0f && TimeInSeconds < 60f) Result = TimeInSeconds.ToString("F1") + " segundos";
            if (TimeInSeconds >= 60f && TimeInSeconds < 3600f) Result = (TimeInSeconds / 60f).ToString("F1") + " minutos";
            if (TimeInSeconds >= 3600f && TimeInSeconds < 86400f) Result = (TimeInSeconds / 3600f).ToString("F1") + " horas";
            if (TimeInSeconds >= 86400f && TimeInSeconds < 604800f) Result = (TimeInSeconds / 86400f).ToString("F1") + " dias";
            if (TimeInSeconds >= 604800f && TimeInSeconds < 2721600f) Result = (TimeInSeconds / 604800f).ToString("F1") + " semanas";
            if (TimeInSeconds >= 2721600f && TimeInSeconds < 31536000f) Result = (TimeInSeconds / 2721600f).ToString("F1") + " meses";
            if (TimeInSeconds >= 31536000f) Result = (TimeInSeconds / 31536000f).ToString("F1") + " anos";
            return Result;
        }

        //Desenha uma barra de evolução horizontal
        public static void DrawPercBar(int PosX, int PosY, int SizeX, int SizeY, string Title, float Min, float Max, float Value)
        {
            int fontsize = 10 + Mathf.RoundToInt(SizeY * Screen.width / 18000);

            GUI.skin.label.fontSize = fontsize;

            int thickness = SizeY - 3 * fontsize;
            float Perc = (Value - Min) / (Max - Min);
            if (Perc < 0f || float.IsNaN(Perc)) { Perc = 0f; }
            if (Perc > 1f) { Perc = 1f; }
            int BarSize = Mathf.RoundToInt(Perc * ((float)SizeX));

            GUI.Label(new Rect(PosX, PosY - 2 * fontsize, SizeX, fontsize * 2), Title);
            DrawLineStretched(new Vector2(PosX, PosY + thickness / 2), new Vector2(PosX + SizeX, PosY + thickness / 2), gray50Texture, thickness);
            if (BarSize != 0)
            {
                DrawLineStretched(new Vector2(PosX, PosY + thickness / 2), new Vector2(PosX + BarSize, PosY + thickness / 2), whiteTexture, thickness);
            }
            GUI.skin.label.fontSize = 4 * fontsize / 5;
            GUI.Label(new Rect(PosX, PosY + thickness, fontsize * 7, 2 * fontsize), Min.ToString());
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperRight;
            GUI.Label(new Rect(PosX + SizeX - fontsize * 7, PosY + thickness, fontsize * 7, 2 * fontsize), Max.ToString());
            centeredStyle.alignment = TextAnchor.UpperLeft;

            //GUI.skin.label.fontSize = 12;
            Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// Draw a line between two points with the specified texture and thickness.
        /// The texture will be stretched to fill the drawing rectangle.
        /// Inspired by code posted by Sylvan
        /// http://forum.unity3d.com/threads/17066-How-to-draw-a-GUI-2D-quot-line-quot?p=407005&viewfull=1#post407005
        /// </summary>
        /// <param name="lineStart">The start of the line</param>
        /// <param name="lineEnd">The end of the line</param>
        /// <param name="texture">The texture of the line</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLineStretched(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            Vector2 lineVector = lineEnd - lineStart;
            float angle = Mathf.Rad2Deg * Mathf.Atan(lineVector.y / lineVector.x);
            //float angle = Mathf.Rad2Deg * FastAtan(lineVector.y / lineVector.x);
            //Debug.Log(angle);
            if (lineVector.x < 0)
            {
                angle += 180;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            // The center of the line will always be at the center
            // regardless of the thickness.
            int thicknessOffset = (int)Mathf.Ceil(thickness / 2);

            GUIUtility.RotateAroundPivot(angle,
                                         lineStart);
            GUI.DrawTexture(new Rect(lineStart.x,
                                     lineStart.y - thicknessOffset,
                                     lineVector.magnitude,
                                     thickness),
                            texture);
            GUIUtility.RotateAroundPivot(-angle, lineStart);
        }

        //arctangente rápida
        public static float FastAtan(float x)
        {
            return 2.914f / (Mathf.Exp(-0.989f * x) + 1f) - 1.47f;
            //return (3.14159265f * x / 4f) + 0.273f * x * (1f - Mathf.Abs(x));
        }

        //Graficos da série
        public static void PlotSeriePrediction(string Titulo, string Xlabel, string Ylabel, int PositionX, int PositionY, int frameWidth, int frameHeight, int[] Xvalues, float[,] SerieAndPrediction, int OuputIndex, int IndexOfPredictionPoint, float[,] PredictionInterval)
        {

            int labelWidth = 150;
            int labelHeight = 51;
            int XLeftOver = 60;
            int YLeftOver = 40;
            int ButtonSize = 32;
            int padding = 2;
            Vector2 Start = new Vector2(0, 0);
            Vector2 Finish = new Vector2(0, 0);
            float percStart = 0f;
            float percFinish = 0f;

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 10 + Screen.width / 500;

            float barWidth = 1;
            int barHeight = frameHeight - 2 * YLeftOver;

            float[] OutputLine = PartOfArrayLine(SerieAndPrediction, OuputIndex);

            float MinValueY = Min(OutputLine) - 0.05f * Range(OutputLine);
            float MaxValueY = Max(OutputLine) + 0.05f * Range(OutputLine);

            float MinValueX = MinInt(Xvalues);
            float MaxValueX = MaxInt(Xvalues);

            //Fundo branco
            GUI.DrawTexture(new Rect(PositionX, PositionY, frameWidth, frameHeight), whiteTexture);
            GUI.color = Color.black;
            //Titulo
            GUI.Label(new Rect(PositionX + XLeftOver, PositionY + YLeftOver / 2 - labelHeight / 2, frameWidth - 2 * XLeftOver, labelHeight + 4), Titulo);
            //Valores do eixo Y
            GUI.Label(new Rect(PositionX + XLeftOver / 2 - labelWidth / 2, PositionY + frameHeight / 2 - labelHeight / 2, labelWidth, labelHeight + 3), Ylabel);
            GUI.Label(new Rect(PositionX + XLeftOver / 2 - labelWidth / 2, PositionY + YLeftOver - labelHeight / 2, labelWidth, labelHeight + 3), MaxValueY.ToString("F2"));
            GUI.color = Color.gray;
            GUI.Label(new Rect(PositionX + XLeftOver / 2 - labelWidth / 2, PositionY + YLeftOver / 2 + frameHeight / 4 - labelHeight / 2, labelWidth, labelHeight + 3), ((3 * MaxValueY + MinValueY) / 4).ToString("F2"));
            GUI.Label(new Rect(PositionX + XLeftOver / 2 - labelWidth / 2, PositionY - YLeftOver / 2 + 3 * (frameHeight) / 4 - labelHeight / 2, labelWidth, labelHeight + 3), ((MaxValueY + 3 * MinValueY) / 4).ToString("F2"));
            GUI.color = Color.black;
            GUI.Label(new Rect(PositionX + XLeftOver / 2 - labelWidth / 2, PositionY + frameHeight - YLeftOver - labelHeight / 2, labelWidth, labelHeight + 3), MinValueY.ToString("F2"));
            //Valores do eixo X
            GUI.Label(new Rect(PositionX + frameWidth / 2 - labelWidth / 2, PositionY + frameHeight - YLeftOver / 2 + labelHeight / 10 - labelHeight / 2, labelWidth, labelHeight + 3), Xlabel);

            GUI.Label(new Rect(PositionX + XLeftOver - labelWidth / 2, PositionY + frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[0].ToString());
            GUI.Label(new Rect(PositionX + XLeftOver + (frameWidth - 2 * XLeftOver) / 4 - labelWidth / 2, PositionY + frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Mathf.RoundToInt(Xvalues.Length / 4)].ToString());
            GUI.Label(new Rect(PositionX + frameWidth / 2 - labelWidth / 2, PositionY + frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Mathf.RoundToInt(Xvalues.Length / 2)].ToString());
            GUI.Label(new Rect(PositionX + XLeftOver + 3 * (frameWidth - 2 * XLeftOver) / 4 - labelWidth / 2, PositionY + frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[3 * Mathf.RoundToInt(Xvalues.Length / 4)].ToString());
            GUI.Label(new Rect(PositionX + frameWidth - XLeftOver - labelWidth / 2, PositionY + frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Xvalues.Length - 1].ToString());

            GUI.color = Color.white;
            //Grid
            //Horizontal
            GUI.DrawTexture(new Rect(PositionX + XLeftOver, PositionY + YLeftOver, frameWidth - 2 * XLeftOver, 1), gray50Texture);
            GUI.DrawTexture(new Rect(PositionX + XLeftOver, PositionY + YLeftOver / 2 + (frameHeight) / 4, frameWidth - 2 * XLeftOver, 1), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + XLeftOver, PositionY + frameHeight / 2, frameWidth - 2 * XLeftOver, 1), gray50Texture);
            GUI.DrawTexture(new Rect(PositionX + XLeftOver, PositionY - YLeftOver / 2 + 3 * (frameHeight) / 4, frameWidth - 2 * XLeftOver, 1), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + XLeftOver, PositionY + frameHeight - YLeftOver, frameWidth - 2 * XLeftOver, 1), gray50Texture);
            //Vertical
            GUI.DrawTexture(new Rect(PositionX + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + (frameWidth - 2 * XLeftOver) / 4 + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray50Texture);
            GUI.DrawTexture(new Rect(PositionX + 3 * (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + frameWidth / 2, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray50Texture);
            GUI.DrawTexture(new Rect(PositionX + 5 * (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + 3 * (frameWidth - 2 * XLeftOver) / 4 + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray50Texture);
            GUI.DrawTexture(new Rect(PositionX + 7 * (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(PositionX + frameWidth - XLeftOver, PositionY + YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            //Margins
            GUI.color = 0.8f * Color.blue + 0.5f * Color.green;
            GUI.DrawTexture(new Rect(PositionX, PositionY, frameWidth, 1), blackTexture);
            GUI.DrawTexture(new Rect(PositionX, PositionY + frameHeight - 1, frameWidth, 1), blackTexture);
            GUI.DrawTexture(new Rect(PositionX, PositionY, 1, frameHeight), blackTexture);
            GUI.DrawTexture(new Rect(PositionX + frameWidth - 1, PositionY, 1, frameHeight), blackTexture);
            GUI.DrawTexture(new Rect(PositionX + frameWidth - 1 - ButtonSize - 4 * padding, PositionY, 1, frameHeight), blackTexture);
            //Legenda
            GUI.color = Color.black;
            GUI.Label(new Rect(PositionX + 2 * frameWidth / 3, PositionY + YLeftOver / 2 - labelHeight / 2, frameWidth / 4 - XLeftOver, labelHeight + 4), "Série");
            GUI.Label(new Rect(PositionX + 4 * frameWidth / 5, PositionY + YLeftOver / 2 - labelHeight / 2, frameWidth / 5 - XLeftOver, labelHeight + 4), "Previsão");
            GUI.color = Color.blue;
            GUI.DrawTexture(new Rect(PositionX + 47 * frameWidth / 60 - 3 * XLeftOver / 2, PositionY + YLeftOver / 2, 20, 2), whiteTexture);//desenhar linha
            GUI.color = Color.red;
            GUI.DrawTexture(new Rect(PositionX + 89 * frameWidth / 100 - 3 * XLeftOver / 2, PositionY + YLeftOver / 2, 20, 2), whiteTexture);//desenhar linha

            //Botões
            if (GUI.Button(new Rect(PositionX + frameWidth - ButtonSize - 4, PositionY + YLeftOver / 2 - ButtonSize / 2 + 4, ButtonSize, ButtonSize), ScreenshotIcon))
            {
                SavePartOfScreen(30, Mathf.RoundToInt(0.45f * Screen.height), Mathf.RoundToInt(Screen.width) - 30, Mathf.RoundToInt(0.4f * Screen.height), "Grafico - " + System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd-MM-yyyy"));
                Common.Speak("Captura de tela salva com sucesso");
            }
            if (MaxValueX == MinValueX)
            {
                GUI.Label(new Rect(PositionX + XLeftOver, PositionY + frameHeight / 2 - labelHeight - 1, frameWidth - 2 * XLeftOver, labelHeight + 4), "valores máximos e mínimos de entrada idênticos");
            }
            int Elements1 = Mathf.RoundToInt((float)(frameWidth - 2 * XLeftOver) * RangeInt(Xvalues) / (MaxValueX - MinValueX));

            GUI.color = Color.white;

            int InicioX1 = Mathf.RoundToInt((float)OutputLine.Length * ((float)MinInt(Xvalues) - MinValueX) / (MaxValueX - MinValueX));

            barWidth = ((float)frameWidth - 2 * (float)XLeftOver) / ((float)OutputLine.Length);

            for (int i = InicioX1; i < InicioX1 + OutputLine.Length - 1; i++)
            {
                percStart = ((OutputLine[i - InicioX1] - MinValueY) / (MaxValueY - MinValueY));
                percFinish = ((OutputLine[i + 1 - InicioX1] - MinValueY) / (MaxValueY - MinValueY));
                if (i < IndexOfPredictionPoint - 1) { GUI.color = Color.blue; }
                else { GUI.color = Color.red; }
                Start = new Vector2(PositionX + XLeftOver + barWidth * i, PositionY + frameHeight - YLeftOver - (int)(((float)barHeight) * percStart));
                Finish = new Vector2(PositionX + XLeftOver + barWidth * (i + 1), PositionY + frameHeight - YLeftOver - (int)(((float)barHeight) * percFinish));
                DrawLineStretched(Start, Finish, whiteTexture, 2);


                //float perc = ((YInterp[i - InicioX1] - MinValueY) / (MaxValueY - MinValueY));
                //if (i < ElementsSerie) { GUI.color = Color.blue; }
                //else { GUI.color = Color.red; }
                //GUI.DrawTexture(new Rect(PositionX + XLeftOver + barWidth * i, PositionY + frameHeight - YLeftOver - (int)(((float)barHeight) * perc), 2, 2), whiteTexture);//desenhar linhas
            }

            GUI.color = Color.white;

            GUI.backgroundColor = Color.white;
            centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperLeft;
            centeredStyle.fontSize = 10 + Screen.width / 500;
            Resources.UnloadUnusedAssets();
        }

        //Graficos
        public static void Plot(string Titulo, string Xlabel, string Ylabel, int PositionX, int PositionY, int Largura, int Altura, string Legenda1, string Legenda2, float[] Xvalues, float[] YValues, float[] Xvalues2, float[] YValues2, Color ColorDefault, Color ColorLow, Color ColorHigh)
        {
            int labelWidth = 150;
            int labelHeight = 51;
            int ButtonSize = 32;
            int XLeftOver = 60;
            int YLeftOver = 40;
            int padding = 2;
            int frameWidth = Largura;
            int frameHeight = Altura;
            float MinValueY = Min(YValues) - 0.05f * Range(YValues);
            float MaxValueY = Max(YValues) + 0.05f * Range(YValues);
            if (Min(YValues2) < MinValueY) MinValueY = Min(YValues2) - 0.05f * Range(YValues2);
            if (Max(YValues2) > MaxValueY) MaxValueY = Max(YValues2) + 0.05f * Range(YValues2);
            Vector2 Start = new Vector2(0, 0);
            Vector2 Finish = new Vector2(0, 0);
            float percStart = 0f;
            float percFinish = 0f;

            float MinValueX = Min(Xvalues);
            float MaxValueX = Max(Xvalues);
            if (Min(Xvalues2) < MinValueX) MinValueX = Min(Xvalues2);
            if (Max(Xvalues2) > MaxValueX) MaxValueX = Max(Xvalues2);

            float barWidth1 = 1f;
            float barWidth2 = 1f;
            int barHeight = frameHeight - 2 * YLeftOver;

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 10 + Screen.width / 500;

            var gs = GUI.skin.GetStyle("Button");
            gs.fontSize = 7 + Screen.width / 500;

            GUI.color = new Color(1f, 1f, 1f, 1f);
            GUI.DrawTexture(new Rect(0, 0, Largura, Altura), whiteTexture);
            GUI.color = Color.black;

            //Titulo
            GUI.Label(new Rect(XLeftOver, YLeftOver / 2 - labelHeight / 2, frameWidth - 2 * XLeftOver, labelHeight + 4), Titulo);

            //Valores do eixo Y
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, frameHeight / 2 - labelHeight / 2, labelWidth, labelHeight + 3), Ylabel);
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, YLeftOver - labelHeight / 2, labelWidth, labelHeight + 3), MaxValueY.ToString("F2"));
            GUI.color = Color.gray;
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, YLeftOver / 2 + frameHeight / 4 - labelHeight / 2, labelWidth, labelHeight + 3), ((3 * MaxValueY + MinValueY) / 4).ToString("F2"));
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, -YLeftOver / 2 + 3 * (frameHeight) / 4 - labelHeight / 2, labelWidth, labelHeight + 3), ((MaxValueY + 3 * MinValueY) / 4).ToString("F2"));
            GUI.color = Color.black;
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, frameHeight - YLeftOver - labelHeight / 2, labelWidth, labelHeight + 3), MinValueY.ToString("F2"));

            //Valores do eixo X
            GUI.Label(new Rect(frameWidth / 2 - labelWidth / 2, frameHeight - YLeftOver / 2 + labelHeight / 10 - labelHeight / 2, labelWidth, labelHeight + 3), Xlabel);

            GUI.Label(new Rect(XLeftOver - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[0].ToString("F2"));
            GUI.Label(new Rect(XLeftOver + (frameWidth - 2 * XLeftOver) / 4 - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Mathf.RoundToInt(Xvalues.Length / 4)].ToString("F2"));
            GUI.Label(new Rect(frameWidth / 2 - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Mathf.RoundToInt(Xvalues.Length / 2)].ToString("F2"));
            GUI.Label(new Rect(XLeftOver + 3 * (frameWidth - 2 * XLeftOver) / 4 - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[3 * Mathf.RoundToInt(Xvalues.Length / 4)].ToString("F2"));
            GUI.Label(new Rect(frameWidth - XLeftOver - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Xvalues.Length - 1].ToString("F2"));

            GUI.color = Color.white;
            //Grid
            //Horizontal
            GUI.DrawTexture(new Rect(XLeftOver, YLeftOver, frameWidth - 2 * XLeftOver, 1), gray50Texture);
            GUI.DrawTexture(new Rect(XLeftOver, YLeftOver / 2 + (frameHeight) / 4, frameWidth - 2 * XLeftOver, 1), gray75Texture);
            GUI.DrawTexture(new Rect(XLeftOver, frameHeight / 2, frameWidth - 2 * XLeftOver, 1), gray50Texture);
            GUI.DrawTexture(new Rect(XLeftOver, -YLeftOver / 2 + 3 * (frameHeight) / 4, frameWidth - 2 * XLeftOver, 1), gray75Texture);
            GUI.DrawTexture(new Rect(XLeftOver, frameHeight - YLeftOver, frameWidth - 2 * XLeftOver, 1), gray50Texture);
            //Vertical
            GUI.DrawTexture(new Rect(XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect((frameWidth - 2 * XLeftOver) / 8 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect((frameWidth - 2 * XLeftOver) / 4 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray50Texture);
            GUI.DrawTexture(new Rect(3 * (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(frameWidth / 2, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray50Texture);
            GUI.DrawTexture(new Rect(5 * (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(3 * (frameWidth - 2 * XLeftOver) / 4 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray50Texture);
            GUI.DrawTexture(new Rect(7 * (frameWidth - 2 * XLeftOver) / 8 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            GUI.DrawTexture(new Rect(frameWidth - XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), gray75Texture);
            //Margins
            GUI.color = 0.8f * Color.blue + 0.5f * Color.green;
            GUI.DrawTexture(new Rect(0, 0, frameWidth, 1), blackTexture);
            GUI.DrawTexture(new Rect(0, frameHeight - 1, frameWidth, 1), blackTexture);
            GUI.DrawTexture(new Rect(0, 0, 1, frameHeight), blackTexture);
            GUI.DrawTexture(new Rect(frameWidth - 1, 0, 1, frameHeight), blackTexture);
            GUI.DrawTexture(new Rect(frameWidth - 1 - ButtonSize - 4 * padding, 0, 1, frameHeight), blackTexture);
            //Legenda
            GUI.color = Color.black;
            GUI.Label(new Rect(2 * frameWidth / 3, YLeftOver / 2 - labelHeight / 2, frameWidth / 4 - XLeftOver, labelHeight + 4), Legenda2);
            GUI.Label(new Rect(4 * frameWidth / 5, YLeftOver / 2 - labelHeight / 2, frameWidth / 5 - XLeftOver, labelHeight + 4), Legenda1);
            GUI.color = Color.blue;
            GUI.DrawTexture(new Rect(47 * frameWidth / 60 - 3 * XLeftOver / 2, YLeftOver / 2, 20, 2), whiteTexture);//desenhar linha
            GUI.color = Color.red;
            GUI.DrawTexture(new Rect(89 * frameWidth / 100 - 3 * XLeftOver / 2, YLeftOver / 2, 20, 2), whiteTexture);//desenhar linha

            //Botões
            if (GUI.Button(new Rect(frameWidth - ButtonSize - 4, YLeftOver / 2 - ButtonSize / 2 + 4, ButtonSize, ButtonSize), ScreenshotIcon))
            {
                SavePartOfScreen(30, Mathf.RoundToInt(0.45f * Screen.height), Mathf.RoundToInt(Screen.width) - 30, Mathf.RoundToInt(0.4f * Screen.height), "Grafico - " + System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd-MM-yyyy"));
                Common.Speak("Captura de tela salva com sucesso");
            }
            //if (GUI.Button(new Rect(frameWidth-ButtonSize-4, YLeftOver/2-ButtonSize/2+4+2*padding+ButtonSize, ButtonSize, ButtonSize),"Vazio")){}
            if (MaxValueX == MinValueX)
            {
                GUI.Label(new Rect(XLeftOver, Altura / 2 - labelHeight - 1, frameWidth - 2 * XLeftOver, labelHeight + 4), "valores máximos e mínimos de entrada idênticos");
            }

            int InicioX1 = Mathf.RoundToInt((float)YValues.Length * (Min(Xvalues) - MinValueX) / (MaxValueX - MinValueX));
            int InicioX2 = Mathf.RoundToInt((float)YValues2.Length * (Min(Xvalues2) - MinValueX) / (MaxValueX - MinValueX));

            barWidth1 = ((float)frameWidth - 2 * (float)XLeftOver) / ((float)YValues.Length);
            barWidth2 = ((float)frameWidth - 2 * (float)XLeftOver) / ((float)YValues2.Length);

            barWidth1 = ((float)frameWidth - 2 * (float)XLeftOver - InicioX1 * barWidth1) / ((float)YValues.Length);
            barWidth2 = ((float)frameWidth - 2 * (float)XLeftOver - InicioX2 * barWidth2) / ((float)YValues2.Length);

            barWidth1 = ((float)frameWidth - 2 * (float)XLeftOver - InicioX1 * barWidth1) / ((float)YValues.Length);
            barWidth2 = ((float)frameWidth - 2 * (float)XLeftOver - InicioX2 * barWidth2) / ((float)YValues2.Length);

            for (int i = InicioX1; i < InicioX1 + YValues.Length - 1; i++)
            {
                percStart = ((YValues[i - InicioX1] - MinValueY) / (MaxValueY - MinValueY));
                percFinish = ((YValues[i + 1 - InicioX1] - MinValueY) / (MaxValueY - MinValueY));
                GUI.color = Color.red;
                Start = new Vector2(XLeftOver + barWidth1 * i, frameHeight - YLeftOver - (int)(((float)barHeight) * percStart));
                Finish = new Vector2(XLeftOver + barWidth1 * (i + 1), frameHeight - YLeftOver - (int)(((float)barHeight) * percFinish));
                DrawLineStretched(Start, Finish, whiteTexture, 2);
            }
            if (Xvalues2 != null && YValues2 != null)
            {
                for (int i = InicioX2; i < InicioX2 + YValues2.Length - 1; i++)
                {
                    percStart = ((YValues2[i - InicioX2] - MinValueY) / (MaxValueY - MinValueY));
                    percFinish = ((YValues2[i + 1 - InicioX2] - MinValueY) / (MaxValueY - MinValueY));
                    GUI.color = Color.blue;
                    Start = new Vector2(XLeftOver + barWidth2 * i, frameHeight - YLeftOver - (int)(((float)barHeight) * percStart));
                    Finish = new Vector2(XLeftOver + barWidth2 * (i + 1), frameHeight - YLeftOver - (int)(((float)barHeight) * percFinish));
                    DrawLineStretched(Start, Finish, whiteTexture, 2);
                }
            }

            GUI.color = Color.white;

            GUI.backgroundColor = Color.white;
            centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperLeft;
            centeredStyle.fontSize = 10 + Screen.width / 500;

            gs = GUI.skin.GetStyle("Button");
            gs.fontSize = 10 + Screen.width / 500;

            GUI.color = Color.white;
            Resources.UnloadUnusedAssets();

        }

        //Elimina os numeros iniciais de um vetor 1D
        public static float[] RemoveInitialNumbers(float[] InputData, int ToRemove)
        {
            float[] Result = new float[InputData.Length - ToRemove];
            for (int i = ToRemove; i < InputData.Length; i++)
            {
                Result[i - ToRemove] = InputData[i];
            }
            return Result;
        }

        //Pega os numeros iniciais de um vetor 1D
        public static float[] GrabInitialNumbers(float[] InputData, int ToGrab)
        {
            float[] Result = new float[ToGrab];
            for (int i = 0; i < ToGrab; i++)
            {
                Result[i] = InputData[i];
            }
            return Result;
        }

        //prencher um vetor com os ultimos valores de outro
        public static void CopyFinalNumbers(float[,] InputArray, float[] TargetArray)
        {
            if (InputArray.GetLength(1) == 1)
            {
                for (int i = 0; i < TargetArray.Length; i++)
                {
                    TargetArray[i] = InputArray[InputArray.GetLength(0) - TargetArray.Length + i, 0];
                }
            }
            if (InputArray.GetLength(1) > 1)
            {
                for (int i = 0; i < InputArray.GetLength(1); i++)
                {
                    TargetArray[i] = InputArray[InputArray.GetLength(0) - 1, i];
                }
            }

        }

        //prencher um vetor de valores com os ultimos valores de outro
        public static void CopyFinalFloatToString(float[,] InputArray, string[] TargetArray)
        {
            if (InputArray.GetLength(1) == 1)
            {
                for (int i = 0; i < TargetArray.Length; i++)
                {
                    TargetArray[i] = InputArray[InputArray.GetLength(0) - TargetArray.Length + i, 0].ToString();
                }
            }
            if (InputArray.GetLength(1) > 1)
            {
                for (int i = 0; i < TargetArray.Length; i++)
                {
                    TargetArray[i] = "0";
                }
            }

        }

        public static float[] LinearInterp(float[] InputData, int TargetLength)
        {
            float[] OutputData = new float[TargetLength];
            float Subdivisions = (float)TargetLength / (InputData.Length - 1);
            float DivisionCount = 0f;
            if (InputData.Length < TargetLength)
            {
                for (int i = 0; i < TargetLength; i++)
                {
                    OutputData[i] = InputData[(int)DivisionCount] + (((float)i - DivisionCount * Subdivisions) / (Subdivisions)) * (InputData[(int)DivisionCount + 1] - InputData[(int)DivisionCount]);
                    if (i >= DivisionCount * Subdivisions + Subdivisions) { DivisionCount += 1; }
                }
            }
            return OutputData;
        }

        public static string GenerateNetworkCode(NeuralNet PointedNetwork, string language)
        {
            string result = "";

            string[,] SymbolicNeurons = new string[PointedNetwork.NeuronsValues.GetLength(0), PointedNetwork.NeuronsValues.GetLength(1)];
            float range = PointedNetwork.InputMax - PointedNetwork.InputMin;
            int InputNum = PointedNetwork.Structure[0];
            int Outputnum = PointedNetwork.Structure[PointedNetwork.Structure.Length - 1];

            //Criação dos vetores
            if (language == "MATLAB") result += "%";
            if (language == "" || language == "C++" || language == "C#") result += "//";
            result += "São necessários criar dois vetores, um de entrada e um de saída\r\n";

            if (language == "MATLAB")
            {
                result += "Entrada = [ ];  %Entre com seus valores separados por espaço aqui\r\n";
                result += "Saida = zeros(1," + Outputnum.ToString() + ");\r\n";
            }
            if (language == "C#")
            {
                result += "float[] Entrada = new float[" + InputNum.ToString() + "] {";
                if (InputNum > 1)
                {
                    for (int i = 0; i < InputNum; i++)
                    {
                        result += ",";
                    }
                }
                result += "};  //Entre com seus valores separados por vírgula dentro dos couchetes\r\n";
                result += "float[] Saida = new float[" + Outputnum.ToString() + "];\r\n";
            }
            if (language == "C++")
            {
                result += "float Entrada[" + InputNum.ToString() + "] = {";
                if (InputNum > 1)
                {
                    for (int i = 0; i < InputNum; i++)
                    {
                        result += ",";
                    }
                }
                result += "};  //Entre com seus valores separados por vírgula dentro dos couchetes\r\n";
                result += "float Saida[" + Outputnum.ToString() + "];\r\n";
            }

            result += "\r\n";

            //normalização
            result += "//Normalização \r\n";
            if (language == "MATLAB") result += "for i=1:" + InputNum.ToString() + "\r\n";
            if (language == "C++" || language == "C#") result += "for (int i = 0; i < " + InputNum.ToString() + "; i++){\r\n";

            if (language == "MATLAB" || language == "") result += "Entrada(i) = 2 * (Entrada(i) -(" + PointedNetwork.InputMin.ToString() + ")) / " + range.ToString() + " - 1; \r\n";
            if (language == "C#" || language == "C++") result += "Entrada[i] = 2 * (Entrada[i] -(" + PointedNetwork.InputMin.ToString() + ")) / " + range.ToString() + " - 1; \r\n";

            if (language == "C++" || language == "C#") result += "}\r\n";
            if (language == "MATLAB") result += "end\r\n";

            int NetworkLength = PointedNetwork.Structure.Length;
            result += "\r\n";
            result += "//Cálculo \r\n";

            for (int i = 0; i < PointedNetwork.Structure[0]; i++)
            {
                if (language == "MATLAB" || language == "") SymbolicNeurons[i, 0] = "Entrada (" + (i + 1).ToString() + ")";
                if (language == "C++" || language == "C#") SymbolicNeurons[i, 0] = "Entrada [" + i.ToString() + "]";
            }

            for (int i = 1; i < PointedNetwork.NeuronsValues.GetLength(1); i++)
            {//camada
                for (int j = 0; j < PointedNetwork.Structure[i]; j++)
                {//Neuronio
                    string IncomingSignal = "";
                    for (int k = 0; k < PointedNetwork.Structure[i - 1]; k++)
                    {//camada anterior
                        IncomingSignal += PointedNetwork.Weights[j * PointedNetwork.Structure[i - 1] + k, i - 1].ToString() + "*" + SymbolicNeurons[k, i - 1];
                        if (k < PointedNetwork.Structure[i - 1] - 1)
                        {
                            if (PointedNetwork.Weights[j * PointedNetwork.Structure[i - 1] + k + 1, i - 1] >= 0)
                            { IncomingSignal += "+"; }
                        }
                    }
                    SymbolicNeurons[j, i] = "(2/(1+exp(-" + PointedNetwork.alpha.ToString() + "*(" + IncomingSignal;
                    if (PointedNetwork.NeuronsBias[j, i - 1] >= 0f)
                    {
                        SymbolicNeurons[j, i] += "+" + PointedNetwork.NeuronsBias[j, i - 1].ToString() + ")))-1)";
                    }
                    if (PointedNetwork.NeuronsBias[j, i - 1] < 0f)
                    {
                        SymbolicNeurons[j, i] += PointedNetwork.NeuronsBias[j, i - 1].ToString() + ")))-1)";
                    }
                }
            }

            for (int i = 0; i < PointedNetwork.Structure[PointedNetwork.Structure.Length - 1]; i++)
            {
                if (language == "C++" || language == "C#") result += "Saida[" + i.ToString() + "] = ";
                if (language == "MATLAB" || language == "") result += "Saida(" + (i + 1).ToString() + ") = ";
                result += PointedNetwork.Weights[i, NetworkLength - 1].ToString() + "*" + SymbolicNeurons[i, NetworkLength - 1] + ";\r\n";
                result += "\r\n";
            }
            //Denormaliza
            result += "//Denormalização \r\n";
            if (language == "MATLAB") result += "for i=1:" + Outputnum.ToString() + "\r\n";
            if (language == "C++" || language == "C#") result += "for (int i = 0; i < " + Outputnum.ToString() + "; i++){\r\n";

            if (language == "MATLAB" || language == "") result += "Saida(i) = " + ((PointedNetwork.OutputMax - PointedNetwork.OutputMin) / 2f).ToString() + "*Saida(i)" + "+" + ((PointedNetwork.OutputMin + PointedNetwork.OutputMax) / 2f).ToString() + ";\r\n";
            if (language == "C++" || language == "C#") result += "Saida[i] = " + ((PointedNetwork.OutputMax - PointedNetwork.OutputMin) / 2f).ToString() + "*Saida[i]" + "+" + ((PointedNetwork.OutputMin + PointedNetwork.OutputMax) / 2f).ToString() + ";\r\n";

            if (language == "C++" || language == "C#") result += "}\r\n";
            if (language == "MATLAB") result += "end\r\n";
            result += "\r\n";

            if (language == "MATLAB")
            {
                result = result.Replace("//", "%");
                result = result.Replace("/", "./");
                result = result.Replace("*", ".*");
            }
            SymbolicNeurons = null;
            //Limpar recursos nao utilizados
            Resources.UnloadUnusedAssets();
            return result;
        }

        //Salvar parte da tela
        public static void SavePartOfScreen(int PositionX, int PositionY, int Largura, int Altura, string Name)
        {

            //using System;

            //criar pastas
            if (!Directory.Exists(Application.dataPath + "/Screenshots")) { System.IO.Directory.CreateDirectory(Application.dataPath + "/Screenshots"); }

            // Create a texture the size of the screen, RGB24 format
            int width = Largura;
            int height = Altura;
            int X = PositionX;
            int Y = PositionY;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Read screen contents into the texture
            tex.ReadPixels(new Rect(X, Y, width, height), 0, 0);
            tex.Apply();

            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();
            //Destroy(tex);

            //also write to a file in the project folder
            File.WriteAllBytes(Application.dataPath + "/Screenshots/" + Name + ".png", bytes);

        }

        //Arquivos de texto
        public static void AddTextToFile(string adress, string value)
        {
            FileStream fs = new FileStream(adress, FileMode.Append, FileAccess.Write, FileShare.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(adress, true, Encoding.Unicode);
            string NextLine = "\r\n" + value;
            sw.Write(NextLine);
            sw.Close();
        }

        public static void LoadData(string FilePath, out string Name, out float[,] InputData, out float[,] OutputData)
        {
            //---------Structure----------
            //Nome
            //Nome
            //Input
            //Input data
            //Output
            //Output Data

            bool Serie = false;
            bool InputOutput = false;

            string[] line = File.ReadAllLines(FilePath);

            InputData = new float[1, 1];
            OutputData = new float[1, 1];

            int OutputIndex = 0;
            //find indexes
            for (int i = 0; i < line.GetLength(0); i++)
            {
                if (line[i].Contains("Output") ||
                    line[i].Contains("Output Data") ||
                    line[i].Contains("Saida") ||
                    line[i].Contains("Saída") ||
                    line[i].Contains("Saidas") ||
                    line[i].Contains("Saídas") ||
                    line[i].Contains("saida") ||
                    line[i].Contains("saída") ||
                    line[i].Contains("saidas") ||
                    line[i].Contains("saídas") ||
                    line[i].Contains("Dados de Saida") ||
                    line[i].Contains("Dados de Saida"))
                {
                    OutputIndex = i; InputOutput = true;
                    //Debug.Log("Indice da saida "+OutputIndex.ToString());
                }
            }

            if (line[1].Contains("Serie") ||
                line[1].Contains("Série") ||
                line[1].Contains("serie") ||
                line[1].Contains("série") ||
                line[1].Contains("Series") ||
                line[2].Contains("Serie") ||
                line[2].Contains("Série") ||
                line[2].Contains("serie") ||
                line[2].Contains("série") ||
                line[2].Contains("Series"))
            {
                Serie = true;
                InputOutput = false;
            }

            Name = line[1]; //nome na segunda linha

            if (Serie)
            {
                string[] OutputDataLine = line[3].Split(',');//Dados de Entrada/saida
                int NumOutputDataLines = OutputDataLine.GetLength(0);
                OutputData = new float[NumOutputDataLines, line.Length - 3];
                InputData = new float[NumOutputDataLines, 1];

                for (int i = 0; i < (line.Length - 3); i++)
                {//dados
                    OutputDataLine = line[3 + i].Split(',');
                    for (int j = 0; j < OutputDataLine.GetLength(0); j++)
                    {
                        OutputData[j, i] = float.Parse(OutputDataLine[j]);
                    }
                }

                for (int i = 0; i < NumOutputDataLines; i++)
                {//apenas contagem
                    InputData[i, 0] = (float)i;
                }
            }

            if (InputOutput)
            {
                string[] InputDataLine = line[3].Split(',');//Dados de entrada
                int NumInputDataLines = InputDataLine.GetLength(0);
                InputData = new float[NumInputDataLines, OutputIndex - 3];

                string[] OutputDataLine = line[OutputIndex + 1].Split(',');//Dados de saida
                int NumOutputDataLines = OutputDataLine.GetLength(0);
                OutputData = new float[NumOutputDataLines, line.GetLength(0) - OutputIndex - 1];

                for (int i = 0; i < (OutputIndex - 3); i++)
                {//Entradas
                    InputDataLine = line[3 + i].Split(',');
                    for (int j = 0; j < NumInputDataLines; j++)
                    {
                        //Debug.Log("Linha "+i+"Coluna"+j);
                        //Debug.Log("Valor entrada " + InputDataLine[j]);
                        InputData[j, i] = float.Parse(InputDataLine[j]);
                    }
                }

                for (int i = 0; i < (line.Length - OutputIndex - 1); i++)
                {//Saidas
                    OutputDataLine = line[i + OutputIndex + 1].Split(',');
                    //Debug.Log("LinhaARQUIVO " + line[i + OutputIndex + 1]);
                    //Debug.Log("Linha " + (i + OutputIndex + 1).ToString());
                    for (int j = 0; j < NumOutputDataLines; j++)
                    {
                        //Debug.Log("Linha " + i + "Coluna" + j);
                        //Debug.Log("Valor saida " + InputDataLine[j]);
                        OutputData[j, i] = float.Parse(OutputDataLine[j]);
                    }
                }
            }

        }

        public static void SaveNN(string FilePath, NeuralNet NN, float[,] Output)
        {
            using (StreamWriter outfile = new StreamWriter(FilePath))
            {
                string content = "";
                outfile.WriteLine("Nome");
                outfile.WriteLine(NN.Name);
                outfile.WriteLine("Valor maximo de entrada");
                outfile.WriteLine(NN.InputMax.ToString());
                outfile.WriteLine("Valor minimo de entrada");
                outfile.WriteLine(NN.InputMin.ToString());
                outfile.WriteLine("Valor maximo de saida");
                outfile.WriteLine(NN.OutputMax.ToString());
                outfile.WriteLine("Valor minimo de saida");
                outfile.WriteLine(NN.OutputMin.ToString());
                outfile.WriteLine("Rede inicializada");
                outfile.WriteLine(NN.Initialized.ToString());
                content = "";
                outfile.WriteLine("Estrutura");
                for (int y = 0; y < NN.Structure.GetLength(0); y++)
                {
                    content += NN.Structure[y].ToString();
                    if (y < NN.Structure.GetLength(0) - 1) content += ",";

                }
                outfile.WriteLine(content);
                content = "";
                outfile.WriteLine("Numero de pesos");
                for (int y = 0; y < NN.NumberOfWeights.GetLength(0); y++)
                {
                    content += NN.NumberOfWeights[y].ToString();
                    if (y < NN.NumberOfWeights.GetLength(0) - 1) content += ",";
                }
                outfile.WriteLine(content);
                outfile.WriteLine("Numero de parametros livres");
                outfile.WriteLine(NN.TotalFreeParameters.ToString());
                outfile.WriteLine("Função de ativação");
                outfile.WriteLine(NN.ActivationFunction);
                outfile.WriteLine("Alpha");
                outfile.WriteLine(NN.alpha.ToString());
                outfile.WriteLine("Pesos");
                for (int x = 0; x < NN.Weights.GetLength(0); x++)
                {
                    content = "";
                    for (int y = 0; y < NN.Weights.GetLength(1); y++)
                    {
                        content += NN.Weights[x, y].ToString();
                        if (y < NN.Weights.GetLength(1) - 1) content += ",";
                    }
                    outfile.WriteLine(content);
                }
                outfile.WriteLine("Bias dos neuronios");
                for (int x = 0; x < NN.NeuronsBias.GetLength(0); x++)
                {
                    content = "";
                    for (int y = 0; y < NN.NeuronsBias.GetLength(1); y++)
                    {
                        content += NN.NeuronsBias[x, y].ToString();
                        if (y < NN.NeuronsBias.GetLength(1) - 1) content += ",";
                    }
                    outfile.WriteLine(content);
                }
                outfile.WriteLine("Status");
                outfile.WriteLine(NN.Status);
                outfile.WriteLine("Erro da rede");
                outfile.WriteLine(NN.NetworkError.ToString());
                outfile.WriteLine("Coeficiente de determinacao");
                outfile.WriteLine(NN.RSquare.ToString());
                outfile.WriteLine("Coeficiente de determinacao ajustado");
                outfile.WriteLine(NN.RSquareAdju.ToString());
                outfile.WriteLine("Erro de treinamento da rede");
                outfile.WriteLine(NN.TrainNetworkError.ToString());
                outfile.WriteLine("Tempo de treinamento (s)");
                outfile.WriteLine(Mathf.RoundToInt(NN.TrainningTime).ToString());
                outfile.WriteLine("Numero de interacoes");
                outfile.WriteLine(NN.NumberIterations.ToString());

                content = "";
                outfile.WriteLine("Saida");
                for (int x = 0; x < Output.GetLength(0); x++)
                {
                    for (int y = 0; y < Output.GetLength(1); y++)
                    {
                        content += Output[x, y].ToString();
                        if (x < Output.GetLength(0) - 1 || y < Output.GetLength(1) - 1) content += ",";
                    }
                }
                outfile.WriteLine(content);
            }
        }

        public static void LoadNN(string FilePath, NeuralNet NN)
        {
            string[] line = File.ReadAllLines(FilePath);

            NN.Name = line[1];                          //Nome
            string[] struc = line[13].Split(',');     //Estrutura
            NN.Structure = new int[struc.Length];
            for (int i = 0; i < NN.Structure.Length; i++)
            {
                NN.Structure[i] = int.Parse(struc[i]);
            }
            NN.ActivationFunction = line[19];         //Função de ativação

            NN.InputMax = float.Parse(line[3]);
            NN.InputMin = float.Parse(line[5]);
            NN.OutputMax = float.Parse(line[7]);
            NN.OutputMin = float.Parse(line[9]);
            NN.Initialized = bool.Parse(line[11]);      //Inicializada

            string[] NumWei = line[15].Split(',');
            NN.NumberOfWeights = new int[NumWei.Length];
            for (int i = 0; i < NN.NumberOfWeights.Length; i++)
            {
                NN.NumberOfWeights[i] = int.Parse(NumWei[i]);
            }

            NN.TotalFreeParameters = int.Parse(line[17]);
            //NN.ActivationFunction = line[19];
            NN.alpha = float.Parse(line[21]);

            NN.Weights = new float[MaxInt(NN.NumberOfWeights), NN.Structure.Length];
            NN.TempWeights = new float[MaxInt(NN.NumberOfWeights), NN.Structure.Length];
            string[] Wei = line[23].Split(',');
            for (int i = 0; i < NN.Weights.GetLength(0); i++)
            {
                Wei = line[23 + i].Split(',');
                for (int j = 0; j < NN.Weights.GetLength(1); j++)
                {
                    NN.Weights[i, j] = float.Parse(Wei[j]);
                }
            }

            int iniline = 25 + NN.Weights.GetLength(0) - 1;
            NN.NeuronsBias = new float[MaxInt(NN.Structure), NN.Structure.Length - 1];
            NN.TempNeuronsBias = new float[MaxInt(NN.Structure), NN.Structure.Length - 1];
            string[] Bia = line[iniline].Split(',');
            for (int i = 0; i < NN.NeuronsBias.GetLength(0); i++)
            {
                Bia = line[iniline + i].Split(',');
                for (int j = 0; j < NN.NeuronsBias.GetLength(1); j++)
                {
                    NN.NeuronsBias[i, j] = float.Parse(Bia[j]);
                }
            }

            NN.NeuronsValues = new float[MaxInt(NN.Structure), NN.Structure.Length];      //linhas: Valores nos Neuronios Colunas:Camadas de neuronios
            NN.NeuronsWeightSum = new float[MaxInt(NN.Structure), NN.Structure.Length];
            NN.Output = new float[NN.Structure[NN.Structure.Length - 1]];

            iniline = 27 + NN.Weights.GetLength(0) + NN.NeuronsBias.GetLength(0) - 2;
            NN.Status = line[iniline];
            NN.NetworkError = float.Parse(line[iniline + 2]);
            NN.RSquare = float.Parse(line[iniline + 4]);
            NN.RSquareAdju = float.Parse(line[iniline + 6]);
            NN.TrainNetworkError = float.Parse(line[iniline + 8]);
            NN.TrainningTime = float.Parse(line[iniline + 10]);
            NN.NumberIterations = int.Parse(line[iniline + 12]);

        }

        public static void SaveNeuralNetworkToFile(string Path, NeuralNet RedeNeural)
        {
            AddTextToFile(Path, "-----------------------------------------------------------------------------------\r\n");
            AddTextToFile(Path, "Estrutura\r\n");
            AddTextToFile(Path, ShowArray(ArrayIntToFloat(RedeNeural.Structure)) + "\r\n");
            AddTextToFile(Path, "Pesos\r\n");
            AddTextToFile(Path, Show2DArray(RedeNeural.Weights) + "\r\n");
            AddTextToFile(Path, "Bias\r\n");
            AddTextToFile(Path, Show2DArray(RedeNeural.NeuronsBias) + "\r\n");
            AddTextToFile(Path, "Neuronios\r\n");
            AddTextToFile(Path, Show2DArray(RedeNeural.NeuronsValues) + "\r\n");
            AddTextToFile(Path, "Erro\r\n");
            AddTextToFile(Path, RedeNeural.NetworkError.ToString() + "\r\n");

        }

        public static float[] ArrayIntToFloat(int[] InputArray)
        {
            float[] Result = new float[InputArray.Length];
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                Result[i] = (float)InputArray[i];
            }
            return Result;
        }

        //Log de atividades
        public static void Log(string Text)
        {
            string adress = Application.dataPath + "/Log.txt";
            if (!File.Exists(adress))
            {
                File.Create(adress);
            }
            Debug.Log(Text);
            AddTextToFile(adress, System.DateTime.Now.ToString("hh.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + " " + Text);
        }

        //Le o log
        public static string[] ReadLog()
        {
            string[] line = File.ReadAllLines(Application.dataPath + "/Log.txt");
            return line;
        }

    }

}
