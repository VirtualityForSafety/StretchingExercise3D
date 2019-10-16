using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionLogger : MonoBehaviour {

    [SerializeField]
    Transform TransformToLog;

    [SerializeField]
    string LogName;

    GlobalLogger.DataType dataType = GlobalLogger.DataType.POSITION;


	
	// Update is called once per frame
	void Update () {
        if (GlobalLogger.isLogging == true)
            GlobalLogger.addLogData(new GlobalLogger.LogDataFormat(LogName, dataType, TransformToLog.position));
	}
}
