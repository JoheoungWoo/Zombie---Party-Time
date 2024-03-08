using UnityEngine;
using Convert = System.Convert;

public static class Utility
{
    public enum CharList
    {
        미뮤,
        슈튜,
        선우나나,
        위트,
        흔한유저,
        성설,
        김준호,
        남동진,
        이유리,
        정예슬,
        콘PD,
        곽준형,
        아도니스,
    }

    public static class Prefs
    {
        #region 기본적인 데이터 처리
        /// <summary>
        /// 키의 존재여부 확인
        /// </summary>
        /// <param name="keyName">체크할 키 이름</param>
        /// <returns>true 혹은 false</returns>
        public static bool CheckData(string keyName)
        {
            return PlayerPrefs.HasKey(keyName);
        }


        /// <summary>
        /// 모든 데이터 삭제
        /// </summary>
        public static void Delete()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 특정 데이터만 삭제
        /// </summary>
        /// <param name="keyName">삭제할 키의 이름</param>
        public static void Delete(string keyName)
        {
            if (CheckData(keyName))
            {
                PlayerPrefs.DeleteKey(keyName);
            }
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        /// <param name="keyName">키 이름</param>
        /// <param name="data">저장할 데이터</param>
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
        /// 데이터 로드
        /// </summary>
        /// <param name="keyName">로드할 키 이름</param>
        /// <returns>로드한 데이터</returns>
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

        #region 응용 데이터 처리

        #endregion
    }
}
