using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLogger : MonoBehaviour {

    //log data type
    public enum DataType
    {
        STRING, INT, POSITION, FLOAT, BOOL, VEC2, VEC3
    };

    

    //log dataFormat
    public class LogDataFormat
    {
        public LogDataFormat(string _name, DataType _type, object _obj)
        {
            name = _name;
            type = _type;
            obj = _obj;
        }

        public string name;
        public DataType type;
        public object obj;
    }

    //log dataFormat + time info.
    public class LogTimeData
    {
        public LogTimeData(float _time, LogDataFormat _logFormat)
        {
            time = _time;
            logFormat = _logFormat;
        }
        public float time;
        public LogDataFormat logFormat;
    }

    //whold logging Data, it adds data every OnUpdate()
    public static Dictionary<string, LogDataFormat> loggingData = new Dictionary<string, LogDataFormat>();

    //log file data contained time and logDataFormat
    public static Dictionary<float, List<LogTimeData>> recordedLoggingData = new Dictionary<float, List<LogTimeData>>();


    public static bool isLogging;

    [SerializeField]
    public bool isLoggingOn;

    

    void Awake()
    {
        GlobalLogger.isLogging = isLoggingOn;
    }
    
    //add log data, it will be called when necessary to log
    public static void addLogData(LogDataFormat logFormat)
    {
        string key = logFormat.name;

        if (!loggingData.ContainsKey(key))
            loggingData.Add(key, logFormat);
        else
            loggingData[key] = logFormat;
    }

    //LogDataFormat to string
    public static string returnByType(LogDataFormat logDataFormat)
    {
        DataType dt = logDataFormat.type;
        string contents = "";

        switch(dt)
        {
            case DataType.POSITION:
                Vector3 t = (Vector3)logDataFormat.obj;
                contents = string.Format("({0:F3}, {1:F3}, {2:F3})", t.x, t.y, t.z);
                break;
            case DataType.VEC2:
                Vector2 v = (Vector2)logDataFormat.obj;
                contents = string.Format("({0:F3}, {1:F3})", v.x, v.y);
                break;
            case DataType.INT:
                contents = ((int)logDataFormat.obj).ToString();
                break;
            case DataType.STRING:
                contents = (string)logDataFormat.obj;
                break;
            case DataType.BOOL:
                contents = ((bool)logDataFormat.obj).ToString();
                break;
            case DataType.FLOAT:
                contents = ((float)logDataFormat.obj).ToString();
                break;
        }

        return contents;
    }



    [SerializeField]
    float TimeRate;

    [SerializeField]
    string prefix;

    float t;
    float currProgressTime = 0.0f;
    

    
    //Save log file
    public void saveRecorder()
    {
        System.DateTime dt = System.DateTime.Now;
        int hour = dt.Hour;
        int minutes = dt.Minute;
        int sec = dt.Second;

        if(!System.IO.Directory.Exists("log"))
        {
            System.IO.Directory.CreateDirectory("log");
        }
        
        
        string logFileName = hour.ToString() + "-" + minutes.ToString() + "-" + sec.ToString() + "_" + prefix + ".logged";

        string contents = "";


        foreach (float key in recordedLoggingData.Keys)
        {
            List<LogTimeData> loggedList = recordedLoggingData[key];
            
            

            for(int i=0; i<loggedList.Count; i++)
            {
                string timeInfo = key.ToString();
                string logName = loggedList[i].logFormat.name;
                string logValue = GlobalLogger.returnByType(loggedList[i].logFormat);               
                
                contents = contents + timeInfo + "," + logName + "," + logValue + "\n";
            }
        }

        
        System.IO.File.WriteAllText(System.IO.Path.Combine("log", logFileName), contents);
    }

	// Use this for initialization
	void Start () {
        t = Time.time;
    }

    public void logOnLateUpdate()
    {
        currProgressTime = Time.time;

        float deltaTime = currProgressTime - t;

        if (deltaTime > TimeRate)
        {
            if (!recordedLoggingData.ContainsKey(currProgressTime))
            {
                recordedLoggingData.Add(currProgressTime, new List<LogTimeData>());
            }

            foreach (string key in loggingData.Keys)
            {
                recordedLoggingData[currProgressTime].Add(new LogTimeData(currProgressTime, loggingData[key]));
            }
            t = currProgressTime;
        }
    }

    public static void addLogDataOnce(LogDataFormat logFormat)
    {
        float currProgressTime = Time.time;

        if (!recordedLoggingData.ContainsKey(currProgressTime))
        {
            recordedLoggingData.Add(currProgressTime, new List<LogTimeData>());
        }
        recordedLoggingData[currProgressTime].Add(new LogTimeData(currProgressTime, logFormat));
    }

    //log on last update
    void LateUpdate()
    {
        if(GlobalLogger.isLogging == true)
            logOnLateUpdate();
    }

    void OnDestroy()
    {
        if (GlobalLogger.isLogging == true)
        {
            Debug.Log("Save Log...");
            saveRecorder();
        }
    }

    

    
}
