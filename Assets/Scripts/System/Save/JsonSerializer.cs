using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.IO;

/// <summary>
/// Dictionary�f�[�^��Json�f�[�^(string)�̕ϊ����s��
/// �Í����ƕ������������ɍs��
/// </summary>
public static class JsonSerializer
{

    //�ۑ�����f�B���N�g����
    private const string DIRECTORY_NAME = "Data";

    //�t�@�C���̃p�X���擾
    private static string GetFilePath(string fileName)
    {

        string directoryPath = Application.persistentDataPath + "/" + DIRECTORY_NAME;

        //�f�B���N�g����������΍쐬
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        //�t�@�C�����͈Í�������
        string encryptedFlieName = Encryption.EncryptString(fileName);
        string filePath = directoryPath + "/" + encryptedFlieName;

        return filePath;
    }

    /// <summary>
    /// Dictionary�f�[�^��json�`���ɕϊ����ĕۑ�����
    /// </summary>
    /// <param name="dic">�ۑ�����Dictionary<string, object>�f�[�^</param>
    /// <param name="fileName">�ۑ��t�@�C����</param>
    public static void Save(Dictionary<string, object> dic, string fileName)
    {

        string jsonStr = Json.Serialize(dic);
        Debug.Log("serialized text = " + jsonStr);

        //json���Í�������
        jsonStr = Encryption.EncryptString(jsonStr);

        string filePath = GetFilePath(fileName);
        File.WriteAllText(filePath, jsonStr);


        Debug.Log("saveFilePath = " + filePath);
    }

    /// <summary>
    /// json�f�[�^��ǂݍ���Dictionary�f�[�^�ɕϊ����ĕԂ�
    /// </summary>
    /// <param name="fileName">�擾����t�@�C���̖��O</param>
    public static Dictionary<string, object> Load(string fileName)
    {

        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
            Debug.Log(fileName + "�͂���܂���I");
            return null;
        }

        string jsonStr = File.ReadAllText(filePath);

        //�擾�����t�@�C���𕡍���
        jsonStr = Encryption.DecryptString(jsonStr);

        Dictionary<string, object> dic = Json.Deserialize(jsonStr) as Dictionary<string, object>;
        return dic;
    }

}