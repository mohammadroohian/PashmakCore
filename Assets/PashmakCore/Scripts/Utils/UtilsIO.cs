using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pashmak.Core.IO
{
    // enum____________________________________________________________________
    public enum VariableType // Show type of value that will be saved.
    {
        Int = 0,
        Float = 1,
        String = 2,
        Bool = 3
    }
    public enum VariableSaveType // Show how data will be saved.
    {
        PlayerPrefs = 0,
        Binary = 1
    }


    // class___________________________________________________________________
    public static class SaveLoad
    {
        // save

        static private void SaveInPlayerPrefs(string key, object value, VariableType valueType)
        {
            switch (valueType)
            {
                case VariableType.Int:
                    PlayerPrefs.SetInt(key, (int)value);
                    break;
                case VariableType.Float:
                    PlayerPrefs.SetFloat(key, (float)value);
                    break;
                case VariableType.String:
                    PlayerPrefs.SetString(key, (string)value);
                    break;
                case VariableType.Bool:
                    PlayerPrefs.SetInt(key, (int)value);
                    break;
            }
        }
        static private void SaveBinary(string key, object value)
        {
            using (FileStream file = File.Create(Application.persistentDataPath + "/" + key + ".prf"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, new KeyValueBinaryContainer(value));
                file.Close();
            }
        }

        // load

        static public object LoadFromPlayerPrefs(string key, VariableType valueType)
        {
            switch (valueType)
            {
                case VariableType.Int:
                    return PlayerPrefs.GetInt(key);
                case VariableType.Float:
                    return PlayerPrefs.GetFloat(key);
                case VariableType.String:
                    return PlayerPrefs.GetString(key);
                case VariableType.Bool:
                    int tmp = PlayerPrefs.GetInt(key);
                    return tmp > 0;
                default:
                    return null;
            }
        }
        static private KeyValueBinaryContainer LoadBinary(string key, VariableType valueType)
        {
            string path = Application.persistentDataPath + "/" + key + ".prf";
            if (File.Exists(path))
            {
                using (FileStream file = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    KeyValueBinaryContainer tmp = (KeyValueBinaryContainer)bf.Deserialize(file);
                    file.Close();

                    if (tmp.devMod != SystemInfo.deviceModel)
                        return new KeyValueBinaryContainer(valueType);

                    return tmp;
                }
            }
            else
            {
                return new KeyValueBinaryContainer(valueType);
            }
        }


        // set 

        /// <summary>
        /// Save new value for key.
        /// </summary>
        /// <param name="key">name of variable we want save</param>
        /// <param name="value">value of variable we want to save</param>
        /// <param name="valueType">type of variable we want to save</param>
        /// <param name="saveType">saving mechanism for variable we want to save</param>
        /// <param name="debugMode">if true changing value will be printed</param>
        static public void Save(string key, object value, VariableType valueType, VariableSaveType saveType, bool debugMode = false)
        {
            switch (saveType)
            {
                case VariableSaveType.PlayerPrefs:
                    SaveInPlayerPrefs(key, value, valueType);
                    break;
                case VariableSaveType.Binary:
                    SaveBinary(key, value);
                    break;
            }

            if (debugMode)
            {
                Debug.Log("Set '" + key + "' to : " + value);
            }
        }
        /// <summary>
        /// Save new int value for key.
        /// </summary>
        /// <param name="key">name of variable we want save</param>
        /// <param name="value">value of variable we want to save</param>
        /// <param name="saveType">saving mechanism for variable we want to save</param>
        static public void SaveInt(string key, int value, VariableSaveType saveType, bool debugMode = false)
            => Save(key, value, VariableType.Int, saveType, debugMode);
        /// <summary>
        /// Save new float value for key.
        /// </summary>
        /// <param name="key">name of variable we want save</param>
        /// <param name="value">value of variable we want to save</param>
        /// <param name="saveType">saving mechanism for variable we want to save</param>
        static public void SaveFloat(string key, float value, VariableSaveType saveType, bool debugMode = false)
            => Save(key, value, VariableType.Float, saveType, debugMode);
        /// <summary>
        /// Save new string value for key.
        /// </summary>
        /// <param name="key">name of variable we want save</param>
        /// <param name="value">value of variable we want to save</param>
        /// <param name="saveType">saving mechanism for variable we want to save</param>
        static public void SaveString(string key, string value, VariableSaveType saveType, bool debugMode = false)
            => Save(key, value, VariableType.String, saveType, debugMode);
        static public void SaveBool(string key, bool value, VariableSaveType saveType, bool debugMode = false)
            => Save(key, value, VariableType.Bool, saveType, debugMode);

        // get

        static public object Load(string key, VariableType valueType, VariableSaveType saveType)
        {
            switch (saveType)
            {
                case VariableSaveType.PlayerPrefs:
                    return SaveLoad.LoadFromPlayerPrefs(key, valueType);
                case VariableSaveType.Binary:
                    return SaveLoad.LoadBinary(key, valueType).value;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Load int value with key.
        /// </summary>
        /// <param name="key">name of variable we want load</param>
        /// <param name="saveType">loading mechanism for variable we want to load</param>
        /// <returns></returns>
        static public int LoadInt(string key, VariableSaveType saveType) => (int)Load(key, VariableType.Int, saveType);
        /// <summary>
        /// Load float value with key.
        /// </summary>
        /// <param name="key">name of variable we want load</param>
        /// <param name="saveType">loading mechanism for variable we want to load</param>
        /// <returns></returns>
        static public float LoadFloat(string key, VariableSaveType saveType) => (float)Load(key, VariableType.Float, saveType);
        /// <summary>
        /// Load string value with key.
        /// </summary>
        /// <param name="key">name of variable we want load</param>
        /// <param name="saveType">loading mechanism for variable we want to load</param>
        /// <returns></returns>
        static public string LoadString(string key, VariableSaveType saveType) => (string)Load(key, VariableType.String, saveType);
        static public bool LoadBool(string key, VariableSaveType saveType) => (bool)Load(key, VariableType.Bool, saveType);
        static public void DeleteKey(string key, VariableSaveType saveType)
        {
            switch (saveType)
            {
                case VariableSaveType.PlayerPrefs:
                    PlayerPrefs.DeleteKey(key);
                    break;
                case VariableSaveType.Binary:
                    string path = Application.persistentDataPath + "/" + key + ".prf";
                    File.Delete(path);
                    break;
            }
        }
    }
    [System.Serializable]
    public class KeyValueBinaryContainer
    {
        public KeyValueBinaryContainer(object value)
        {
            this.value = value;
            this.devMod = SystemInfo.deviceModel;
        }
        public KeyValueBinaryContainer(VariableType valueType)
        {
            // Set value.
            switch (valueType)
            {
                case VariableType.Int:
                    this.value = 0;
                    break;
                case VariableType.Float:
                    this.value = 0.0f;
                    break;
                case VariableType.String:
                    this.value = "";
                    break;
                default:
                    this.value = null;
                    break;
            }

            //Set device model.
            this.devMod = SystemInfo.deviceModel;
        }
        public object value;
        public string devMod;
    }
}