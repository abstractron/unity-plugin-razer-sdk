﻿//#define VERBOSE_LOGGING
#if UNITY_ANDROID && !UNITY_EDITOR
using Android.Graphics.Drawables;
using System;
using UnityEngine;
#endif

namespace com.razerzone.store.sdk
{
    public class Controller
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        public class ButtonData
        {
            private static IntPtr _jcButtonData = IntPtr.Zero;
            private static IntPtr _jfButtonDrawable = IntPtr.Zero;
            private static IntPtr _jfButtonName = IntPtr.Zero;
            private IntPtr _instance = IntPtr.Zero;

            static ButtonData()
            {
                try
                {
                    {
                        string strName = "com/razerzone/store/sdk/Controller$ButtonData";
                        _jcButtonData = AndroidJNI.FindClass(strName);
                        if (_jcButtonData != IntPtr.Zero)
                        {
#if VERBOSE_LOGGING
                            Debug.Log(string.Format("Found {0} class", strName));
#endif
                        }
                        else
                        {
                            Debug.LogError(string.Format("Failed to find {0} class", strName));
                            return;
                        }
                    }

                    {
                        string strField = "buttonDrawable";
                        _jfButtonDrawable = AndroidJNI.GetFieldID(_jcButtonData, strField, "Landroid/graphics/drawable/Drawable;");
                        if (_jfButtonDrawable != IntPtr.Zero)
                        {
#if VERBOSE_LOGGING
                            Debug.Log(string.Format("Found {0} field", strField));
#endif
                        }
                        else
                        {
                            Debug.LogError(string.Format("Failed to find {0} field", strField));
                            return;
                        }
                    }

                    {
                        string strField = "buttonName";
                        _jfButtonName = AndroidJNI.GetFieldID(_jcButtonData, strField, "Ljava/lang/String;");
                        if (_jfButtonName != IntPtr.Zero)
                        {
#if VERBOSE_LOGGING
                            Debug.Log(string.Format("Found {0} field", strField));
#endif
                        }
                        else
                        {
                            Debug.LogError(string.Format("Failed to find {0} field", strField));
                            return;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(string.Format("Exception loading JNI - {0}", ex));
                }
            }

            public IntPtr Instance
            {
                get { return _instance; }
                set { _instance = value; }
            }

            public Drawable buttonDrawable
            {
                get
                {
                    if (_instance == IntPtr.Zero)
                    {
                        Debug.LogError("_instance is not initialized");
                        return null;
                    }
                    if (_jfButtonDrawable == IntPtr.Zero)
                    {
                        Debug.LogError("_jfButtonDrawable is not initialized");
                        return null;
                    }
                    IntPtr result = AndroidJNI.GetObjectField(_instance, _jfButtonDrawable);
                    if (result == IntPtr.Zero)
                    {
                        Debug.LogError("Failed to get drawable");
                        return null;
                    }
                    Drawable retVal = Drawable.GetObject(result);
                    return retVal;
                }
            }

            public string buttonName
            {
                get
                {
                    if (_instance == IntPtr.Zero)
                    {
                        Debug.LogError("_instance is not initialized");
                        return null;
                    }
                    if (_jfButtonName == IntPtr.Zero)
                    {
                        Debug.LogError("_jfButtonName is not initialized");
                        return null;
                    }
                    IntPtr result = AndroidJNI.GetObjectField(_instance, _jfButtonName);
                    if (result == IntPtr.Zero)
                    {
                        Debug.LogError("Failed to get button name");
                        return null;
                    }
                    String retVal = AndroidJNI.GetStringUTFChars(result);
                    AndroidJNI.DeleteLocalRef(result);
                    return retVal;
                }
            }
        }

        private const string LOG_TAG = "Controller";
#endif

        public const int AXIS_LS_X = 0;
        public const int AXIS_LS_Y = 1;
        public const int AXIS_RS_X = 11;
        public const int AXIS_RS_Y = 14;
        public const int AXIS_L2 = 17;
        public const int AXIS_R2 = 18;

        public const int BUTTON_O = 96;
        public const int BUTTON_U = 99;
        public const int BUTTON_Y = 100;
        public const int BUTTON_A = 97;
        public const int BUTTON_L1 = 102;
        public const int BUTTON_R1 = 103;
        public const int BUTTON_L3 = 106;
        public const int BUTTON_R3 = 107;
        public const int BUTTON_DPAD_UP = 19;
        public const int BUTTON_DPAD_DOWN = 20;
        public const int BUTTON_DPAD_RIGHT = 22;
        public const int BUTTON_DPAD_LEFT = 21;
        public const int BUTTON_MENU = 82;

        public const int MAX_CONTROLLERS = 4;

#if UNITY_ANDROID && !UNITY_EDITOR

        private static IntPtr _jcController = IntPtr.Zero;
        private static IntPtr _jmGetButtonData = IntPtr.Zero;
        private static IntPtr _jmGetControllerByPlayer = IntPtr.Zero;
        private static IntPtr _jmGetDeviceName = IntPtr.Zero;
        private IntPtr _instance = IntPtr.Zero;

        static Controller()
        {
            try
            {
                {
                    string strName = "com/razerzone/store/sdk/Controller";
                    IntPtr localRef = AndroidJNI.FindClass(strName);
                    if (localRef != IntPtr.Zero)
                    {
#if VERBOSE_LOGGING
                        Debug.Log(string.Format("Found {0} class", strName));
#endif
                        _jcController = AndroidJNI.NewGlobalRef(localRef);
                        AndroidJNI.DeleteLocalRef(localRef);
                    }
                    else
                    {
                        Debug.LogError(string.Format("Failed to find {0} class", strName));
                        return;
                    }
                }

                {
                    string strMethod = "getControllerByPlayer";
                    _jmGetControllerByPlayer = AndroidJNI.GetStaticMethodID(_jcController, strMethod, "(I)Lcom/razerzone/store/sdk/Controller;");
                    if (_jmGetControllerByPlayer != IntPtr.Zero)
                    {
#if VERBOSE_LOGGING
                        Debug.Log(string.Format("Found {0} method", strMethod));
#endif
                    }
                    else
                    {
                        Debug.LogError(string.Format("Failed to find {0} method", strMethod));
                        return;
                    }
                }

                {
                    string strMethod = "getDeviceName";
                    _jmGetDeviceName = AndroidJNI.GetMethodID(_jcController, strMethod, "()Ljava/lang/String;");
                    if (_jmGetDeviceName != IntPtr.Zero)
                    {
#if VERBOSE_LOGGING
                        Debug.Log(string.Format("Found {0} method", strMethod));
#endif
                    }
                    else
                    {
                        Debug.LogError(string.Format("Failed to find {0} method", strMethod));
                        return;
                    }
                }

                {
                    string strMethod = "getButtonData";
                    _jmGetButtonData = AndroidJNI.GetStaticMethodID(_jcController, strMethod, "(I)Lcom/razerzone/store/sdk/Controller$ButtonData;");
                    if (_jmGetButtonData != IntPtr.Zero)
                    {
#if VERBOSE_LOGGING
                        Debug.Log(string.Format("Found {0} method", strMethod));
#endif
                    }
                    else
                    {
                        Debug.LogError(string.Format("Failed to find {0} method", strMethod));
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(string.Format("Exception loading JNI - {0}", ex));
            }
        }

        private Controller(IntPtr instance)
        {
            _instance = instance;
        }

        public static Controller getControllerByPlayer(int deviceId)
        {
            if (_jcController == IntPtr.Zero)
            {
                Debug.LogError("_jcController is not initialized");
                return null;
            }
            if (_jmGetControllerByPlayer == IntPtr.Zero)
            {
                Debug.LogError("_jmGetControllerByPlayer is not initialized");
                return null;
            }
            IntPtr result = AndroidJNI.CallStaticObjectMethod(_jcController, _jmGetControllerByPlayer, new jvalue[] { new jvalue() { i = deviceId } });

            if (result == IntPtr.Zero)
            {
                //might not be connected
                //Debug.LogError("Failed to get Controller");
                return null;
            }
            Controller retVal = new Controller(result);
            return retVal;
        }

        public string getDeviceName()
        {
            if (_instance == IntPtr.Zero)
            {
                Debug.LogError("_instance is not initialized");
                return null;
            }
            if (_jmGetDeviceName == IntPtr.Zero)
            {
                Debug.LogError("_jmGetDeviceName is not initialized");
                return null;
            }
            IntPtr result = AndroidJNI.CallObjectMethod(_instance, _jmGetDeviceName, new jvalue[0]);

            if (result == IntPtr.Zero)
            {
                //might not be connected
                //Debug.LogError("Failed to get device name");
                return null;
            }
            String retVal = AndroidJNI.GetStringUTFChars(result);
            AndroidJNI.DeleteLocalRef(result);
            return retVal;
        }

        public static ButtonData getButtonData(int button)
        {
            if (_jcController == IntPtr.Zero)
            {
                Debug.LogError("_jcController is not initialized");
                return null;
            }
            if (_jmGetButtonData == IntPtr.Zero)
            {
                Debug.LogError("_jmGetButtonData is not initialized");
                return null;
            }
            IntPtr result = AndroidJNI.CallStaticObjectMethod(_jcController, _jmGetButtonData, new jvalue[] { new jvalue() { i = button } });
            if (result == IntPtr.Zero)
            {
                Debug.LogError("Failed to get ButtonData");
                return null;
            }
            ButtonData retVal = new ButtonData();
            retVal.Instance = result;
            return retVal;
        }
#endif
    }
}
