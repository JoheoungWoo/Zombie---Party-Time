using UnityEngine;
using Convert = System.Convert;

public static class Utility
{
    public enum CharList
    {
        �̹�,
        ��Ʃ,
        ���쳪��,
        ��Ʈ,
        ��������,
        ����,
        ����ȣ,
        ������,
        ������,
        ������,
        ��PD,
        ������,
        �Ƶ��Ͻ�,
    }

    public static class Prefs
    {
        #region �⺻���� ������ ó��
        /// <summary>
        /// Ű�� ���翩�� Ȯ��
        /// </summary>
        /// <param name="keyName">üũ�� Ű �̸�</param>
        /// <returns>true Ȥ�� false</returns>
        public static bool CheckData(string keyName)
        {
            return PlayerPrefs.HasKey(keyName);
        }


        /// <summary>
        /// ��� ������ ����
        /// </summary>
        public static void Delete()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// Ư�� �����͸� ����
        /// </summary>
        /// <param name="keyName">������ Ű�� �̸�</param>
        public static void Delete(string keyName)
        {
            if (CheckData(keyName))
            {
                PlayerPrefs.DeleteKey(keyName);
            }
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="keyName">Ű �̸�</param>
        /// <param name="data">������ ������</param>
        public static void Save<T>(string keyName, T data)
        {
            switch (typeof(T).Name)
            {
                case "String":
                    PlayerPrefs.SetString(keyName, data.ToString());
                    break;

                case "Single":
                    PlayerPrefs.SetFloat(keyName, Convert.ToSingle(data));
                    break;

                case "Int32":
                    PlayerPrefs.SetInt(keyName, Convert.ToInt32(data));
                    break;

                default:
                    Debug.LogError("Unsupported type: " + data);
                    break;
            }
        }

        /// <summary>
        /// ������ �ε�
        /// </summary>
        /// <param name="keyName">�ε��� Ű �̸�</param>
        /// <returns>�ε��� ������</returns>
        public static object Load<T>(string keyName)
        {
            object tempData = null;
            if (CheckData(keyName))
            {
                switch (typeof(T).Name)
                {
                    case "String":
                        tempData = (object)PlayerPrefs.GetString(keyName);
                        break;
                    case "Single":
                        tempData = (object)PlayerPrefs.GetFloat(keyName);
                        break;
                    case "Int32":
                        tempData = (object)PlayerPrefs.GetInt(keyName);
                        break;
                    default:
                        break;
                }

                if (tempData != null)
                {
                    return tempData;
                }
            }
            {
                return tempData;
            }
        }
        #endregion

        #region ���� ������ ó��

        #endregion
    }
}
