using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientPropertycalc : MonoBehaviour
{
    public int MaxNumberOfPointsInShader = 2;
    public GameObject[] ObjectPoints = new GameObject[2];
    public GameObject[] TargetsToSetData = new GameObject[2];
    public Vector4[] PointData = new Vector4[2];

    public float NextUpdate;
    public float UpdateInterval = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //cada um é atualizado em um instante diferente
        NextUpdate = Random.Range(0f, UpdateInterval);
        //atualiza as posições
        UpdatePositions();
        //Quantos pontos o Shader permite? só testa até 20 pontos
        if (TargetsToSetData.Length > 0) //Se há objetos no vetor para setar
        {
            for (int i = 0; i < 20; i++)
            {
                if (TargetsToSetData[0].GetComponent<MeshRenderer>().material.HasProperty("_P"+ i.ToString())) MaxNumberOfPointsInShader = i+1;
            }
        }
        else  //Caso não haja objetos na lista, setar para o objeto com o script
        {
            for (int i = 0; i < 20; i++)
            {
                if (gameObject.GetComponent<MeshRenderer>().material.HasProperty("_P" + i.ToString())) MaxNumberOfPointsInShader = i+1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > NextUpdate)
        {
            //atualiza as posições
            UpdatePositions();

            if (TargetsToSetData.Length > 0) //Se há objetos no vetor para setar
            {
                for (int i = 0; i < TargetsToSetData.Length; i++)
                {
                    for (int j = 0; j < MaxNumberOfPointsInShader; j++)
                    {
                        if (TargetsToSetData[i] != null) //Se o objeto não for nulo
                        {
                            if (j < ObjectPoints.Length)//se ele pertence a um ponto com propriedade definida, setar o valor
                                TargetsToSetData[i].GetComponent<MeshRenderer>().material.SetVector("_P" + j.ToString(), PointData[j]);
                            else //caso não, setar um valor distante para não causar interferência
                                TargetsToSetData[i].GetComponent<MeshRenderer>().material.SetVector("_P" + j.ToString(), new Vector4(0.01f * float.MaxValue, 0.01f * float.MaxValue, 0.01f * float.MaxValue, 0f));
                        }
                        else
                        {
                            UnityEngine.Debug.LogWarning("Objeto nulo setado no script de calculo de propriedade, encontrado no objeto: " + gameObject.name);
                        }
                    }
                }
            }
            else  //Caso não haja objetos na lista, setar para o objeto com o script
            {
                for (int j = 0; j < MaxNumberOfPointsInShader; j++)
                {
                    if (j < ObjectPoints.Length)//se ele pertence a um ponto com propriedade definida, setar o valor
                        gameObject.GetComponent<MeshRenderer>().material.SetVector("_P" + j.ToString(), PointData[j]);
                    else //caso não, setar um valor distante para não causar interferência
                        gameObject.GetComponent<MeshRenderer>().material.SetVector("_P" + j.ToString(), new Vector4(0.01f * float.MaxValue, 0.01f * float.MaxValue, 0.01f * float.MaxValue, 0f));
                }
            }

            NextUpdate += UpdateInterval;
        }
    }

    public void UpdatePositions()
    {
        for (int i = 0; i < ObjectPoints.Length; i++)
        {
            PointData[i].x = ObjectPoints[i].transform.position.x;
            PointData[i].y = ObjectPoints[i].transform.position.y;
            PointData[i].z = ObjectPoints[i].transform.position.z;
        }
    }
}
