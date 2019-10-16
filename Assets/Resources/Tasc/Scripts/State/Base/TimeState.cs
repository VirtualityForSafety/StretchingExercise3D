using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

namespace Tasc
{
    public class TimeState : State
    {
        // we could not implement TimeState for task and state as subclass because of equality comparison at Condition
        
        public Parameter<TimeSpan> value;
        private Stopwatch stopwatch;
        //private Dictionary<string, Stopwatch> stopwatches;
        private static TimeState globalTimer = null;
        private static readonly object padlock = new object();
        private bool initializedWithValue;

        public static TimeState GlobalTimer
        {
            get
            {
                lock (padlock)
                {
                    if (globalTimer == null)
                    {
                        globalTimer = new TimeState();
                        globalTimer.StartTimer();
                    }
                    return globalTimer;
                }
            }
        }

        private TimeState()
        {
            name = "TimeState";
            description = "Time elapsed state";
            //stopwatches = new Dictionary<string, Stopwatch>();
            stopwatch = new Stopwatch();
            value = new Parameter<TimeSpan>(new TimeSpan(0, 0, 0));
            initializedWithValue = false;
        }

        public TimeState(int hour, int min, int sec) : this()
        {
            value = new Parameter<TimeSpan>(new TimeSpan(hour, min, sec));
            initializedWithValue = true;
        }

        public TimeState(TimeSpan timeSpan) : this()
        {
            value = new Parameter<TimeSpan>(timeSpan);
            initializedWithValue = true;
        }

        public override string ToString()
        {
            return value.GetValue().ToString();
        }

        public void StartTimer()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        public bool IsTimerOn()
        {
            return stopwatch.IsRunning;
        }

        public TimeSpan StopAndGetTimer()
        {
            StopTimer();
            return GetElapsed();
        }

        public void StopTimer()
        {
            stopwatch.Stop();
        }

        public TimeSpan GetElapsed()
        {
            return stopwatch.Elapsed;
        }

        public double GetElapsedTimeS()
        {
            return stopwatch.Elapsed.TotalSeconds;
        }

        public void UpdateValue()
        {
            value.SetValue(GetElapsed());
            //UnityEngine.Debug.Log(GetElapsedTimeMS());
        }

        public static void UpdateGlobalTimer()
        {
            GlobalTimer.UpdateValue();
            //UnityEngine.Debug.Log(GlobalTimer.GetElapsedTimeS());
        }

        public static string GetGlobalTimeString()
        {
            return globalTimer.ToString();
        }

        public static TimeSpan GetGlobalTimer()
        {
            if(globalTimer == null)
            {
                globalTimer = new TimeState();
                globalTimer.StartTimer();
            }
            return globalTimer.value.GetValue();
        }

        public int CompareTo(TimeState other)
        {
            //UnityEngine.Debug.Log(value.GetValue().ToString());
            //UnityEngine.Debug.Log(other.value.GetValue().ToString());
            return value.CompareTo(other.value);
        }

        // internal comparison of given value and the running stopwatch
        public bool IsOver()
        {
            if (!initializedWithValue)
                return false;
            if (!IsTimerOn())
                return false;
            return (stopwatch.Elapsed >= value.GetValue());
        }

        public override int CompareTo(object obj)
        {
            return CompareTo(obj as TimeState);
        }

        public static TimeState operator -(TimeState b, TimeState c)
        {
            return new TimeState(b.value.GetValue() - c.value.GetValue());
        }

        public static TimeState operator +(TimeState b, TimeState c)
        {
            return new TimeState(b.value.GetValue() + c.value.GetValue());
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
