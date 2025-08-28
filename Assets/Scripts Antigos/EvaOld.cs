using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;

public class EvaOld : MonoBehaviour
{

    public CentralArduino ScriptCentral;
    public bool RelatorioGeral;
    public GameObject[] aneis;
    private bool Apresentacao = false;
    public float UpdateTime = 6f;
    public float Velocidade = 10f;
    public float UpdateTimeMark = 6f;
    public int Limite;
    public int Error = 0;
    private Process Proc;
    public Animator anim;
    public float Timer = 6f;
    public bool EvaStartedHere = false;

    // Use this for initialization
    void Start()
    {
        StartEva();
        //anim = GetComponent<Animator> ();
        if (ScriptCentral.Sensores.Length <= aneis.Length) { Limite = ScriptCentral.Sensores.Length; }
        if (ScriptCentral.Sensores.Length > aneis.Length) { Limite = aneis.Length; }
    }

    void OnMouseDown()
    {
        StartCoroutine(RelatorioEva(true));
    }

    IEnumerator RelatorioEva(bool falado)
    {
        string Relatorio = "";
        int TempError = 0;
        if (falado)
        {
            if (ScriptCentral.Arduino & ScriptCentral.ArduinoConectado)
            {
                Relatorio += "O sistema está conectado com o Arduino ...";
                yield return new WaitForSeconds(0.01f);
            }
            if (ScriptCentral.Simulation)
            {
                Relatorio += "O sistema está em modo simulaçao ...";
                yield return new WaitForSeconds(0.01f);
            }
            if (ScriptCentral.RemoteControl)
            {
                Relatorio += "O sistema está em modo de controle remoto ...";
                yield return new WaitForSeconds(0.01f);
            }
            if (!CentralArduino.CheckForInternetConnection())
            {
                Relatorio += "O computador está sem internet ...";
                yield return new WaitForSeconds(0.01f);
            }
        }

        if (ScriptCentral.Arduino & !ScriptCentral.ArduinoConectado)
        {
            TempError += 1;
            if (falado) { Relatorio += "O Arduino foi desconectado..."; }
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < ScriptCentral.Sensores.Length; i++)
        {
            yield return new WaitForSeconds(0.001f);
            if (ScriptCentral.SensTempoDesdeUtimaAtualizacao[i] > 100f)
            {
                TempError += 1;
                if (falado) { Relatorio += " O " + ScriptCentral.Sensores[i] + " " + i.ToString("D2") + " " + ScriptCentral.DescriçaoSensor[i] + " parou de funcionar a " + ScriptCentral.SensTempoDesdeUtimaAtualizacao[i].ToString("f0") + " segundos ..."; }
                yield return new WaitForSeconds(0.01f);
            }

            //if (CentralArduino.Media(CentralArduino.PartOfArray(ScriptCentral.AxisYToPlot, i, 0))<ScriptCentral.LimiteMinSensores[i]){if (falado) {Relatorio+="No " + ScriptCentral.Sensores [i] + " " +ScriptCentral.DescriçaoSensor[i]+". A "+ScriptCentral.TipoSensores[i]+ " média está abaixo do limite inferior. a variável está fora do controle...";yield return new WaitForSeconds(0.01f);}}
            //if (CentralArduino.Media(CentralArduino.PartOfArray(ScriptCentral.AxisYToPlot, i, 0))>ScriptCentral.LimiteMaxSensores[i]){if (falado) {Relatorio+="No " + ScriptCentral.Sensores [i] + " " +ScriptCentral.DescriçaoSensor[i]+". A "+ScriptCentral.TipoSensores[i]+ " média está acima do limite superior. a variável está fora do controle...";yield return new WaitForSeconds(0.01f);}}

            if (ScriptCentral.ValoresAtuaisSensores[i] > ScriptCentral.LimiteMaxSensores[i])
            {
                TempError += 1;
                if (falado) { Relatorio += "A " + ScriptCentral.TipoSensores[i] + " no " + ScriptCentral.Sensores[i] + " " + i.ToString("D2") + " " + ScriptCentral.DescriçaoSensor[i] + " está acima do limite superior..."; yield return new WaitForSeconds(0.01f); }
                yield return new WaitForSeconds(0.01f);
            }

            if (ScriptCentral.ValoresAtuaisSensores[i] < ScriptCentral.LimiteMinSensores[i])
            {
                TempError += 1;
                if (falado) { Relatorio += "A " + ScriptCentral.TipoSensores[i] + " no " + ScriptCentral.Sensores[i] + " " + i.ToString("D2") + " " + ScriptCentral.DescriçaoSensor[i] + " está abaixo do limite inferior..."; yield return new WaitForSeconds(0.01f); }
                yield return new WaitForSeconds(0.01f);
            }
        }
        Error = TempError;
        if (falado)
        {
            if (Error == 0) { Relatorio += "o sistema aparenta estar funcionando normalmente..."; yield return new WaitForSeconds(0.01f); }
            if (Error == 1) { Relatorio += "o sistema apresentou " + Error.ToString() + " erro"; yield return new WaitForSeconds(0.01f); }
            if (Error > 1) { Relatorio += "o sistema apresentou " + Error.ToString() + " erros"; yield return new WaitForSeconds(0.01f); }
            CentralArduino.Speak(Relatorio);
        }
        //CentralArduino.CheckSensorStatus ();
    }

    void OnMouseEnter()
    {
        anim.SetBool("MouseOver", true);
        if (Apresentacao && Random.Range(0f, 100.0f) > 68f)
        {
            CentralArduino.Speak("Se desejar um relatório geral, clique em mim!");
        }
        if (!Apresentacao)
        {
            CentralArduino.Speak("Eu sou EVA, sua assistente virtual de supervisão, Se desejar um relatório geral, clique em mim!");
            Apresentacao = true;
        }
        //GetComponent<Renderer>().material.SetFloat( "_LerpColor", 0f);
    }

    void OnMouseExit()
    {
        anim.SetBool("MouseOver", false);
    }

    //Iniciar Eva
    public void StartEva()
    {
        string Eva = Application.dataPath + "/Eva/Eva.exe";
        int EvasProcs = System.Diagnostics.Process.GetProcessesByName("Eva").Length;

        if (File.Exists(Eva) && EvasProcs == 0)
        {
            UnityEngine.Debug.Log("Eva existe");
            Proc = new Process();
            Proc.StartInfo.FileName = Eva;
            Proc.StartInfo.UseShellExecute = false;
            //Proc.StartInfo.Arguments = "-silent";
            Proc.StartInfo.CreateNoWindow = true;
            Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //evita de mostrar a janela de prompt
            Proc.Start();
            EvaStartedHere = true;
        }
        if (!EvaStartedHere && EvasProcs == 0)
        {
            GetComponent<Renderer>().material.SetFloat("_EvaOut", 1f);
            CentralArduino.Speak("...O reconhecimento de voz está indisponível!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //cor de eva

        if (Error == 0) { GetComponent<Renderer>().material.SetColor("_Color", Color.green); }
        if (Error == 1) { GetComponent<Renderer>().material.SetColor("_Color", Color.yellow); }
        if (Error > 1) { GetComponent<Renderer>().material.SetColor("_Color", Color.red); }

        if (Time.time >= UpdateTimeMark)
        {
            //giro dos aneis
            for (int i = 0; i < Limite; i++)
            {
                float ValueRotation;
                ValueRotation = (Velocidade) * CentralArduino.TaxaFinal(CentralArduino.PartOfArray(ScriptCentral.TempoValoresSensores, i, 0), CentralArduino.PartOfArray(ScriptCentral.ValoresSensores, i, 0)) / (ScriptCentral.ValoresAtuaisSensores[i] + 0.0001f);
                aneis[i].GetComponent<Rotate>().ZSpeed = ValueRotation;
            }

            StartCoroutine(RelatorioEva(false));

            UpdateTimeMark = UpdateTime + Time.time;
        }

        if (RelatorioGeral)
        {
            StartCoroutine(RelatorioEva(true));
            RelatorioGeral = false;
        }

    }

    void OnApplicationQuit()
    {
        int EvasProcs = System.Diagnostics.Process.GetProcessesByName("Eva").Length;
        if (EvaStartedHere && EvasProcs > 0)
        {
            Proc.CloseMainWindow();
            Proc.Close();
        }
    }


}
